using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XnaGameCore.GameLogic.State;

namespace XnaGameCore
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ButtonComponent// : Microsoft.Xna.Framework.DrawableGameComponent
    {
        
        private Texture2D buttonImage;
        private String name;
        private States.ButtonStateEnum state;

        public States.ButtonStateEnum State
        {
            get { return state; }
            set 
            { 
                state = value;
                int column = (int)state % _col;
                int row = (int)state / _col;
                currSourceRect.X = column * buttonWidth;
                currSourceRect.Y = row * buttonHeight;

            }
        }
        public int _rows;
        public int _col;
        public int TotalFrame
        {
            get { return _col * _rows; }

        }
        public Vector2 location = Vector2.Zero;
        MouseState LastState;
        Rectangle bound;
        

       
        public bool isActive = false;

       
        private int buttonWidth;
        private int buttonHeight;
        private Rectangle currSourceRect;
        private Game game;
        private string buttonText;
        private SpriteFont font;
        private Vector2 textLocation;
        public ButtonComponent(Game game, String name, Vector2 location, int Width, int Height, string btnText)            
        {            
            this.name = name;
            this.location = location;
            buttonWidth = Width;
            buttonHeight = Height;
            bound = new Rectangle((int)location.X, (int)location.Y, buttonWidth, buttonHeight);
            currSourceRect = new Rectangle(0, 0, buttonWidth, buttonHeight);
            this.game = game;
            buttonText = btnText;
            //GetTotalFrame();
            
            LoadContent();
            // TODO: Construct any child components here
        }
        
        public void GetTotalFrame()
        {
            _rows = buttonImage.Height / buttonHeight;
            _col = buttonImage.Width / buttonWidth;
           
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization code here

           
        }
        protected void LoadContent()
        {
            buttonImage = this.game.Content.Load<Texture2D>(name);
            GetTotalFrame();
            font = this.game.Content.Load<SpriteFont>("Arial");
            textLocation = new Vector2(this.location.X + buttonWidth / 2 - font.MeasureString(buttonText).X / 2, this.location.Y + this.buttonHeight / 2 - font.MeasureString(buttonText).Y / 2);
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            MouseState state = Mouse.GetState();            
            if (state.LeftButton == ButtonState.Pressed && LastState.LeftButton == ButtonState.Released)
            {
                if (bound.Contains(new Point(state.X, state.Y)))
                {
                    //MouseDown
                    if (State == States.ButtonStateEnum.BS_NORMAL)
                    {
                        State = States.ButtonStateEnum.BS_HOLD;
                        if (onMouseDown != null)
                        {
                            onMouseDown();
                        }
                    }
                    //Mouse move

                    else
                    {
                        if (State == States.ButtonStateEnum.BS_HOLD)
                        {
                            if (onDrag != null)
                            {
                                onDrag();
                            }
                        }
                    }
                }
            }
            else
            {
                if (State == States.ButtonStateEnum.BS_HOLD)
                {
                    State = isActive == false ? States.ButtonStateEnum.BS_NORMAL : States.ButtonStateEnum.BS_HOLD;
                    if (onMouseUp != null)
                    {
                        onMouseUp();
                    }
                }
            }
            LastState = state;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            //spriteBatch.Begin();
            if (!isActive)
            {
                spriteBatch.Draw(this.buttonImage, bound,
                    new Rectangle(0,0,buttonWidth,buttonHeight), 
                    Color.White);
            }
            else
            {
                spriteBatch.Draw(this.buttonImage, bound, currSourceRect, Color.White);
            }
            spriteBatch.DrawString(font, buttonText, textLocation, Color.Red);
           // spriteBatch.End();           
        }

        private Action onMouseDown = null;
        public Action OnMouseDown
        {

            set { onMouseDown = value; }
        }

        private Action onMouseUp = null;
        public Action OnMouseUp
        {
            set { onMouseUp = value; }
        }

        private Action onDrag = null;

        public Action OnDrag
        {

            set { onDrag = value; }
        }
    }
}
