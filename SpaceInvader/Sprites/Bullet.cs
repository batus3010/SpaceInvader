using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader.Sprites
{
    public class Bullet : Sprite
    {
        private float _timer;

        public Bullet(Texture2D texture) : base(texture) 
        {

        }

        /// <summary>
        /// Update the lifespan and position of bullet
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="sprites"></param>
        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(_timer > LifeSpan)
            {
                IsRemoved = true;
            }

            // bullet speed
            Position.Y -= 10f;
        }

        public override Rectangle Bounds
        {
            get
            {
                // Adjust the bounding box to fit the visible area of the bullet
                // Here, we're making it smaller to better match the visible area of the bullet
                int width = 1; // Adjust as needed
                int height = 5; // Adjust as needed

                return new Rectangle(
                    (int)(Position.X - Origin.X) + (_texure.Width - width) / 2,
                    (int)(Position.Y - Origin.Y) + (_texure.Height - height) / 2,
                    width,
                    height
                );
            }
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    // Draw the bullet
        //    base.Draw(spriteBatch);

        //    // Draw outline bounds in red, FOR DEBUG
        //    var bounds = Bounds;
        //    var rectTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        //    rectTexture.SetData(new[] { Color.White });

        //    spriteBatch.Draw(rectTexture, new Rectangle(bounds.Left, bounds.Top, bounds.Width, 1), Color.Red); // Top
        //    spriteBatch.Draw(rectTexture, new Rectangle(bounds.Left, bounds.Bottom, bounds.Width, 1), Color.Red); // Bottom
        //    spriteBatch.Draw(rectTexture, new Rectangle(bounds.Left, bounds.Top, 1, bounds.Height), Color.Red); // Left
        //    spriteBatch.Draw(rectTexture, new Rectangle(bounds.Right, bounds.Top, 1, bounds.Height + 1), Color.Red); // Right
        //}




    }
}
