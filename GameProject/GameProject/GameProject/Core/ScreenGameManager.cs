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
        public GameManager gameManager;
        public ScreenGameManager(Game game, SpriteBatch spriteBatch, GameManager manager)
            : base(game, spriteBatch)
        {
            gameManager = manager;           
        }

        public override void Initialize()
        {
            this.Append(States.ScreenState.GS_SPLASH_SCREEN, new Splash(this, this.game, this.spriteBatch, gameManager));
            this.Append(States.ScreenState.GS_MENU, new Menu(this, this.game, this.spriteBatch, gameManager));
            this.Append(States.ScreenState.GS_HOST, new Host(this, this.game, this.spriteBatch, gameManager));
            this.Append(States.ScreenState.GS_MAIN_GAME, new MainGame(this, this.game, this.spriteBatch, gameManager));

            this.Append(States.ScreenState.GS_JOIN, new Join(this, this.game, this.spriteBatch,this.gameManager));
            base.Initialize();
           
        }

    }
}
