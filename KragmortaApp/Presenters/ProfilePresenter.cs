using System;
using System.Collections.Generic;
using KragmortaApp.Drawables;
using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Presenters
{
    public class ProfilePresenter : Presenter
    {
        private Profile _profile;

        private ProfileDrawable _drawable;

        private Corner AlignCorner = Corner.TopRight;

        public ProfilePresenter(Profile profile)
        {
            _drawable = new ProfileDrawable(profile);

            Reshape(Engine.Instance.WindowWidth, Engine.Instance.WindowHeight);
        }

        public override void Render(RenderTarget target)
        {
            target.Draw(_drawable);
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
                _drawable.SetPosition(0, 0);
            }
            else if (AlignCorner == Corner.TopRight)
            {
                _drawable.SetPosition(width - ProfileDrawable.BackgroundWidth, 0);
            }
            else
            {
                throw new KragException($"Unsupported Corner for Profiles Presenter ({AlignCorner})");
            }
        }
    }
}