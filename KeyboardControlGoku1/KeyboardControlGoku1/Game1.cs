using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KeyboardControlGoku1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static List<Projectile> projectiles = new List<Projectile>();
        static List<Spiritbomb> spiritbombprojs = new List<Spiritbomb>();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Texture2D gokuStanceAtlas, gokuMoveAtlas, gokuKickAtlas, gokuPunchAtlas,gokuPower1Atlas,gokuspiritbombAtlas;
        //Texture2D naruStanceAtlas, naruMoveAtlas, naruKickAtlas, naruPunchAtlas,naruPower1Atlas,naruFallBackAtlas,naruspAtlas;
        static Texture2D gokuProjectile1, naruProjectile1, spiritbombProj;
        KeyboardState oldState, newState;
        Goku goku;
        Naruto naru;
        const int WIN_WIDTH = 800;
        const int WIN_HEIGHT = 600;

        bool punching, kicking;
        bool deactKeys;
        bool rasenganhit = false;
        bool naruprevhit, gokuprevhit;
        int timer = 0;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WIN_WIDTH;
            graphics.PreferredBackBufferHeight = WIN_HEIGHT;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //gokuStanceAtlas = Content.Load<Texture2D>("sprites");
            //gokuMoveAtlas = Content.Load<Texture2D>("flying");
            //gokuKickAtlas = Content.Load<Texture2D>("gokukick");
            //gokuPunchAtlas = Content.Load<Texture2D>("punchAtlas");
            //gokuPower1Atlas = Content.Load<Texture2D>("gokupower1");
            gokuProjectile1 = Content.Load<Texture2D>("slice03_03");
            spiritbombProj = Content.Load<Texture2D>("bomb");

            goku = new Goku(new Vector2(WIN_WIDTH / 4, WIN_HEIGHT / 2), Content, "gokukick", "punchAtlas", "flying", "sprites","gokupower1", "gokuspiritbomb", "gokufall","gokujump","gokuhit","gokudown","gokuface","gokutop","gokupowerup");

            //naruStanceAtlas = Content.Load<Texture2D>("sprites");
            //naruMoveAtlas = Content.Load<Texture2D>("flying");
            //naruKickAtlas = Content.Load<Texture2D>("gokukick");
            //naruPunchAtlas = Content.Load<Texture2D>("punchAtlas");
            naruProjectile1=Content.Load<Texture2D>("naruproj");

            naru = new Naruto(new Vector2(WIN_WIDTH * 6 / 8, WIN_HEIGHT / 2), Content, "narukick", "narupunch", "narurun", "narustance","narupower1","narutoback","narusp","narujump","naruhit","narudown","naruface","narutop","narupowerup");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
            KeyboardState key = Keyboard.GetState();
            KeyboardState newState = Keyboard.GetState();
            #region listUpdates
            
            foreach (Projectile projectile in projectiles)
            {
                projectile.Update(gameTime);
            }

            foreach (Spiritbomb bomb in spiritbombprojs)
            {
                bomb.Update(gameTime);
            }

            //Hits and Downs 
            foreach (Projectile projectile in projectiles)
            {
                if (projectile.Type==ProjectileType.gokuPower1 &&
                    projectile.Active &&
                    Naruto.DrawRectangle.Intersects(projectile.CollisionRectangle))
                {
                    projectile.Active = false;
                    naru.Hit = true;
                    Naruto.Health = Naruto.Health - 25;
                }
                if (projectile.Type == ProjectileType.naruPower1 &&
                    projectile.Active &&
                    Goku.DrawRectangle.Intersects(projectile.CollisionRectangle))
                {
                    projectile.Active = false;
                    goku.Hit = true;
                    Goku.Health = Goku.Health - 15;
                }
                
            }
            //spiritbomb hit
            foreach(Spiritbomb spiritbomb in spiritbombprojs)
            if (spiritbomb.Type == ProjectileType.spiritbomb &&
                    spiritbomb.Active &&
                    spiritbomb.CollisionRectangle.Contains(Naruto.DrawRectangle.Center.X, Naruto.DrawRectangle.Center.Y))
            {
                spiritbomb.Active = false;
                Spiritbomb.BombMoving = false;
                naru.Down = true;
                Naruto.Health = Naruto.Health - 50;
                deactKeys = false;//as soon as hit, activate keys
            }
            //rasengan hit
            
            if (naru.Ransengan)
            {   
                deactKeys=true;
                if(Naruto.DrawRectangle.Intersects(Goku.DrawRectangle))
                {
                    goku.Down=true;
                    rasenganhit = true;
                    
                }
            }

            if (rasenganhit&&!goku.Down)
            {
                deactKeys = false;
                rasenganhit = false; Goku.Health = Goku.Health - 40;
            }
            //disable keyboard when SPs
            if (goku.SpiritBomb)
                deactKeys = true;//when bomb released, deact keys
            
            if (deactKeys)
            {
                key = new KeyboardState();
            }

            //punches and kicks
            if (Goku.DrawRectangle.Intersects(Naruto.DrawRectangle))
            {
               
                timer = gameTime.ElapsedGameTime.Milliseconds;
                bool hpdecrease = false;
                if ((goku.Punch || goku.Kick)&&Goku.HitFrame)
                {
                    naru.Hit = true; naruprevhit = true;
                }
                if ((naru.Kick || naru.Punch)&&Naruto.HitFrame)
                {
                    goku.Hit = true; gokuprevhit = true;
                }
            }
            if (!naru.Hit && naruprevhit)
            {
                naruprevhit = false;
                Naruto.Health -= 5;
            }
            if (!goku.Hit && gokuprevhit&&!Goku.Death)
            {
                gokuprevhit = false;
                Goku.Health -= 5;
            }
            // clean out inactive projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                if (!projectiles[i].Active)
                {
                    projectiles.RemoveAt(i);
                }
            }
            for (int i = spiritbombprojs.Count - 1; i >= 0; i--)
            {
                if (!spiritbombprojs[i].Active)
                {
                    spiritbombprojs.RemoveAt(i);
                }
            }

            if (Naruto.Death || Goku.Death)
                deactKeys = true;
            #endregion
            

            goku.Update(gameTime, key);
            naru.Update(gameTime, key,newState);
            
            #region characterFacing

            if (goku.RectanglePosition.X < naru.RectanglePosition.X)
            {
                goku.FaceRight = true;
                naru.FaceRight = false;
                goku.SpriteEffects = SpriteEffects.None;
                naru.SpriteEffects = SpriteEffects.FlipHorizontally;
            }
            else if (goku.RectanglePosition.X > naru.RectanglePosition.X)
            {
                goku.FaceRight = false;
                naru.FaceRight = true;
                goku.SpriteEffects = SpriteEffects.FlipHorizontally;
                naru.SpriteEffects = SpriteEffects.None;
            }


            #endregion

            #region characterCollisions
            if (!goku.RectanglePosition.Intersects(naru.RectanglePosition))
            {
                //there was some if statement here...
                goku.CanMoveRight = true;
                naru.CanMoveLeft = true;
                goku.CanMoveLeft = true;
                naru.CanMoveRight = true;

            }
            else if (goku.RectanglePosition.Intersects(naru.RectanglePosition))
            {
                //Naruto.SP = false;
                if (Goku.isFaceRight)
                {
                    goku.CanMoveRight = false;
                    naru.CanMoveLeft = false;
                }
                if (!Goku.isFaceRight)
                {
                    naru.CanMoveRight = false;
                    goku.CanMoveLeft = false;
                }
            }

            if (goku.RectanglePosition.Intersects(naru.RectanglePosition) && Naruto.isRasengan)
            {
                //Naruto.spMove = false;
                if (Goku.isFaceRight)
                {
                    goku.CanMoveRight = true;
                    naru.CanMoveLeft = true;
                }
                if (!Goku.isFaceRight)
                {
                    naru.CanMoveRight = true;
                    goku.CanMoveLeft = true;
                }
            }
            #endregion
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            goku.Draw(spriteBatch);
            naru.Draw(spriteBatch);
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }

            foreach (Spiritbomb bomb in spiritbombprojs)
            {
                bomb.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static Texture2D GetProjectileSprite(ProjectileType type)
        {
            // replace with code to return correct projectile sprite based on projectile type
            if (type == ProjectileType.gokuPower1)
            {
                return gokuProjectile1;
            }
            else if (type == ProjectileType.naruPower1)
            {
                return naruProjectile1;
            }
            else
            {
                return spiritbombProj;
            }
        }
        public static void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }
        public static void AddspiritbombProjectile(Spiritbomb spiritbombprojectile)
        {   
            if(spiritbombprojs.Count==0)
            spiritbombprojs.Add(spiritbombprojectile);
        }
    }
}
