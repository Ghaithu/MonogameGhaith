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
    public class Entity
    {
        protected int health;
        protected SpriteSheetAnimation moveAnimation;
        protected float moveSpeed;

        protected ContentManager content;
        protected FileManager fileManager;

        protected Texture2D image;

        protected List<List<string>> attributes, contents;

        protected Vector2 position;
        protected float gravity;
        protected Vector2 velocity;
        protected Vector2 prevPosition;
        protected bool activateGravity;
        protected bool syncTilePosition;

        public Vector2 PrevPosition
        {
            get { return prevPosition; }
        }

        public SpriteSheetAnimation Animation
        {
            get { return moveAnimation; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool ActiveateGravity
        {
            set { activateGravity = value; }
        }

        public bool SyncTilePosition
        {
            get { return syncTilePosition; }
            set { syncTilePosition = value; }
        }

        public virtual void LoadContent(ContentManager content, InputManager inputManager)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            attributes = new List<List<string>>();
            contents = new List<List<string>>();

        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime, InputManager inputManager/*, Collision col*/, Layer layer)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
