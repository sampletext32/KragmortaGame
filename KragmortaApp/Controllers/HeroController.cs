using System;
using KragmortaApp.Entities;
using KragmortaApp.Presenters;

namespace KragmortaApp.Controllers
{
    public class HeroController : ControllerBase
    {
        private HeroModel _hero;

        public HeroController(HeroModel hero)
        {
            _hero = hero;
        }

        public void Activate()
        {
            _hero.IsCurrentHero = true;
            _hero.MarkDirty();
        }

        public void Deactivate()
        {
            _hero.IsCurrentHero = false;
            _hero.MarkDirty();
        }
    }
}