using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KeyboardControlGoku1
{
    public class Projectile
    {
        #region Fields

        bool active = true;
        ProjectileType type;

        // drawing support
        Texture2D sprite;
        Rectangle drawRectangle;
        SpriteEffects spriteEffect;
        int currentframe=0, columns=1, rows=1;
        int elapsedtime=0;
        // velocity information
        float xVelocity;

        #endregion

        #region constructor
        public Projectile(ProjectileType type, Texture2D sprite, int x, int y, 
            float xVelocity)
        {
            this.type = type;
            this.sprite = sprite;
            this.xVelocity = xVelocity;
            drawRectangle = new Rectangle(x - sprite.Width / 2, 
                y - sprite.Height / 2, sprite.Width,
                sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets and sets whether or not the projectile is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
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
            if (type == ProjectileType.gokuPower1)
            {
                if (Goku.isFaceRight)
                { drawRectangle.X += (int)(xVelocity * gameTime.ElapsedGameTime.Milliseconds); 
                }
                else if (!Goku.isFaceRight)
                {  drawRectangle.X -= (int)(xVelocity * gameTime.ElapsedGameTime.Milliseconds);
                spriteEffect = SpriteEffects.FlipHorizontally;
                }
            }
            if (type == ProjectileType.naruPower1)
            {
                if (Naruto.isFaceRight)
                    drawRectangle.X += (int)(1.5*xVelocity * gameTime.ElapsedGameTime.Milliseconds);
                else if (!Naruto.isFaceRight)
                {drawRectangle.X -= (int)(1.5*xVelocity * gameTime.ElapsedGameTime.Milliseconds);
                spriteEffect = SpriteEffects.FlipHorizontally;
                }
                
            }
            // check for outside game window
            if (drawRectangle.Right < 0)
            {
                active = false;
            }
            if (drawRectangle.Left > GC.WINDOW_WIDTH)
            {
                active = false;
            }
            //if (drawRectangle.Right < 0)
            //{
            //    active = false;
            //}
            //if (drawRectangle.Left > GC.WINDOW_WIDTH)
            //{
            //    active = false;
            //}
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

        #endregion
    }
}
