using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaGameCore.GameLogic.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Network;
using Newtonsoft.Json.Linq;
using GameProject.GameLogic;
using XnaGameCore;
using XnaGameCore.GameLogic.State;

namespace GameProject.Core
{
    public class Join:JoinScreen
    {
        GameManager gameManager;
        Client client;
        private JArray listRoomData;

        public JArray ListRoomData
        {
            get { return listRoomData; }
            set 
            {
                listRoomData = value;
                InfoLine info;
                float y = 10;
                for (int i = 0; i < listRoomData.Count; i++)
                {
                    JObject infoObject = (JObject)listRoomData[i];
                    info = new InfoLine((int)infoObject[GameKeys.ROOMID], (int)infoObject[GameKeys.NUMBER_USERS],y);
                    y += 20;
                    infoList.Add(info);
                }
            }
        }
        List<InfoLine> infoList;
        public Join(ScreenGameManager scrManager, Game game, SpriteBatch spriteBatch, GameManager manager)
            : base(scrManager, game, spriteBatch)
        {
            gameManager = manager;            

            Initialize();
        }

        public override void Initialize()
        {
            client = gameManager.client;
            infoList = new List<InfoLine>();
            backButton = new ButtonComponent(this.game, "buttonImg", new Vector2(100, 250), 150, 50, "Back");
            addButtonHandler();
            base.Initialize();
        }

        private void addButtonHandler()
        {
            backButton.OnMouseDown = delegate() { backButtonHandler(); };
        }

        private void backButtonHandler()
        {
            scrManager.PlayScreen(States.ScreenState.GS_MENU);
        }

        public override void Draw(GameTime gameTime)
        {
            int y = 10;
            for (int i = 0; i < infoList.Count; i++)
            {
                 infoList[i].Draw(game.GraphicsDevice,spriteBatch, y);
                 y += 20;
            }
            base.Draw(gameTime);
        }
    }

    class InfoLine
    {
        public float pos;
        Texture2D texture;
        int roomid;
        int playerNum;
        public InfoLine(int id, int num, float pos)
        {
            this.pos = pos;
            roomid = id;
            playerNum = num;
           
        }


        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch, int y)
        {
            pos = y;
            texture = new Texture2D(device,1,1);
            texture.SetData(new[] {Color.Red});
           // device.Clear(Color.Green);
            spriteBatch.Draw(texture, new Rectangle(100,y,500,20), Color.Red);
        }
    }
}
