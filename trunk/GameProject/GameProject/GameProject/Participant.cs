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
                        camera = new CameraComponent(game, new Vector3(128, 8, 128), new Vector3(128, 8, 138), new Vector3(0, 1, 0));
                        turret = new Turret("Model/turret", modelEffect, new Vector3(128, 0, 128), camera, game);
                        break;
                    case 2:
                        camera = new CameraComponent(game, new Vector3(128, 8, 128), new Vector3(128, 8, 138), new Vector3(0, 1, 0));
                        turret = new Turret("Model/turret", modelEffect, new Vector3(128, 0, 128), camera, game);
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
