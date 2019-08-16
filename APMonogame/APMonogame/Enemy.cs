using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace APMonogame
{
    public class Enemy:Entity
    {
        public FloatRect Rect
        {
            get { return new FloatRect(position.X, position.Y, moveAnimation.FrameWidth, moveAnimation.FrameHeight); }

        }
        public override void LoadContent(ContentManager content, InputManager inputManager)
        {
            base.LoadContent(content, inputManager);
            //fileManager = new FileManager();
            //moveAnimation = new SpriteSheetAnimation();
            //Vector2 tempFrames = Vector2.Zero;
            //moveSpeed = 100f;

            //fileManager.LoadContent("Load/Enemy.vke", attributes, contents);
            //for (int i = 0; i < attributes.Count; i++)
            //{
            //    for (int j = 0; j < attributes[i].Count; j++)
            //    {
            //        switch (attributes[i][j])
            //        {
            //            case "Image":
            //                image = this.content.Load<Texture2D>(contents[i][j]);
            //                break;
            //            case "Position":
            //                string[] frames = contents[i][j].Split(' ');
            //                position = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
            //                break;
            //        }
            //    }
            //}

            //gravity = 200f;
            //velocity = Vector2.Zero;
            //syncTilePosition = false;
            //activateGravity = true;
            //moveAnimation.Frames = new Vector2(3, 4);
            //moveAnimation.LoadContent(content, image, "", position);

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            //moveAnimation.UnloadContent();
        }



        public override void Update(GameTime gameTime, InputManager input/*, Collision col*/, Layer layer)
        {
            syncTilePosition = false;
            prevPosition = position;

            if (activateGravity)
                velocity.Y += gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                velocity.Y = 0;

            //moveAnimation.Position = position;
            //moveAnimation.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //moveAnimation.Draw(spriteBatch);

        }
    }
}
