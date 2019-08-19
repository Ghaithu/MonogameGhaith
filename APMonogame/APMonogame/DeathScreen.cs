using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace APMonogame
{
    public class DeathScreen:GameScreen
    {
        SpriteFont font;
        MenuManager menu;
        Player player;
        Texture2D urded;
        bool isLoaded;
        public bool IsLoaded
        {
            get { return isLoaded; }
            set { isLoaded = value; }
        }
        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            isLoaded = true;
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("Font1");
            menu = new MenuManager();
            player = new Player();
            menu.LoadContent(content, "Death");
            urded = content.Load<Texture2D>("Gameover");

        }
        public override void UnloadContent()
        {
            
            base.UnloadContent();
            menu.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
           
            inputManager.Update();
            menu.Update(gameTime, inputManager);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(urded, new Rectangle(0, 0, 1280, 720), Color.White);

            menu.Draw(spriteBatch);
        }
    }
}
