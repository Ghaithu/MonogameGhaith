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
    public class SpriteSheetAnimation : Animation
    {

        int frameCounter;
        int switchFrame;
        Vector2 frames;
        Vector2 currentFrame;

        public Texture2D Image
        {
            get { return image; }
        }
        public Rectangle SourceRect
        {
            set { sourceRect = value; }
        }
        public Vector2 Frames
        {
            set { frames = value; }

        }

        public Vector2 CurrentFrame
        {
            set { currentFrame = value; }
            get { return currentFrame; }
        }

        public int FrameWidth
        {
            get { return image.Width / (int)frames.X; }
        }

        public int FrameHeight
        {
            get { return image.Height / (int)frames.Y; }
        }

        public SpriteSheetAnimation()
        {
            frameCounter = 0;
            switchFrame = 100;
        }


        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            base.LoadContent(Content, image, text, position);
            frameCounter = 0;
            switchFrame = 100;
            frames = new Vector2(3, 4);
            currentFrame = new Vector2(0, 0);
            sourceRect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight, FrameWidth , FrameHeight);
        }

        public override void Update(GameTime gameTime)
        {
            
            if (isActive)
            {
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                //switcht frames
                if (frameCounter >= switchFrame)
                {
                    frameCounter = 0;
                    currentFrame.X++;
                    //scrollt door mijn spritesheet en als het het einde van mijn image tegen komt dan restart het opnieuw
                    if (currentFrame.X * FrameWidth >= image.Width)
                        currentFrame.X = 0;


                }

            }
            else
            {
                frameCounter = 0;
                currentFrame.X = 1;
            }
            
            
            sourceRect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight, FrameWidth, FrameHeight);

        }


    }
}
