using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Configuration;
using XnaGameCore;

using GameProject.GameLogic;
using GameProject.Network;


namespace GameProject
{
    public class Turret:CustomModel
    {
        Game game;        
        CameraComponent camera;
        Bullet bullet;
        int bulletIndex = 0;
        float eslapedTime = 0;
        Texture2D gunTexture;
        Vector3 bulletLeftOffset = new Vector3(2.8f,-2.7f,11.5f);
        Vector3 bulletRightOffset = new Vector3(-2.8f, -2.7f, 11.5f);
        public GameKeys.TURRET_STATE_LR stateLR;
        public GameKeys.TURRET_STATE_UD stateUD;
        private GameKeys.TURRET_STATE_UD lastStateUD;
        private GameKeys.TURRET_STATE_LR lastStateLR;
        Participant participant;
        
        public Turret(string assetName, Effect effect, Vector3 position,CameraComponent camera, Game game, Participant p)
            : base ( assetName, effect, position, game)
        {
            
            participant = p;
            this.game = game;
            this.camera = camera;
            stateLR = GameKeys.TURRET_STATE_LR.STAYLR;
            stateUD = GameKeys.TURRET_STATE_UD.STAYUD;
            Vector3[] bulletPosition = new Vector3[100];
            for (int i = 0; i < bulletPosition.Length; i++)
            {
                bulletPosition[i] = new  Vector3(0,10,0);
            }

            gunTexture = game.Content.Load<Texture2D>("Texture/Metal");
            Texture2D bulletTexture = game.Content.Load<Texture2D>("Texture/bullet");
            this.bullet = new Bullet(game.GraphicsDevice, game.Content, bulletTexture, new Vector2(0.5f), bulletPosition);
        }



        public void Move(GameTime gameTime, TargetBillboard target)
        {
            float yRotate = 0.01f * (int)stateUD;
            float xRotate = 0.01f * (int)stateLR;
            Update(xRotate, yRotate, target, gameTime);
        }

        public void Shoot()
        {  
            this.Fire(camera.cameraDirection);
        }


        public void Update(float xRotate, float yRotate,TargetBillboard target,GameTime gameTime)
        {
            this.xRotation += xRotate;
            this.yRotation += yRotate;
            int moveLR = 0;
            int moveUD = 0;
            if (xRotate > 0)
            {
                stateLR = GameKeys.TURRET_STATE_LR.LEFT;
                moveLR = 1;
            }
            else if (xRotate < 0)
            {
                stateLR = GameKeys.TURRET_STATE_LR.RIGHT;
                moveLR = -1;
            }
            else
            {
                stateLR = GameKeys.TURRET_STATE_LR.STAYLR;
                moveLR = 0;
            }
            if (yRotate > 0)
            {
                stateUD = GameKeys.TURRET_STATE_UD.UP;   
            }
            else if(yRotate < 0)
            {
                stateUD = GameKeys.TURRET_STATE_UD.DOWN;
            }
            else
            {
                stateUD = GameKeys.TURRET_STATE_UD.STAYUD;
            }

            if (lastStateLR != stateLR || lastStateUD != stateUD)
	        {
		        if (this.participant.isMe)
	            {
                    RequestHandler.SendPlayerMove(participant.room.client, moveLR, 0);
	            }
                lastStateUD = stateUD;
                lastStateLR = stateLR;
	        }
            eslapedTime += gameTime.ElapsedGameTime.Milliseconds;
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (eslapedTime >= 100)
                {
                    this.Fire(camera.cameraDirection);
                    eslapedTime = 0;
                }
            }
            bullet.Update(gameTime,target);
            


            #region rotation
            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Up))
            {
                zRotation += 0.01f;
            }
            else if (key.IsKeyDown(Keys.Down))
            {
                zRotation -= 0.01f;
            }
          
            else if (key.IsKeyDown(Keys.Left))
            {
                yRotation += 0.01f;
            }
            else if (key.IsKeyDown(Keys.Right))
            {
                yRotation -= 0.01f;
            }
            else if (key.IsKeyDown(Keys.PageDown))
            {
                xRotation += 0.01f;
            }
            else if (key.IsKeyDown(Keys.End))
            {
                xRotation -= 0.01f;
            }
#endregion
        }
        public override void DrawModel(string technique, float scaleRate, CameraComponent camera)
        {
            try
            {
                
                //  model.Root.Transform = camera.world;
                bullet.Draw(camera.view, camera.projection, camera.cameraUp, Vector3.Cross(camera.cameraUp, camera.cameraDirection));
                //left right Rotation
                Matrix modelWorld = Matrix.CreateScale(scaleRate)
                * Matrix.CreateFromYawPitchRoll(yRotation, xRotation, zRotation)
                * Matrix.CreateTranslation(position);


                Matrix[] modelBone = new Matrix[model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(modelBone);
                foreach (ModelMesh mesh in model.Meshes)
                {
                    lock (mesh)
                    {
                        Matrix worldMatrix;
                        worldMatrix = modelBone[mesh.ParentBone.Index] * modelWorld;

                        foreach (Effect currentEffect in mesh.Effects)
                        {
                            currentEffect.CurrentTechnique = currentEffect.Techniques[technique];
                            currentEffect.Parameters["xView"].SetValue(camera.view);
                            currentEffect.Parameters["xProjection"].SetValue(camera.projection);
                            currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                            currentEffect.Parameters["xTexture"].SetValue(gunTexture);
                            currentEffect.Parameters["WorldInverseTranspose"].SetValue(
                              Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * camera.world)));
                        }
                        mesh.Draw();
                    }
                   
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //base.DrawModel(technique, scaleRate, camera);
        }
        private void Fire(Vector3 direction)
        {
           // Console.WriteLine(xRotation);
          //  Console.WriteLine(yRotation);

            Vector3 firePosition = camera.cameraPosition + camera.cameraDirection * 9;

            bullet.SetFirePosition(firePosition, direction, bulletIndex);
            
            if (bulletIndex >= bullet.billboardPosition.Length - 1)
            {
                bulletIndex = 0;
            }
            else
            {
                bulletIndex++;
            }
        }
    }
}
