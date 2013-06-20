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
using XnaGameCore.GameLogic.Screens;
using XnaGameCore.GameLogic.State;


namespace XnaGameCore.GameLogic.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class Screens 
    {
       
        public States.ScreenState state;       
        public ScreenManager scrManager;
        protected Texture2D backGround;
        protected SpriteBatch spriteBatch;
        protected Game game;
        public Screens(ScreenManager scrManager, Game game, SpriteBatch _spritebatch)            
        {
            spriteBatch = _spritebatch;
            this.scrManager = scrManager;
            this.game = game;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public abstract void Initialize();

        protected abstract void LoadContent();
        
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public abstract void Update(GameTime gameTime);


        public abstract void Draw(GameTime gameTime);
       


    }
}
