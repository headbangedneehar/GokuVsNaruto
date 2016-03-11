using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KeyboardControlGoku1
{
    public class Spiritbomb
    {
        Texture2D sprite;
        Rectangle drawRectangle;
        SpriteEffects spriteEffect;
        int currentframe=0, columns=8 , rows=1;
        // velocity information
        Vector2 velocity;
        int elapsedtime = 0;
        ProjectileType type=ProjectileType.spiritbomb;
        bool active=true;
         static bool bombMove;
        const float BASE_SPEED = 0.2f;

        #region constructor
        public Spiritbomb(Texture2D sprite, int x, int y)
        {
            this.sprite = sprite;
            //drawRectangle = new Rectangle(x - sprite.Width / 2, 
            //    y - sprite.Height / 2, sprite.Width,
            //    sprite.Height);
            drawRectangle.X = x;
            drawRectangle.Y = y;
        }

        #endregion

        #region properties
        /// <summary>
        /// Gets and sets whether or not the projectile is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public static bool BombMoving
        {
            get { return bombMove; }
            set { bombMove = value; }
        }
        /// <summary>
        /// Gets the projectile type
        /// </summary>
        public ProjectileType Type
        {
            get { return type; }
        }

        /// <summary>
        /// Gets the collision rectangle for the projectile
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the projectile's location and makes inactive when it
        /// leaves the game window
        /// </summary>
        public void Update(GameTime gameTime)
        {   
            // move projectile
            if (type == ProjectileType.spiritbomb)
            {   
                SetTarget(new Vector2(Naruto.DrawRectangle.X, Naruto.DrawRectangle.Y-40));
                elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
                if (currentframe >= 0 && currentframe <= 6)
                {
                    bombMove = false;
                    if (elapsedtime % 10 == 0)
                    {
                        currentframe++;
                        active = true;
                    }
                }
                if (currentframe == 7)
                {
                    bombMove = true;
                    currentframe = 7;
                    if (!active)
                    {
                        currentframe = 0;
                        elapsedtime = 0;
                    }
                }
                if (bombMove)
                {
                    if (Goku.isFaceRight)
                    {
                        drawRectangle.X += (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds);
                        drawRectangle.Y += (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds);
                    }
                    else if (!Goku.isFaceRight)
                    {
                        drawRectangle.X -= (int)(velocity.X * gameTime.ElapsedGameTime.Milliseconds);
                        drawRectangle.Y -= (int)(velocity.Y * gameTime.ElapsedGameTime.Milliseconds);
                        spriteEffect = SpriteEffects.FlipHorizontally;
                    }
                }
            }
            
            // check for outside game window
            if (drawRectangle.Right < 0)
            {
                Active = false;
            }
            if (drawRectangle.Left > GC.WINDOW_WIDTH)
            {
                Active = false;
            }
            if (drawRectangle.Bottom < 0)
            {
                Active = false;
            }
            if (drawRectangle.Top > GC.WINDOW_HEIGHT)
            {
                Active = false;
            }
        }

        /// <summary>
        /// Draws the projectile
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle();
            int height, width;
            height = sprite.Height / rows;
            width = sprite.Width / columns;
            int ro = currentframe / columns;
            int col = currentframe % columns;
            sourceRectangle = new Rectangle(width * col, height * ro, width, height);
            drawRectangle.Width = width;
            drawRectangle.Height = height;
            //drawRectangle.X = (int)location.X - sourceRectangle.Width / 2;
            //drawRectangle.Y = (int)location.Y - sourceRectangle.Height / 2;

            spriteBatch.Draw(sprite, drawRectangle,sourceRectangle, Color.White,0f,new Vector2(0,0),spriteEffect,0f);
        }


        public void SetTarget(Vector2 target)
        {
            // set teddy velocity based on teddy center location and target
            if(Goku.isFaceRight)
            velocity = Vector2.Normalize(target - new Vector2(drawRectangle.X,drawRectangle.Y)) * BASE_SPEED;
            if (!Goku.isFaceRight)
                velocity = Vector2.Normalize(target - new Vector2(drawRectangle.X, drawRectangle.Y)) * -BASE_SPEED;
        }
        #endregion
    }
}
