using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WaterSimulation
{
    public class WaterSimulator : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        
        private BlockHandler _waterHandler;
        private bool slowmode = false;
        private int totalTimeLastSeccond = 0;

        public WaterSimulator()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            base.Initialize();
            
            var elements = InitializeElements();
            _waterHandler = new BlockHandler(elements);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Water.Sprite = this.Content.Load<Texture2D>("Sprites/water");
            Tile.Sprite = this.Content.Load<Texture2D>("Sprites/tile");            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            bool waterCreated = false;
            //var waterCreated =  HandleInputs(Mouse.GetState(), Keyboard.GetState());
            HandleInputs(Mouse.GetState(), Keyboard.GetState());

            if (waterCreated && !slowmode)
            {
                slowmode = true;
            }

            if(slowmode)
            {
                if ((int)gameTime.TotalGameTime.TotalSeconds > totalTimeLastSeccond)
                {
                    totalTimeLastSeccond = (int)gameTime.TotalGameTime.TotalSeconds;
                    _waterHandler.MoveWater();
                }
            }
            else
            {
                _waterHandler.MoveWater();
            }

            base.Update(gameTime);
        }

        private bool HandleInputs(MouseState mouseState, KeyboardState keyboardState)
        {
            return _waterHandler.HandleInput(mouseState, keyboardState);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Render tiles
            _spriteBatch.Begin();

            _waterHandler.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private new List<Block> InitializeElements()
        {
            var elements = new List<Block>();
            InitializeTiles(elements);
            InitializeWater(elements);

            return elements;
        }

        private void InitializeTiles(List<Block> elements)
        {
           /* elements.Add(new Tile(64, 96));
            elements.Add(new Tile(64, 128));
            elements.Add(new Tile(64, 160));
            elements.Add(new Tile(96, 160));
            elements.Add(new Tile(128, 160));
            elements.Add(new Tile(160, 160));
            elements.Add(new Tile(160, 128));
            elements.Add(new Tile(160, 96));

            elements.Add(new Tile(160, 320));
            elements.Add(new Tile(160, 288));
            elements.Add(new Tile(192, 320));
            elements.Add(new Tile(224, 320));
            elements.Add(new Tile(224, 288));*/
            /* elements.Add(new Tile(192, 160));
             elements.Add(new Tile(256, 160));
             elements.Add(new Tile(320, 160));
             elements.Add(new Tile(320, 128));
             elements.Add(new Tile(320, 96));*/
        }

        private void InitializeWater(List<Block> elements)
        {
          /*  elements.Add(new Water(192, 272));
            elements.Add(new Water(208, 240));
            elements.Add(new Water(192, 224));*/
            // elements.Add(new Water(128, 64));
            // elements.Add(new Water(144, 64));
        }
    }
}
