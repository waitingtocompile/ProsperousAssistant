using FIOSharp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using FIOSharp;
using System.Diagnostics.CodeAnalysis;

namespace ProsperousAssistant
{
	public class ColorGroup : IEquatable<ColorGroup>
	{
		public class ActiveColorsChangedEventArgs : EventArgs
		{
			public ActiveColorsChangedEventArgs(IReadOnlyDictionary<string, ColorGroup> newColors)
			{
				NewColors = newColors;
			}

			public IReadOnlyDictionary<string, ColorGroup> NewColors { get; }
			public bool SuppressFileWrite { get; set; } = false;
			public bool Cancel { get; set; } = false;
		}


		public static IReadOnlyDictionary<string, ColorGroup> DEFAULT_COLORS => DefaultColors;

		private readonly static Dictionary<string, ColorGroup> DefaultColors = new Dictionary<string, ColorGroup>(StringComparer.OrdinalIgnoreCase);

		public static IReadOnlyDictionary<string, ColorGroup> ActiveColors {
			get {
				if (_activeColors == null) _activeColors = DEFAULT_COLORS;
				return _activeColors;
			}
		}

		private static IReadOnlyDictionary<string, ColorGroup> _activeColors;

		public static void SetActiveColors(IEnumerable<ColorGroup> newColors)
		{
			IReadOnlyDictionary<string, ColorGroup> newDict = (newColors == null) ? DefaultColors : ConvertAndAddDefaults(newColors);
			var args = new ActiveColorsChangedEventArgs(newDict);
			OnActiveColorsChanged?.Invoke(null, args);
			if (args.Cancel) return;
			CheckInvalidateCachedIcons(newDict);
			_activeColors = newDict;
			if (!args.SuppressFileWrite)
			{
				WriteActiveColorsToFile();
			}
			
		}

		public static EventHandler<ActiveColorsChangedEventArgs> OnActiveColorsChanged;

		private static Dictionary<Material, Bitmap> MaterialImages;

		private const int ICON_SIZE = 40;
		private const float ICON_FONT_SIZE = 10.5f;

		public static string COLORS_FILE_PATH => $"{Settings.StoragePath}\\colors.json";

		private static IReadOnlyDictionary<string, ColorGroup> ConvertAndAddDefaults(IEnumerable<ColorGroup> newColors)
		{
			Dictionary<string, ColorGroup> newDict = newColors.ToDictionary(col => col.Name, StringComparer.OrdinalIgnoreCase);
			foreach(var pair in DefaultColors)
			{
				if (!newDict.ContainsKey(pair.Key))
				{
					newDict.Add(pair.Key, pair.Value);
				}
			}
			return newDict;
		}

		public static void LoadActiveColorsFromFile(string filePath = null)
		{
			if(filePath == null)
			{
				filePath = COLORS_FILE_PATH;
			}

			if (!File.Exists(filePath))
			{
				//no colour file exists, change nothing
				return;
			}

			using (StreamReader streamReader = new StreamReader(filePath))
			{
				using JsonTextReader jsonReader = new JsonTextReader(streamReader);

				try
				{
					JArray jArray = (JArray)JToken.ReadFrom(jsonReader);
					Dictionary<string, ColorGroup> readColors = new Dictionary<string, ColorGroup>();
					SetActiveColors(jArray.Select(token => token.ToObject<ColorGroup>()));

				}
				catch (Exception ex)
				{
					throw new JsonSchemaException("Invalid format on colorgroups json", ex);
				}
			}
		}

		public static void WriteActiveColorsToFile(string filePath = null)
		{
			if (filePath == null)
			{
				filePath = COLORS_FILE_PATH;
			}

			using (StreamWriter streamWriter = new StreamWriter(filePath, false))
			{
				using JsonTextWriter jsonWriter = new JsonTextWriter(streamWriter);
				jsonWriter.Formatting = Formatting.Indented;
				JArray.FromObject(_activeColors.Values).WriteTo(jsonWriter);
			}
		}

		private static void CheckInvalidateCachedIcons(IReadOnlyDictionary<string, ColorGroup> newColors)
		{
			//we need to make sure that if the colour does not match, we invalidate our old icons
			List<Material> toDelete = new List<Material>();
			foreach(var ImagePair in MaterialImages)
			{
				if(newColors[ImagePair.Key.Ticker] != _activeColors[ImagePair.Key.Ticker])
				{
					toDelete.Add(ImagePair.Key);
				}
			}

			foreach(Material mat in toDelete)
			{
				Bitmap image = MaterialImages[mat];
				MaterialImages.Remove(mat);
				image.Dispose();
			}
		}


