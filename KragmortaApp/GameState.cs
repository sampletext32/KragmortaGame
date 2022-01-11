using System.Collections.Generic;
using System.Linq;
using KragmortaApp.Entities;
using KragmortaApp.Entities.Buttons;
using KragmortaApp.Entities.ContextMenus;
using KragmortaApp.Enums;
using KragmortaApp.FileDatas;

namespace KragmortaApp
{
    public class GameState
    {
        public static GameState Instance;

        public int HeroCount { get; private set; }

        public int CurrentPlayerIndex { get; set; }

        public GameField Field;

        public IReadOnlyList<HeroModel> Heroes => _heroes;
        private List<HeroModel> _heroes;
        public RigorModel Rigor => _rigor;
        private RigorModel _rigor;

        public IReadOnlyList<Profile> Profiles => _profiles;
        private List<Profile> _profiles;

        public IReadOnlyList<MovementDeck> Decks => _heroes.Select(h => h.MovementDeck).ToList();
        public PushedStateModel PushedStateModel { get; set; }

        public Path Path;
        public Push Push;
        public Portals Portals;
        public Bookshelf Bookshelf;

        public MovementCardContextMenuModel MovementCardContextMenuModel;

        public ColorsStorage ColorsStorage;

        public FinishButtonModel FinishButtonModel;

        public static void InitForPlayers(int count)
        {
            Instance = new(count);
        }

        public static void InitFromFileData(GameFileData fileData)
        {
            Instance = new(fileData);
        }

        public GameState(GameFileData fileData)
        {
            HeroCount                    = fileData.HeroCount;
            CurrentPlayerIndex           = fileData.CurrentPlayerIndex;
            _heroes                      = fileData.Heroes.Select(f => new HeroModel(f)).ToList();
            _rigor                       = new RigorModel(fileData.Rigor);
            _profiles                    = fileData.Profiles.Select(f => new Profile(f)).ToList();
            Field                        = new GameField(fileData.Field);
            MovementCardContextMenuModel = new MovementCardContextMenuModel();
            Path                         = new Path(fileData.Path);
            Push                         = new Push(fileData.Push);
            PushedStateModel             = new PushedStateModel(fileData.PushedStateModel, _heroes);
            FinishButtonModel            = new FinishButtonModel();
            ColorsStorage                = new ColorsStorage();

            Portals   = new Portals(Field);
            Bookshelf = new Bookshelf(Field);
        }

        public GameFileData ToFileData()
        {
            return new GameFileData()
            {
                HeroCount          = HeroCount,
                CurrentPlayerIndex = CurrentPlayerIndex,
                Path               = Path.ToFileData(),
                Push               = Push.ToFileData(),
                PushedStateModel   = PushedStateModel.ToFileData(),
                Field              = Field.ToFileData(),
                Heroes             = _heroes.Select(h => h.ToFileData()).ToList(),
                Rigor              = _rigor.ToFileData(),
                Profiles           = _profiles.Select(p => p.ToFileData()).ToList()
            };
        }

        private GameState()
        {
        }

        private GameState(int count)
        {
            HeroCount = count;

            Field = new GameField(7, 10, HeroCount);

            _rigor = new RigorModel();
            _rigor.SetFieldPosition(3, 9);
            
            _heroes   = new List<HeroModel>(HeroCount);
            _profiles = new List<Profile>(HeroCount);

            for (int i = 0; i < HeroCount; i++)
            {
                _heroes.Add(new HeroModel(i + 1, (i) % Field.SizeX, (i) / Field.SizeX));
                _profiles.Add(new Profile($"Hero {i + 1}"));
            }

            SetHeroesPositions();

            MovementCardContextMenuModel = new MovementCardContextMenuModel();

            Path      = new Path();
            Push      = new Push();
            Portals   = new Portals(Field);
            Bookshelf = new Bookshelf(Field);

            PushedStateModel = new PushedStateModel();

            FinishButtonModel = new FinishButtonModel();

            ColorsStorage = new ColorsStorage();
        }

        private void SetHeroesPositions()
        {
            if (Field.FieldType == FieldType.Mini)
            {
                foreach (var hero in _heroes)
                {
                    hero.SetFieldPosition((int)hero.Id - 1, 4);
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