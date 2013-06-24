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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace XnaGameCore
{
    class CameraComponent
    {
         public Matrix view;
        public Matrix projection;
        public Vector3 cameraPosition;
        Vector3 cameraDirection;
        Vector3 cameraUp;
        public MouseState preMouseState;
        Game game;

        public CameraComponent(Game game, Vector3 position, Vector3 target, Vector3 up)
        {
            this.game = game;
            cameraPosition = position;
            cameraDirection = target - position;
            cameraDirection.Normalize();
            cameraUp = up;
            CreatLookAt();


            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)game.Window.ClientBounds.Width /
                (float)game.Window.ClientBounds.Height,
                2, 200);

            Init();

        }
        public void CreatLookAt()
        {
            
            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
        public void Init()
        {
            Mouse.SetPosition(game.Window.ClientBounds.Width / 2,
                game.Window.ClientBounds.Height / 2);
            preMouseState = Mouse.GetState();
        }
        public void Update(GameTime gameTime)
        {

            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp,
                (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - preMouseState.X)));

            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(
                Vector3.Cross(cameraUp, cameraDirection),
                (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - preMouseState.Y)));

            //cameraUp = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(
            //    Vector3.Cross(cameraUp, cameraDirection),
            //    (MathHelper.PiOver4 / 100) * (Mouse.GetState().Y - preMouseState.Y)));
            
            preMouseState = Mouse.GetState();

            

            CreatLookAt();
    }
    }
}