		public static ColorGroup GetColor(Material material)
		{
			return ActiveColors[material.Category.Replace("(", "").Replace(")", "")];
		}

		//get the color for a given building, or the default building colour if one isn't defined
		public static ColorGroup GetColor(Building building)
		{
			if (ActiveColors.ContainsKey(building.Ticker))
			{
				return ActiveColors[building.Ticker];
			}
			else return ActiveColors["building"];
		}

		public static Bitmap GetOrCreateIcon(Material material)
		{
			if(MaterialImages == null)
			{
				MaterialImages = new Dictionary<Material, Bitmap>();
				Application.ApplicationExit += (object sender, EventArgs e) =>
				{
					foreach (var image in MaterialImages.Values)
					{
						image.Dispose();
					}
					MaterialImages = null;
				};
			}

			if (MaterialImages.ContainsKey(material))
			{
				return MaterialImages[material];
			}

			Bitmap bitmap = GetColor(material).CreateIconFrom(material.Ticker);
			MaterialImages.Add(material, bitmap);
			return bitmap;
		}

		/// <summary>
		/// Create an icon in the given colour scheme.
		/// Warning: this icon isn't memory managed in any capacity, you need to handle it's cleanup when you're done with it.
		/// It also isn't updated if the colour scheme changes
		/// Use the nice type specific methods to create icons that are managed and cached properly
		/// </summary>
		public Bitmap CreateIconFrom(string label, int iconSize = ICON_SIZE, float fontSize = ICON_FONT_SIZE)
		{
			Bitmap bitmap = new Bitmap(iconSize, iconSize);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				using (LinearGradientBrush brush = CreateLinearGradientBrush(iconSize, iconSize))
				{
					graphics.FillRectangle(brush, new Rectangle(0, 0, iconSize, iconSize));
				}
				using (Brush brush = new SolidBrush(Text))
				{
					using StringFormat format = new StringFormat();
					format.Alignment = StringAlignment.Center;
					format.LineAlignment = StringAlignment.Center;
					graphics.DrawString(label, new Font(FontHelper.DroidSans, fontSize, FontStyle.Bold), brush, new Rectangle(0, 0, iconSize, iconSize), format);

				}
			}

			return bitmap;
		}


		[JsonProperty(Required = Required.Always)]
		public string Name { get; }
		[JsonProperty(Required = Required.Always)]
		public Color Dark { get; }
		[JsonProperty(Required = Required.Always)]
		public Color Light { get; }
		[JsonProperty(Required = Required.Always)]
		public Color Text { get; }






		private static Color ShiftColor(Color color, int amount)
		{
			return Color.FromArgb(color.A, Math.Clamp(color.R + amount, 0, 255), Math.Clamp(color.G + amount, 0, 255), Math.Clamp(color.B + amount, 0, 255));
		}


		public ColorGroup(string name, ColorGroup source)
			: this(name, source, false) { }

		public ColorGroup(string name, Color baseColor)
			: this(name, baseColor, false) { }

		public ColorGroup(string name, Color dark, Color light, Color text)
			: this(name, dark, light, text, false) { }


		private ColorGroup(string name, ColorGroup source, bool addToDefaults)
			: this(name, source.Dark, source.Light, source.Text, addToDefaults) { }

		private ColorGroup(string name, Color baseColor, bool addToDefaults)
			: this(name, ShiftColor(baseColor, -25), baseColor, ShiftColor(baseColor, 102), addToDefaults) { }

		private ColorGroup(string name, Color dark, Color light, Color text, bool addToDefaults)
		{
			Name = name;
			Dark = dark;
			Light = light;
			Text = text;
			if(addToDefaults) DefaultColors[name] = this;
		}


		/// <summary>
		/// Create a Linear gradient brush with the given parameters, drawing from light to dark. Remember to listen for changes to ActiveColors and do dispose of the brush when you're finished drawing
		/// </summary>
		public LinearGradientBrush CreateLinearGradientBrush(int width, int height, LinearGradientMode mode = LinearGradientMode.Vertical)
		{
			return CreateLinearGradientBrush(new Rectangle(0, 0, width, height), mode);
		}

		/// <summary>
		/// Create a Linear gradient brush with the given parameters, drawing from light to dark. Remember to listen for changes to ActiveColors and do dispose of the brush when you're finished drawing
		/// </summary>
		public LinearGradientBrush CreateLinearGradientBrush(Rectangle size, LinearGradientMode mode = LinearGradientMode.Vertical)
		{
			return new LinearGradientBrush(size, Light, Dark, mode);
		}

