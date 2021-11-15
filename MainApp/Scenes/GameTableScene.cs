﻿using System.Collections.Generic;
using MainApp.Entities;
using MainApp.Entities.Controllers;
using MainApp.Entities.Enums;
using MainApp.Entities.Models;
using MainApp.Entities.Presenters;
using SFML.Graphics;

namespace MainApp.Scenes
{
    public class GameTableScene : Scene
    {
        private GameField _field;

        private Profile _profile;

        private ProfilePresenter _profilePresenter;
        private GameFieldPresenter _fieldPresenter;
        private MovementDeckPresenter _movementDeckPresenter;

        private GameFieldController _fieldController;
        private ShiftController _shiftController;

        public override void OnCreate()
        {
            _field = new GameField(10, 7);

            _profile = new Profile()
            {
                Nickname = "Igrovogo personaja"
            };


            _profilePresenter = new ProfilePresenter(_profile, Corner.TopRight);
            _fieldPresenter   = new GameFieldPresenter(_field);

            _movementDeckPresenter = new MovementDeckPresenter();

            _fieldController = new GameFieldController(_field, _fieldPresenter);
            _shiftController = new ShiftController(2, _movementDeckPresenter, _fieldController);
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
                return;
            }
            _fieldController.OnMouseMoved(x, y);
        }

        public override void OnMouseButtonPressed(int x, int y, KragMouseButton mouseButton)
        {
            if (_movementDeckPresenter.IsMouseWithinBounds(x, y))
            {
                // _movementDeckController.OnMouseButtonPressed(x, y, mouseButton);
                _shiftController.MovementDeckController.OnMouseButtonPressed(x, y, mouseButton);
                return;
            }

            if (_fieldPresenter.IsMouseWithinBounds(x, y))
            {
                _fieldController.OnMouseButtonPressed(x, y, mouseButton);
                _shiftController.OnMouseButtonPressed(x, y, mouseButton);
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