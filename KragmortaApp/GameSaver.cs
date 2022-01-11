using System.IO;
using System.Text.Json;
using KragmortaApp.FileDatas;

namespace KragmortaApp
{
    public class GameSaver
    {
        public void Save(GameState state)
        {
            var json = JsonSerializer.Serialize(state.ToFileData(), new JsonSerializerOptions()
            {
                WriteIndented = true
            });

            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }
            File.WriteAllText("saves/save.json", json);
        }

        public void Load()
        {
            if (File.Exists("saves/save.json"))
            {
                var jsonString = File.ReadAllText("saves/save.json");

                var gameFileData = JsonSerializer.Deserialize<GameFileData>(jsonString);
                
                // TODO (Not important): Validate loaded save file and try-catch all file logic
                
                GameState.InitFromFileData(gameFileData);
            }
            else
            {
                throw new KragException("saves/save.json file wasn't found");
            }
        }
    }
}