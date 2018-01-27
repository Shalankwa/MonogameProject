using Game1.Code;
using Game1.Code.Components;
using Game1.Code.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private BaseObject player;
        private InputManager manageInput;

		public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
			Resolution.Init(ref graphics);
			Content.RootDirectory = "Content";

			//this.graphics.PreferredBackBufferHeight = 240;
			//this.graphics.PreferredBackBufferWidth = 320;

			Resolution.SetVirtualResolution(320, 270);
			Resolution.SetResolution(320, 270, false);

			player = new BaseObject();
            manageInput = new InputManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
			// TODO: Add your initialization logic here
			//Window.AllowUserResizing = true;

			base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player.AddComponent(new Sprite(Content.Load<Texture2D>("link_full"), 16, 16, new Vector2(50, 50)));
            player.AddComponent(new PlayerInput());
			player.AddComponent(new Animation(16, 16));

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            manageInput.Update(gameTime.ElapsedGameTime.Milliseconds);
            player.Update(gameTime.ElapsedGameTime.Milliseconds);
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGoldenrodYellow);

			spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Resolution.getTransformationMatrix());

			player.Draw(spriteBatch);

            //Debug.WriteLine("Testing Debug");

            spriteBatch.End();
			
            base.Draw(gameTime);
        }
    }
}
