using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XnaGameCore.GameLogic.Screens;
using XnaGameCore.GameLogic.State;
using XnaGameCore;
using GameProject.Core;
using XnaGameNetworkEngine;
using GameProject.Network;
using System.Configuration;
using GameProject.GameLogic;

namespace GameProject.Core
{
    public class MainGame:GameScreen
    {
        CameraComponent camera;
        LoadMap map;
        Turret turret;
        Skybox skyBox;
        BillboardSystem trees;
        Effect mapEffect;
        GameManager gameManager;

        Vector3 turretPosition;


        public Room room;
        public MainGame(ScreenManager scrManager, Game game, SpriteBatch spriteBatch,GameManager gameManager)
            : base(scrManager, game, spriteBatch)
        {
            this.game = game;
            this.gameManager = gameManager;
            this.LoadContent();


        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            
        }

        protected override void LoadContent()
        {
            turretPosition = new Vector3(128, 10, 128);
           // modelEffect = game.Content.Load<Effect>("Effect/LightingEffect");

            mapEffect = game.Content.Load<Effect>("Effect/MapEffect");

     //       camera = new CameraComponent(game, turretPosition + new Vector3(0f, 0.8f, 0f), turretPosition +  new Vector3(0f, 0.8f, 10f), new Vector3(0, 1, 0),gameManager.mouse);


            map = new LoadMap("../../../Map/map1.bmp", "Texture/grass", game);
         //   turret = new Turret("Model/gun2", modelEffect, turretPosition, camera, game);
            skyBox = new Skybox("Model/cube", "Effect/Skybox", "Texture/Islands", game.Content);


            Texture2D treeTexture = game.Content.Load<Texture2D>("Texture/tree_billboard");
            Vector3[] treePosition = new Vector3[2];
            treePosition[0] = new Vector3(20, 20, 40);
            treePosition[1] = new Vector3(40, 20, 60);
            trees = new BillboardSystem(game.GraphicsDevice, game.Content, treeTexture, new Vector2(40), treePosition);

        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            if (enable)
            {
               // camera.Update(gameTime);

           //     turret.Update(camera.upDownRotation, camera.leftRightRotation,gameTime);


                room.Update(gameTime);
                //for (int i = 0; i < room.clientList.Count; i++)
                //{
                //    if (gameManager.client.parentParticipant.isMe)
                //    {
                //        camera = gameManager.client.parentParticipant.camera;
                //    }
                //}
               // turret.Update(camera.upDownRotation, camera.leftRightRotation);
               // turret.Update(0f, 0f);

                
            }
        }
        public void setMainCamera()
        {
            foreach (var item in room.clientList)
            {
                Participant p = item.Value;
                if (p.isMe)
                {
                    camera = p.camera;
                    break;
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
           
            //RasterizerState rs = new RasterizerState();
            //rs.CullMode = CullMode.None;
            //rs.FillMode = FillMode.Solid;
          //  game.GraphicsDevice.RasterizerState = rs;
           
            map.DrawMap(mapEffect, "AddTexture", camera.view, camera.projection, Matrix.Identity);
            //turret.DrawModel("Lighting", 0.1f, camera);
            room.Draw(gameTime);
            skyBox.Draw(camera.view, camera.projection, camera.cameraPosition);
            trees.Draw(camera.view,camera.projection,camera.cameraUp,Vector3.Cross(camera.cameraUp,camera.cameraDirection));
            base.Draw(gameTime);
            this.enable = true;
        }
    }
}
