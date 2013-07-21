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
        Effect mapEffect;
        Turret turret;

   

        public MainGame(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
            this.game = game;
            this.LoadContent();
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here


        }

        protected override void LoadContent()
        {
            camera = new CameraComponent(game,new Vector3(0,6,-30),new Vector3(0,0,0),new Vector3(0,1,0));
            map = new LoadMap("../../../map.bmp","Texture/grass",game);
            mapEffect = game.Content.Load<Effect>("Effect/MapEffect");
           // turret = new Turret(game, camera);
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (enable)
            {
                camera.Update(gameTime);
            }
           // turret.Update(gameTime);
            
        }

        public override void Draw(GameTime gameTime)
        {
            
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            rs.FillMode = FillMode.Solid;
            game.GraphicsDevice.RasterizerState = rs;
            map.DrawMap(mapEffect, "AddTexture", camera.view, camera.projection, Matrix.Identity);
            //turret.Draw(gameTime);
            this.enable = true;
        }
    }
}
