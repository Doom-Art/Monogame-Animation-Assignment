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
        public Tribble(Texture2D texture, Rectangle rect, Vector2 speed, bool grow)
        {
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
            _grow = grow;
        }
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
