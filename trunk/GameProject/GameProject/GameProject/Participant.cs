using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XnaGameCore;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class Participant
    {
        #region Properties
        Turret turret;
        public CameraComponent camera;
        Effect modelEffect;
        public int hp;
        public string nickName;
        public int ClientId;
        public bool isMe;
        public int position;
        Game game;
        Vector3 gunPosition, gunPosition2;
        #endregion

        public Participant(int id, Game game)
        {
            hp = 100;
            this.ClientId = id;
            this.game = game;
                 
            Init();
        }

        public void Init()
        {
            modelEffect = game.Content.Load<Effect>("Effect/LightingEffect");
            gunPosition = new Vector3(90, 5, 128);
            gunPosition2 = new Vector3(180, 5, 128);
        }

        public void Update(GameTime gameTime)
        {
            if (isMe)
            {
                //Camera
                camera.Update(gameTime);
                //1
                turret.Update(camera.upDownRotation, camera.leftRightRotation,gameTime);

            }
            else
            {
                //2
                //turret.Update();
            }
        }

        public void Draw(GameTime gameTime)
        {
            turret.DrawModel("Lighting", 0.1f, camera);
        }

        internal void CreateCamera(int position, Game game)
        {
            try
            {
                switch (position)
                {
                    case 1:
                        camera = new CameraComponent(game, gunPosition + new Vector3(0,0.8f,0),gunPosition + new Vector3(0, 0.8f, 10), new Vector3(0, 1, 0));
                        turret = new Turret("Model/gun2", modelEffect, gunPosition, camera, game);
                        break;
                    case 2:
                        camera = new CameraComponent(game, gunPosition2 + new Vector3(0, 0.8f, 0), gunPosition2 + new Vector3(0, 0.8f, 10), new Vector3(0, 1, 0));
                        turret = new Turret("Model/gun2", modelEffect, gunPosition2, camera, game);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());
                Console.WriteLine(e.Message.ToString());
            }
        }
    }
}
