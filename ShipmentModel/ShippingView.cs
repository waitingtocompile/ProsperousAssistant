using FIOSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProsperousAssistant.Util;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProsperousAssistant.ShipmentModel
{
	public partial class ShippingView : UserControl, ISavesState
	{
		private ShipmentCalculator Calculator { get; }

		public CachedDataHelper DataHelper { get; }

		public string ShipmentRowsDir => Settings.StoragePath + "\\shipping calculator\\row presets";
		public string ShipmentSettingsDir => Settings.StoragePath + "\\shipping calculator\\settings presets";


		public ShippingView(CachedDataHelper dataHelper)
		{
			InitializeComponent();
			DataHelper = dataHelper;
			Calculator = new ShipmentCalculator(dataHelper);
		}

		public void SaveState()
		{
			Task.WhenAll(SaveRowsAsync($"{ShipmentRowsDir}\\{Settings.LastSettingPath}"), 
				SaveSettingsAsync($"{ShipmentSettingsDir}\\{Settings.LastSettingPath}"))
				.Wait();
		}

		public async Task SaveRowsAsync(string file)
		{
			Task<JObject> rowsTask = Task.Run(Calculator.RowsToJson);
			using (StreamWriter stream = File.CreateText(file))
			{
				using JsonTextWriter jsonWriter = new JsonTextWriter(stream);
				jsonWriter.Formatting = Formatting.Indented;
				await (await rowsTask).WriteToAsync(jsonWriter);
			}
		}

		public async Task SaveSettingsAsync(string file)
		{
			Task<JObject> settingsTask = Task.Run(Calculator.SettingsToJson);
			using (StreamWriter stream = File.CreateText(file))
			{
				using JsonTextWriter jsonWriter = new JsonTextWriter(stream);
				jsonWriter.Formatting = Formatting.Indented;
				await (await settingsTask).WriteToAsync(jsonWriter);
			}
		}

		private bool ReadRows(string filePath)
		{
			if (!File.Exists(filePath)) return false;
			JObject jObject;
			using (StreamReader stream = File.OpenText(filePath))
			{
				using JsonTextReader jsonReader = new JsonTextReader(stream);
				try
				{
					jObject = (JObject)JToken.ReadFrom(jsonReader);
				}
				catch
				{
					throw new JsonSchemaException("Invalid json schema, expected a json object");
				}
			}
			Calculator.RowsFromJson(jObject);
			return true;
		}

		private bool ReadSettings(string filePath)
		{
			if (!File.Exists(filePath)) return false;
			JObject jObject;
			using (StreamReader stream = File.OpenText(filePath))
			{
				using JsonTextReader jsonReader = new JsonTextReader(stream);
				try
				{
					jObject = (JObject)JToken.ReadFrom(jsonReader);
				}
				catch
				{
					throw new JsonSchemaException("Invalid json schema, expected a json object");
				}
			}
			Calculator.SettingsFromJson(jObject);
			return true;
		}
	}
}
