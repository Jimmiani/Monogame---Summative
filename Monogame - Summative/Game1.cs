using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Monogame___Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState, prevMouseState;
        Color musicColour;

        // Images
        Texture2D bankBackgroundTexture, blurredBankBackgroundTexture, menuTexture, menuBackgroundTexture;
        Rectangle window, menuRect;

        // Buttons
        Texture2D playBtnTexture, settingsBtnTexture, noBtnTexture, musicBtnTexture, instructionsBtnTexture, backBtnTexture;
        Rectangle playBtnRect, settingsBtnRect, noBtnRect, musicBtnRect, instructionsBtnRect, backBtnRect;

        // Fonts
        SpriteFont titleFont, howToPlayFont;
        string instructions;

        // Songs
        Song hitmanSong;

        enum Screen
        {
            Intro,
            MenuScreen,
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

            hitmanSong = Content.Load<Song>("Audio/hitman");
            MediaPlayer.Play(hitmanSong);
            musicColour = Color.White;


            instructions = "Welcome to Vault Raiders! You and your crew are expert\n" +
                           "thieves, and tonight is your big heist. Your goal is\n" +
                           "simple: infiltrate the bank, crack open the vault, and get\n" +
                           "away with the cash before the cops show up and ruin your\n" +
                           "plans. But there's a catch. The clock is ticking, and if\n" +
                           "you're caught, you’ll be heading to jail faster than you\n" +
                           "can say getaway car.";


            // Buttons
            playBtnRect = new Rectangle((window.Width - 300) / 2, 350, 300, 178);
            settingsBtnRect = new Rectangle(690, 30, 80, 80);
            musicBtnRect = new Rectangle(240, 240, 110, 110);
            noBtnRect = new Rectangle(590, 100, 80, 80);
            instructionsBtnRect = new Rectangle(450, 240, 110, 110);
            backBtnRect = new Rectangle(120, 100, 80, 80);

            // Images
            menuRect = new Rectangle((window.Width - 500) / 2, (window.Height - 350) / 2, 500, 350);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Backgrounds
            bankBackgroundTexture = Content.Load<Texture2D>("Backgrounds/bankBackground");
            blurredBankBackgroundTexture = Content.Load<Texture2D>("Backgrounds/blurredBankBackground");
            menuBackgroundTexture = Content.Load<Texture2D>("Backgrounds/menuBackground");
            
            // Buttons
            playBtnTexture = Content.Load<Texture2D>("Buttons/playBtn");
            settingsBtnTexture = Content.Load<Texture2D>("Buttons/settingsBtn");
            noBtnTexture = Content.Load<Texture2D>("Buttons/noBtn");
            musicBtnTexture = Content.Load<Texture2D>("Buttons/musicBtn");
            instructionsBtnTexture = Content.Load<Texture2D>("Buttons/instructionsBtn");
            backBtnTexture = Content.Load<Texture2D>("Buttons/backBtn");
            
            // Fonts
            titleFont = Content.Load<SpriteFont>("Fonts/titleFont");
            howToPlayFont = Content.Load<SpriteFont>("Fonts/howToPlayFont");

            // Images
            menuTexture = Content.Load<Texture2D>("Images/menuScreen");
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (screen == Screen.Intro)
            {
                // Play Button
                if (playBtnRect.Contains(mouseState.Position))
                {
                    playBtnRect = new Rectangle((window.Width - 320) / 2, 345, 320, 190);
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        playBtnRect = new Rectangle((window.Width - 280) / 2, 355, 280, 166);
                    }
                }
                else if (!playBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        playBtnRect = new Rectangle((window.Width - 300) / 2, 350, 300, 178);
                    }
                }

                // Settings Button
                if (settingsBtnRect.Contains(mouseState.Position))
                {
                    settingsBtnRect = new Rectangle(684, 24, 92, 92);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        settingsBtnRect = new Rectangle(696, 36, 68, 68);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.MenuScreen;
                            settingsBtnRect = new Rectangle(690, 30, 80, 80);
                        }
                    }
                }
                else if (!settingsBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        settingsBtnRect = new Rectangle(690, 30, 80, 80);
                    }
                }
            }
            
            else if (screen == Screen.MenuScreen)
            {
                // No Button
                if (noBtnRect.Contains(mouseState.Position))
                {
                    noBtnRect = new Rectangle(584, 94, 92, 92);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        noBtnRect = new Rectangle(596, 106, 68, 68);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.Intro;
                            noBtnRect = new Rectangle(590, 100, 80, 80);
                        }
                    }
                }
                else if (!noBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        noBtnRect = new Rectangle(590, 100, 80, 80);
                    }
                }

                // Music Button
                if (musicBtnRect.Contains(mouseState.Position))
                {
                    musicBtnRect = new Rectangle(234, 236, 122, 122);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        musicBtnRect = new Rectangle(246, 246, 98, 98);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            musicBtnRect = new Rectangle(240, 240, 110, 110);
                            if (MediaPlayer.State == MediaState.Playing)
                            {
                                musicColour = Color.DarkGray;
                                MediaPlayer.Pause();
                            }
                            else if (MediaPlayer.State == MediaState.Paused)
                            {
                                musicColour = Color.White;
                                MediaPlayer.Resume();
                            }
                        }
                    }
                }
                else if (!musicBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        musicBtnRect = new Rectangle(240, 240, 110, 110);
                    }
                }

                // Instructions Button
                if (instructionsBtnRect.Contains(mouseState.Position))
                {
                    instructionsBtnRect = new Rectangle(446, 234, 122, 122);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        instructionsBtnRect = new Rectangle(456, 246, 98, 98);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.InstructionsScreen;
                            instructionsBtnRect = new Rectangle(450, 240, 110, 110);
                        }
                    }
                }
                else if (!instructionsBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        instructionsBtnRect = new Rectangle(450, 240, 110, 110);
                    }
                }
            }

            else if (screen == Screen.InstructionsScreen)
            {
                // No Button
                if (noBtnRect.Contains(mouseState.Position))
                {
                    noBtnRect = new Rectangle(584, 94, 92, 92);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        noBtnRect = new Rectangle(596, 106, 68, 68);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.Intro;
                            noBtnRect = new Rectangle(590, 100, 80, 80);
                        }
                    }
                }
                else if (!noBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        noBtnRect = new Rectangle(590, 100, 80, 80);
                    }
                }

                // Back Button
                if (backBtnRect.Contains(mouseState.Position))
                {
                    backBtnRect = new Rectangle(114, 94, 92, 92);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        backBtnRect = new Rectangle(126, 106, 68, 68);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.MenuScreen;
                            backBtnRect = new Rectangle(120, 100, 80, 80);
                        }
                    }
                }
                else if (!backBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        backBtnRect = new Rectangle(120, 100, 80, 80);
                    }
                }
            }

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
                _spriteBatch.DrawString(titleFont, "Vault", new Vector2(30, 90), Color.DarkOrange, MathHelper.ToRadians(-20), new Vector2(0, 0), 1, SpriteEffects.None, 0);
                _spriteBatch.DrawString(titleFont, "Raiders!", new Vector2(105, 180), Color.SteelBlue, 0, new Vector2(0, 0), (float)1.5, SpriteEffects.None, 0);
            }
            else if (screen == Screen.MenuScreen)
            {
                _spriteBatch.Draw(menuBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(menuTexture, menuRect, Color.White);
                _spriteBatch.Draw(noBtnTexture, noBtnRect, Color.White);
                _spriteBatch.Draw(musicBtnTexture, musicBtnRect, musicColour);
                _spriteBatch.Draw(instructionsBtnTexture, instructionsBtnRect, Color.White);
            }
            else if (screen == Screen.InstructionsScreen)
            {
                _spriteBatch.Draw(menuBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(menuTexture, menuRect, Color.White);
                _spriteBatch.Draw(noBtnTexture, noBtnRect, Color.White);
                _spriteBatch.Draw(backBtnTexture, backBtnRect, Color.White);
                _spriteBatch.DrawString(titleFont, "How to Play", new Vector2(277, 160), Color.Black, 0, new Vector2(0, 0), (float)0.4, SpriteEffects.None, 0);
                _spriteBatch.DrawString(howToPlayFont, instructions, new Vector2(140, 190), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
