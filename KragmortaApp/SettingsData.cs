using System.IO;
using System.Text.Json;

namespace KragmortaApp
{
    public class SettingsData
    {
        public int ResolutionWidth { get; set; }
        public int ResolutionHeight { get; set; }

        public bool FullScreen { get; set; }
        public bool EnableSounds { get; set; }
        
        public int Volume { get; set; }

        public void Save()
        {
            var json = JsonSerializer.Serialize(this);
            
            File.WriteAllText("settings.json", json);
        }

        public static SettingsData Load()
        {
            if (!File.Exists("settings.json"))
            {
                new SettingsData(){ResolutionWidth = 1280, ResolutionHeight = 720, Volume = 100}.Save();
            }
            var json = File.ReadAllText("settings.json");
            return JsonSerializer.Deserialize<SettingsData>(json);
        }
    }
}