using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Monogame_Animation_Assignment
{
    internal class Tribble
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        private bool _grow;
        private static Random rand = new Random();
        public Tribble(Texture2D texture, Rectangle rect, Vector2 speed, bool grow)
        {
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
            _grow = grow;
        }
        public Tribble(Texture2D texture, GraphicsDeviceManager graphics)
        {
            _texture=texture;
            int size = rand.Next(80, 150);
            _rectangle = new Rectangle(rand.Next(0, graphics.PreferredBackBufferWidth - size), rand.Next(0, graphics.PreferredBackBufferHeight - size), size, size);
            _speed = new Vector2(rand.Next(3, 6), rand.Next(3, 6));
            _grow = Convert.ToBoolean(rand.Next(2)); 
        }
        /* ignore this
        public Tribble(Texture2D texture, Rectangle parentTribble, Vector2 speed)
        {
            _texture = texture;
            _speed = speed;
            _rectangle = new Rectangle(parentTribble.X, parentTribble.Y, 80, 80);
            _grow = Convert.ToBoolean(rand.Next(2));
        }
        */
        public Texture2D Texture
        {
            get { return _texture; }
        }
        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }
        public void Move()
        {
            _rectangle.Offset(_speed);
        }
        /*
        public Tribble Multiply(Texture2D texture)
        {
            return new Tribble(texture, Bounds, _speed);
        }
        */
        public bool Move(GraphicsDeviceManager graphics)
        {
            bool multiply = false;
            Move();
            if (Bounds.Right >= graphics.PreferredBackBufferWidth || Bounds.Left <= 0)
            {
                BounceLeftRight();
                SizeChange();
                if (Bounds.Height > 130)
                    multiply = true;
            }
            if (Bounds.Bottom >= graphics.PreferredBackBufferHeight || Bounds.Top <= 0)
            {
                BounceTopBottom();
                SizeChange();
                if(Bounds.Height > 130)
                    multiply = true;
            }
            return multiply;
        }
        public void BounceLeftRight()
        {
            _speed.X *= -1;
        }
        public void BounceTopBottom()
        {
            _speed.Y *= -1;
        }
        public void Grow()
        {
            for (int i = 0; i < 2; i++)
            {
                Move();
            }
            _rectangle.Height += 5;
            _rectangle.Width += 5;
        }
        public void Shrink()
        {
            _rectangle.Height -= 5;
            _rectangle.Width -= 5;
        }
        public void SizeChange()
        {
            if (_grow)
                Grow();
            else
                Shrink();
            if (_rectangle.Height > 150)
                _grow = false;
            else if (_rectangle.Height < 80)
                _grow = true;
        }
        public void Draw(SpriteBatch _spritebatch)
        {
            _spritebatch.Draw(_texture, _rectangle, Color.White);
        }
    }

}
