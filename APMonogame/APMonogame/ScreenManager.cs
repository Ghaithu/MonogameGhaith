﻿using System;
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
    class ScreenManager
    {
        #region Variables
        GameScreen currentScreen;
        GameScreen newScreen;
        //creating custom content manager
        ContentManager content;

        //ScreenManager Instance
        private static ScreenManager instance;


        //ScreenStack
        Stack<GameScreen> screenStack = new Stack<GameScreen>();
        //screens width and height
        Vector2 dimensions;

        bool transition;
        //Animation animation = new Animation(); not working t38
        FadeAnimation fade = new FadeAnimation();
        Texture2D fadeTexture;
        Texture2D nullImage;
        Texture2D background;

        InputManager inputManager;

        #endregion

        #region properties
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }
        #endregion

        #region Main Methods
        public void AddScreen(GameScreen screen, InputManager inputManager)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.Alpha = 0.0f;
            fade.ActivateValue = 1.0f;
            this.inputManager = inputManager;


        }
        public void AddScreen(GameScreen screen, InputManager inputManager, float alpha)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.ActivateValue = 1.0f;
            if (alpha != 1.0f)
                fade.Alpha = 1.0f - alpha;
            else
                fade.Alpha = alpha;
            fade.Increase = true;
            this.inputManager = inputManager;

        }

        public Texture2D NullImage
        {
            get { return nullImage; }
        }

        //INITIALIZE
        public void Initialize()
        {

            currentScreen = new SplashScreen();
            fade = new FadeAnimation();
            inputManager = new InputManager();
            

        }
        //LOAD CONTENT
        public void LoadContent(ContentManager Content)
        {
           
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content, inputManager);
            background = Content.Load<Texture2D>("background");
            nullImage = this.content.Load<Texture2D>("null");
            fadeTexture = this.content.Load<Texture2D>("fade");
            fade.LoadContent(content, fadeTexture, "", Vector2.Zero);
            fade.Scale = dimensions.X;
        }

        //UPDATE
        public void Update(GameTime gameTime)
        {
            if (!transition)
                currentScreen.Update(gameTime);
            else
                Transition(gameTime);
        }

        //DRAW
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (transition)
                fade.Draw(spriteBatch);
        }

        public ContentManager Content
        {
            get { return content; }
        }
        #endregion

        #region Private Methods
        private void Transition(GameTime gameTime)
        {
            fade.Update(gameTime);
            if(fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                screenStack.Push(newScreen);
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent(content, this.inputManager);
            }
            else if(fade.Alpha == 0.0f)
            {
                transition = false;
                fade.IsActive = false ;
            }
        }
        #endregion
    }
}
