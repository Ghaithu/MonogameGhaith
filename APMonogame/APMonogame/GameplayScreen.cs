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
    public class GameplayScreen:GameScreen
    {
        Player player;
        Map map;
        DeathScreen deathScreen;
        public static int id = 1;
        public static bool loaded;
        public static bool map1End;
        public static bool map2End;
        public static bool map3End;
        Texture2D background;
        bool random;
        public bool Loaded
        {
            get { return loaded; }
            set { loaded = value; }
        }
        public bool Map1End
        {
            get { return map1End; }
            set { map1End = value; }

        }
        public bool Map2End
        {
            get { return map2End; }
            set { map2End = value; }
        }
        public bool Map3End
        {
            get { return map2End; }
            set { map2End = value; }
        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }



        public override void LoadContent(ContentManager content, InputManager inputManager)
        {
            base.LoadContent(content, inputManager);
            player = new Player();
            map = new Map();
            deathScreen = new DeathScreen();
            player.LoadContent(content, inputManager);
            map.LoadContent(content,map, $"Map{id}");
            loaded = true;
            background = content.Load<Texture2D>("background");
            if (random)
                loaded = true;

        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            map.UnloadContent();  
        }

        public override void Update(GameTime gameTime)
        {
            
            inputManager.Update();
            player.Update(gameTime, inputManager, map.layer);

            if (!loaded && map1End || !loaded && map2End || !loaded && map3End)
            {
                loaded = true;               
                map.LoadContent(content, map, $"Map{id}");

            }

            if(player.PlayerLives == 0)
            {

                ScreenManager.Instance.AddScreen(new DeathScreen(), inputManager);
                

            }

            map.Update(gameTime, ref player);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(background, new Rectangle(0, 0, 1280, 720), Color.White);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
