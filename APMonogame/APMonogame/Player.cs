using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace APMonogame
{
    public class Player:Entity
    {
        float jumpSpeed = 2400f;
        int playerLives = 5;
        SpriteFont font;
        Tile keyTile;
        public FloatRect Rect
        {
            get { return new FloatRect(position.X, position.Y, moveAnimation.FrameWidth, moveAnimation.FrameHeight); }

        }

        public int PlayerLives
        {
            get { return playerLives; }
            set { playerLives = value; }
        }

        public override void LoadContent(ContentManager content, InputManager inputManager)
        {
            base.LoadContent(content, inputManager);
            fileManager = new FileManager(); 
            moveAnimation = new SpriteSheetAnimation();
            Vector2 tempFrames = Vector2.Zero;
            keyTile = new Tile();
            moveSpeed = 350f;
            font = content.Load<SpriteFont>("MenuFont");


            fileManager.LoadContent("Load/Player.vke", attributes, contents);
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Image":
                            image = this.content.Load<Texture2D>(contents[i][j]);
                            break;
                        case "Position":
                            string[] frames = contents[i][j].Split(' ');
                            position = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                            break;
                    }
                }
            }

            gravity = 200f;
            velocity = Vector2.Zero;
            syncTilePosition = false;
            activateGravity = true;
            moveAnimation.Frames = new Vector2(3, 4);
            moveAnimation.LoadContent(content, image, "", position);
           
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            moveAnimation.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager input, Layer layer)
        {
            syncTilePosition = false;
            prevPosition = position;
            moveAnimation.IsActive = true;
            if (input.KeyDown(Keys.Right, Keys.D))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0);
                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
                
            else if (input.KeyDown(Keys.Left, Keys.A))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 4);
                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                moveAnimation.IsActive = false;
                velocity.X = 0;

            }

            if (input.KeyDown(Keys.Space)&& !activateGravity)
            {
                
                velocity.Y = -jumpSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                activateGravity = true;
            }
            

            if (activateGravity)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y=0;

            position += velocity;

            moveAnimation.Position = position;
            moveAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
            spriteBatch.DrawString(font, $"Lives:{playerLives}", new Vector2(0, 50), Color.White);
            spriteBatch.DrawString(font, $"Keys:{keyTile.KeyCounter}", new Vector2(0, 90), Color.White);

        }
    }
}
