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
    public class Layer
    {
        List<List<Tile>> tiles;
        List<List<string>> attributes, contents;
        List<string> motion, solid, trap,key, door1, door2, door3;
        FileManager fileManager;
        ContentManager content;
        Texture2D tileSheet;
        string[] getMotion;
        public static Vector2 TileDimensions
        {
            get { return new Vector2(40, 40); }

        }
        public void LoadContent(Map map, string layerID)
        {
            tiles = new List<List<Tile>>();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
            fileManager = new FileManager();
            motion = new List<string>();
            solid = new List<string>();
            trap = new List<string>();
            door1 = new List<string>();
            key = new List<string>();
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            fileManager.LoadContent($"Load/{map.ID}.vke", attributes, contents);
            int indexY = 0;
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "TileSet":
                            tileSheet = content.Load<Texture2D>(contents[i][j]);
                            break;
                        case "Key":
                            key.Add(contents[i][j]);
                            break;
                        case "Trap":
                            trap.Add(contents[i][j]);
                            break;
                        case "Door1":
                            door1.Add(contents[i][j]);
                            break;
                        case "Solid":
                            solid.Add(contents[i][j]);
                            break;                       
                        case "Motion":
                            motion.Add(contents[i][j]);
                            break;
                        
                        case "StartLayer":
                            List<Tile> tempTiles = new List<Tile>();
                            Tile.Motion tempMotion = Tile.Motion.Static;
                            Tile.State tempState;

                            for (int k = 0; k < contents[i].Count; k++)
                            {
                                string[] split = contents[i][k].Split(',');
                                tempTiles.Add(new Tile());



                                if (solid.Contains(contents[i][k]))
                                    tempState = Tile.State.Solid;
                                else if (trap.Contains(contents[i][k]))
                                    tempState = Tile.State.Trap;
                                else if (door1.Contains(contents[i][k]))
                                    tempState = Tile.State.Door;
                                else if (key.Contains(contents[i][k]))
                                    tempState = Tile.State.Key;

                                else
                                    tempState = Tile.State.Passive;


                                foreach(string m in motion)
                                {
                                    getMotion = m.Split(':');                              
                                    if (getMotion[0] == contents[i][k])
                                    {
                                        
                                        tempMotion = (Tile.Motion)Enum.Parse(typeof(Tile.Motion), getMotion[1]);
                                        break;
                                    }
                                         
                                }

                                tempTiles[k].SetTile(tempState, tempMotion, new Vector2(k * 40, indexY * 40), tileSheet,
                                    new Rectangle(int.Parse(split[0]) * 40, int.Parse(split[1]) * 40, 40, 40));
                            }
                            tiles.Add(tempTiles);
                            indexY++;
                            break;
                       
                    }
                }
            }
        }

        public void Update(GameTime gameTime,ref Player player)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                for (int j = 0; j < tiles[i].Count; j++)
                {
                    tiles[i][j].Update(gameTime, ref player);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                for (int j = 0; j < tiles[i].Count; j++)
                {
                    tiles[i][j].Draw(spriteBatch);
                }
            }
        }
    }
}
