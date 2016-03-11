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
    class Naruto
    {
        Texture2D naruStanceAtlas, naruMoveAtlas, naruPunchAtlas, naruKickAtlas, naruPower1Atlas, naruFallBackRightAtlas, naruspAtlas, narujumpAtlas, naruHitAtlas, naruDownAtlas, narufaceAtlas, narutopAtlas, narupowerupAtlas;
        Texture2D atlas;
        Vector2 location;
        Vector2 velocity = Vector2.Zero;
        public static Rectangle drawRectangle, sourceRectangle;
        int rows, columns, totalframes, currentframe = 0, elapsedtime = 0,elapsedJumptime=0; //int elapsedtime2 = 0;
        bool isStance = true;
        bool isMoveRight, isMoveLeft, isKick, isPunch, isPower1, isFire,isJump,isHit,isDown,isPowerup;
        public static bool isFaceRight = true;
        bool canMoveRight = true, canMoveLeft = true;
        public static bool spMove=false;
        //bool handsDown, handsUp, drawLastFrame;
        public static bool isRasengan;
        SpriteEffects spriteEffects = SpriteEffects.None;
        //KeyboardState oldState, newState;
        bool prevMoveLeftState,prevMoveRightState,prevStanceState;
        private int elapsedMovetime;
        //bool validate = true;

        //strings and hp power stuff
        static int health=100,power=50;
        string healthString = "HEALTH: " + health;
        string powerString = "POWER:  " + power;
        static bool dead = false;
        int powertime;
        static bool hitframe;
        SpriteFont font;

        //CONSTRUCTOR-----------------------------------------------
        public Naruto(Vector2 location, ContentManager content, string kickSpriteName, string punchSpriteName, string naruMoveSpriteName, string naruStanceSpriteName, string naruPower1name, string naruFallBackRightSpriteName, string naruspSpriteName, string naruJumpSpriteName, string naruHitAtlasName, string naruDownAtlasName, string narufaceAtlasName, string narutopAtlasName, string narupowerupAtlasName)
        {
            this.location = location;
            naruStanceAtlas = content.Load<Texture2D>(naruStanceSpriteName);
            naruMoveAtlas = content.Load<Texture2D>(naruMoveSpriteName);
            naruKickAtlas = content.Load<Texture2D>(kickSpriteName);
            naruPunchAtlas = content.Load<Texture2D>(punchSpriteName);
            naruPower1Atlas = content.Load<Texture2D>(naruPower1name);
            naruFallBackRightAtlas = content.Load<Texture2D>(naruFallBackRightSpriteName);
            naruspAtlas = content.Load<Texture2D>(naruspSpriteName);
            narujumpAtlas = content.Load<Texture2D>(naruJumpSpriteName);
            naruHitAtlas = content.Load<Texture2D>(naruHitAtlasName);
            naruDownAtlas = content.Load<Texture2D>(naruDownAtlasName);
            narufaceAtlas = content.Load<Texture2D>(narufaceAtlasName);
            narutopAtlas = content.Load<Texture2D>(narutopAtlasName);
            narupowerupAtlas = content.Load<Texture2D>(narupowerupAtlasName);
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

        public bool Ransengan
        {
            get { return isRasengan; }
            set { isRasengan = value; }
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
            get{return drawRectangle;}
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






        public void Update(GameTime gameTime, KeyboardState oldState,KeyboardState newState)
        {
            //this.oldState = oldstate;
            //this.newState = newstate;
            #region naru
            //Stance = true;
            if (Stance)
            {
                NaruStance(naruStanceAtlas, gameTime, 1, 4);
                prevStanceState = true;
            }

            if (oldState.IsKeyDown(Keys.NumPad6) && //newState.IsKeyDown(Keys.NumPad6) &&
                RectanglePosition.X < (GC.WINDOW_WIDTH - GC.naruAtlasRectX) &&
                CanMoveRight)
            {
                prevMoveRightState = true;
                Stance = false;
                MoveRight = true;
                MoveLeft = false;
                NaruMove(gameTime);
                //if(FaceRight&&MoveRight)
                //NaruMove(naruMoveAtlas,1, 8, gameTime);
                //else if (!FaceRight&&MoveRight)
                //{ NaruFallBackRight(naruFallBackRightAtlas, 1, 1, gameTime); }
            }
            else
            { Stance = true;
            MoveRight = false;
            }
            
            if (oldState.IsKeyDown(Keys.NumPad4) && //newState.IsKeyDown(Keys.NumPad4) &&
                RectanglePosition.X > 0 &&
                CanMoveLeft)
            {
                prevMoveLeftState = true;
                Stance = false; 
                MoveLeft = true; 
                MoveRight = false;
                NaruMove(gameTime);
                //if(!FaceRight&&MoveLeft)
                //NaruMove(naruMoveAtlas,1, 8, gameTime);
                //else if (FaceRight&&MoveLeft)
                //{ NaruFallBackRight(naruFallBackRightAtlas, 1, 1, gameTime); }
                
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

            if (prevStanceState && (Punch || Kick || isRasengan || Power1 || Jump || Hit || Down || PowerUp))
            {
                currentframe = 0; elapsedtime = 0; elapsedMovetime = 0;
                prevStanceState = false;

            }

            if (oldState.IsKeyDown(Keys.O)&&!Kick&&!Power1)
                Punch = true;
            if (Punch)
            {
                Stance = false;
                //MoveLeft = false;
                //MoveRight = false;
                Kick = false;
                Power1 = false;
                NaruPunch(naruPunchAtlas, 1, 5, gameTime);
            }

            if (oldState.IsKeyDown(Keys.K) && (!Punch && !Kick && !isRasengan && !Power1 && !Jump && !Hit && !Down && !MoveRight && !MoveLeft))
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
                Powerup(narupowerupAtlas, gameTime, 1, 5);
            }
            if (oldState.IsKeyUp(Keys.K)) { PowerUp = false; }

            if (oldState.IsKeyDown(Keys.P)&&!Punch&&!Power1)
                Kick = true;
            if (Kick)
            {
                Stance = false;
                //MoveLeft = false;
                //MoveRight = false;
                Punch = false;
                Power1 = false;
                NaruKick(naruKickAtlas, 1, 7, gameTime);
            }

            if (oldState.IsKeyDown(Keys.I)&&power>=20&&!Kick&&!Punch)
                Power1 = true;
            if (Power1)
            {

                Stance = false;
                //MoveLeft = false;
                //MoveRight = false;
                Punch = false;
                Kick = false;
                //handsUp = true;
                NaruPower1(naruPower1Atlas, 1, 5, gameTime);

            }

            if (oldState.IsKeyDown(Keys.NumPad8))
                Jump = true;
            if (Jump)
            {
                Stance = false;
                isRasengan = false;
                NaruJump( gameTime);//narujumpAtlas, 1, 4, 
            }
            

            if (oldState.IsKeyDown(Keys.L)
                && (Naruto.DrawRectangle.X-Goku.DrawRectangle.X)>200 &&
                power>=70)// ||(oldState.IsKeyDown(Keys.Space) && oldState.IsKeyDown(Keys.NumPad4)))
                isRasengan = true;
            if (isRasengan)
            {

                Stance = false;
                MoveLeft = false;
                MoveRight = false;
                Punch = false;
                Kick = false;
                //handsUp = true;
                NaruRasengan(naruspAtlas, 1, 19, gameTime);

            }




            if (Fire)
            {
                //add code wrt facing side + or - x axis center coords...
                int projposi = 0;
                if (FaceRight)
                    projposi = 50;
                else if (!FaceRight)
                    projposi = -50;
                Fire = false;
                Projectile projectile = new Projectile(ProjectileType.naruPower1, Game1.GetProjectileSprite(ProjectileType.naruPower1),
                                        RectanglePosition.Center.X +projposi, RectanglePosition.Center.Y + 5, GC.PROJECTILE_SPEED);

                Game1.AddProjectile(projectile);

            }
            


            if (MoveLeft)
            {
                location.X += -1 * gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                //location.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
                drawRectangle.X = (int)location.X - sourceRectangle.Width / 2;
                //drawRectangle.Y = (int)location.Y - sourceRectangle.Height / 2;
                
            }

            if (MoveRight)
            {
                location.X += 1 * gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                //location.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
                drawRectangle.X = (int)location.X - sourceRectangle.Width / 2;
                //drawRectangle.Y = (int)location.Y - sourceRectangle.Height / 2;
            }

            if (isRasengan)
            {
                MoveLeft = false;
                MoveRight = false;
                Punch = false;
                Kick = false;
                Stance = false;
                //Jump = false;
                //PowerUp = false;
            }

            if (spMove && FaceRight)
            {
                location.X += 1 * gameTime.ElapsedGameTime.Milliseconds * 0.6f;
                location.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
                drawRectangle.X = (int)location.X - sourceRectangle.Width / 2;
                drawRectangle.Y = (int)location.Y - sourceRectangle.Height / 2;
            }

            if (spMove && !FaceRight)
            {
                location.X += -1 * gameTime.ElapsedGameTime.Milliseconds * 0.6f;
                location.Y += velocity.Y * gameTime.ElapsedGameTime.Milliseconds;
                drawRectangle.X = (int)location.X - sourceRectangle.Width / 2;
                drawRectangle.Y = (int)location.Y - sourceRectangle.Height / 2;
            }

            if(RectanglePosition.Intersects(Goku.DrawRectangle))
            {
            canMoveLeft=true;canMoveRight=true;
            }

            oldState = newState;

            if (Hit)
            {
                //MoveLeft = false;
                //MoveRight = false;
                //Power1 = false;
                //Jump = false;
                //Kick = false;
                //Punch = false;
                Stance = false;
                NaruHit(naruHitAtlas, 1, 3, gameTime);
            }
            if (Down)
            {
                Stance = false;
                MoveLeft = false;
                MoveRight = false;
                Power1 = false;
                Jump = false;
                Kick = false;
                Punch = false;

                NaruDown(naruDownAtlas, 1, 6, gameTime);
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
            if (dead)
            {
                Stance = false;
                canMoveLeft = false;
                CanMoveRight = false;
                NaruDown(naruDownAtlas, 1, 6, gameTime);
            }
            #endregion

        }

        #region methods
        public void NaruStance(Texture2D atlas, GameTime gameTime, int ro, int col)
        {
            
            this.atlas = atlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            
            if (elapsedtime %30==0)
            {

                currentframe++;
                if ((currentframe == totalframes))//||(currentframe>totalframes-1))
                {
                    currentframe = 0;
                    elapsedtime = 0;
                }
            }
            if(currentframe>totalframes-1)
            {
                currentframe = 0;
                elapsedtime = 0;
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
                if (currentframe >= 0 && currentframe <= 2)
                {
                    if (elapsedtime % 20 == 0)
                    {
                        currentframe++;
                    }
                }
                if (currentframe >= 3 && currentframe <= 4)
                {
                    if (elapsedtime % 30 == 0)
                    {
                        if (currentframe == 3)
                            currentframe = 4;
                        else if (currentframe == 4)
                            currentframe = 3;
                    }
                }
                //if (elapsedtime % 30 == 0)
                //{
                //    currentframe++;
                //    if (currentframe == totalframes)
                //    {
                //        currentframe = 0;

                //        Stance = true;
                //        PowerUp = false;
                //    }
                //}
            }
        }

        public void NaruFallBackRight(Texture2D FallBackRightatlas, int ro, int col, GameTime gameTime)
        {
            if (!Kick && !Punch && !Power1 && !Hit)
            {
                currentframe = 0;
                this.atlas = FallBackRightatlas;
                rows = ro;
                columns = col;
                totalframes = rows * columns;

                elapsedMovetime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedtime % 30 == 0)
                {
                    currentframe++; elapsedtime = 0;
                    if (currentframe == totalframes)
                    {
                        currentframe = 0;
                        elapsedMovetime = 0;
                    }
                }
            }
        }

        public void NaruMove(GameTime gameTime)
        {
            if (!Kick && !Punch && !Power1 && !Hit &&!Down)
            {
                if (!FaceRight && MoveLeft)
                {
                    this.atlas = naruMoveAtlas;
                    rows = 1;
                    columns = 8;
                    totalframes = rows * columns;
                }
                else if (FaceRight && MoveLeft)
                {
                    NaruFallBackRight(naruFallBackRightAtlas, 1, 1, gameTime);
                }
                if (FaceRight && MoveRight)
                {
                    this.atlas = naruMoveAtlas;
                    rows = 1;
                    columns = 8;
                    totalframes = rows * columns;
                }
                else if (!FaceRight && MoveRight)
                {
                    NaruFallBackRight(naruFallBackRightAtlas, 1, 1, gameTime);
                }
                //}

                //this.atlas = naruMoveAtlas;
                //rows = ro;
                //columns = col;
                //totalframes = rows * columns;
                elapsedMovetime += gameTime.ElapsedGameTime.Milliseconds;
                {
                    if (elapsedMovetime % 5 == 0)
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
        //Texture2D naruMoveAtlas, int ro, int col,
        public void NaruJump( GameTime gameTime)
        {
            if (!Kick && !Punch && !Power1 &&!Hit&&!Down)
            {
                this.atlas = narujumpAtlas;
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
                }
                else if (elapsedJumptime >= 1200 && elapsedJumptime <= 1800) 
                {
                    if (!Kick && !Punch && !Power1 && !Hit && !Down)
                        currentframe = 2;
                    if (location.Y != GC.WINDOW_HEIGHT / 2)
                    {
                        location.Y += gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                        drawRectangle.Y = (int)location.Y;
                    }
                }
                else if (elapsedJumptime >= 1800)// && elapsedtime <= 2200)
                {
                    if (!Kick && !Punch && !Power1 && !Hit && !Down)
                        currentframe = 3;
                    if (location.Y != GC.WINDOW_HEIGHT / 2)
                    {
                        location.Y += gameTime.ElapsedGameTime.Milliseconds * 0.3f;
                        drawRectangle.Y = (int)location.Y;
                    }
                    else
                    {
                        if (!Kick && !Punch && !Power1 && !Hit && !Down)
                        {
                            elapsedJumptime = 0;
                            Stance = true;
                            Jump = false;
                        }
                    }
                }
            }

        }

        public void NaruPunch(Texture2D narupunchatlas, int ro, int col, GameTime gameTime)
        {
            this.atlas = narupunchatlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if (currentframe >= 1 && currentframe <= 2)
                hitframe = true;
            else hitframe = false;
            if (elapsedtime % 10 == 0)
            {
                currentframe++;
                if (currentframe == totalframes)
                {
                    currentframe = 0;
                    isStance = true;
                    isPunch = false;
                    elapsedtime = 0;
                }
            }
        }

        public void NaruHit(Texture2D naruhitatlas, int ro, int col, GameTime gameTime)
        {
            if (!Punch && !Kick && !Power1)
            {
                this.atlas = naruhitatlas;
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
        public void NaruDown(Texture2D narudownatlas, int ro, int col, GameTime gameTime)
        {
            this.atlas = narudownatlas;
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
                        currentframe=1;
                    }
                }
            }
        }

        public void NaruKick(Texture2D narukickatlas, int ro, int col, GameTime gameTime) //add keyboard state , if key is down move left or right
        {
            this.atlas = narukickatlas;
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
                    isStance = true;
                    isKick = false;
                    elapsedtime = 0;
                }
            }
        }


        public void NaruPower1(Texture2D narupower1atlas, int ro, int col, GameTime gameTime) //add keyboard state , if key is down move left or right
        {
            bool powerdecrease = false;
            this.atlas = narupower1atlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            if(currentframe>=0 && currentframe<=3)
            {
                if (elapsedtime % 20 == 0)
                {
                    if(currentframe==0&&!powerdecrease)
                    power = power - 20;
                    powerdecrease = true;
                    currentframe++;
                    elapsedtime=0;
                    if (currentframe == 3)
                        Fire = true;
                }
            }
            if(currentframe == 4)
            {
                if (elapsedtime >= 0 && elapsedtime <= 500)
                {
                    currentframe = 4;
                }
                else { currentframe = 0; elapsedtime = 0; Power1 = false; }
            }
        }


        

        public void NaruRasengan(Texture2D narusp, int ro, int col, GameTime gameTime)
        {
            bool powerdecrease=false;
            this.atlas = naruspAtlas;
            rows = ro;
            columns = col;
            totalframes = rows * columns;
            totalframes = rows * columns;
            elapsedtime += gameTime.ElapsedGameTime.Milliseconds;
            {
                //handling 1st 2 frames, stationary
                if (currentframe >= 0 && currentframe<=9)
                {
                    //validate = true;
                    spMove = false;
                    if (elapsedtime % 20 == 0)
                    {
                        if(currentframe==0&&!powerdecrease)
                            power = power - 70;
                            powerdecrease = true;
                            currentframe++;

                    }
                }
                //handling moving frames 2-7, moving
                if (9 < currentframe && currentframe < 15)
                {
                    spMove = true;
                    
                    if (elapsedtime % 5 == 0)
                    {
                        currentframe++;
                    }
                }
                
                //if (validate && RectanglePosition.X - Goku.sourceRectangle.Width - 60 <= (Goku.sourceRectangle.X + Goku.sourceRectangle.Width))
                //{
                //    spMove = false;
                //    currentframe = 7;
                //    validate = false;
                //}
                if (currentframe == 15)
                {   
                    if (RectanglePosition.Intersects(Goku.DrawRectangle))
                    {
                        spMove = false;
                        currentframe++; 
                    }
                }
                //handling frames after move
                if (currentframe > 15)
                {   
                    if (elapsedtime % 20 == 0)
                    {
                        currentframe++;
                        if (currentframe == totalframes)                       
                        {
                            isRasengan = false; 
                            currentframe = 0; 
                            Stance = true; 
                            elapsedtime = 0;
                        }
                    }
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
            Rectangle drawFaceRectangle=new Rectangle(630,430,narufaceAtlas.Width,narufaceAtlas.Height);
            if(isRasengan)
            spriteBatch.Draw(narufaceAtlas, drawFaceRectangle, Color.White);
            Rectangle drawFaceTopRectangle = new Rectangle(770 - narutopAtlas.Width, 30, narutopAtlas.Width, narutopAtlas.Height);
            spriteBatch.Draw(narutopAtlas, drawFaceTopRectangle, Color.White);

            Vector2 xy = new Vector2((int)location.X - sourceRectangle.Width / 2, (int)location.Y - sourceRectangle.Height / 2);
            //spriteBatch.Begin();
            //spriteBatch.Draw(atlas, drawRectangle, sourceRectangle, Color.White);
            spriteBatch.Draw(atlas, drawRectangle, sourceRectangle, Color.White, (float)0, new Vector2(0, 0), spriteEffects, 0f);
            spriteBatch.DrawString(font, healthString, new Vector2(600, 100), Color.White);
            spriteBatch.DrawString(font, powerString, new Vector2(600, 150), Color.White);
        }
    }
}
        #endregion