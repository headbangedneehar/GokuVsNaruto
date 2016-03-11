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
    class Goku
    {
        Texture2D gokuStanceAtlas, gokuMoveAtlas, gokuPunchAtlas, gokuKickAtlas, gokuPower1Atlas, gokuFallBackAtlas, gokuspiritbombAtlas, gokujumpAtlas, gokuHitAtlas, gokuDownAtlas, gokufaceAtlas,gokutopAtlas,gokupowerupAtlas;
        Texture2D atlas;
        Vector2 location;
        Vector2 velocity = Vector2.Zero;
        public static Rectangle drawRectangle;
        Rectangle sourceRectangle;
        int rows, columns, totalframes, currentframe = 0, elapsedtime = 0; //int elapsedtime2 = 0;
        bool isStance = true;
        bool isMoveRight, isMoveLeft, isKick, isPunch, isPower1, isFire, isSpiritBomb, isSpiritBombFire,isJump,isHit,isDown,isPowerup;
        public static bool isFaceRight=true;
        bool canMoveRight = true, canMoveLeft = true;
        bool prevMoveRightState,prevMoveLeftState,prevStanceState;
        //bool handsDown,handsUp,drawLastFrame;
        SpriteEffects spriteEffects = SpriteEffects.None;
        int elapsedJumptime=0;
        private int elapsedMovetime;
        int elapsedpoweruptime = 0;
        //strings
        static int health = 100, power = 50;
        string healthString = "HEALTH: " + health;
        string powerString = "POWER:  " + power;
        static bool dead = false;
        int powertime;
        static bool hitframe=false;
        SpriteFont font;
        //CONSTRUCTOR-----------------------------------------------
        public Goku(Vector2 location, ContentManager content, string kickSpriteName, string punchSpriteName, string gokuMoveSpriteName, string gokuStanceSpriteName, string gokuPower1name, string spiritbombAtlasName, string gokuFallBackAtlasName, string gokuJumpSpriteName, string gokuHitAtlasName, string gokuDownAtlasName, string gokufaceAtlasName, string gokutopAtlasName, string gokupowerupAtlasName)
        {
            this.location = location;
            gokuStanceAtlas = content.Load<Texture2D>(gokuStanceSpriteName);
            gokuMoveAtlas = content.Load<Texture2D>(gokuMoveSpriteName);
            gokuKickAtlas = content.Load<Texture2D>(kickSpriteName);
            gokuPunchAtlas = content.Load<Texture2D>(punchSpriteName);
            gokuPower1Atlas = content.Load<Texture2D>(gokuPower1name);
            gokuspiritbombAtlas = content.Load<Texture2D>(spiritbombAtlasName);
            gokuFallBackAtlas = content.Load<Texture2D>(gokuFallBackAtlasName);
            gokujumpAtlas = content.Load<Texture2D>(gokuJumpSpriteName);
            gokuHitAtlas = content.Load<Texture2D>(gokuHitAtlasName);
            gokuDownAtlas = content.Load<Texture2D>(gokuDownAtlasName);
            gokufaceAtlas = content.Load<Texture2D>(gokufaceAtlasName);
            gokutopAtlas = content.Load<Texture2D>(gokutopAtlasName);
            gokupowerupAtlas = content.Load<Texture2D>(gokupowerupAtlasName);
            font = content.Load<SpriteFont>("Arial20");
        }



        public static bool Death
        {
            get { return dead; }
        }

        public static bool HitFrame
        {
            get { return hitframe; }
        }

        public static int Health
        {
            get { return health; }
            set { health = value; }
        }

        public bool Hit
        {
            get { return isHit; }
            set { isHit = value; }
        }

        public bool PowerUp
        {
            get { return isPowerup; }
            set { isPowerup = value; }
        }

        public bool Down
        {
            get { return isDown; }
            set { isDown = value; }
        }

        public bool Stance
        {
            get { return isStance; }
            set { isStance = value; }
        }

        public bool Jump
        {
            get { return isJump; }
            set { isJump = value; }
        }

        public bool SpiritBomb
        {
            get { return isSpiritBomb; }
            set { isSpiritBomb = value; }
        }

        public bool SpiritBombFire
        {
            get { return isSpiritBombFire; }
            set { isSpiritBombFire = value; }
        }

        public bool FaceRight
        {
            get { return isFaceRight; }
            set { isFaceRight = value; }
        }

        public bool MoveRight
        {
            get { return isMoveRight; }
            set { isMoveRight = value; }
        }
        public bool MoveLeft
        {
            get { return isMoveLeft; }
            set { isMoveLeft = value; }
        }

        public bool Kick
        {
            get { return isKick; }
            set { isKick = value; }
        }

        public bool Punch
        {
            get { return isPunch; }
            set { isPunch = value; }
        }

        public bool Power1
        {
            get { return isPower1; }
            set { isPower1 = value; }
        }

        public bool Fire
        {
            get { return isFire; }
            set { isFire = value; }
        }

        public Rectangle RectanglePosition
        {
            get { return drawRectangle; }
        }

        public static Rectangle DrawRectangle
        {
            get { return drawRectangle; }
        }

        public SpriteEffects SpriteEffects
        {
            get { return spriteEffects; }
            set { spriteEffects = value; }
        }

        public bool CanMoveRight
        {
            get { return canMoveRight; }
            set { canMoveRight = value; }
        }

        public bool CanMoveLeft
        {
            get { return canMoveLeft; }
            set { canMoveLeft = value; }
        }






        public void Update(GameTime gameTime, KeyboardState key)
        {
            
            #region goku
            //Stance = true;
            if (Stance)
            {
                GokuStance(gokuStanceAtlas, gameTime, 1, 4);
                prevStanceState = true;
            }

            if (key.IsKeyDown(Keys.Right) &&
                RectanglePosition.X < (GC.WINDOW_WIDTH - GC.gokuAtlasRectX) &&
                CanMoveRight)
            {
                prevMoveRightState = true;
                Stance = false;
                PowerUp = false;
                //isPowerup = false;
                MoveRight = true;
                //Punch = false;
                //Kick = false;
                //Power1 = false;
                //SpiritBomb = false;
                GokuMove(gameTime);
                //if (FaceRight && MoveRight)
                //    GokuMove(gokuMoveAtlas, 1, 4, gameTime);
                //else if (!FaceRight && MoveRight)
                //{ GokuFallBackRight(gokuFallBackAtlas, 1, 1, gameTime); }
            }
            else
            {
                Stance = true;
                MoveRight = false;
            }

            if (key.IsKeyDown(Keys.Left) &&
                RectanglePosition.X > 0 &&
                CanMoveLeft)
            {
                prevMoveLeftState = true;
                Stance = false;
                PowerUp = false;
                //isPowerup = false;
                MoveLeft = true;
                //Punch = false;
                //Kick = false;
                //Power1 = false;
                //SpiritBomb = false;
                GokuMove(gameTime);
                //if (!FaceRight && MoveLeft)
                //    GokuMove(gokuMoveAtlas, 1, 4, gameTime);
                //else if (FaceRight && MoveLeft)
                //{ GokuFallBackRight(gokuFallBackAtlas, 1, 1, gameTime); }
            }
            else
            {
                Stance = true;
                MoveLeft = false;
            }
            if ((!MoveRight && prevMoveRightState))
            {
                if (Power1)
                {
                    prevMoveRightState = false;
                }
                else
                {
                    prevMoveRightState = false;
                    currentframe = 0;
                }
            }
            if ((!MoveLeft && prevMoveLeftState))
            {
                if (Power1)
                {
                    prevMoveLeftState = false;
                }
                else
                {
                    prevMoveLeftState = false;
                    currentframe = 0;
                }
            }

            if (prevStanceState && (Punch || Kick || SpiritBomb || Power1 || Jump || Hit || Down || PowerUp))
            {
                currentframe = 0; elapsedtime = 0; elapsedMovetime = 0;// elapsedpoweruptime = 0;
                prevStanceState = false;
            }

            if (key.IsKeyDown(Keys.W)&&!Kick&&!Power1)
                Punch = true;
            if (Punch)
            {
                Stance = false;
                PowerUp = false;
                //isPowerup = false;
                //MoveLeft = false;
                //MoveRight = false;
                Kick = false;
                Power1 = false;
                GokuPunch(gokuPunchAtlas, 1, 7, gameTime);
            }

            if (key.IsKeyDown(Keys.A)&&(!Punch && !Kick && !SpiritBomb && !Power1 && !Jump && !Hit && !Down&&!MoveRight&&!MoveLeft))
                PowerUp = true;
            if (PowerUp)
            {
                Stance = false;
                //isPowerup = false;
                //MoveLeft = false;
                //MoveRight = false;
                //Punch = false;
                //Kick = false;
                //Power1 = false;
                Powerup(gokupowerupAtlas, gameTime, 1, 2);
            }
            if (key.IsKeyUp(Keys.A)) { PowerUp = false;  } 
            
            if (key.IsKeyDown(Keys.E)&&!Power1&&!Punch)
                Kick = true;
            if (Kick)
            {
                Stance = false;
                PowerUp = false;
                //isPowerup = false;
                //MoveLeft = false;
                //MoveRight = false;
                Punch = false;
                Power1 = false;
                GokuKick(gokuKickAtlas, 1, 6, gameTime);
            }

            if (key.IsKeyDown(Keys.Q)&&power>=30&&!Punch&&!Kick)
                Power1 = true;
            if (Power1)
            {

                Stance = false; //isPowerup = false;
                PowerUp = false;
                GokuPower1(gokuPower1Atlas, 1, 9, gameTime);
                
            }

            if (key.IsKeyDown(Keys.S)&&
                (Naruto.DrawRectangle.X - Goku.DrawRectangle.X) > 200 && power>=80)
                SpiritBomb = true;
            if (SpiritBomb)
            {
                canMoveLeft = false;
                canMoveRight = false;
                Stance = false;// isPowerup = false;
                PowerUp = false;
                MoveLeft = false;
                MoveRight = false;
                Punch = false;
                Kick = false;
                Power1 = false;
                //handsUp = true;
                GokuSpiritbomb(gokuspiritbombAtlas, 1, 11, gameTime);
            }
            if (!SpiritBomb)
            {
                canMoveLeft = true;
                canMoveRight = true;
            }


            if (key.IsKeyDown(Keys.Up))
                Jump = true;
            if (Jump)
            {
                Stance = false; //isPowerup = false;
                PowerUp = false;
                SpiritBomb = false;
                SpiritBombFire = false;
                GokuJump(gameTime);//narujumpAtlas, 1, 4, 
            }
            

            if (Fire)
            {
                int projposi = 0;
                if (FaceRight)
                    projposi = 50;
                else if (!FaceRight)
                    projposi = -50;
                Fire = false;
                Projectile projectile = new Projectile(ProjectileType.gokuPower1, Game1.GetProjectileSprite(ProjectileType.gokuPower1),
                                        RectanglePosition.Center.X+projposi, RectanglePosition.Center.Y-5, GC.PROJECTILE_SPEED);

                Game1.AddProjectile(projectile);
                
            }

            if (SpiritBombFire)
            {
                SpiritBombFire = false;
                int yfactor = -100, xfactor;
                if (FaceRight)
                {
                    xfactor = -20;
                }
                else
                    xfactor = -40;
                Spiritbomb bomb = new Spiritbomb(Game1.GetProjectileSprite(ProjectileType.spiritbomb), DrawRectangle.X + xfactor, DrawRectangle.Y + yfactor);
                Game1.AddspiritbombProjectile(bomb);

            }


            if (MoveLeft)
            {

                location.X += -1 * gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                location.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
                drawRectangle.X = (int)location.X - sourceRectangle.Width / 2;
                drawRectangle.Y = (int)location.Y - sourceRectangle.Height / 2;

            }

            if (MoveRight)
            {

                location.X += 1 * gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                location.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
                drawRectangle.X = (int)location.X - sourceRectangle.Width / 2;
                drawRectangle.Y = (int)location.Y - sourceRectangle.Height / 2;

            }

            if (Hit)
            {
                //MoveLeft = false;
                //MoveRight = false;
                //Power1 = false;
                //Jump = false;
                //Kick = false;
                //Punch = false;
                Stance = false; //isPowerup = false;
                GokuHit(gokuHitAtlas, 1, 3, gameTime);
            }
            if (Down)
            {
                Stance = false; isPowerup = false;
                MoveLeft = false;
                MoveRight = false;
                Power1 = false;
                Jump = false;
                Kick = false;
                Punch = false;

                GokuDown(gokuDownAtlas, 1, 6, gameTime);
            }

            powertime += gameTime.ElapsedGameTime.Milliseconds;
            if (power < 100)
            {
                if (!PowerUp)
                {
                    if (powertime % 200 == 0)
                    {
                        power += 1;
                    }
                }
                if (PowerUp)
                {
                    if (powertime % 10 == 0)
                    {
                        power += 1;
                    }
                }
            }

            powerString = "POWER:  " + power;
            //health updates
            healthString = "HEALTH: " + health;
            //handling death
            if (health <= 0)
            {
                dead = true;
            }
            if(dead)
            {
                Stance = false;
                canMoveLeft = false;
                CanMoveRight = false;
                GokuDown(gokuDownAtlas, 1, 6, gameTime);
            }
            #endregion

        }

        #region methods
        public void GokuStance(Texture2D atlas, GameTime gameTime,int ro,int col)
        {
            this.atlas = atlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedtime % 30 == 0)
            {
                currentframe++;
                if (currentframe == totalframes)
                {
                    currentframe = 0;
                }
            }
        }
        public void Powerup(Texture2D powerupatlas, GameTime gameTime, int ro, int col)
        {
            if (!Stance && !Kick && !Punch && !Power1 && !Hit && !Down)
            {
                this.atlas = powerupatlas;
                rows = ro;
                columns = col;
                totalframes = rows * columns;
                elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedtime % 30 == 0)
                {
                    currentframe++;
                    if (currentframe == totalframes)
                    {
                        currentframe = 0;

                        //Stance = true;
                        //PowerUp = false;
                    }
                }
                if (currentframe > totalframes - 1)
                {
                    currentframe = 0;
                    elapsedtime = 0;
                }
            }
        }
        public void GokuJump(GameTime gameTime)
        {
            if (!Kick && !Punch && !Power1 &&!Hit &&!Down)
            {
                this.atlas = gokujumpAtlas;
                rows = 1;
                columns = 4;
                totalframes = rows * columns;
            }
            elapsedJumptime += gameTime.ElapsedGameTime.Milliseconds * 3;
            {
                //up in air equation
                //elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedJumptime >= 0 && elapsedJumptime <= 1000)
                {
                    if (!Kick && !Punch && !Power1 && !Hit && !Down)
                        currentframe = 0;
                    location.Y += -1 * gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                    drawRectangle.Y = (int)location.Y;
                }
                else if (elapsedJumptime >= 1000 && elapsedJumptime <= 1200)
                {
                    if (!Kick && !Punch && !Power1 && !Hit && !Down)
                        currentframe = 1;
                    //if (currentframe == totalframes)
                    //{
                    //    currentframe = 0; 
                    //    Stance = true; isJump = false; ;
                    //    elapsedtime = 0;
                    //}

                }
                else if (elapsedJumptime >= 1200 && elapsedJumptime <= 1800)
                {
                    if (!Kick && !Punch && !Power1 && !Hit && !Down)
                        currentframe = 1;
                    if (location.Y != GC.WINDOW_HEIGHT / 2)
                    {
                        location.Y += gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                        drawRectangle.Y = (int)location.Y;
                    }
                }
                else if (elapsedJumptime >= 1800)// && elapsedtime <= 2200)
                {
                    if (!Kick && !Punch && !Power1 && !Hit && !Down)
                    {
                        if (elapsedJumptime < 1850)
                            currentframe = 2;
                        currentframe = 3;
                    }
                    if (location.Y != GC.WINDOW_HEIGHT / 2)
                    {
                        location.Y += gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                        drawRectangle.Y = (int)location.Y;
                    }
                    else
                    {
                        if (!Kick && !Punch && !Power1 && !Hit && !Down)
                        {//    currentframe = 0;
                            elapsedJumptime = 0;
                            Stance = true;
                            Jump = false;
                        }
                    }
                }
            }

        }
        public void GokuFallBackRight(Texture2D FallBackRightatlas, int ro, int col, GameTime gameTime)
        {
            if (!Kick && !Punch && !Power1 && !Hit && !Down)
            {
                currentframe = 0;
                this.atlas = FallBackRightatlas;
                rows = ro;
                columns = col;
                totalframes = rows * columns;
                elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedtime % 30 == 0)
                {
                    currentframe++;
                    if (currentframe == totalframes)
                    {
                        currentframe = 0;
                        elapsedtime = 0;
                    }
                }
            }
        }
        //Texture2D moveAtlas, int ro, int col, 
        public void GokuMove(GameTime gameTime)
        {
            if (!Kick && !Punch && !Power1 && !Hit && !Down)
            {
                if (!FaceRight && MoveLeft)
                {
                    this.atlas = gokuMoveAtlas;
                    rows = 1;
                    columns = 4;
                    totalframes = rows * columns;
                }
                else if (FaceRight && MoveLeft)
                {
                    GokuFallBackRight(gokuFallBackAtlas, 1, 1, gameTime);
                }
                if (FaceRight && MoveRight)
                {
                    this.atlas = gokuMoveAtlas;
                    rows = 1;
                    columns = 4;
                    totalframes = rows * columns;
                }
                else if (!FaceRight && MoveRight)
                {
                    GokuFallBackRight(gokuFallBackAtlas, 1, 1, gameTime);
                }

                elapsedMovetime += gameTime.ElapsedGameTime.Milliseconds;
                {
                    if (elapsedMovetime % 30 == 0)
                    {
                        currentframe++; elapsedMovetime = 0;
                        if (currentframe == totalframes)
                        {
                            currentframe = 0; Stance = true;
                            elapsedMovetime = 0;
                        }

                    }
                }
            }
        }
       
        public void GokuPunch(Texture2D gokupunchatlas, int ro, int col, GameTime gameTime)
        {
            this.atlas = gokupunchatlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentframe >= 2 && currentframe <= 4)
                hitframe = true;
            else hitframe = false;
            if (elapsedtime % 10 == 0)
                {
                    currentframe++;
                    if (currentframe == totalframes)
                    {
                        currentframe = 0;

                        Stance = true;
                        Punch = false;
                    }
                }
         }

        public void GokuKick(Texture2D gokukickatlas, int ro, int col, GameTime gameTime) //add keyboard state , if key is down move left or right
        {
            this.atlas = gokukickatlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentframe >= 2 && currentframe <= 3)
                hitframe = true;
            else hitframe = false;
            if (elapsedtime % 10 == 0)
            {
                currentframe++;
                if (currentframe == totalframes)
                {
                    currentframe = 0;
                    isStance = true;
                    isKick = false;
                }
            }
        }


        public void GokuPower1(Texture2D gokupower1atlas, int ro, int col, GameTime gameTime) //add keyboard state , if key is down move left or right
        {
            bool powerdecrease = false;
            this.atlas = gokupower1atlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentframe >= 0 && currentframe <= 7)
            {
                if (elapsedtime % 20 == 0)
                {if(currentframe==0&&!powerdecrease)
                    power = power - 30;
                    powerdecrease = true;
                    currentframe++;
                    elapsedtime = 0;
                    if (currentframe == 7)
                        Fire = true;
                }
            }
            if (currentframe == 8)
            {
                if (elapsedtime >= 0 && elapsedtime <= 500)
                {
                    currentframe = 8;

                }
                else { currentframe = 0; elapsedtime = 0; Power1 = false; }
            }
        }

        public void GokuSpiritbomb(Texture2D gokuspiritbombatlas, int ro, int col, GameTime gameTime) //add keyboard state , if key is down move left or right
        {
            bool powerdecrease = false;
            this.atlas = gokuspiritbombatlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if(currentframe==0)
            {
                
                if(elapsedtime%10==0)
                {
                    if (!powerdecrease)
                        power = power - 80;
                    powerdecrease = true;
                    //SpiritBombFire = true;
                    currentframe++;
                    elapsedtime = 0;
                }
            }
            if (currentframe == 1)
            { 
                if (elapsedtime % 10 == 0)
                {
                    SpiritBombFire = true;
                    currentframe++;
                }
            }
            if (currentframe >= 2 && currentframe <= 10)
            {
                //if (currentframe == 1)
                //    SpiritBombFire = true;
                if (elapsedtime % 10 == 0)
                {
                    currentframe++;
                    if (currentframe == 10)
                    {
                        SpiritBomb = false;
                        elapsedtime = 0;
                        currentframe = 0;
                    }
                }
            }
        }

        public void GokuHit(Texture2D gokuhitatlas, int ro, int col, GameTime gameTime)
        {
            if (!Punch && !Kick && !Power1 &&!isPowerup)
            {
                this.atlas = gokuhitatlas;
                rows = ro;
                columns = col;
                totalframes = rows * columns;
                elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedtime % 10 == 0)
                {
                    currentframe++; //elapsedtime = 0;
                    if (currentframe == totalframes)
                    {
                        currentframe = 0;
                        Stance = true;
                        Hit = false;
                        elapsedtime = 0;
                    }
                }
            }
        }
        public void GokuDown(Texture2D gokudownatlas, int ro, int col, GameTime gameTime)
        {
            this.atlas = gokudownatlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if (!dead)
            {
                if (elapsedtime % 20 == 0)
                {
                    currentframe++; //elapsedtime = 0;

                    if (currentframe == totalframes)
                    {
                        currentframe = 0;
                        Stance = true;
                        Down = false;
                        elapsedtime = 0;
                    }
                }
            }
            if (dead)
            {
                if (currentframe == 0)
                    if (elapsedtime % 20 == 0)
                    {
                        currentframe++;
                    }
                if (currentframe == 1)
                {
                    if (elapsedtime % 20 == 0)
                    {
                        currentframe++;
                    }
                }
                if (currentframe == 2)
                {
                    currentframe = 2;
                }
            }
        }
                

        public void Draw(SpriteBatch spriteBatch)
        {
            int height, width;
            height = atlas.Height / rows;
            width = atlas.Width / columns;
            int ro = currentframe / columns;
            int col = currentframe % columns;
            sourceRectangle = new Rectangle(width * col, height * ro, width, height);
            drawRectangle.Width = width;
            drawRectangle.Height = height;
            drawRectangle.X = (int)location.X - sourceRectangle.Width / 2;
            drawRectangle.Y = (int)location.Y - sourceRectangle.Height / 2;
            Rectangle drawFaceRectangle = new Rectangle(30, 430, gokufaceAtlas.Width, gokufaceAtlas.Height);
            if (SpiritBomb||Spiritbomb.BombMoving)
                spriteBatch.Draw(gokufaceAtlas, drawFaceRectangle, Color.White);
            Rectangle drawFaceTopRectangle = new Rectangle(30, 30, gokutopAtlas.Width, gokutopAtlas.Height);
            spriteBatch.Draw(gokutopAtlas, drawFaceTopRectangle, Color.White);
            Vector2 xy = new Vector2((int)location.X - sourceRectangle.Width / 2, (int)location.Y - sourceRectangle.Height / 2);
            //spriteBatch.Begin();
            //spriteBatch.Draw(atlas, drawRectangle, sourceRectangle, Color.White);
            spriteBatch.Draw(atlas, drawRectangle, sourceRectangle, Color.White, (float)0, new Vector2(0, 0), spriteEffects, 0f);
            spriteBatch.DrawString(font, healthString, new Vector2(30, 100), Color.White);
            spriteBatch.DrawString(font, powerString, new Vector2(30, 150), Color.White);
        }
 
    }
}
        #endregion