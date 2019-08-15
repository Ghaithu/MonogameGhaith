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
    class EntityManager
    {
        List<Entity> entities;
        List<List<string>> attributes, contents;
        FileManager fileManager;
        public List<Entity> Entities
        {
            get { return entities; }
        }

        public void LoadContent(string entityType, ContentManager Content, string fileName, string identifier,InputManager input)
        {

        }
    }
}
