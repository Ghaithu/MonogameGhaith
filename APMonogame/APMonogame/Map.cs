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
        //public Collision collision;
        string id;
        public string ID
        {
            get { return id; }
            
        }
        public void LoadContent(ContentManager content, Map map,string mapID)
        {
            layer = new Layer();
            //collision = new Collision();
            id = mapID;

            layer.LoadContent(map, "Layer1");
            //collision.LoadContent(content, mapID);

        }

        public void UnloadContent()
        {
            //layer.UnloadContent();
            //collision.UnloadContent();
        }

        public void Update(GameTime gameTime, ref Player player/*, ref Enemy enemy*/)
        {
            layer.Update(gameTime, ref player/*, ref enemy*/);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            layer.Draw(spriteBatch);
        }
    }
}
