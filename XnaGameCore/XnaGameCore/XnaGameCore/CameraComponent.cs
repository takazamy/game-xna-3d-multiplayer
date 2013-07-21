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
        Game game;
        int mouseSpeed = 100; // lower is faster
        public float leftRightRotation = 0;
        public float upDownRotation = 0;
        MouseComponent mouse;
        Vector2 preMouseLocation;
        bool isFirstTime = true;

        public CameraComponent(Game game, Vector3 position, Vector3 target, Vector3 up,MouseComponent mouse)
        {
            this.game = game;
            this.mouse = mouse;
            cameraPosition = position;
            cameraTarget = target;
            cameraDirection = target - position;
            cameraUp = up;
            
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)game.Window.ClientBounds.Width /
                (float)game.Window.ClientBounds.Height,
                2, 2000);

            CreatLookAt();

        }
        private void CreatLookAt()
        {

            view = Matrix.CreateLookAt(cameraPosition, cameraPosition + cameraDirection, cameraUp);
        }
        private void Init()
        {
            Mouse.SetPosition(683, 384);
            mouse.location = new Vector2( game.Window.ClientBounds.Width / 2,
               game.Window.ClientBounds.Height / 2);

          //  mouse.location = new Vector2(683, 384);
            preMouseLocation = mouse.location;
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
            Vector2 currentMouseLoacation = mouse.location;
            Console.WriteLine("current: {0}",currentMouseLoacation);
            Console.WriteLine("pre: {0}", preMouseLocation);
            leftRightRotation = (-MathHelper.PiOver4 / mouseSpeed) * (currentMouseLoacation.X - preMouseLocation.X);
            upDownRotation = (MathHelper.PiOver4 / mouseSpeed) * (currentMouseLoacation.Y - preMouseLocation.Y);
            preMouseLocation = mouse.location;

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