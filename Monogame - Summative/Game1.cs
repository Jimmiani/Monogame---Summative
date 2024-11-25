using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame___Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bankBackroundTexture, blurredBankBackroundTexture, backBtnTexture;
        Rectangle window, backBtnRect;

        enum Screen
        {
            Intro,
            OptionScreen,
            InstructionsScreen,
            MainScreen,
            EndScreen
        }
        Screen screen;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;
            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            // Buttons
            backBtnRect = new Rectangle(700, 25, 75, 73);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bankBackroundTexture = Content.Load<Texture2D>("bankBackround");
            blurredBankBackroundTexture = Content.Load<Texture2D>("blurredBankBackround");
            backBtnTexture = Content.Load<Texture2D>("backBtn");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(bankBackroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(backBtnTexture, backBtnRect, Color.White);
            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
