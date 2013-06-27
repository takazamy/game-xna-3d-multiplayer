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
            createButton.Update(gameTime);
            backButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(backGround, Vector2.Zero, Color.White);
            if (createButton != null)
            {
                createButton.Draw(gameTime, spriteBatch);
            }
            if (backButton != null)
            {
                backButton.Draw(gameTime, spriteBatch);
            }
            
        }
    }
}
