using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace WaterSimulation
{
    public class WaterSimulator : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Block> elements;

        public WaterSimulator()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {            
            base.Initialize();

            InitializeElements();
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


            MouseInputHandler.HandleClick(Mouse.GetState(), elements);
            WaterHandler.MoveWater(elements);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Render tiles
            _spriteBatch.Begin();

            foreach(var element in elements)
            {
                element.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitializeElements()
        {
            elements = new List<Block>();
            InitializeTiles();
            InitializeWater();
        }

        private void InitializeTiles()
        {
            elements.Add(new Tile(64, 96));
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
            elements.Add(new Tile(224, 288));
            /* elements.Add(new Tile(192, 160));
             elements.Add(new Tile(256, 160));
             elements.Add(new Tile(320, 160));
             elements.Add(new Tile(320, 128));
             elements.Add(new Tile(320, 96));*/
        }

        private void InitializeWater()
        {
            elements.Add(new Water(128, 64));
        }
    }
}
