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
    class GameWin:GameScreen
    {
        SpriteFont font;
        MenuManager menu;
        Player player;
        Tile tile;
        Texture2D youWin;
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
                font = this.content.Load<SpriteFont>("MenuFont");
            menu = new MenuManager();
            tile = new Tile();
            player = new Player();
            menu.LoadContent(content, "Death");

            youWin = content.Load<Texture2D>("TestGameWin");

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
            
            spriteBatch.Draw(youWin, new Rectangle(0, 0, 1280, 720), Color.White);
            spriteBatch.DrawString(font, $"You finished with {tile.PrevLives} lives remaining!", new Vector2(450, 630), Color.White);
            menu.Draw(spriteBatch);
        }
    }
}
