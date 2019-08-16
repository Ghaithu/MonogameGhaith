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
        public enum State { Solid, Passive, Trap, Door, Key };
        public enum Motion { Static, Horizontal, Vertical };

        GameplayScreen mapChange = new GameplayScreen();
        State state;
        Motion motion;
        
        Vector2 position, prevPosition, velocity;
        Texture2D tileImage;

        float range;
        int counter;
        int keyCounter = 0;
        bool increase;
        float moveSpeed;
        bool onTile;
        int id = 0;

        Animation animation;

        public void LoadContent(ContentManager content)
        {
            
        }
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

        public void Update(GameTime gameTime, ref Player player/*, ref Enemy enemy*/)
        {
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
                //
                if (!player.SyncTilePosition)
                {

                    player.Position += velocity;
                    player.SyncTilePosition = true;

                }
                //if (!enemy.SyncTilePosition)
                //{
                //    enemy.Position += velocity;
                //    enemy.SyncTilePosition = true;
                //}

                //if (enemy.Rect.Right < rect.Left || enemy.Rect.Left > rect.Right || enemy.Rect.Bottom != rect.Top)
                //{
                //    onTile = false;
                //    enemy.ActiveateGravity = true;
                //}

                if (player.Rect.Right < rect.Left || player.Rect.Left > rect.Right || player.Rect.Bottom != rect.Top)
                {
                    onTile = false;
                    player.ActiveateGravity = true;
                }




            }


            if (player.Position.Y > 1400)
            {
                player.Position = new Vector2(0, 0);
                Console.WriteLine("you're udner");




            }

            //if (player.Position.X >= 1181 && player.Position.Y >= 111)
            //{
            //    GameplayScreen.id++;
            //    GameplayScreen.loaded = false;
            //    GameplayScreen.map1End = true;
            //    player.Position = new Vector2(0, 0);

            //}

            //PLAYER COLLISION
            if (player.Rect.Intersects(rect) && state == State.Solid)
            {
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
            
            else if (player.Rect.Intersects(rect) && state == State.Trap)
            {
                player.Position = new Vector2(0, 0);
                Console.WriteLine("You dead");

            }

            else if (player.Rect.Intersects(rect) && state == State.Key)
            {
                tileImage.Dispose();
                keyCounter++;

            }
            else if(player.Rect.Intersects(rect) && state == State.Door && keyCounter == 2)
            {
                GameplayScreen.id++;
                GameplayScreen.loaded = false;
                GameplayScreen.map1End = true;
                player.Position = new Vector2(0, 0);
            }

            
            player.Animation.Position = player.Position;
            //Console.WriteLine($"Current keys:{keyCounter}");
            
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch);
        }
    }
}
