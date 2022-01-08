using System.Collections.Generic;
using KragmortaApp.Entities;
using KragmortaApp.Entities.Buttons;
using KragmortaApp.Entities.ContextMenus;
using KragmortaApp.Enums;

namespace KragmortaApp
{
    public class GameState
    {
        public static GameState Instance;

        public readonly int HeroCount;

        public readonly GameField Field;

        public IReadOnlyList<HeroModel> Heroes => _heroes;
        private readonly List<HeroModel> _heroes;

        public IReadOnlyList<MovementDeck> Decks => _decks;
        private readonly List<MovementDeck> _decks;

        public readonly Path Path;
        public readonly Push Push;
        public readonly Portal Portal;
        public readonly Bookshelf Bookshelf;

        public readonly Profile Profile;

        public MovementCardContextMenuModel MovementCardContextMenuModel;

        public ColorsStorage ColorsStorage;

        public readonly FinishButtonModel FinishButtonModel;
        public static void InitForPlayers(int count)
        {
            Instance = new(count);
        }

        private GameState(int count)
        {
            HeroCount = count;
            
            Field = new GameField(7, 10, HeroCount);
            
            _heroes = new List<HeroModel>(HeroCount);
            for (int i = 0; i < HeroCount; i++)
            {
                _heroes.Add(new HeroModel(i + 1, $"Hero {i + 1}", (i) % Field.SizeX, (i) / Field.SizeX));
            }

            SetHeroesPositions();
            
            Profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };
           

            MovementCardContextMenuModel = new MovementCardContextMenuModel();

            Path      = new Path();
            Push      = new Push();
            Portal    = new Portal(Field);
            Bookshelf = new Bookshelf(Field);

            FinishButtonModel = new FinishButtonModel();

            ColorsStorage = new ColorsStorage();
        }

        private void SetHeroesPositions()
        {
            if (Field.FieldType == FieldType.Mini)
            {
                foreach (var hero in _heroes)
                {
                    hero.SetFieldPosition((int)hero.Id - 1,4);
                }

                return;
            }

            if (Field.FieldType == FieldType.Medium)
            {
                foreach (var hero in _heroes)
                {
                    var heroX = (int)hero.Id - 1;
                    hero.SetFieldPosition(heroX, heroX > 3 ? 3 : 2);
                }

                return;
            }

            if (Field.FieldType == FieldType.Large)
            {
                for (int i = 0; i < 7; i++)
                {
                    _heroes[i].SetFieldPosition((int)_heroes[i].Id - 1, 0);
                }

                if (_heroes.Count == 8)
                {
                    _heroes[7].SetFieldPosition(0, 1);
                }
            }
        }
    }
}