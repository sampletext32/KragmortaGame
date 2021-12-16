﻿using System.Collections.Generic;
using KragmortaApp.Entities;
using KragmortaApp.Entities.ContextMenus;

namespace KragmortaApp
{
    public class GameState
    {
        public static GameState Instance;
        
        public readonly int HeroCount = 2;

        public readonly GameField Field;

        public IReadOnlyList<HeroModel> Heroes => _heroes;
        private readonly List<HeroModel> _heroes;

        public IReadOnlyList<MovementDeck> Decks => _decks;
        private readonly List<MovementDeck> _decks;

        public readonly Path Path;
        public readonly Push Push;

        public readonly Profile Profile;
        
        public MovementCardContextMenuModel MovementCardContextMenuModel;

        public GameState()
        {
            Instance = this;
            
            Field = new GameField(10, 7);

            Profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };
            _heroes = new List<HeroModel>(HeroCount);

            for (int i = 0; i < HeroCount; i++)
            {
                _heroes.Add(new HeroModel($"Hero {i + 1}", i * 2, 0));
            }

            MovementCardContextMenuModel = new MovementCardContextMenuModel();

            Path = new Path();
            Push = new Push();
        }
    }
}