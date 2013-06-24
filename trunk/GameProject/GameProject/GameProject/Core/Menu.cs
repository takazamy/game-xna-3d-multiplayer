using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaGameCore.GameLogic.Screens;
using XnaGameCore.GameLogic.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaGameCore;
namespace GameProject.Core
{
    public class Menu:MenuScreen
    {
        public Menu(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
            Initialize();
        }

        public override void Initialize()
        {
            Texture2D menuImage = new Texture2D(this.game.GraphicsDevice, 800, 600);
            menuImage = this.game.Content.Load<Texture2D>("menuscreen");
            this.backGround = menuImage;

            //this.playBtn = new ButtonComponent(this.game
            base.Initialize();
        }
    }
}
