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

        public Turret(string assetName, Effect effect, Vector3 position,CameraComponent camera, Game game)
            : base ( assetName, effect, position, game)
        {
            this.game = game;
            this.camera = camera;

            Vector3[] bulletPosition = new Vector3[100];
            for (int i = 0; i < bulletPosition.Length; i++)
            {
                bulletPosition[i] = new  Vector3(0,10,0);
            }

            gunTexture = game.Content.Load<Texture2D>("Texture/Metal");
            Texture2D bulletTexture = game.Content.Load<Texture2D>("Texture/bullet");
            this.bullet = new Bullet(game.GraphicsDevice, game.Content, bulletTexture, new Vector2(0.5f), bulletPosition);
        }


        public void Update(float xRotate, float yRotate,GameTime gameTime)

        {
            this.xRotation += xRotate;
            this.yRotation += yRotate;

          
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
            bullet.Update(gameTime);
            


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
