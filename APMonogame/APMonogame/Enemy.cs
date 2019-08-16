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
