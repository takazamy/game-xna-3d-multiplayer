using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaGameCore.GameLogic.Screens;
using XnaGameCore.GameLogic.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameProject.Core
{
    public class ScreenGameManager:ScreenManager
    {
        public ScreenGameManager(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            Initialize();
        }

        public override void Initialize()
        {
            this.Append(States.ScreenState.GS_SPLASH_SCREEN,new Splash(this,this.game,this.spriteBatch));
            this.Append(States.ScreenState.GS_MENU, new Menu(this,this.game,this.spriteBatch));
            base.Initialize();
        }
    }
}
