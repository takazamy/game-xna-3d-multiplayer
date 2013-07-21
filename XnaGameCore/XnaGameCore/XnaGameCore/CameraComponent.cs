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
    public class CameraComponent
    {
        public Matrix view;
        public Matrix projection;
        public Matrix world = Matrix.Identity;
        public Vector3 cameraPosition;
        public Vector3 cameraDirection;
        public Vector3 cameraUp;
        Vector3 cameraTarget;
        MouseState preMouseState;
        public Quaternion mouseQuaternion = Quaternion.Identity;
        public Quaternion mouseRotate;
        Game game;
        int mouseSpeed = 100; // lower is faster
        public float leftRightRotation = 0;
        public float upDownRotation = 0;
        bool isFirstTime = true;

        public CameraComponent(Game game, Vector3 position, Vector3 target, Vector3 up,MouseComponent mouse)
        {
            this.game = game;
            cameraPosition = position;
            cameraTarget = target;
            cameraDirection = target - position;
            cameraUp = up;
            CreatLookAt();


            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)game.Window.ClientBounds.Width /
                (float)game.Window.ClientBounds.Height,
                2, 2000);
        
        }
        private void CreatLookAt()
        {

            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
        private void Init()
        {
            Mouse.SetPosition(game.Window.ClientBounds.Width / 2,
                game.Window.ClientBounds.Height / 2);
            preMouseState = Mouse.GetState();
        }
        public void Update(GameTime gameTime)
        {
            if (isFirstTime)
            {
                Init();
                isFirstTime = false;
            }
            else
            {
                this.MouseLook();
                this.CameraMove();
            }
        }
        private void MouseLook()
        {


            MouseState currentState = Mouse.GetState();
            leftRightRotation = (-MathHelper.PiOver4 / mouseSpeed) * (currentState.X - preMouseState.X);
            upDownRotation = (MathHelper.PiOver4 / mouseSpeed) * (currentState.Y - preMouseState.Y);
            preMouseState = Mouse.GetState();


            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp, leftRightRotation));
            cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(cameraUp, cameraDirection), upDownRotation));
            cameraDirection = Vector3.Normalize(cameraDirection);

            CreatLookAt();
        }
        private void CameraMove()
        {
            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.W))
            {
                cameraPosition += cameraDirection;
            }
            else if (key.IsKeyDown(Keys.S))
            {
                cameraPosition -= cameraDirection;
            }

            if (key.IsKeyDown(Keys.A))
            {
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection);
            }
            else if (key.IsKeyDown(Keys.D))
            {

                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection);
            }


        }
    }
}