using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvader.Sprites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace SpaceInvader
{
    public class Game1 : Game
    {
        Texture2D shipTexture;
        Vector2 shipPosition;
        float shipSpeed;

        Texture2D enemy1Texture;
        Texture2D enemy2Texture;
        Texture2D enemy3Texture;
        Vector2 enemy3Position;
        float enemySpeed;

        // properties of bullet
        public Bullet Bullet;
        List<Sprite> sprites;
        private KeyboardState previousKey;
        private KeyboardState currentKey;
        private SoundEffect bulletSound;
        private float bulletSoundVolume = 0.25f;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public Game1()
        {
            // create the screen and set its size to 720p
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 896;
            _graphics.PreferredBackBufferHeight = 1024;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            shipPosition = new Vector2(_graphics.PreferredBackBufferWidth/2, _graphics.PreferredBackBufferHeight/2 + 300);
            shipSpeed = 300f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            sprites = new List<Sprite>();
            // TODO: use this.Content to load your game content here
            // Load game images and sounds
            shipTexture = Content.Load<Texture2D>("assets/MainShip");
            enemy1Texture = Content.Load<Texture2D>("assets/enemy_1");
            enemy2Texture = Content.Load<Texture2D>("assets/enemy_2");
            enemy3Texture = Content.Load<Texture2D>("assets/enemy_3");

            Action<Texture2D, Vector2> AddNewEnemy = (Texture2D texture, Vector2 position) =>
                sprites.Add(new Enemy(texture, position));
            //AddNewEnemy(enemy1Texture, new Vector2(120, 110));
            //AddNewEnemy(enemy2Texture, new Vector2(200, 100));
            //AddNewEnemy(enemy3Texture, new Vector2(300, 110));
            //AddNewEnemy(enemy1Texture, new Vector2(380, 110));
            //AddNewEnemy(enemy2Texture, new Vector2(460, 100));
            //AddNewEnemy(enemy3Texture, new Vector2(560, 110));

            Vector2 formationStartPosition = new Vector2(100, 100);

            // Define the number of rows and columns in the formation
            int numRows = 5;
            int numCols = 9;

            // Define the horizontal and vertical spacing between enemies
            int horizontalSpacing = 89;
            int verticalSpacing = 40;

            // Loop through each row and column to create enemies
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    Texture2D enemyTexture = enemy1Texture;
                    if (row == 1 || row == 2)
                    {
                        enemyTexture = enemy2Texture;
                        horizontalSpacing = 90;
                        verticalSpacing = 65;
                    }
                    if (row == 3 || row == 4)
                    {
                        formationStartPosition = new Vector2(105, 100);
                        enemyTexture = enemy3Texture;
                        horizontalSpacing = 90;
                        verticalSpacing = 70;
                    }
                    Vector2 enemyPosition = formationStartPosition + new Vector2(col * horizontalSpacing, row * verticalSpacing);
                    AddNewEnemy(enemyTexture, enemyPosition);
                }
            }

            Bullet = new Bullet(Content.Load<Texture2D>("assets/bullet"));
            bulletSound = Content.Load<SoundEffect>("sounds/bulletSound");
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Moving logic, Adjust ship position based on keyboard input and elapsed time
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                shipPosition.X -= shipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                shipPosition.X += shipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (shipPosition.X > _graphics.PreferredBackBufferWidth - shipTexture.Width / 2)
            {
                shipPosition.X = _graphics.PreferredBackBufferWidth - shipTexture.Width / 2;
            }
            else if (shipPosition.X < shipTexture.Width / 2)
            {
                shipPosition.X = shipTexture.Width / 2;
            }

            // shooting logic
            previousKey = currentKey;
            currentKey = Keyboard.GetState();
            if(currentKey.IsKeyDown(Keys.Space) && previousKey.IsKeyUp(Keys.Space))
            {
                // create a new bullet
                var bullet = Bullet.Clone() as Bullet;
                bullet.Position = shipPosition;
                bullet.LinearVelocity = 2f;
                bullet.LifeSpan = 2f;
                sprites.Add(bullet);

                // play sound for bullet
                bulletSound.Play(bulletSoundVolume, 0.0f, 0.0f);
            }
            foreach (Sprite sprite in sprites.ToArray())
            {
                sprite.Update(gameTime, sprites);
            }

            foreach (Sprite bullet in sprites.Where(sprite => sprite.GetType() == typeof(Bullet)).ToArray())
            {
                foreach (Sprite enemy in sprites.Where(sprite => sprite.GetType() == typeof(Enemy)).ToArray())
                {
                    if (bullet.Bounds.Intersects(enemy.Bounds))
                    {
                        // If collision detected, remove both bullet and enemy
                        bullet.IsRemoved = true;
                        enemy.IsRemoved = true;
                    }
                }
            }
            // check and remove all bullets marked as IsRemoved
            sprites.RemoveAll(sprite => sprite.IsRemoved);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            // draw ship
            _spriteBatch.Draw(shipTexture, shipPosition, null, Color.White, 0f, new Vector2(shipTexture.Width/2, shipTexture.Height/2), 
                                Vector2.One, SpriteEffects.None, 0f);
            // draw bullets and enemies
            foreach(var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
