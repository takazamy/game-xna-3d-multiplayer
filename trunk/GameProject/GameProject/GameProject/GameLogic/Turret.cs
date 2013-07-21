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
        Effect turretEffect;
        ModelBone turretLeftBone, turretRightBone, turretCenterBone, turretBaseBone;
        Matrix leftTransform, rightTransform, centerTransform, baseTransform;
        Bullet turretBullet;
        Vector3 bulletLeftOffset = new Vector3(2.8f,-2.7f,11.5f);
        Vector3 bulletRightOffset = new Vector3(-2.8f, -2.7f, 11.5f);

        public Turret(string assetName, Effect effect, Vector3 position,CameraComponent camera, Game game)
            : base ( assetName, effect, position, game)
        {
            this.game = game;
            this.camera = camera;

            Vector3[] bulletPosition = new Vector3[1];
            for (int i = 0; i < bulletPosition.Length; i++ )
            {
                bulletPosition[i] = camera.cameraPosition + bulletRightOffset;
            }
            Texture2D bulletTexture = game.Content.Load<Texture2D>("Texture/bullet");
            this.turretBullet = new Bullet(game.GraphicsDevice, game.Content, bulletTexture, new Vector2(1), bulletPosition);
        }

        public void Update(float xRotate, float yRotate)
        {

            turretBullet.Update(camera.cameraDirection);


            
            //this.zRotation += xRotate;
            //this.yRotation += yRotate;
            //yRotation = MathHelper.PiOver2;

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

        }
        public override void DrawModel(string technique, float scaleRate, CameraComponent camera)
        {
          //  model.Root.Transform = camera.world;
            turretBullet.Draw(camera.view, camera.projection, camera.cameraUp, Vector3.Cross(camera.cameraUp, camera.cameraDirection));
            //left right Rotation
            Matrix modelWorld = Matrix.CreateScale(scaleRate)
            * Matrix.CreateFromYawPitchRoll(MathHelper.PiOver2 + yRotation, xRotation, zRotation)
            * Matrix.CreateTranslation(position);
            // center rotation
            Matrix modelWorld1 = Matrix.CreateScale(scaleRate)
            * Matrix.CreateFromYawPitchRoll(MathHelper.PiOver2 + yRotation, 0f, 0f)
            * Matrix.CreateTranslation(position);
            // base rotation
            Matrix modelWorld2 = Matrix.CreateScale(scaleRate)
            * Matrix.CreateFromYawPitchRoll(MathHelper.PiOver2, 0f, 0f)
            * Matrix.CreateTranslation(position);

            int i = 0;
            Matrix[] modelBone = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelBone);
            foreach (ModelMesh mesh in model.Meshes)
            {
                Matrix worldMatrix;
                if (mesh.Name == "turret_2_b")
                {
                    worldMatrix = modelBone[mesh.ParentBone.Index] * modelWorld2;
                }
                else if (mesh.Name == "turret_2_t")
                {
                    worldMatrix = modelBone[mesh.ParentBone.Index] * modelWorld1;
                }
                else
                {
                    worldMatrix = modelBone[mesh.ParentBone.Index] * modelWorld;
                }


                foreach (Effect currentEffect in mesh.Effects)
                {
                    currentEffect.CurrentTechnique = currentEffect.Techniques[technique];
                    currentEffect.Parameters["xView"].SetValue(camera.view);
                    currentEffect.Parameters["xProjection"].SetValue(camera.projection);
                    currentEffect.Parameters["xWorld"].SetValue(worldMatrix);
                    currentEffect.Parameters["xTexture"].SetValue(modelTextures[i++]);
                    currentEffect.Parameters["WorldInverseTranspose"].SetValue(
                      Matrix.Transpose(Matrix.Invert(mesh.ParentBone.Transform * camera.world)));
                }
                mesh.Draw();
            }


            //base.DrawModel(technique, scaleRate, camera);
        }

    }
}
