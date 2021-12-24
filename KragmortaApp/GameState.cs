using System.Collections.Generic;
using KragmortaApp.Entities;
using KragmortaApp.Entities.Buttons;
using KragmortaApp.Entities.ContextMenus;

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
            Field     = new GameField(7, 10);
            // Field = new GameField(10, 7);

            Profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };
            _heroes = new List<HeroModel>(HeroCount);

            for (int i = 0; i < HeroCount; i++)
            {
                _heroes.Add(new HeroModel(i + 1, $"Hero {i + 1}", (i) % Field.SizeX, (i) / Field.SizeX));
            }

            MovementCardContextMenuModel = new MovementCardContextMenuModel();

            Path   = new Path();
            Push   = new Push();
            Portal = new Portal(Field);

            FinishButtonModel = new FinishButtonModel();

            ColorsStorage = new ColorsStorage();
        }
    }
}