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


namespace XnaGameCore
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MouseComponent //: Microsoft.Xna.Framework.DrawableGameComponent
    {
      
        private Texture2D mouseImage;
        private Vector2 location = Vector2.Zero;
        private string name;
        private Game game;
        public MouseComponent(Game game,String name)           
        {
            this.name = name;
            this.game = game;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization code here
            
        }

        protected void LoadContent()
        {
            mouseImage = this.game.Content.Load<Texture2D>(name);           
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            MouseState state = Mouse.GetState();
            this.location.X = state.X - this.mouseImage.Width / 2;
            this.location.Y = state.Y - this.mouseImage.Height / 2;
           
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {           
            spriteBatch.Draw(this.mouseImage, this.location, Color.White);           
        }
    }
}
