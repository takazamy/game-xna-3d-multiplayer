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
    public class Splash:SplashScreen
    {
        public Splash(ScreenManager scrManage, Game game, SpriteBatch spriteBatch)
            : base(scrManage, game, spriteBatch)
        {
            Initialize();
        }

        public override void Initialize()
        {
            Texture2D splashImage = new Texture2D(this.game.GraphicsDevice, 800, 600);
            splashImage = this.game.Content.Load<Texture2D>("SplashScreen");
            this.backGround = splashImage;
            base.Initialize();
        }
    }
}