		#region equality bits
		public bool Equals([AllowNull] ColorGroup other)
		{
			if (other == null) return false;
			return other.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)
				&& other.Light.ToArgb().Equals(Light.ToArgb())
				&& other.Dark.ToArgb().Equals(Dark.ToArgb())
				&& Text.ToArgb().Equals(Text.ToArgb());
		}

		public override bool Equals(object o)
		{
			if (o is ColorGroup colorGroup) return Equals(colorGroup);
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name.GetHashCode(StringComparison.OrdinalIgnoreCase), Dark, Light, Text);
		}

		public static bool operator ==(ColorGroup colorGroup1, ColorGroup colorGroup2)
		{
			return colorGroup1.Equals(colorGroup2);
		}

		public static bool operator !=(ColorGroup colorGroup1, ColorGroup colorGroup2)
		{
			return !colorGroup1.Equals(colorGroup2);
		}
		#endregion


		#region auto generated default category colors		
		public static readonly ColorGroup DEFAULT_AGRICULTURAL_PRODUCTS = new ColorGroup("agricultural products",
			Color.FromArgb(92, 18, 18),
			Color.FromArgb(117, 43, 43),
			Color.FromArgb(219, 145, 145),
			true);

		public static readonly ColorGroup DEFAULT_ALLOYS = new ColorGroup("alloys",
			Color.FromArgb(77, 77, 77),
			Color.FromArgb(102, 102, 102),
			Color.FromArgb(204, 204, 204),
			true);

		public static readonly ColorGroup DEFAULT_CHEMICALS = new ColorGroup("chemicals",
			Color.FromArgb(183, 46, 91),
			Color.FromArgb(208, 71, 116),
			Color.FromArgb(255, 173, 218),
			true);

		public static readonly ColorGroup DEFAULT_CONSTRUCTION_MATERIALS = new ColorGroup("construction materials",
			Color.FromArgb(24, 91, 211),
			Color.FromArgb(49, 116, 236),
			Color.FromArgb(151, 218, 255),
			true);

		public static readonly ColorGroup DEFAULT_CONSTRUCTION_PARTS = new ColorGroup("construction parts",
			Color.FromArgb(35, 30, 68),
			Color.FromArgb(60, 55, 93),
			Color.FromArgb(162, 157, 195),
			true);

		public static readonly ColorGroup DEFAULT_CONSTRUCTION_PREFABS = new ColorGroup("construction prefabs",
			Color.FromArgb(54, 54, 54),
			Color.FromArgb(79, 79, 79),
			Color.FromArgb(181, 181, 181),
			true);

		public static readonly ColorGroup DEFAULT_CONSUMABLES_BASIC = new ColorGroup("consumables basic",
			Color.FromArgb(73, 85, 97),
			Color.FromArgb(98, 110, 122),
			Color.FromArgb(200, 212, 224),
			true);

		public static readonly ColorGroup DEFAULT_CONSUMABLES_LUXURY = new ColorGroup("consumables luxury",
			Color.FromArgb(9, 15, 15),
			Color.FromArgb(34, 40, 40),
			Color.FromArgb(136, 142, 142),
			true);

		public static readonly ColorGroup DEFAULT_DRONES = new ColorGroup("drones",
			Color.FromArgb(91, 46, 183),
			Color.FromArgb(116, 71, 208),
			Color.FromArgb(218, 173, 255),
			true);

		public static readonly ColorGroup DEFAULT_ELECTRONIC_DEVICES = new ColorGroup("electronic devices",
			Color.FromArgb(86, 20, 147),
			Color.FromArgb(111, 45, 172),
			Color.FromArgb(213, 147, 255),
			true);

		public static readonly ColorGroup DEFAULT_ELECTRONIC_PARTS = new ColorGroup("electronic parts",
			Color.FromArgb(92, 30, 122),
			Color.FromArgb(117, 55, 147),
			Color.FromArgb(219, 157, 249),
			true);

		public static readonly ColorGroup DEFAULT_ELECTRONIC_PIECES = new ColorGroup("electronic pieces",
			Color.FromArgb(73, 85, 97),
			Color.FromArgb(98, 110, 122),
			Color.FromArgb(200, 212, 224),
			true);

		public static readonly ColorGroup DEFAULT_ELECTRONIC_SYSTEMS = new ColorGroup("electronic systems",
			Color.FromArgb(49, 24, 7),
			Color.FromArgb(74, 49, 32),
			Color.FromArgb(176, 151, 134),
			true);

		public static readonly ColorGroup DEFAULT_ELEMENTS = new ColorGroup("elements",
			Color.FromArgb(91, 46, 183),
			Color.FromArgb(116, 71, 208),
			Color.FromArgb(218, 173, 255),
			true);

		public static readonly ColorGroup DEFAULT_ENERGY_SYSTEMS = new ColorGroup("energy systems",
			Color.FromArgb(51, 24, 216),
			Color.FromArgb(76, 49, 241),
			Color.FromArgb(178, 151, 255),
			true);

		public static readonly ColorGroup DEFAULT_FUELS = new ColorGroup("fuels",
			Color.FromArgb(30, 123, 30),
			Color.FromArgb(55, 148, 55),
			Color.FromArgb(157, 250, 157),
			true);

		public static readonly ColorGroup DEFAULT_GASES = new ColorGroup("gases",
			Color.FromArgb(67, 77, 87),
			Color.FromArgb(92, 102, 112),
			Color.FromArgb(194, 204, 214),
			true);

		public static readonly ColorGroup DEFAULT_LIQUIDS = new ColorGroup("liquids",
			Color.FromArgb(80, 41, 23),
			Color.FromArgb(105, 66, 48),
			Color.FromArgb(207, 168, 150),
			true);

		public static readonly ColorGroup DEFAULT_MEDICAL_EQUIPMENT = new ColorGroup("medical equipment",
			Color.FromArgb(9, 15, 15),
			Color.FromArgb(34, 40, 40),
			Color.FromArgb(136, 142, 142),
			true);

		public static readonly ColorGroup DEFAULT_METALS = new ColorGroup("metals",
			Color.FromArgb(16, 92, 87),
			Color.FromArgb(41, 117, 112),
			Color.FromArgb(143, 219, 214),
			true);

		public static readonly ColorGroup DEFAULT_MINERALS = new ColorGroup("minerals",
			Color.FromArgb(73, 85, 97),
			Color.FromArgb(98, 110, 122),
			Color.FromArgb(200, 212, 224),
			true);

		public static readonly ColorGroup DEFAULT_ORES = new ColorGroup("ores",
			Color.FromArgb(57, 95, 96),
			Color.FromArgb(82, 120, 121),
			Color.FromArgb(184, 222, 223),
			true);

		public static readonly ColorGroup DEFAULT_PLASTICS = new ColorGroup("plastics",
			Color.FromArgb(26, 60, 162),
			Color.FromArgb(51, 85, 187),
			Color.FromArgb(153, 187, 255),
			true);

		public static readonly ColorGroup DEFAULT_SHIP_ENGINES = new ColorGroup("ship engines",
			Color.FromArgb(14, 57, 14),
			Color.FromArgb(39, 82, 39),
			Color.FromArgb(141, 184, 141),
			true);

		public static readonly ColorGroup DEFAULT_SHIP_KITS = new ColorGroup("ship kits",
			Color.FromArgb(29, 36, 16),
			Color.FromArgb(54, 61, 41),
			Color.FromArgb(156, 163, 143),
			true);

		public static readonly ColorGroup DEFAULT_SHIP_PARTS = new ColorGroup("ship parts",
			Color.FromArgb(59, 45, 148),
			Color.FromArgb(84, 70, 173),
			Color.FromArgb(186, 172, 255),
			true);

		public static readonly ColorGroup DEFAULT_SHIP_SHIELDS = new ColorGroup("ship shields",
			Color.FromArgb(132, 82, 34),
			Color.FromArgb(157, 107, 59),
			Color.FromArgb(255, 209, 161),
			true);

		public static readonly ColorGroup DEFAULT_SOFTWARE_COMPONENTS = new ColorGroup("software components",
			Color.FromArgb(67, 77, 87),
			Color.FromArgb(92, 102, 112),
			Color.FromArgb(194, 204, 214),
			true);

		public static readonly ColorGroup DEFAULT_SOFTWARE_SYSTEMS = new ColorGroup("software systems",
			Color.FromArgb(26, 60, 162),
			Color.FromArgb(51, 85, 187),
			Color.FromArgb(153, 187, 255),
			true);

		public static readonly ColorGroup DEFAULT_SOFTWARE_TOOLS = new ColorGroup("software tools",
			Color.FromArgb(6, 6, 29),
			Color.FromArgb(31, 31, 54),
			Color.FromArgb(133, 133, 156),
			true);

		public static readonly ColorGroup DEFAULT_TEXTILES = new ColorGroup("textiles",
			Color.FromArgb(80, 41, 23),
			Color.FromArgb(105, 66, 48),
			Color.FromArgb(207, 168, 150),
			true);

		public static readonly ColorGroup DEFAULT_UNIT_PREFABS = new ColorGroup("unit prefabs",
			Color.FromArgb(59, 45, 148),
			Color.FromArgb(84, 70, 173),
			Color.FromArgb(186, 172, 255),
			true);

		public static readonly ColorGroup DEFAULT_UTILITY = new ColorGroup("utility",
			Color.FromArgb(54, 54, 54),
			Color.FromArgb(79, 79, 79),
			Color.FromArgb(181, 181, 181),
			true);

		public static readonly ColorGroup DEFAULT_BUILDING = new ColorGroup("building",
			Color.FromArgb(54, 54, 54),
			Color.FromArgb(79, 79, 79),
			Color.FromArgb(65, 153, 173),
			true);
		#endregion

		#region handbuilt default colors
		public static readonly ColorGroup DEFAULT_EXT = new ColorGroup("EXT", DEFAULT_ORES, true);
		public static readonly ColorGroup DEFAULT_COL = new ColorGroup("COL", DEFAULT_GASES, true);
		public static readonly ColorGroup DEFAULT_RIG = new ColorGroup("RIG", DEFAULT_LIQUIDS, true);
		
		public static readonly ColorGroup DEFAULT_BMP = new ColorGroup("BMP", Color.FromArgb(130, 180, 255), true);
		public static readonly ColorGroup DEFAULT_FRM = new ColorGroup("FRM", DEFAULT_AGRICULTURAL_PRODUCTS, true);
		public static readonly ColorGroup DEFAULT_FP = new ColorGroup("FP", Color.FromArgb(255, 130, 150), true);
		public static readonly ColorGroup DEFAULT_INC = new ColorGroup("INC", DEFAULT_AGRICULTURAL_PRODUCTS, true);
		public static readonly ColorGroup DEFAULT_PP1 = new ColorGroup("PP1",
			Color.FromArgb(215, 210, 55),
			Color.FromArgb(240, 235, 80),
			Color.FromArgb(90, 80, 30),
			true);
		public static readonly ColorGroup DEFAULT_SME = new ColorGroup("SME", DEFAULT_METALS, true);
		public static readonly ColorGroup DEFAULT_WEL = new ColorGroup("WEL", DEFAULT_CONSTRUCTION_PARTS, true);

		public static readonly ColorGroup DEFAULT_CHP = new ColorGroup("CHP", DEFAULT_CHEMICALS, true);
		public static readonly ColorGroup DEFAULT_CLF = new ColorGroup("CLF", DEFAULT_TEXTILES, true);
		public static readonly ColorGroup DEFAULT_EMD = new ColorGroup("EDM", DEFAULT_ELECTRONIC_DEVICES, true);
		public static readonly ColorGroup DEFAULT_FER = new ColorGroup("FER", DEFAULT_CONSUMABLES_LUXURY, true);
		public static readonly ColorGroup DEFAULT_FS = new ColorGroup("FS", DEFAULT_ELECTRONIC_PIECES, true);
		public static readonly ColorGroup DEFAULT_GF = new ColorGroup("GF", DEFAULT_CONSTRUCTION_MATERIALS, true);
		public static readonly ColorGroup DEFAULT_HYF = new ColorGroup("HYF", ShiftColor(DEFAULT_AGRICULTURAL_PRODUCTS.Light, -25), true);
		public static readonly ColorGroup DEFAULT_PPF = new ColorGroup("PPF", ShiftColor(DEFAULT_PLASTICS.Light, -25), true);
		public static readonly ColorGroup DEFAULT_POL = new ColorGroup("POL", DEFAULT_PLASTICS, true);
		public static readonly ColorGroup DEFAULT_PP2 = new ColorGroup("PP2", Color.FromArgb(240, 195, 50), true);
		public static readonly ColorGroup DEFAULT_REF = new ColorGroup("REF", DEFAULT_FUELS, true);
		public static readonly ColorGroup DEFAULT_UPF = new ColorGroup("UPF", DEFAULT_UNIT_PREFABS, true);
		public static readonly ColorGroup DEFAULT_WPL = new ColorGroup("WPL", DEFAULT_TEXTILES, true);

		//todo: add colours for techs and up
		#endregion

	}
}
