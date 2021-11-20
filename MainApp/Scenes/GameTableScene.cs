﻿using System.Collections.Generic;
using MainApp.Controllers;
using MainApp.Entities;
using MainApp.Enums;
using MainApp.Handlers;
using MainApp.Presenters;
using SFML.Graphics;

namespace MainApp.Scenes
{
    public class GameTableScene : Scene
    {
        #region Models
        
        private GameField _field;

        private int _heroesCount;
        private List<HeroModel> _heroes;
        private List<MovementDeck> _decks;
        private List<PathCell> _path;
        
        #endregion
        
        
        
        #region Presenters
        
        private ProfilePresenter _profilePresenter;
        private GameFieldPresenter _fieldPresenter;
        private MovementDeckPresenter _movementDeckPresenter;
        private List<HeroPresenter> _heroPresenters;
        private PathPresenter _pathPresenter;

        #endregion
        
        
        #region Controllers
        
        private GameFieldController _fieldController;
        private MovementDeckController _movementDeckController;
        private ShiftController _shiftController;
        private PathController _pathsController;

        #endregion
        #region Handlers

        private GameFieldHandler _gameFieldHandler;
        private MovementDecksHandler _movementDecksHandler;

        #endregion

        #region Layers

        

        #endregion


        private Profile _profile;




        
        // TODO: init all models,
        // TODO: init all controllers,
        // TODO: init all handlers, pass them necessary controllers
        
        // TODO: make LayersStack,
        // TODO: make Layer entity
        
        // TODO: Connect Controllers and Presenters to models,
        // TODO: Change models due to Controllers,
        // TODO: Show models via Presenters
        
        // TODO: Connect Layer to its Presenter,
        // TODO: Connect Layer to its Handler
        
        public override void OnCreate()
        {
            _field = new GameField(10, 7);

            _profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };


            // Initiating all Models:
            var heroesCount = 2;
             
            
            // Initiating all Presenters:
            
            // Initiating all Controllers:
            
            // Initiating all Handlers:
            
            // Initiating LayersStack:
            
            // Initiating all Layers:
            
            _profilePresenter = new ProfilePresenter(_profile, Corner.TopRight);
            _fieldPresenter   = new GameFieldPresenter(_field);

            _movementDeckPresenter = new MovementDeckPresenter();

            _fieldController = new GameFieldController(_field, _fieldPresenter);
            _shiftController = new ShiftController(2, _movementDeckPresenter, _fieldController);
            _pathsController = new PathController(_field, _fieldPresenter, _shiftController);
        }

        public override void OnUpdate(float deltaTime)
        {
        }

        public override void OnRender(RenderTarget target)
        {
            _fieldPresenter.Render(target);
            _profilePresenter.Render(target);
            foreach (var heroPresenter in _shiftController.HeroPresenters)
            {
                heroPresenter.Render(target);
            }

            _movementDeckPresenter.Render(target);
        }

        public override void OnMouseMoved(int x, int y)
        {
            if (_movementDeckPresenter.IsMouseWithinBounds(x, y))
            {
                _fieldController.OnMouseExit();
                return;
            }

            if (_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                _fieldController.OnMouseMoved(x, y);
            }
            else
            {
                // TODO: this is being called for every move outside field, but should only be called once
                _fieldController.OnMouseExit();
            }
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            if (_movementDeckPresenter.IsMouseWithinBounds(x, y))
            {
                // _movementDeckController.OnMouseButtonPressed(x, y, mouseButton);
                _shiftController.MovementDeckController.OnMouseButtonPressed(x, y, mouseButton);

                if (_shiftController.MovementDeckController.HasSelectedCard())
                {
                    _pathsController.UnhighlightPaths();
                    _pathsController.HighlightPaths();
                }

                return;
            }

            if (_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                _fieldController.OnMouseButtonPressed(x, y, mouseButton);
                _shiftController.OnMouseButtonPressed(x, y, mouseButton);

                _pathsController.UnhighlightPaths();
                if (_shiftController.WasLastMoveSuccessful() &&
                    _shiftController.MovementDeckController.HasActivatedCard())
                {
                    _pathsController.HighlightPaths();
                }
            }
        }

        public override void OnMouseButtonReleased(int x, int y, KragMouseButton mouseButton)
        {
            _fieldController.OnMouseButtonReleased(x, y, mouseButton);
            _shiftController.OnMouseButtonReleased(x, y, mouseButton);
        }

        public override void OnWindowResized(int width, int height)
        {
            _profilePresenter.OnWindowResized(width, height);
            _movementDeckPresenter.OnWindowResized(width, height);
        }
    }
}