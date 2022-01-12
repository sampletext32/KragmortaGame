using System;
using KragmortaApp.Entities;
using KragmortaApp.Enums;
using SFML.Graphics;
using SFML.System;

namespace KragmortaApp.Drawables
{
    public class MovementCardDrawable : Drawable
    {
        private MovementCard _movementCard;

        private Font _font;

        private Text _firstText;
        private Text _secondText;

        private RectangleShape _backgroundRectangle;

        private Sprite _backgroundSprite;
        private RenderTexture _backgroundGoblinRenderTexture;
        private RenderTexture _backgroundRigorRenderTexture;

        /// <summary>
        /// Sprite of icon in the left top corner.
        /// </summary>
        private Sprite _iconLT;

        /// <summary>
        /// Sprite of icon in the left bottom corner.
        /// </summary>
        private Sprite _iconLB;

        /// <summary>
        /// Sprite of icon in the right top corner.
        /// </summary>
        private Sprite _iconRT;

        /// <summary>
        /// Sprite of icon in the right bottom corner.
        /// </summary>
        private Sprite _iconRB;

        private int _iconSize = 15;

        private int _fontHeight = 24;

        private int _x;
        private int _y;
        public static readonly int Width = 130;
        public static readonly int Height = 180;

        public MovementCardDrawable()
        {
            _backgroundRectangle                  = new RectangleShape();
            _backgroundRectangle.Size             = new Vector2f(Width + 4, Height + 4);
            _backgroundRectangle.OutlineThickness = 2;
            _backgroundRectangle.FillColor        = new Color(255, 255, 255, 100);

            _font = Engine.Instance.FontCache.GetOrCache("arial");

            _firstText  = new Text();
            _secondText = new Text();

            _firstText.Font  = _font;
            _secondText.Font = _font;

            _firstText.CharacterSize  = (uint)_fontHeight;
            _secondText.CharacterSize = (uint)_fontHeight;
            _firstText.FillColor      = Color.Black;
            _secondText.FillColor     = Color.Black;


            _backgroundSprite = new Sprite();

            _iconLT = new Sprite();
            _iconRT = new Sprite();

            _iconLB = new Sprite();
            _iconRB = new Sprite();

            // LoadIconsSprites();
        }

        private void LoadIconsTextures()
        {
            _iconLT.Texture =
                Engine.Instance.TextureCache.GetOrCache(Enum.GetName(typeof(CellType), _movementCard.FirstType));
            _iconRT.Texture = Engine.Instance.TextureCache.GetOrCache(Enum.GetName(typeof(CellType), _movementCard.FirstType));

            _iconLB.Texture = Engine.Instance.TextureCache.GetOrCache(Enum.GetName(typeof(CellType), _movementCard.SecondType));
            _iconRB.Texture = Engine.Instance.TextureCache.GetOrCache(Enum.GetName(typeof(CellType), _movementCard.SecondType));

            ProcessScalingOfIcons();
        }

        private void ProcessScalingOfIcons()
        {
            Vector2f appliedScale;
            switch (_movementCard.FirstType)
            {
                case CellType.Blue:
                {
                    appliedScale = new Vector2f(0.45f, 0.45f);
                    break;
                }
                case CellType.Common:
                {
                    appliedScale = new Vector2f(0.2f, 0.2f);
                    break;
                }
                case CellType.Green:
                {
                    appliedScale = new Vector2f(0.45f, 0.45f);
                    break;
                }
                case CellType.Orange:
                {
                    appliedScale = new Vector2f(0.36f, 0.36f);
                    break;
                }
                case CellType.Red:
                {
                    appliedScale = new Vector2f(0.23f, 0.23f);
                    break;
                }
                default:
                    appliedScale = new Vector2f();
                    break;
            }

            _iconLT.Scale = _iconRT.Scale = appliedScale;
            
            switch (_movementCard.SecondType)
            {
                case CellType.Blue:
                {
                    appliedScale = new Vector2f(0.45f, 0.45f);
                    break;
                }
                case CellType.Common:
                {
                    appliedScale = new Vector2f(0.2f, 0.2f);
                    break;
                }
                case CellType.Green:
                {
                    appliedScale = new Vector2f(0.45f, 0.45f);
                    break;
                }
                case CellType.Orange:
                {
                    appliedScale = new Vector2f(0.36f, 0.36f);
                    break;
                }
                case CellType.Red:
                {
                    appliedScale = new Vector2f(0.23f, 0.23f);
                    break;
                }
            }

            _iconLB.Scale = _iconRB.Scale = appliedScale;
        }

