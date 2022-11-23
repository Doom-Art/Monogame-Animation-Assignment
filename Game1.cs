using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_Animation_Assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D tribbleGreyTexture;
        private Rectangle tribbleGreyRect;
        private Vector2 tribbleGreySpeed;
        private Texture2D tribbleOrangeTexture;
        private Rectangle tribbleOrangeRect;
        private Vector2 tribbleOrangeSpeed;
        private Texture2D tribbleCreamTexture;
        private Rectangle tribbleCreamRect;
        private Vector2 tribbleCreamSpeed;
        private Texture2D tribbleBrownTexture;
        private Rectangle tribbleBrownRect;
        private Vector2 tribbleBrownSpeed;
        public static Random rand;
        private int bgNum;
        private float rotation;
        SoundEffect tribbleCoo;
        MouseState mouseState;
        Texture2D tribbleIntroTexture;
        SpriteFont title;
        float seconds;
        float startTime;
        enum Screen
        {
            Intro,
            TribbleYard,
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
            rand = new Random();
            _graphics.PreferredBackBufferWidth = (800);
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            bgNum = 0;
            rotation = 1;
            tribbleGreyRect = new Rectangle(rand.Next(0,700), rand.Next(0,500), 100, 100);
            tribbleGreySpeed = new Vector2(2, 2);
            tribbleOrangeRect = new Rectangle(rand.Next(0, 700), rand.Next(0, 500), 100, 100);
            tribbleOrangeSpeed = new Vector2(2, 0);
            tribbleCreamRect = new Rectangle(rand.Next(0, 700), rand.Next(0, 500), 100, 100);
            tribbleCreamSpeed = new Vector2(0, -3);
            tribbleBrownRect = new Rectangle(rand.Next(0, 700), rand.Next(0, 500), 100, 100);
            tribbleBrownSpeed = new Vector2(rand.Next(2,10), rand.Next(2,10));
            // TODO: Add your initialization logic here
            this.Window.Title = "Tribble";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCoo = Content.Load<SoundEffect>("tribble_coo");
            tribbleIntroTexture = Content.Load<Texture2D>("tribble_intro");
            title = Content.Load<SpriteFont>("titleFont");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed){
                    screen = Screen.TribbleYard;
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                }

            }
            else if (screen == Screen.TribbleYard)
            {
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
                this.Window.Title = $"Tribble Madness, Orange Current Speed: {tribbleOrangeSpeed.X}, Game Time: {seconds.ToString("0.0")}";
                if (seconds >= 40)
                    screen = Screen.EndScreen;
                //Grey's Code
                tribbleGreyRect.X += (int)tribbleGreySpeed.X;
                tribbleGreyRect.Y += (int)tribbleGreySpeed.Y;
                if (tribbleGreyRect.Right >= _graphics.PreferredBackBufferWidth || tribbleGreyRect.Left <= 0){
                    tribbleGreySpeed.X *= (-1);

                }
                if (tribbleGreyRect.Bottom >= _graphics.PreferredBackBufferHeight || tribbleGreyRect.Top <= 0){
                    tribbleGreySpeed.Y *= (-1);

                }
                //Cream's Code
                tribbleCreamRect.X += (int)tribbleCreamSpeed.X;
                tribbleCreamRect.Y += (int)tribbleCreamSpeed.Y;
                if (tribbleCreamRect.Right >= _graphics.PreferredBackBufferWidth || tribbleCreamRect.Left <= 0){
                    tribbleCreamSpeed.X *= -1;

                }
                if (tribbleCreamRect.Bottom >= _graphics.PreferredBackBufferHeight || tribbleCreamRect.Top <= 0){
                    tribbleCreamSpeed.Y *= -1;

                }
                //Orange's Code
                tribbleOrangeRect.X += (int)tribbleOrangeSpeed.X;
                tribbleOrangeRect.Y += (int)tribbleOrangeSpeed.Y;
                if (tribbleOrangeRect.Left >= _graphics.PreferredBackBufferWidth){
                    tribbleOrangeRect.X = -100;
                    tribbleOrangeSpeed.X = rand.Next(1, 11);
                    if (!tribbleCoo.IsDisposed)
                        tribbleCoo.Play();
                }

                //Brown's Code
                tribbleBrownRect.X += (int)tribbleBrownSpeed.X;
                tribbleBrownRect.Y += (int)tribbleBrownSpeed.Y;
                if (tribbleBrownRect.Right >= _graphics.PreferredBackBufferWidth || tribbleBrownRect.Left <= 0){
                    tribbleBrownSpeed.X *= (-1);
                    bgNum = rand.Next(0, 5);
                    rotation *= -1;

                }
                if (tribbleBrownRect.Bottom >= _graphics.PreferredBackBufferHeight || tribbleBrownRect.Top <= 0){
                    tribbleBrownSpeed.Y *= (-1);
                    bgNum = rand.Next(0, 5);
                }
            }
            else if (screen == Screen.EndScreen){
                if(mouseState.LeftButton == ButtonState.Pressed){
                    Exit();
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (bgNum == 0)
                GraphicsDevice.Clear(Color.Cyan);
            else if (bgNum == 1)
                GraphicsDevice.Clear(Color.Tomato);
            else if (bgNum == 2)
                GraphicsDevice.Clear(Color.BlueViolet);
            else if (bgNum == 3)
                GraphicsDevice.Clear(Color.Gold);
            else if (bgNum == 4)    
                GraphicsDevice.Clear(Color.Green);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(title, "Welcome to the world of tribbles. click the mouse button to move", new Vector2(5, 500), Color.Black);
                _spriteBatch.DrawString(title, "onto the next screen. After the tribbles bounce for 40 seconds the", new Vector2(5, 525), Color.Black);
                _spriteBatch.DrawString(title, "program will move to the end screen.", new Vector2(5, 550), Color.Black);
            }
            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.Draw(tribbleGreyTexture, tribbleGreyRect, Color.White);
                _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.White);
                _spriteBatch.Draw(tribbleOrangeTexture, tribbleOrangeRect, Color.White);
                _spriteBatch.Draw(tribbleBrownTexture, tribbleBrownRect, null, Color.White, rotation, new Vector2(tribbleBrownTexture.Width / 2f, tribbleBrownTexture.Height / 2f), SpriteEffects.None, 0f);
            }
            else if(screen == Screen.EndScreen)
            {
                _spriteBatch.DrawString(title, "Thank you for playing tribble yard. Click to end the program.", new Vector2(20, 20), Color.Black);
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here
            if (rotation > 360 || rotation < -360)
                rotation = 1;
            else if (rotation > 0)
                rotation += Convert.ToSingle(0.3);
            else if (rotation < 0)
                rotation -= Convert.ToSingle(0.3);

            base.Draw(gameTime);
        }
    }
}