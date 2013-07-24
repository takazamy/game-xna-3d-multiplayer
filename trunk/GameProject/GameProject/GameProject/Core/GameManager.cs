using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameProject.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Configuration;
using XnaGameCore.GameLogic.State;
using XnaGameCore;
using GameProject.GameLogic;

namespace GameProject.Core
{
    public class GameManager
    {
        public Client client;
        public ScreenGameManager scrManager;
        public string address = ConfigurationManager.AppSettings["ServerIP"].ToString();
        public int port = int.Parse(ConfigurationManager.AppSettings["Port"]);
        public MouseComponent mouse;
        SpriteBatch spriteBatch;
        public Room room;
        public GameManager(Game game, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            scrManager = new ScreenGameManager(game, spriteBatch, this);
            mouse = new MouseComponent(game, "mouse");
            client = new Client(game, scrManager);
            scrManager.Initialize();
            scrManager.PlayScreen(States.ScreenState.GS_SPLASH_SCREEN);
          //  room = new Room();
        }


        public void Update(GameTime gameTime)
        {
            mouse.Update(gameTime);
            scrManager.Update(gameTime);
           // room.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            scrManager.currentScreen.Draw(gameTime);  

            if(scrManager.currentScreen.state != States.ScreenState.GS_MAIN_GAME)
                mouse.Draw(gameTime, this.spriteBatch);
           // room.Draw(gameTime);
        }
    }
}