        public void SetCard(MovementCard card)
        {
            _movementCard = card;

            if (card == null)
            {
                return;
            }

            var texture = _movementCard.MovementCardType == MovementCardType.Goblin
                ? Engine.Instance.TextureCache.GetOrCache("movement/goblin-move")
                : Engine.Instance.TextureCache.GetOrCache("movement/mortis-move");
            _backgroundSprite.Texture = texture;
            _backgroundSprite.Scale = new Vector2f((float)Width / _backgroundSprite.Texture.Size.X,
                (float)Height / _backgroundSprite.Texture.Size.Y);

            _firstText.DisplayedString  = _movementCard.FirstType.ToString();
            _secondText.DisplayedString = _movementCard.SecondType.ToString();

            LoadIconsTextures();

            Update();
        }

        public void SetPosition(int x, int y)
        {
            _x = x;
            _y = y;

            _backgroundRectangle.Position = new Vector2f(x - 2, y - 2);
            _backgroundSprite.Position    = new Vector2f(x, y);

            _firstText.Position  = new Vector2f(x, y);
            _secondText.Position = new Vector2f(x, y + _fontHeight * 1.5f);

            SetIconsPositions(x, y);
        }

        private void SetIconsPositions(int x, int y)
        {
            switch (_movementCard.FirstType)
            {
                case CellType.Blue:
                {
                    _iconLT.Position = new Vector2f(x + 13, y + 1);
                    _iconRT.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRT.GetGlobalBounds().Width - 15,
                        y + 1);
                    
                    break;
                }
                case CellType.Common:
                {
                    _iconLT.Position = new Vector2f(x + 15, y + 10);
                    _iconRT.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRT.GetGlobalBounds().Width - 15,
                        y + 10);
                    
                    break;
                }
                case CellType.Green:
                {
                    _iconLT.Position = new Vector2f(x + 13, y + 1);
                    _iconRT.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRT.GetGlobalBounds().Width - 15,
                        y + 1);
                    
                    break;
                }
                case CellType.Orange:
                {
                    _iconLT.Position = new Vector2f(x + 15, y + 4);
                    _iconRT.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRT.GetGlobalBounds().Width - 17,
                        y + 4);
                    
                    break;
                }
                case CellType.Red:
                {
                    _iconLT.Position = new Vector2f(x + 17, y + 9);
                    _iconRT.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRT.GetGlobalBounds().Width - 19,
                        y + 9);
                    
                    break;
                }
            }

            var verticalOffset = 28;
            switch (_movementCard.SecondType)
            {
                case CellType.Blue:
                {
                    _iconLB.Position = new Vector2f(x + 8, y + verticalOffset);
                    _iconRB.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRB.GetGlobalBounds().Width - 12,
                        y + verticalOffset);
                    
                    break;
                }
                case CellType.Common:
                {
                    _iconLB.Position = new Vector2f(x + 10, y + verticalOffset + 5);
                    _iconRB.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRB.GetGlobalBounds().Width - 15,
                        y + verticalOffset + 8);
                    
                    break;
                }
                case CellType.Green:
                {
                    _iconLB.Position = new Vector2f(x + 8, y + verticalOffset);
                    _iconRB.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRB.GetGlobalBounds().Width - 13,
                        y + verticalOffset + 2);
                    
                    break;
                }
                case CellType.Orange:
                {
                    _iconLB.Position = new Vector2f(x + 10, y + verticalOffset + 2);
                    _iconRB.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRB.GetGlobalBounds().Width - 15,
                        y + verticalOffset + 4);
                    
                    break;
                }
                case CellType.Red:
                {
                    _iconLB.Position = new Vector2f(x + 11, y + verticalOffset + 6);
                    _iconRB.Position = new Vector2f(x + _backgroundSprite.GetGlobalBounds().Width - _iconRB.GetGlobalBounds().Width - 17,
                        y + verticalOffset + 9);
                    
                    break;
                }
            }

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (_movementCard is null)
            {
                return;
            }

            if (_movementCard.Dirty)
            {
                Update();
                _movementCard.ClearDirty();
            }

            target.Draw(_backgroundRectangle, states);
            target.Draw(_backgroundSprite, states);
            
            target.Draw(_iconLT);
            target.Draw(_iconLB);
            target.Draw(_iconRT);
            target.Draw(_iconRB);
            
            // target.Draw(_firstText, states);
            // target.Draw(_secondText, states);
        }

        private void Update()
        {
            if (_movementCard.Activated)
            {
                _backgroundRectangle.OutlineColor = _backgroundRectangle.FillColor = new Color(255, 0, 0);
            }
            else if (_movementCard.Selected)
            {
                _backgroundRectangle.OutlineColor = _backgroundRectangle.FillColor = new Color(255, 0, 0, 100);
            }
            else
            {
                _backgroundRectangle.OutlineColor = _backgroundRectangle.FillColor = new Color(255, 255, 255, 100);
            }
        }

        public bool TryGetCardFromMousePosition(int x, int y, out MovementCard card)
        {
            if (x < _x ||
                x >= _x + Width ||
                y < _y ||
                y >= _y + Height
            )
            {
                card = null;
                return false;
            }

            card = _movementCard;
            return true;
        }
    }
}