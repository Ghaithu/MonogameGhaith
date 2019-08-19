using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace APMonogame
{
    public class MenuManager
    {
        #region Variables

        //items die via de externe file zullen upgeload worden
        List<string> menuItems;
        //Animatie, link type en link ID die ook via de externe file worden upgeload
        List<string> animationTypes,linkType, linkID;
        List<Texture2D> menuImages;
        List<Animation> tempAnimation;
        List<List<Animation>> animation;
        List<List<string>> attributes, contents;
        Game1 game;

        ContentManager content;   
        FileManager fileManager;
        Player player;
        DeathScreen deathScreen;



        int itemNumber;
        //int axis;
        string align = "";
        int screen;

        public int Screen
        {
            get { return screen; }
            set { screen = value; }
        }

        Vector2 position;        
        SpriteFont font;

        #endregion
        #region Private Methods

        //hier gebruik ik null images voor reference, de bedoeling was om extra images in de menu te zetten
        private void SetMenuItems()
        {
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (menuImages.Count == i)
                    menuImages.Add(ScreenManager.Instance.NullImage);

            }

            for(int i = 0; i < menuImages.Count; i++)
            {
                if (menuItems.Count == i)
                    menuItems.Add("");
            }
        }
        private void SetAnimations()
        {
            Vector2 dimensions = Vector2.Zero;
            Vector2 pos = Vector2.Zero;
            //centreren van de menu items
            if (align.Contains("Center"))
            {
                //berekent de totale breedte en lengte van alle menu items samen
                for (int i = 0; i < menuItems.Count; i++)
                {
                    dimensions.X += font.MeasureString(menuItems[i]).X + menuImages[i].Width;
                    dimensions.Y += font.MeasureString(menuItems[i]).Y + menuImages[i].Height;
                }
                
                //aftrekken van de dimenties van de menu items van de originele scherm lengte om een start positie te hebben
                pos.Y = (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2;
                
            }
            else
            {
                pos = position;
            }
            tempAnimation = new List<Animation>();

            //het inladen van fade animatie voor de menu items
            for (int i = 0; i < menuImages.Count; i++)
            {
                dimensions = new Vector2(font.MeasureString(menuItems[i]).X + menuImages[i].Width, font.MeasureString(menuItems[i]).Y + menuImages[i].Height);

                
                pos.X = (ScreenManager.Instance.Dimensions.X - dimensions.X) / 2;

                for (int j = 0; j < animationTypes.Count; j++)
                {
                    switch (animationTypes[j])
                    {
                        case "Fade":
                            tempAnimation.Add(new FadeAnimation());
                            tempAnimation[tempAnimation.Count - 1].LoadContent(content, menuImages[i], menuItems[i], pos);
                            tempAnimation[tempAnimation.Count - 1].Font = font;
                            break;
                    }

                }

                if (tempAnimation.Count > 0)
                    animation.Add(tempAnimation);
                tempAnimation = new List<Animation>();

                
                    pos.Y += dimensions.Y;

            }
        }

        #endregion
        #region Main Methods

        
        public void LoadContent(ContentManager content, string id)
        {
            //variable instantiation
            this.content = new ContentManager(content.ServiceProvider, "Content");
            menuItems = new List<string>();
            menuImages = new List<Texture2D>();
            animationTypes = new List<string>();
            animation = new List<List<Animation>>();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            linkType = new List<string>();
            linkID = new List<string>();
            player = new Player();
            deathScreen = new DeathScreen();
            itemNumber = 0;
            screen = 1;
            position = Vector2.Zero;
            fileManager = new FileManager();
            game = new Game1();
            //Het laden van attributes en contents van de menu
            fileManager.LoadContent($"Load/Menus{screen}.vke", attributes, contents);

            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Font":
                            font = this.content.Load<SpriteFont>(contents[i][j]);
                            break;
                        case "Item":
                            menuItems.Add(contents[i][j]);
                            break;
                        case "Image":
                            menuImages.Add(this.content.Load<Texture2D>(contents[i][j]));
                            break;
                        case "Position":
                            string[] temp = contents[i][j].Split(' ');
                            position = new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
                            break;
                        case "Animation":
                            animationTypes.Add(contents[i][j]);
                            break;
                        case "Align":
                            align = contents[i][j];
                            break;
                        case "LinkType":
                            linkType.Add(contents[i][j]);
                            break;
                        case "LinkID":
                            linkID.Add(contents[i][j]);
                            break;
                    }
                }
            }

            SetMenuItems();
            SetAnimations();
            
        }

        public void UnloadContent()
        {
            content.Unload();
            fileManager = null;
            position = Vector2.Zero;
            animation.Clear();
            menuItems.Clear();
            menuImages.Clear();
            animationTypes.Clear();
        }

        public void Update(GameTime gameTime, InputManager inputManager)
        {
            //Menu input
                if (inputManager.KeyPressed(Keys.Down, Keys.S))
                    itemNumber++;
                else if (inputManager.KeyPressed(Keys.Up, Keys.W))
                    itemNumber--;
            
            //Keuze van menu items
            if (inputManager.KeyPressed(Keys.Enter, Keys.Z))
            {
                if (linkType[itemNumber] == "Screen")
                {
                    Type newClass = Type.GetType("APMonogame." + linkID[itemNumber]);
                    ScreenManager.Instance.AddScreen((GameScreen)Activator.CreateInstance(newClass), inputManager);
                }
                
                    
            }

            //check voor itemnumbers nooit onder de 0 of hoger dan aantal itemnumbers
            if (itemNumber < 0)
                itemNumber = 0;
            else if (itemNumber > menuItems.Count - 1)
                itemNumber = menuItems.Count - 1;
            //text animate toevoegen
            for(int i = 0; i < animation.Count; i++)
            {
                for (int j = 0; j < animation[i].Count; j++)
                {
                    if (itemNumber == i)
                        animation[i][j].IsActive = true;
                    else
                        animation[i][j].IsActive = false;

                    animation[i][j].Update(gameTime);

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < animation.Count; i++)
            {
                for (int j = 0; j < animation[i].Count; j++)
                {
                    animation[i][j].Draw(spriteBatch);
                }
                
                
                    
                
            }
        }

        #endregion
    }
}
