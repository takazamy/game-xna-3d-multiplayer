using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGameCore.GameLogic.Screens
{
    public class JoinScreen:Screens
    {
        public ButtonComponent joinButton;
        public ButtonComponent backButton;
        public ButtonComponent scanButton;
        public JoinScreen(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
        }
        public override void Initialize()
        {
           
        }

        protected override void LoadContent()
        {
            
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (joinButton != null)
            {
                joinButton.Update(gameTime);
            }
            if (scanButton != null)
            {
                scanButton.Update(gameTime);
            }
            if (backButton != null)
            {
                backButton.Update(gameTime);
            }
           
            
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (backGround != null)
            {
                spriteBatch.Draw(backGround, Vector2.Zero, Color.White);
            }
           
            if (scanButton != null)
            {
                scanButton.Draw(gameTime, spriteBatch);
            }
            if (joinButton != null)
            {
                joinButton.Draw(gameTime, spriteBatch);
            }
            if (backButton != null)
            {
                backButton.Draw(gameTime, spriteBatch);
            }
            this.enable = true;
        }
    }
}
