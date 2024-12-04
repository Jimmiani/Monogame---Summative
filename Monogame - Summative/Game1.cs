using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Monogame___Summative
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState, prevMouseState;
        KeyboardState keyboardState;
        Color musicColour, timerColour;
        bool soundFinished;
        float finalSeconds;
        Random generator;
        int page1Num, page2Num, page3Num;
        string comboCode;
        // Images
        Texture2D bankBackgroundTexture, blurredBankBackgroundTexture, menuTexture, menuBackgroundTexture, blackBackgroundTexture, insideBankBackgroundTexture, mainBankBackgroundTexture, codeLockTexture;
        Texture2D finalTimeBoxTexture, page1Texture, page2Texture, page3Texture, page3FlatTexture, loseBackgroundTexture, winBackgroundTexture;
        Rectangle window, menuRect, vaultRect, codeLockRect, finalTimeBoxRect, page1Rect, page2Rect, page3Rect;

        // Buttons
        Texture2D playBtnTexture, settingsBtnTexture, noBtnTexture, musicBtnTexture, instructionsBtnTexture, backBtnTexture, homeBtnTexture;
        Rectangle playBtnRect, settingsBtnRect, noBtnRect, musicBtnRect, instructionsBtnRect, backBtnRect, homeBtnRect;

        // Fonts
        SpriteFont titleFont, howToPlayFont, timeFont, vaultFont;
        string instructions;

        // Audio
        Song hitmanSong;
        SoundEffect destructionSound, alarmSound;
        SoundEffectInstance destructionInstance;

        // Spritesheet
        Texture2D stickmanSpritesheet, cropTexture;
        List<Texture2D> stickmanTextures;
        float stickSeconds;
        Vector2 stickSpeed;
        Rectangle stickRect;
        int stickIndex;

        enum Screen
        {
            Intro,
            MenuScreen,
            InstructionsScreen,
            CutsceneScreen,
            CutsceneScreen2,
            MainScreen,
            Page1Screen,
            Page2Screen,
            Page3Screen,
            VaultScreen,
            LoseScreen,
            WinScreen
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
            generator = new Random();
            page1Num = 8;
            page2Num = 2;
            page3Num = 7;
            musicColour = Color.White;
            timerColour = Color.Black;
            soundFinished = false;
            finalSeconds = 0;
            comboCode = "";


            instructions = "Welcome to Vault Raiders! You and your crew\n" +
                           "are expert thieves, and tonight is your big\n" +
                           "heist. Your goal is simple: infiltrate the\n" +
                           "bank, crack open the vault, and get away with\n" +
                           "the cash before the cops show up and ruin your\n" +
                           "plans. But there's a catch. The clock is ticking,\n" +
                           "and if you're caught, you'll be in a prison\n" +
                           "cell before you even realize. Do you have what\n" +
                           "it takes to pull off the perfect heist, or will\n" +
                           "you be behind bars before sunrise?";


            // Buttons
            playBtnRect = new Rectangle((window.Width - 300) / 2, 350, 300, 178);
            settingsBtnRect = new Rectangle(690, 30, 80, 80);
            musicBtnRect = new Rectangle(240, 240, 110, 110);
            noBtnRect = new Rectangle(590, 100, 80, 80);
            instructionsBtnRect = new Rectangle(450, 240, 110, 110);
            backBtnRect = new Rectangle(120, 100, 80, 80);
            homeBtnRect = new Rectangle(700, 500, 80, 80);

            // Images
            menuRect = new Rectangle((window.Width - 500) / 2, (window.Height - 350) / 2, 500, 350);
            vaultRect = new Rectangle(217, 78, 367, 366);
            codeLockRect = new Rectangle((window.Width - 300) / 2, (window.Height - 310) / 2, 300, 310);
            finalTimeBoxRect = new Rectangle(20, 20, 200, 100);
            page1Rect = new Rectangle(587, 34, 90, 78);
            page2Rect = new Rectangle(644, 532, 60, 64);
            page3Rect = new Rectangle(75, 438, 70, 57);
            

            // Spritesheet
            stickmanTextures = new List<Texture2D>();
            stickSeconds = 0f;
            stickSpeed = new Vector2(3, 0);
            stickRect = new Rectangle(-180, 380, 180, 165);
            stickIndex = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Backgrounds
            bankBackgroundTexture = Content.Load<Texture2D>("Backgrounds/bankBackground");
            blurredBankBackgroundTexture = Content.Load<Texture2D>("Backgrounds/blurredBankBackground");
            menuBackgroundTexture = Content.Load<Texture2D>("Backgrounds/menuBackground");
            blackBackgroundTexture = Content.Load<Texture2D>("Backgrounds/blackBackground");
            insideBankBackgroundTexture = Content.Load<Texture2D>("Backgrounds/insideBankBackground");
            mainBankBackgroundTexture = Content.Load<Texture2D>("Backgrounds/mainBankBackground");
            loseBackgroundTexture = Content.Load<Texture2D>("Backgrounds/loseBackground");
            winBackgroundTexture = Content.Load<Texture2D>("Backgrounds/winBackground");
            
            // Buttons
            playBtnTexture = Content.Load<Texture2D>("Buttons/playBtn");
            settingsBtnTexture = Content.Load<Texture2D>("Buttons/settingsBtn");
            noBtnTexture = Content.Load<Texture2D>("Buttons/noBtn");
            musicBtnTexture = Content.Load<Texture2D>("Buttons/musicBtn");
            instructionsBtnTexture = Content.Load<Texture2D>("Buttons/instructionsBtn");
            backBtnTexture = Content.Load<Texture2D>("Buttons/backBtn");
            homeBtnTexture = Content.Load<Texture2D>("Buttons/homeBtn");

            // Audio
            destructionSound = Content.Load<SoundEffect>("Audio/destructionAudio");
            destructionInstance = destructionSound.CreateInstance();
            alarmSound = Content.Load<SoundEffect>("Audio/bankAlarm");

            // Fonts
            titleFont = Content.Load<SpriteFont>("Fonts/titleFont");
            howToPlayFont = Content.Load<SpriteFont>("Fonts/howToPlayFont");
            timeFont = Content.Load<SpriteFont>("Fonts/timeFont");
            vaultFont = Content.Load<SpriteFont>("Fonts/vaultFont");

            // Images
            menuTexture = Content.Load<Texture2D>("Images/menuScreen");
            codeLockTexture = Content.Load<Texture2D>("Images/codeLock");
            finalTimeBoxTexture = Content.Load<Texture2D>("Images/finalTimeBox");
            page1Texture = Content.Load<Texture2D>("Images/page1");
            page2Texture = Content.Load<Texture2D>("Images/page2");
            page3Texture = Content.Load<Texture2D>("Images/page3");
            page3FlatTexture = Content.Load<Texture2D>("Images/page3Flat");

            // Spritesheet

            stickmanSpritesheet = Content.Load<Texture2D>("Spritesheets/stickmanSpritesheet");
            Rectangle sourceRect;


            int width = stickmanSpritesheet.Width / 4;
            int height = stickmanSpritesheet.Height / 2;

            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    sourceRect = new Rectangle(x * width, y * height, width, height);
                    cropTexture = new Texture2D(GraphicsDevice, width, height);
                    Color[] data = new Color[width * height];
                    stickmanSpritesheet.GetData(0, sourceRect, data, 0, data.Length);
                    cropTexture.SetData(data);
                    if (stickmanTextures.Count < 8)
                    {
                        stickmanTextures.Add(cropTexture);
                    }
                }
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            prevMouseState = mouseState;
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(hitmanSong);
            }
            if (screen == Screen.Intro)
            {
                // Play Button
                if (playBtnRect.Contains(mouseState.Position))
                {
                    playBtnRect = new Rectangle((window.Width - 320) / 2, 345, 320, 190);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        playBtnRect = new Rectangle((window.Width - 280) / 2, 355, 280, 166);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.CutsceneScreen;
                            playBtnRect = new Rectangle((window.Width - 300) / 2, 350, 300, 178);
                        }
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

            else if (screen == Screen.CutsceneScreen)
            {
                // Stick Man
                stickRect.X += (int)stickSpeed.X;
                stickRect.Y += (int)stickSpeed.Y;
                stickSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (stickSeconds >= 0.09f)
                {
                    stickSeconds = 0f;
                    stickIndex++;
                    if (stickIndex >= stickmanTextures.Count)
                    {
                        stickIndex = 0;
                    }
                }
                if (stickRect.X >= 325 && destructionInstance.State == SoundState.Stopped && !soundFinished)
                {
                    destructionInstance.Play();
                    soundFinished = true;
                }
                if (destructionInstance.State == SoundState.Stopped && soundFinished)
                {
                    screen = Screen.CutsceneScreen2;
                    stickSpeed = new Vector2(5, 0);
                    stickRect = new Rectangle(-360, 200, 360, 330);
                    stickSeconds = 0;
                    alarmSound.Play();
                }
            }

            else if (screen == Screen.CutsceneScreen2)
            {
                // Stick Man
                stickRect.X += (int)stickSpeed.X;
                stickRect.Y += (int)stickSpeed.Y;
                stickSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (stickSeconds >= 0.09f)
                {
                    stickSeconds = 0f;
                    stickIndex++;
                    if (stickIndex >= stickmanTextures.Count)
                    {
                        stickIndex = 0;
                    }
                }
                if (stickRect.X >= window.Width)
                {
                    screen = Screen.MainScreen;
                }
            }

            else if (screen == Screen.MainScreen)
            {
                // Page 1
                if (page1Rect.Contains(mouseState.Position))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        screen = Screen.Page1Screen;
                    }
                }
                // Page 2
                if (page2Rect.Contains(mouseState.Position))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        screen = Screen.Page2Screen;
                    }
                }
                // Page 3
                if (page3Rect.Contains(mouseState.Position))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        screen = Screen.Page3Screen;
                    }
                }
                if (vaultRect.Contains(mouseState.Position))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        screen = Screen.VaultScreen;
                    }
                }
            }
            else if (screen == Screen.VaultScreen)
            {
                if (comboCode.Length == 0 && keyboardState.IsKeyDown(Keys.D8))
                {
                    comboCode += "8     ";
                }
                if (comboCode.Length == 6 && keyboardState.IsKeyDown(Keys.D2))
                {
                    comboCode += "2     ";
                }
                if (comboCode.Length == 12 && keyboardState.IsKeyDown(Keys.D7))
                {
                    comboCode += "7";
                }
                if (comboCode.Length == 13 && keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.WinScreen;
                }
            }
            if (screen == Screen.Page1Screen || screen == Screen.Page2Screen || screen == Screen.Page3Screen || screen == Screen.VaultScreen)
            {
                noBtnRect = new Rectangle(700, 20, 80, 80);
                // No Button
                if (noBtnRect.Contains(mouseState.Position))
                {
                    noBtnRect = new Rectangle(694, 14, 92, 92);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        noBtnRect = new Rectangle(706, 26, 68, 68);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.MainScreen;
                            noBtnRect = new Rectangle(700, 20, 80, 80);
                        }
                    }
                }
                else if (!noBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        noBtnRect = new Rectangle(700, 20, 80, 80);
                    }
                }
            }
            if (screen == Screen.Page1Screen || screen == Screen.Page2Screen || screen == Screen.Page3Screen || screen == Screen.VaultScreen || screen == Screen.MainScreen)
            {
                finalSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (finalSeconds >= 45)
                {
                    screen = Screen.LoseScreen;
                }
                if (finalSeconds > 35)
                {
                    timerColour = Color.Red;
                }
            }
            if (screen == Screen.LoseScreen || screen == Screen.WinScreen)
            {
                // Home Button
                if (homeBtnRect.Contains(mouseState.Position))
                {
                    homeBtnRect = new Rectangle(694, 494, 92, 92);
                    if (prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        homeBtnRect = new Rectangle(706, 506, 68, 68);
                        if (mouseState.LeftButton == ButtonState.Released)
                        {
                            screen = Screen.Intro;
                            homeBtnRect = new Rectangle(700, 500, 80, 80);
                            finalSeconds = 0;
                            soundFinished = false;
                            timerColour = Color.Black;
                            stickRect = new Rectangle(-180, 380, 180, 165);
                            stickSpeed = new Vector2(3, 0);
                            stickSeconds = 0;
                            stickIndex = 0;
                            page1Num = generator.Next(10);
                            page2Num = generator.Next(10);
                            page3Num = generator.Next(10);
                        }
                    }
                }
                else if (!noBtnRect.Contains(mouseState.Position))
                {
                    if (prevMouseState.LeftButton == ButtonState.Released)
                    {
                        homeBtnRect = new Rectangle(700, 500, 80, 80);
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
                _spriteBatch.DrawString(howToPlayFont, instructions, new Vector2(190, 215), Color.Black, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            }
            else if (screen == Screen.CutsceneScreen)
            {
                _spriteBatch.Draw(bankBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(stickmanTextures[stickIndex], stickRect, Color.White);
                if (stickRect.X >= 325)
                {
                    _spriteBatch.Draw(blackBackgroundTexture, new Vector2(0, 0), Color.White);
                }
            }
            else if (screen == Screen.CutsceneScreen2)
            {
                _spriteBatch.Draw(insideBankBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(stickmanTextures[stickIndex], stickRect, Color.White);
            }
            else if (screen == Screen.MainScreen)
            {
                _spriteBatch.Draw(mainBankBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(page1Texture, page1Rect, Color.White);
                _spriteBatch.Draw(page2Texture, page2Rect, Color.White);
                _spriteBatch.Draw(page3Texture, page3Rect, Color.White);
            }
            else if (screen == Screen.Page1Screen)
            {
                _spriteBatch.Draw(blackBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(page1Texture, new Vector2(100, 43), Color.White);
                _spriteBatch.DrawString(howToPlayFont, "1)", new Vector2 (215, 168), Color.Black);
                _spriteBatch.DrawString(titleFont, page1Num.ToString(), new Vector2(365, 247), Color.Black);
            }
            else if (screen == Screen.Page2Screen)
            {
                _spriteBatch.Draw(blackBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(page2Texture, new Vector2(150, 35), Color.White);
                _spriteBatch.DrawString(howToPlayFont, "2)", new Vector2(222, 200), Color.Black);
                _spriteBatch.DrawString(titleFont, page2Num.ToString(), new Vector2(350, 270), Color.Black);
            }
            else if (screen == Screen.Page3Screen)
            {
                _spriteBatch.Draw(blackBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(page3FlatTexture, new Vector2(206, 25), Color.White);
                _spriteBatch.DrawString(howToPlayFont, "3)", new Vector2(289, 139), Color.Black);
                _spriteBatch.DrawString(titleFont, page3Num.ToString(), new Vector2(385, 236), Color.Black);
            }
            else if (screen == Screen.VaultScreen)
            {
                _spriteBatch.Draw(blackBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(codeLockTexture, codeLockRect, Color.White);
                _spriteBatch.DrawString(vaultFont, comboCode, new Vector2(295, 380), Color.Black);
            }
            else if (screen == Screen.LoseScreen)
            {
                _spriteBatch.Draw(loseBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(titleFont, "ARRESTED!", new Vector2(95, 18), Color.Red, 0, new Vector2(0, 0), (float)1.25, SpriteEffects.None, 0);
                _spriteBatch.DrawString(titleFont, "You Lost!", new Vector2(160, 495), Color.Black);
            }
            else if (screen == Screen.WinScreen)
            {
                _spriteBatch.Draw(winBackgroundTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.DrawString(titleFont, "ESCAPED!", new Vector2(128, 18), Color.LimeGreen, 0, new Vector2(0, 0), (float)1.25, SpriteEffects.None, 0);
                _spriteBatch.DrawString(titleFont, "You Win!", new Vector2(197, 495), Color.Black);
            }
            if (screen == Screen.Page1Screen || screen == Screen.Page2Screen || screen == Screen.Page3Screen || screen == Screen.VaultScreen)
            {
                _spriteBatch.Draw(noBtnTexture, noBtnRect, Color.White);
            }
            if (screen == Screen.Page1Screen || screen == Screen.Page2Screen || screen == Screen.Page3Screen || screen == Screen.VaultScreen || screen == Screen.MainScreen)
            {
                _spriteBatch.Draw(finalTimeBoxTexture, finalTimeBoxRect, Color.White);
                _spriteBatch.DrawString(timeFont, (45 - finalSeconds).ToString("00.0"), new Vector2(50, 27), timerColour);
            }
            if (screen == Screen.LoseScreen || screen == Screen.WinScreen)
            {
                _spriteBatch.Draw(homeBtnTexture, homeBtnRect, Color.White);
            }
                _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
