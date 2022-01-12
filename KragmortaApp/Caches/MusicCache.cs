using System.Collections.Generic;
using SFML.Audio;

namespace KragmortaApp.Caches
{
    public class MusicCache
    {
        private Dictionary<string, Music> _musics;

        public MusicCache()
        {
            _musics = new();
        }

        public Music GetOrCache(string name)
        {
            if (_musics.ContainsKey(name))
            {
                return _musics[name];
            }
            else
            {
                var music = new Music($"assets/music/{name}.ogg");
                music.Volume  = Engine.Instance.Settings.Volume;
                _musics[name] = music;
                return music;
            }
        }

        public void SetVolume(int volume)
        {
            if (volume < 0 || volume > 100)
            {
                throw new KragException($"Invalid volume value: {volume}");
            }

            foreach (var (key, value) in _musics)
            {
                value.Volume = volume;
            }
        }
    }
}