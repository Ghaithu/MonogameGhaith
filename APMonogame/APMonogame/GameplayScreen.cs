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
        Enemy enemy;
        Map map;
        public static int id = 1;
        public static bool loaded;
        public static bool map1End;
        public static bool map2End;
        public static bool map3End;
        bool random;
        
        

        public override void LoadContent(ContentManager content, InputManager inputManager)
        {
            base.LoadContent(content, inputManager);
            player = new Player();
            enemy = new Enemy();
            map = new Map();
            player.LoadContent(content, inputManager);
            map.LoadContent(content,map, $"Map{id}");
            loaded = true;

            


            if (random)
                loaded = true;

            





        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            enemy.UnloadContent();
             map.UnloadContent();  
        }

        public override void Update(GameTime gameTime)
        {
            //Console.WriteLine(map1End);
            inputManager.Update();
            player.Update(gameTime, inputManager, map.layer);
            enemy.Update(gameTime, inputManager, map.layer);

            if (!loaded && map1End)
            {
                loaded = true;               
                map.LoadContent(content, map, $"Map{id}");

            }
            map.Update(gameTime, ref player/*, ref enemy*/);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
        }
    }
}
