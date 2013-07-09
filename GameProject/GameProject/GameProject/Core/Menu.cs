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
        private Game1 game1;
        public Menu(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
            Initialize();
        }

        public override void Initialize()
        {
            game1 = (Game1)game;
            Texture2D menuImage = new Texture2D(this.game.GraphicsDevice, 800, 600);
            menuImage = this.game.Content.Load<Texture2D>("menuscreen");
            this.backGround = menuImage;

            this.playBtn = new ButtonComponent(this.game, "buttonImg", new Vector2(100, 100), 150, 50, "Play");
            //this.HostBtn = new ButtonComponent(this.game, "buttonImg", new Vector2(100, 100), 150, 50, "Create Game");
            //this.LanBtn = new ButtonComponent(this.game, "buttonImg", new Vector2(270, 100), 150, 50, "Join Game");
            this.ExitBtn = new ButtonComponent(this.game, "buttonImg", new Vector2(440, 100), 150, 50, "Exit");
            addButtonHandler();
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            //HostBtn.Update(gameTime);
            //LanBtn.Update(gameTime);
            playBtn.Update(gameTime);
            ExitBtn.Update(gameTime);
            base.Update(gameTime);
        }
        private void addButtonHandler()
        {
            //HostBtn.OnMouseDown = delegate() { hostButtonHandler(); };
            //LanBtn.OnMouseDown = delegate() { lanButtonHandler(); };
            playBtn.OnMouseDown = delegate() { playButtonHandler(); };
            ExitBtn.OnMouseDown = delegate() { exitButtonHandler(); };
        }

        private void playButtonHandler()
        {
            game1.client.Connect(game1.address, game1.port);
            //scrManager.PlayScreen(States.ScreenState.GS_HOST);
        }

        

        private void exitButtonHandler()
        {
            this.game.Exit();
            //scrManager.PlayScreen(States.ScreenState.GS_MAIN_GAME);
        }
    }
}
