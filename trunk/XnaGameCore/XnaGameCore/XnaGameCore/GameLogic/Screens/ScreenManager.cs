using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XnaGameCore.GameLogic.State;


namespace XnaGameCore.GameLogic.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScreenManager
    {

        #region Properties
        public Dictionary<States.ScreenState, Screens> screenList;
        public Game game;
        public SpriteBatch spriteBatch;
        
        public Screens currentScreen;

        public States.ScreenState state = States.ScreenState.GS_SPLASH_SCREEN;

        private int curIndex = -1;
        #endregion

        public ScreenManager(Game game, SpriteBatch spriteBatch)            
        {
            screenList = new Dictionary<States.ScreenState, Screens>();
            this.game = game;
            this.spriteBatch = spriteBatch;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public virtual void Initialize()
        {
            // TODO: Add your initialization code here           
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            currentScreen.Update(gameTime);           
        }

        public void Append(States.ScreenState keys, Screens screen)
        {
            screenList[keys] = screen;       
        }

        public void PlayScreen(States.ScreenState key)
        {
            if (currentScreen != null)
            {
                currentScreen.enable = false;
            }
           
            currentScreen = screenList[key];            
            state = key;       
        }

        public Screens GetScreensByState(States.ScreenState key)
        {
            Screens screen = screenList[key];
            return screen;
        }
        
    }
}
