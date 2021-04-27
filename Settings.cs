﻿using ProsperousAssistant.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Text;

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
					str = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\ProsperousAssistant";
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