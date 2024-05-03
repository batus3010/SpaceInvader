using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader.Sprites
{
    internal class Enemy : Sprite 
    {
        private float movementTimer;
        private float movementInterval = 1.5f;
        public Enemy(Texture2D texture, Vector2 position) : base(texture)
        {
            Position = position;
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            // Implement enemy movement logic here
            // For example, move the enemy downward over time

            movementTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (movementTimer >= movementInterval)
            {
                Position.Y += LinearVelocity;
                movementTimer = 0f; // Reset the timer
            }

            base.Update(gameTime, sprites);
        }

        public override Rectangle Bounds
        {
            get
            {
                // Adjust the bounding box to fit the visible area of the enemy sprite
                int width = _texure.Width - 30; // Adjust as needed
                int height = _texure.Height - 50; // Adjust as needed

                return new Rectangle(
                    (int)(Position.X - Origin.X) + (_texure.Width - width) / 2,
                    (int)(Position.Y - Origin.Y) + (_texure.Height - height) / 2,
                    width,
                    height
                );
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texure, Position, null, Color.White, 0f, Origin, 1, SpriteEffects.None, 0);

            // DEBUGGING BOUNDS
            //var bounds = Bounds;
            //var rectTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
            //rectTexture.SetData(new[] { Color.White });

            //spriteBatch.Draw(rectTexture, new Rectangle(bounds.Left, bounds.Top, bounds.Width, 1), Color.Red); // Top
            //spriteBatch.Draw(rectTexture, new Rectangle(bounds.Left, bounds.Bottom, bounds.Width, 1), Color.Red); // Bottom
            //spriteBatch.Draw(rectTexture, new Rectangle(bounds.Left, bounds.Top, 1, bounds.Height), Color.Red); // Left
            //spriteBatch.Draw(rectTexture, new Rectangle(bounds.Right, bounds.Top, 1, bounds.Height + 1), Color.Red); // Right
        }
    }
}
