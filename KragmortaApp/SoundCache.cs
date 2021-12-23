using System.Collections.Generic;
using SFML.Audio;

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
                sound.Volume  = Engine.Instance.Settings.Volume;
                _sounds[name] = sound;
                return sound;
            }
        }

        public void SetVolume(int volume)
        {
            if (volume < 0 || volume > 100)
            {
                throw new KragException($"Invalid volume value: {volume}");
            }

            foreach (var (key, value) in _sounds)
            {
                value.Volume = volume;
            }
        }
    }
}