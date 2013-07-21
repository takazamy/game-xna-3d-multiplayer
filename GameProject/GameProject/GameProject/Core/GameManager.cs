using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameProject.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;
using XnaGameCore.GameLogic.State;

namespace GameProject.Core
{
    public class GameManager
    {
        public Client client;
        public ScreenGameManager scrManager;
        public string address = ConfigurationManager.AppSettings["ServerIP"].ToString();
        public int port = int.Parse(ConfigurationManager.AppSettings["Port"]);

        public GameManager(Game game, SpriteBatch spriteBatch)
        {
            
            scrManager = new ScreenGameManager(game, spriteBatch, this);
            
            client = new Client(game, scrManager);
            scrManager.Initialize();
            scrManager.PlayScreen(States.ScreenState.GS_SPLASH_SCREEN);
            
        }


        public void Update(GameTime gameTime)
        {
            scrManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            scrManager.currentScreen.Draw(gameTime);
        }
    }
}
