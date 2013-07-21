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

namespace XnaGameCore
{
    public class CustomModel
    {
        Game game;
        protected List<Texture2D> modelTextures = new List<Texture2D>();
        protected Model model;
        protected Vector3 position;
        protected Matrix world = Matrix.Identity;
        protected float xRotation, yRotation, zRotation;
        protected Quaternion modelQuaternion = Quaternion.Identity;

        public CustomModel(string assetName, Effect effect, Vector3 position, Game game)
        {
            this.game = game;
            this.model = this.game.Content.Load<Model>(assetName);
            this.position = position;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect currentEffect in mesh.Effects)
                {
                    Texture2D tempTex = currentEffect.Texture;
                    modelTextures.Add(tempTex);
                }
            }

            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    meshPart.Effect = effect.Clone();
        }

        public virtual void DrawModel(string technique, float scaleRate, CameraComponent camera)
        {
            Matrix modelWorld = Matrix.CreateScale(scaleRate)
             * Matrix.CreateFromYawPitchRoll(MathHelper.PiOver2 + yRotation, xRotation, zRotation)
             * Matrix.CreateTranslation(position);

            int i = 0;
            Matrix[] modelBone = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelBone);
            foreach (ModelMesh mesh in model.Meshes)
            {
                Matrix worldMatrix = modelBone[mesh.ParentBone.Index] * modelWorld;
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
        }
    }
}
