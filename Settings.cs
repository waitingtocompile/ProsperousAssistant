using System;
using System.Configuration;
using System.Reflection;

namespace ProsperousAssistant
{
	public static class Settings
	{
		public static Configuration Config { get; }
		public static KeyValueConfigurationCollection AppSettings { get; }



		public static string StoragePath { 
			get
			{
				string str = AppSettings["StoragePath"].Value;
				if(str == "")
				{
					str = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\Prosperous Assistant";
					StoragePath = str;
				}
				return str;
			}
			set => UpdateConfigField("StoragePath", value);
		}

		public static string APIKey
		{
			get => AppSettings["ApiKey"].Value;
			set => UpdateConfigField("ApiKey", value);
		}

		public static bool NeverPromptCreateKey
		{
			get => bool.Parse(AppSettings["NeverPromptCreateKey"].Value);
			set => UpdateConfigField("NeverPromptCreateKey", value.ToString());
		}

		public static string LastSettingPath
		{
			get => AppSettings["LastSettingPath"].Value;
			set => UpdateConfigField("LastSettingPath", value);
		}

		public static void UpdateConfigField(string key, string value)
		{
			AppSettings[key].Value = value;
			Config.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(key);
		}

		static Settings()
		{
			Config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
			AppSettings = Config.AppSettings.Settings;
		}
	}
}
