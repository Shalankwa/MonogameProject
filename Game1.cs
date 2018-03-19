using Game1.Code;
using Game1.Code.Components;
using Game1.Code.Components.AIControllers;
using Game1.Code.Managers;
using Game1.Code.Map;
using Game1.Code.Screens;
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

		private InputManager manageInput;
		private ScreenManager screenManager;

		// Screen Sizes for spritebatch rendering
		Point virtualScreenSize = new Point(320, 240);
		Point screenSize = new Point(1080, 720);

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			//Resolution.Init(ref graphics);
			Content.RootDirectory = "Content";

			this.graphics.PreferredBackBufferWidth = screenSize.X;
			this.graphics.PreferredBackBufferHeight = screenSize.Y;

			// Resolution class method of setting resolution, Uses Matrix, causes pixel errors
			//Resolution.SetVirtualResolution(320, 270);		
			//Resolution.SetResolution(320 * 2, 270 * 2, false);

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

			screenManager = new ScreenManager(Content, virtualScreenSize);
			//screenManager.loadNewScreen(new ScreenDungeon(screenManager, virtualScreenSize, Content));
			screenManager.loadNewScreen(new ScreenDungeon(screenManager, Content));


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
			screenManager.Update(gameTime.ElapsedGameTime.Milliseconds);


			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			//spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Resolution.getTransformationMatrix());

			SpriteBatch targetBatch = new SpriteBatch(GraphicsDevice);
			RenderTarget2D target = new RenderTarget2D(GraphicsDevice, virtualScreenSize.X, virtualScreenSize.Y);
			GraphicsDevice.SetRenderTarget(target);


			spriteBatch.Begin(sortMode:SpriteSortMode.FrontToBack);

			screenManager.Draw(spriteBatch);

			spriteBatch.End();

			//set rendering back to the back buffer
			GraphicsDevice.SetRenderTarget(null);

			//render target to back buffer
			targetBatch.Begin();
			targetBatch.Draw(target, new Rectangle(0, 0, screenSize.X, screenSize.Y), Color.White);
			targetBatch.End();

			base.Draw(gameTime);

			//STOP THE LEAK
			target.Dispose();
			targetBatch.Dispose();
		}
	}
}
