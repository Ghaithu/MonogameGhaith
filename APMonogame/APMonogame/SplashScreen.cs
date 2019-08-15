﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace APMonogame
{
    public class SplashScreen : GameScreen
    {
        SpriteFont font;
        List<FadeAnimation> fade;
        List<Texture2D> images;         
        FileManager fileManager;

        int imageNumber;

        //FadeAnimation FAnimation; not working t38

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("Font1");
            imageNumber = 0;
            fileManager = new FileManager();
            fade = new List<FadeAnimation>();
            //FAnimation = new FadeAnimation(); not working t38
            images = new List<Texture2D>();

            fileManager.LoadContent("Load/Splash.vke", attributes, contents);
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Image":
                            images.Add(this.content.Load<Texture2D>(contents[i][j]));
                            fade.Add(new FadeAnimation());
                            break;
                    }
                }
            }
            for (int i = 0; i < attributes.Count; i++)
            {
                fade[i].LoadContent(content, images[i],"", new Vector2(-230,-70));
                fade[i].Scale = 1.2f;
                fade[i].IsActive = true;
            }
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            
            
            inputManager.Update();

            

            fade[imageNumber].Update(gameTime);
            


            if (fade[imageNumber].Alpha == 0.0f)
                imageNumber++;
            if(imageNumber >= fade.Count - 1 || inputManager.KeyPressed(Keys.Z))
            {
                ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
            }

            Debug.WriteLine(imageNumber);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fade[imageNumber].Draw(spriteBatch);
        }
        
    }
}
