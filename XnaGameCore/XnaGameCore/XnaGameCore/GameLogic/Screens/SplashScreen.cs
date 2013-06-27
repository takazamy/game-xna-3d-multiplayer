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
    public class SplashScreen : Screens
    {


        public SplashScreen(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
            this.state = States.ScreenState.GS_SPLASH_SCREEN;           
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
           
        }

        protected override void LoadContent()
        {
           // backGround = this.Game.Content.Load<Texture2D>("SplashScreen");
            
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed)   
            {
                scrManager.PlayScreen(States.ScreenState.GS_MENU); 
            }
           
            
        }

        public override void Draw(GameTime gameTime)
        {           
            spriteBatch.Draw(backGround, Vector2.Zero, Color.White);           
        }

    }
}
