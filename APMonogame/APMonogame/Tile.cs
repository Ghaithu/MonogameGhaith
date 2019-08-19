using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace APMonogame
{
    public class Tile:GameplayScreen
    {
        #region Variables & Objects
        public enum State { Solid, Passive, Trap, Door, Key };
        public enum Motion { Static, Horizontal, Vertical };

        GameplayScreen mapChange = new GameplayScreen();
        State state;
        Motion motion;
        
        Vector2 position, prevPosition, velocity;
        Texture2D tileImage;
        DeathScreen deathScreen = new DeathScreen();
        MenuManager menu = new MenuManager();
        Animation animation;

        float range;
        int counter;
        static int keyCounter = 0;
        bool increase;
        float moveSpeed;
        bool onTile;
        int id = 0;
        bool isGrabbing;
        bool placeHolder;
        bool isTrapped;
        int oldKeys;
        
        public int KeyCounter
        {
            get { return keyCounter; }
            set { keyCounter = value; }
        }
        #endregion
        #region HelpMethods
        //to be explained
        private Texture2D CropImage(Texture2D tileSheet, Rectangle tileArea)
        {
            Texture2D croppedImage = new Texture2D(tileSheet.GraphicsDevice, tileArea.Width, tileArea.Height);

            Color[] tileSheetData = new Color[tileSheet.Width * tileSheet.Height];
            Color[] croppedImageData = new Color[croppedImage.Width * croppedImage.Height];

            tileSheet.GetData<Color>(tileSheetData);

            int index = 0;
            for (int y = 0; y < tileArea.Y + tileArea.Height; y++)
            {
                for (int x = tileArea.X; x < tileArea.X + tileArea.Width; x++)
                {
                    croppedImageData[index] = tileSheetData[y * tileSheet.Width + x];
                    index++;
                }
            }
            croppedImage.SetData<Color>(croppedImageData);
            return croppedImage;
        }
        //tobeexplained
        public void SetTile(State state, Motion motion, Vector2 position, Texture2D tileSheet, Rectangle tileArea)
        {
            
            this.state = state;
            this.motion = motion;
            this.position = position;
            increase = true;
            onTile = false;
            velocity = Vector2.Zero;
            tileImage = CropImage(tileSheet, tileArea);
            moveSpeed = 100f;
            range = 50;
            counter = 0;
            
            animation = new Animation();
            animation.LoadContent(ScreenManager.Instance.Content, tileImage, "", position);
            

            


        }
        //Key Tracker
        public void increment()
        {
            if (keyCounter != 2 && !mapChange.Map1End)
                keyCounter++;
            else if (keyCounter != 4 && !mapChange.Map2End)
                keyCounter++;
            else if (keyCounter != 6 && !mapChange.Map3End)
                keyCounter++;

            Console.WriteLine($"keys = {keyCounter}");
            Console.WriteLine($"old key = {oldKeys}");

        }

        public void prevState()
        {
            oldKeys = keyCounter;
        }
        #endregion
        #region Gameloop, Collision & Draw
        public void Update(GameTime gameTime, ref Player player)
        {
            id = 0;
            isGrabbing = false;
            placeHolder = false;
            isTrapped = false;
            
            counter++;
            prevPosition = position;
            if (counter >= range)
            {
                counter = 0;
                increase = !increase;
            }
            //controls the motion of the tiles that are uploaded with horizontal attribute
            if (motion == Motion.Horizontal)
            {
                if (increase)
                    velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            //controls the motion of the tiles that are uploaded with Vertical attribute
            else if (motion == Motion.Vertical)
            {
                if (increase)
                    velocity.Y = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    velocity.Y = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            position += velocity;
            animation.Position = position;

            //instance of the FloatRect class that defines the intersection of rectangles with each other (player & tiles)
            FloatRect rect = new FloatRect(position.X, position.Y, 40, 40);

            //triggered when Player is on the tile
            if (onTile)
            {
                
                if (!player.SyncTilePosition)
                {

                    player.Position += velocity;
                    player.SyncTilePosition = true;

                }
                

                if (player.Rect.Right < rect.Left || player.Rect.Left > rect.Right || player.Rect.Bottom != rect.Top)
                {
                    onTile = false;
                    player.ActiveateGravity = true;
                }
            }

            //Falling off map respawn
            if (player.Position.Y > 1400 && !map1End)
            {
                
                player.Position = new Vector2(50, 450);
                Console.WriteLine("you're under");
                if (keyCounter <= 0)
                    keyCounter = 0;

                player.PlayerLives--;
                Console.WriteLine($"Keys={keyCounter}, Lives={player.PlayerLives}");
                Console.WriteLine(deathScreen.IsLoaded);
                

            }
            else if(player.Position.Y > 1400 && map1End && !map2End || player.Position.Y > 1400 && map1End && map2End)
            {
                player.Position = new Vector2(45, 45);
                Console.WriteLine("you're under");
                player.PlayerLives--;
                if (keyCounter <= 0)
                    keyCounter = 0;
                Console.WriteLine($"Keys={keyCounter}");
            }

            //reset
            if(player.PlayerLives == 0)
            {
                mapChange.ID = 1;
                keyCounter = 0;
                mapChange.Map1End = false;
                mapChange.Map2End = false;
                mapChange.Map3End = false;
                mapChange.Loaded = true;


            }

            //Solid PLAYER COLLISION
            if (player.Rect.Intersects(rect) && state == State.Solid)
            {
                isGrabbing = false;
                placeHolder = false;
                FloatRect prevPlayer = new FloatRect(player.PrevPosition.X, player.PrevPosition.Y, player.Animation.FrameWidth, player.Animation.FrameHeight);

                FloatRect prevTile = new FloatRect(prevPosition.X, prevPosition.Y, Layer.TileDimensions.X, Layer.TileDimensions.Y);


                if (player.Rect.Bottom >= rect.Top && prevPlayer.Bottom <= prevTile.Top)
                {
                    player.Position = new Vector2(player.Position.X, position.Y - player.Animation.FrameHeight);
                    player.ActiveateGravity = false;
                    onTile = true;

                }
                else if (player.Rect.Top <= rect.Bottom && prevPlayer.Top >= prevTile.Bottom)

                {
                    player.Position = new Vector2(player.Position.X, position.Y + 40);
                    player.Velocity = new Vector2(player.Velocity.X, 0);
                    player.ActiveateGravity = true;
                }

                
                else
                {
                    player.Position -= player.Velocity;
                }
            }
            //Trap Collision
            else if (player.Rect.Intersects(rect) && state == State.Trap)
            {
                if (id != 1)
                {
                    player.PlayerLives--;
                    isTrapped = true;
                    id = 1;
                    if (isTrapped && !placeHolder)
                    {
                        if (!Map1End)
                            player.Position = new Vector2(50, 450);
                        else
                            player.Position = new Vector2(45, 45);
                    }
                    placeHolder = true;
                    Console.WriteLine($"You dead and have {keyCounter} keys.");
                    if (keyCounter <= 0)
                        keyCounter = 0;
                }

            }
            //Key Collision
            else if (player.Rect.Intersects(rect) && state == State.Key)
            {

                tileImage.Dispose();
                position = Vector2.Zero;
                isGrabbing = true;
                if (isGrabbing && !placeHolder)
                {
                    increment();
                }
                placeHolder = true;


            }
            //Door Collision
            else if (player.Rect.Intersects(rect) && state == State.Door && keyCounter == 2)
            {
                if (!mapChange.Map1End)
                { 
                    mapChange.ID++;
                    mapChange.Loaded = false;
                    mapChange.Map1End = true;
                    player.Position = new Vector2(45, 45);
                    if (keyCounter <= 0)
                      keyCounter = 0;
                }
            }
            else if(player.Rect.Intersects(rect) && state == State.Door && keyCounter == 4)
            {
                if (!mapChange.Map2End)
                {
                    mapChange.ID++;
                    mapChange.Loaded = false;
                    mapChange.Map2End = true;
                    player.Position = new Vector2(45, 45);
                    if (keyCounter <= 0)
                        keyCounter = 0;
                }
            }
            else if (player.Rect.Intersects(rect) && state == State.Door && keyCounter == 6)
            {
                if (!mapChange.Map3End)
                {
                    mapChange.ID++;
                    mapChange.Loaded = false;
                    mapChange.Map3End = true;
                    player.Position = new Vector2(45, 45);
                    if (keyCounter <= 0)
                        keyCounter = 0;
                }
            }



            player.Animation.Position = player.Position;
       
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
        #endregion
    }
}
