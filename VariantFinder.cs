using FIOSharp;
using FIOSharp.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace ProsperousAssistant
{
	public static class VariantFinder
	{
		//there currently isn't any UI for editing variant indicators at runtime, not sure if it's something I want to bother with


		private static Dictionary<Recipe, string> DefinedVariantStrings = new Dictionary<Recipe, string>();
		private static FlexibleReadWriteLock readWriteLock = new FlexibleReadWriteLock();

		public static string GetVariantString(Recipe recipe)
		{
			return readWriteLock.RunInRead(() =>
			{
				if (DefinedVariantStrings.ContainsKey(recipe)) return DefinedVariantStrings[recipe];
				return "";
			});
		}

		//get a snapshot of the all current variant strings
		public static Dictionary<Recipe, string> CopyVariantStrings()
		{
			return readWriteLock.RunInRead(() => new Dictionary<Recipe, string>(DefinedVariantStrings));
		}
		public static void AddOrUpdateVariantString(Recipe recipe, string str)
		{
			readWriteLock.RunInWrite(() =>
			{
				if (str == null)
				{
					DefinedVariantStrings.Remove(recipe);
				}
				else
				{
					DefinedVariantStrings[recipe] = str;
				}
			});
		}

		public static async Task AddOrUpdateVariantStringAsync(Recipe recipe, string str)
		{
			await readWriteLock.RunInWriteAsync(() => Task.Run(() => {
				{
					if (str == null)
					{
						DefinedVariantStrings.Remove(recipe);
					}
					else
					{
						DefinedVariantStrings[recipe] = str;
					}
				} }));
		}

		public static async Task ApplyRecipeStringsFromFile(IVariableDataSource dataHelper, TextReader input, bool clearFirst)
		{
			Task<List<Material>> materialsTask = dataHelper.GetMaterialsAsync();
			Task<List<Building>> buildingsTask = dataHelper.GetBuildingsAsync();
			Task<List<Recipe>> recipesTask = dataHelper.GetRecipesAsync();

			IEnumerable<JObject> recipeObjects;

			using (JsonTextReader jsonTextReader = new JsonTextReader(input))
			{
				JArray jArray = (JArray)await JToken.ReadFromAsync(jsonTextReader);
				recipeObjects = jArray.Children<JObject>();
			}


			(Recipe, string)[] results = await Task.WhenAll<(Recipe, string)>(recipeObjects.Select(async jObject => (Recipe.FromJson(jObject, await materialsTask, await buildingsTask), jObject.GetValue("VariantString").ToObject<string>())));
			var recipes = await recipesTask;
			if (results.Any(pair => !recipes.Contains(pair.Item1))) throw new InvalidOperationException("Unknown recipe in variant strings file");
			await readWriteLock.RunInWriteAsync(() =>Task.Run(() =>
			{
				if (clearFirst) DefinedVariantStrings.Clear();
				foreach((Recipe recipe, string str) in results)
				{
					DefinedVariantStrings[recipe] = str;
				}

			}));
		}




		public static async Task WriteVariantStringsToFile(TextWriter output)
		{
			Task<JObject[]> task = readWriteLock.RunInReadAsync<JObject[]>(() => Task.WhenAll<JObject>(DefinedVariantStrings.Select(pair => Task.Run<JObject>(() =>
			{
				JObject jObject = pair.Key.ToJson();
				jObject.Add("VariantString", pair.Value);
				return jObject;
			}))));

			JArray jArray = new JArray();
			foreach(JObject jObject in await task)
			{
				jArray.Add(jObject);
			}
			using(JsonTextWriter writer = new JsonTextWriter(output))
			{
				writer.Formatting = Formatting.Indented;
				await jArray.WriteToAsync(writer);
			}
		}
	}
}
