using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;

namespace KragmortaApp
{
    public class SoundCache
    {
        private Dictionary<string, Sound> _sounds;

        public SoundCache()
        {
            _sounds = new();
        }

        public Sound GetOrCache(string name)
        {
            if (_sounds.ContainsKey(name))
            {
                return _sounds[name];
            }
            else
            {
                var soundBuffer = new SoundBuffer($"assets/sounds/{name}.ogg");
                var sound       = new Sound(soundBuffer);
                _sounds[name] = sound;
                return sound;
            }
        }
    }
}