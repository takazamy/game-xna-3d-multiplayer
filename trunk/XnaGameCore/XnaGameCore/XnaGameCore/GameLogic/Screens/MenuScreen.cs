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
    public class MenuScreen : Screens
    {
        public ButtonComponent playBtn = null;
        public ButtonComponent ExitBtn = null;
        public ButtonComponent HelpBtn = null;
        public ButtonComponent LanBtn = null;
        public ButtonComponent HostBtn = null;
        public ButtonComponent CreditBtn = null;
        public MenuScreen(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
            this.state = States.ScreenState.GS_MENU;
            // TODO: Construct any child components here
            this.backGround = backGround;
           
           
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
          
           

        }
        private void HideButton() 
        {
            //playBtn.Visible = false;
            //playBtn.Enabled = false;
            
            //ExitBtn.Visible = false;
            //ExitBtn.Enabled = false;
            //HelpBtn.Visible = false;
            //HelpBtn.Enabled = false;
            //LanBtn.Visible = false;
            //LanBtn.Enabled = false;
            //CreditBtn.Visible = false;
            //CreditBtn.Enabled = false; 
        }
        protected override void LoadContent()
        {
           
            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (enable)
            {
                if (playBtn != null)
                {
                    playBtn.Update(gameTime);
                }
                if (ExitBtn != null)
                {
                    ExitBtn.Update(gameTime);
                }
                if (HelpBtn != null)
                {
                    HelpBtn.Update(gameTime);
                }
                if (LanBtn != null)
                {
                    LanBtn.Update(gameTime);
                }
                if (HostBtn != null)
                {
                    HostBtn.Update(gameTime);
                }
                if (CreditBtn != null)
                {
                    CreditBtn.Update(gameTime);
                }
            }
           
        }

        public override void Draw(GameTime gameTime)
        {
            //this.game.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Draw(backGround, Vector2.Zero, Color.White);
            if (playBtn != null)
            {
                playBtn.Draw(gameTime, this.spriteBatch);
            }
            if (ExitBtn != null)
            {
                ExitBtn.Draw(gameTime, this.spriteBatch);
            }
            if (HelpBtn != null)
            {
                HelpBtn.Draw(gameTime, this.spriteBatch);
            }
            if (LanBtn != null)
            {
                LanBtn.Draw(gameTime, this.spriteBatch);
            }
            if (HostBtn != null)
            {
                HostBtn.Draw(gameTime, this.spriteBatch);
            }
            if (CreditBtn != null)
            {
               CreditBtn.Draw(gameTime, this.spriteBatch);
            }
            this.enable = true;
        }
    }
}
