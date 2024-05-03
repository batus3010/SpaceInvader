using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader.Sprites
{
    public class Sprite : ICloneable
    {
        protected Texture2D _texure;
        protected float _rotation;
        // determine if keyboard is pressed
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;

        public Vector2 Position;
        public Vector2 Origin;

        public Vector2 Direction;
        public float LinearVelocity = 4f;

        public float LifeSpan = 0f;
        public bool IsRemoved = false;

        public Sprite(Texture2D texture)
        {
            _texure = texture;
            Origin = new Vector2(_texure.Width / 2, _texure.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {


        }
        public virtual Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    (int)(Position.X - Origin.X),
                    (int)(Position.Y - Origin.Y),
                    _texure.Width,
                    _texure.Height
                );
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texure, Position, null, Color.White, 0f, Origin, 1, SpriteEffects.None, 0);
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
