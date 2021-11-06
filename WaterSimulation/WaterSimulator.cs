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

        public WaterSimulator()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            base.Initialize();
            
            var elements = new List<Block>();
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

            HandleInputs(Mouse.GetState(), Keyboard.GetState());

            _waterHandler.MoveWater();

            base.Update(gameTime);
        }

        private void HandleInputs(MouseState mouseState, KeyboardState keyboardState)
        {
            _waterHandler.HandleInput(mouseState, keyboardState);
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
    }
}
