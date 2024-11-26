using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame___Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState, prevMouseState;
        Texture2D bankBackgroundTexture, blurredBankBackgroundTexture, playBtnTexture, settingsBtnTexture;
        Rectangle window, playBtnRect, settingsBtnRect;

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
            playBtnRect = new Rectangle((window.Width - 300) / 2, (window.Height - 178) / 2, 300, 178);
            settingsBtnRect = new Rectangle(650, 30, 120, 120);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bankBackgroundTexture = Content.Load<Texture2D>("Backgrounds/bankBackground");
            blurredBankBackgroundTexture = Content.Load<Texture2D>("Backgrounds/blurredBankBackground");
            playBtnTexture = Content.Load<Texture2D>("Buttons/playBtn");
            settingsBtnTexture = Content.Load<Texture2D>("Buttons/settingsBtn");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(blurredBankBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(playBtnTexture, playBtnRect, Color.White);
                _spriteBatch.Draw(settingsBtnTexture, settingsBtnRect, Color.White);
            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
