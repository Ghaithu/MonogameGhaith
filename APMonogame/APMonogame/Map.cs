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
    public class Map
    {
        public Layer layer;
        
        string id;
        public string ID
        {
            get { return id; }
            
        }
        public void LoadContent(ContentManager content, Map map,string mapID)
        {
            layer = new Layer();
          
            id = mapID;

            layer.LoadContent(map, "Layer1");
          

        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            layer.Update(gameTime, ref player);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            layer.Draw(spriteBatch);
        }
    }
}
