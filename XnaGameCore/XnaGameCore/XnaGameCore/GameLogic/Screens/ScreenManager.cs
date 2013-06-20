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


namespace XnaGameCore.GameLogic.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScreenManager : Microsoft.Xna.Framework.GameComponent
    {

        #region Properties
        public Dictionary<States.ScreenState, Screens> screenList;
        //public List<Screens> ScreenList;
        //public Screens this[int index]
        //{
        //    get
        //    {
        //        return ScreenList[index];
        //    }
        //    set
        //    {
        //        ScreenList[index] = value;
        //    }
        //}
        public Screens currentScreen;

        public States.ScreenState state = States.ScreenState.GS_SPLASH_SCREEN;

        private int curIndex = -1;
        #endregion

        public ScreenManager(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            screenList = new Dictionary<States.ScreenState, Screens>();

            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        public void Append(States.ScreenState keys, Screens screen)
        {
            screenList[keys] = screen;
        //    screen.index = this.ScreenList.Count - 1;
        //    ScreenList.Add(screen);
        //    this.Game.Components.Add(screen);
        }

        public void PlayScreen(States.ScreenState key)
        {
            currentScreen = screenList[key];
        //    CurScreen.Visible = false;
        //    CurScreen.Enabled = false;
        //    this.CurScreen = ScreenList[index];
        //    this.curIndex = index;
        //    this.CurScreen.Visible = true;
        //    CurScreen.Enabled = true;
        }
        //public void PlayScreen(States.ScreenState state)
        //{
        //    try
        //    {
        //        foreach (Screens scr in this.ScreenList)
        //        {
        //            if (scr.state == state)
        //            {
        //                CurScreen.Visible = false;
        //                CurScreen.Enabled = false;
        //                CurScreen = scr;
        //                curIndex = scr.index;
        //                this.CurScreen.Visible = true;
        //                CurScreen.Enabled = true;
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //}
        public void NextScreen()
        {
            state++;
            currentScreen = screenList[state];
        //    CurScreen.Visible = false;
        //    CurScreen.Enabled = false;
        //    this.CurScreen = ScreenList[++curIndex];
        //    this.CurScreen.Visible = true;
        //    CurScreen.Enabled = true;
        }

        //public void PrevScreen()
        //{
        //    CurScreen.Visible = false;
        //    CurScreen.Enabled = false;
        //    CurScreen = ScreenList[--curIndex];
        //    this.CurScreen.Visible = true;
        //    CurScreen.Enabled = true;
        //}

        //public void UpdateIndex()
        //{
        //    for (int i = 0; i < this.ScreenList.Count; i++)
        //    {
        //        ScreenList[i].index = i;
        //    }
        //}

    }
}
