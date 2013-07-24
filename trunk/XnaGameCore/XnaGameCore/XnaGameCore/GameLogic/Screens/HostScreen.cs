using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaGameCore.GameLogic.Screens
{
    public class HostScreen:Screens
    {
        public ButtonComponent createButton;
        public ButtonComponent joinButton;
        public ButtonComponent backButton;
        public HostScreen(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
           
        }

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
           
            if (enable)
            {
                if (createButton != null)
                {
                    createButton.Update(gameTime);
                }
                if (backButton != null)
                {
                    backButton.Update(gameTime);
                }
                if (joinButton != null)
                {
                    joinButton.Update(gameTime);
                }
            }
           
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backGround, Vector2.Zero, Color.White*0.5f);
            spriteBatch.End();

            if (createButton != null)
            {
                createButton.Draw(gameTime, spriteBatch);
            }
            if (backButton != null)
            {
                backButton.Draw(gameTime, spriteBatch);
            }
            if (joinButton != null)
            {
                joinButton.Draw(gameTime, spriteBatch);
            }
            this.enable = true;

        }

        protected override void LoadContent()
        {

        }
    }
}
