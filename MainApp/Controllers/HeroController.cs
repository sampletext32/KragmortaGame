using System;
using MainApp.Entities;
using MainApp.Presenters;

namespace MainApp.Controllers
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