using System;
using System.IO;
using Newtonsoft.Json;

using WpfApp1.Models;

namespace WpfApp1
{
    public static class AppSettings
    {
        public static Settings? Settings 
        {
            get => _settings;
        }

        public static bool LoadSettingsFromJson(string path)
        {
            Settings? deserialized = null;
            try
            {
                var json = File.ReadAllText(path);
                deserialized = JsonConvert.DeserializeObject<Settings>(json);
            }
            catch (Exception) { }
            finally
            {
                _settings = deserialized ?? new Settings();
            }
            return deserialized is not null;
        }

        public static bool SaveSettingsToJson(string path)
        {
            try
            {
                var json = JsonConvert.SerializeObject(_settings);
                File.WriteAllText(path, json);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static Settings? _settings;
    }
}
