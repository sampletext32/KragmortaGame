using System;
using System.Collections.Generic;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class ProfilesPresenter : Presenter
    {
        private List<Profile> _profiles;

        private List<ProfileDrawable> _drawables;

        private Corner AlignCorner = Corner.TopRight;

        public ProfilesPresenter(List<Profile> profiles)
        {
            _drawables = new List<ProfileDrawable>(profiles.Count);
            for (var i = 0; i < profiles.Count; i++)
            {
                _drawables.Add(new ProfileDrawable(profiles[i]));
            }

            Reshape(Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);
        }

        public override void Render(RenderTarget target)
        {
            for (var i = 0; i < _drawables.Count; i++)
            {
                target.Draw(_drawables[i]);
            }
        }

        public override void OnWindowResized(int width, int height)
        {
            Reshape(width, height);
        }

        public override bool IsMouseWithinBounds(int x, int y)
        {
            return false;
        }

        private void Reshape(int width, int height)
        {
            if (AlignCorner == Corner.TopLeft)
            {
                for (var i = 0; i < _drawables.Count; i++)
                {
                    _drawables[i].SetPosition(0, i * (ProfileDrawable.BackgroundHeight + 10));
                }
            }
            else if (AlignCorner == Corner.TopRight)
            {
                for (var i = 0; i < _drawables.Count; i++)
                {
                    _drawables[i].SetPosition(width - ProfileDrawable.BackgroundWidth, i * (ProfileDrawable.BackgroundHeight + 10));
                }
            }
            else
            {
                throw new KragException($"Unsupported Corner for Profiles Presenter ({AlignCorner})");
            }
        }
    }
}