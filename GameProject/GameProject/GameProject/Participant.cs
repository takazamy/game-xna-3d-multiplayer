﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XnaGameCore;
using Microsoft.Xna.Framework.Graphics;
using GameProject.GameLogic;

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
        private Vector3 rotationAngle;
        private Vector3 phapTuyen;
        Vector3 gunPosition, gunPosition2;
        public Room room;
        TargetBillboard target;

        #endregion

        public Participant(int id, Game game)
        {
            hp = 100;
            this.ClientId = id;
            this.game = game;
            rotationAngle = new Vector3();
            phapTuyen = new Vector3();
            Init();
        }

        public void Init()
        {
            modelEffect = game.Content.Load<Effect>("Effect/LightingEffect");
            gunPosition = new Vector3(90, 5, 128);
            gunPosition2 = new Vector3(180, 5, 128);


            Texture2D targetTexture = game.Content.Load<Texture2D>("Texture/Bullet");
            Vector3[] targetPosition = new Vector3[10];
            for (int i = 0; i < targetPosition.Length; i++)
            {
                targetPosition[i] = new Vector3(0, -10, 0);
            }
            target = new TargetBillboard(game.GraphicsDevice, game.Content, targetTexture, new Vector2(10), targetPosition);
            
        }

        public void UpdateTurretMove(GameKeys.TURRET_STATE_LR stateLR, GameKeys.TURRET_STATE_UD stateUD)
        {
            turret.stateLR = stateLR;
            turret.stateUD = stateUD;
        }
        public void Update(GameTime gameTime)
        {
            
            if (isMe)
            {
                target.Update(gameTime);
                //Camera
         //       camera.Update(gameTime);
                //1 
               // Console.WriteLine("ClientId: " + this.ClientId + " isme:" + isMe);
                turret.Update(camera.upDownRotation, camera.leftRightRotation,target,gameTime);

            }
            else
            {
                //2
               // Console.WriteLine("ClientId: " + this.ClientId + " isme:" + isMe);
                //turret.Update();
                //camera.Update(gameTime);
                //1

                turret.Move(gameTime, target);
            }
           
        }

        public void Draw(GameTime gameTime)
        {
            turret.DrawModel("Lighting", 0.1f, camera);
          //  Console.WriteLine("ClientId:" + this.ClientId + "turret:" + turret.position);

            target.Draw(camera.view, camera.projection, camera.cameraUp, Vector3.Cross(camera.cameraUp, camera.cameraDirection));
            //Console.WriteLine("ClientId:" + this.ClientId + "turret:" + turret.position);
        }

        internal void CreateCamera(int pos, Game game)
        {
            
                switch (pos)
                {
                    case 1:
                        camera = new CameraComponent(game, gunPosition + new Vector3(0, 0.8f, 0), gunPosition + new Vector3(0, 0.8f, 10), new Vector3(0, 1, 0));
                        break;
                    case 2:
                        camera = new CameraComponent(game, gunPosition2 + new Vector3(0, 0.8f, 0), gunPosition2 + new Vector3(0, 0.8f, 10), new Vector3(0, 1, 0));
                        break;
                    default:
                        break;
                }
           
           
            try
            {
                switch (this.position)
                {
                    case 1:                        
                        turret = new Turret("Model/gun2", modelEffect, gunPosition, camera, game,this);
                        break;
                    case 2:                        
                        turret = new Turret("Model/gun2", modelEffect, gunPosition2, camera, game, this);
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
