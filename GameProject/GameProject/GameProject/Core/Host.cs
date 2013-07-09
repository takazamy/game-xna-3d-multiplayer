using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaGameCore.GameLogic.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaGameCore.GameLogic.State;
using XnaGameCore;

namespace GameProject.Core
{
    public class Host:HostScreen
    {
        private Game1 game1;
        public Host(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
            Initialize();
        }

        public override void Initialize()
        {
            this.game1 = (Game1)game;
            Texture2D menuImage = new Texture2D(this.game.GraphicsDevice, 800, 600);
            menuImage = this.game.Content.Load<Texture2D>("hostImg");
            this.backGround = menuImage;
            createButton = new ButtonComponent(this.game, "buttonImg", new Vector2(100, 100), 150, 50, "Create");
            joinButton = new ButtonComponent(this.game, "buttonImg", new Vector2(100, 180), 150, 50, "Join");
            backButton = new ButtonComponent(this.game, "buttonImg", new Vector2(100, 250), 150, 50, "Back");
            addButtonHandler();

            base.Initialize();
        }
        private void addButtonHandler()
        {
            createButton.OnMouseDown = delegate() { createButtonHandler(); };
            backButton.OnMouseDown = delegate() { backButtonHandler(); };
            joinButton.OnMouseDown = delegate() { joinButtonHandler(); };
        }

        private void joinButtonHandler()
        {

        }

        private void backButtonHandler()
        {
            scrManager.PlayScreen(States.ScreenState.GS_MENU);
        }

        private void createButtonHandler()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
           
            base.Draw(gameTime);
            
        }
    }
}
