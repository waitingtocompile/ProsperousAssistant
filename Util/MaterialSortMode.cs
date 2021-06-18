using FIOSharp.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ProsperousAssistant.Util
{
	public class MaterialSortMode : IComparer<Material>
	{
		public static IReadOnlyDictionary<string, MaterialSortMode> ALL_MODES => allModes;
		private static readonly Dictionary<string, MaterialSortMode> allModes = new Dictionary<string, MaterialSortMode>();

		public static MaterialSortMode NAME { get; } = new MaterialSortMode("Name", (mat1, mat2) => mat1.Name.CompareTo(mat2.Name));
		public static MaterialSortMode TICKER { get; } = new MaterialSortMode("Ticker", (mat1, mat2) => mat1.Ticker.CompareTo(mat2.Ticker));
		public static MaterialSortMode CATEGORY { get; } = new MaterialSortMode("Category", (mat1, mat2) =>
		{
			int comp = mat1.Category.CompareTo(mat2.Category);
			if (comp == 0)
			{
				comp = NAME.Compare(mat1, mat2);
			}
			return comp;
		});

		

		public string Name { get; }
		private readonly Func<Material, Material, int> comparer;
		public int Compare([AllowNull] Material x, [AllowNull] Material y) => comparer(x, y);

		private MaterialSortMode(string name, Func<Material, Material, int> func)
		{
			Name = name;
			comparer = func;
			allModes.Add(name, this);
		}


		//get the material that the defined material should be inserted after, accoding to a given sort mode, or null if the item should be instered at the front
		//I think this might not work properly an more? I'm not using it any anyway so who knows
		public static Material? InsertAfter(Material materialToInsert, IEnumerable<Material> insertTo, IComparer<Material> sortMode)
		{
			Material? insertAfter = null;

			foreach(Material material in insertTo)
			{
				if(sortMode.Compare(materialToInsert, material) >= 0)
				{
					//our material to insert belongs after the currently inspected item, compare to our currently considered item
					if(insertAfter.HasValue && sortMode.Compare(material, insertAfter.Value) < 0)
					{
						//our inspected material is after the currently held item, replace
						insertAfter = material;
					}
				}

			}

			return insertAfter;
		}


		
	}
}
