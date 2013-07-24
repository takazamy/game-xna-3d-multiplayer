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
using Microsoft.Xna.Framework.Input;

namespace GameProject.Core
{
    public class Join:JoinScreen
    {
        GameManager gameManager;
        Client client;
        SpriteFont font;
        private JArray listRoomData;
        MouseState LastState;
        public JArray ListRoomData
        {
            get { return listRoomData; }
            set 
            {
                listRoomData = value;
                InfoLine info;
                int y = 10;
                for (int i = 0; i < listRoomData.Count; i++)
                {
                    JObject infoObject = (JObject)listRoomData[i];
                    info = new InfoLine((int)infoObject[GameKeys.ROOMID], (int)infoObject[GameKeys.NUMBER_USERS],y,font);
                    y += 25;
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
            font = this.game.Content.Load<SpriteFont>("Arial");
            addButtonHandler();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (infoList.Count == 0 )
            {
                return;
            }
            
            int last = (infoList.Count - 1);
            InfoLine lastinfo = infoList[last];
            float yB = (float)lastinfo.pos;
            backButton.location = new Vector2(100, yB + 30);
            if (this.enable)
            {
                
                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released)
                {
                    for (int i = 0; i < infoList.Count; i++)
                    {
                        
                        if (infoList[i].CheckClick(gameManager.mouse.location))
                        {
                            RequestHandler.SendJoinRoom(client, infoList[i].roomid);
                            break;
                        }
                    }
                }

                LastState = mouseState;
            }
           
            base.Update(gameTime);
        }
        private void addButtonHandler()
        {
            backButton.OnMouseDown = delegate() { backButtonHandler(); };
        }

        private void backButtonHandler()
        {
            scrManager.PlayScreen(States.ScreenState.GS_HOST);
        }

        public override void Draw(GameTime gameTime)
        {
            
            for (int i = 0; i < infoList.Count; i++)
            {
                 infoList[i].Draw(game.GraphicsDevice,spriteBatch);
                 
            }
            base.Draw(gameTime);
        }
    }

    class InfoLine
    {
        public int pos;
        Texture2D texture;
        public int roomid;
        int playerNum;
        SpriteFont font;
        Rectangle bound;
       
        public InfoLine(int id, int num, int pos, SpriteFont font)
        {
            this.font = font;
            this.pos = pos;
            roomid = id;
            playerNum = num;
            bound = new Rectangle(100, pos, 500, 20);
        }

        public Boolean CheckClick(Vector2 mouseLocation)
        {
            return bound.Contains(new Point((int)mouseLocation.X,(int) mouseLocation.Y));
        }

        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            texture = new Texture2D(device,1,1);
            texture.SetData(new[] {Color.Red});
           // device.Clear(Color.Green);
            spriteBatch.Draw(texture, bound, Color.Red);
            spriteBatch.DrawString(font, roomid.ToString(), new Vector2(110,(float)pos), Color.Blue);
            spriteBatch.DrawString(font, playerNum.ToString(), new Vector2(300, (float)pos), Color.Blue);
            string wi = (bound.Width+bound.X).ToString();
            spriteBatch.DrawString(font, bound.X.ToString(), new Vector2(400, (float)pos), Color.Blue);
            spriteBatch.DrawString(font, wi, new Vector2(450, (float)pos), Color.Blue);
            string he = (bound.Height + bound.Y).ToString();
            spriteBatch.DrawString(font, bound.Y.ToString(), new Vector2(500, (float)pos), Color.Blue);
            spriteBatch.DrawString(font, he, new Vector2(550, (float)pos), Color.Blue);
            spriteBatch.End();
        }
    }
}
