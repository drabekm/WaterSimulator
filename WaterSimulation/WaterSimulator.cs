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

        private Dictionary<Vector2, Block> elements;

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

            //Move water
            List<Water> waterBlocks = elements.Values.Where(x => x is Water).Select(x => x as Water).ToList();
            waterBlocks.OrderBy(x => x.Y);

            foreach(var water in waterBlocks)
            {
                var bellowPosition = new Vector2(water.X, water.Y + Block.Size);

                if (!elements.ContainsKey(bellowPosition))
                {
                    var newWater = new Water(water.X, water.Y + Block.Size, water.GiveWater());
                    elements.Add(newWater.Position, newWater);
                }
                else if (elements[bellowPosition] is Water)
                {
                    var otherWater = elements[bellowPosition] as Water;
                    otherWater.WaterAmount += water.GiveWater();
                }
            }

            waterBlocks = elements.Values.Where(x => x is Water).Select(x => x as Water).ToList();
            foreach (var water in waterBlocks)
            {
                if (water.LostTooMuchWater())
                {
                    elements.Remove(water.Position);
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Render tiles
            _spriteBatch.Begin();

            foreach(var element in elements.Values)
            {
                element.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitializeElements()
        {
            elements = new Dictionary<Vector2, Block>();
            InitializeTiles();
            InitializeWater();
        }

        private void InitializeTiles()
        {
            elements.Add(new Vector2(64, 96), new Tile(64, 96));
            elements.Add(new Vector2(64, 128), new Tile(64, 128));
            elements.Add(new Vector2(64, 160), new Tile(64, 160));
            elements.Add(new Vector2(96, 160), new Tile(96, 160));
            elements.Add(new Vector2(128, 160), new Tile(128, 160));
            elements.Add(new Vector2(160, 160), new Tile(160, 160));
            elements.Add(new Vector2(160, 128), new Tile(160, 128));
            elements.Add(new Vector2(160, 96), new Tile(160, 96));
        }

        private void InitializeWater()
        {
            elements.Add(new Vector2(128, 96), new Water(128, 96));
        }
    }
}
