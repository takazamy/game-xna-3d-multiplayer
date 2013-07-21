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
using XnaGameCore.GameLogic.Screens;
using XnaGameCore.GameLogic.State;

namespace XnaGameCore.GameLogic.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameScreen : Screens
    {
       // Character myChar;
        //TileMap tile;
       // Camera camera;
     //   private List<Units> PlayerUnitsList;
       // private List<Units> EnemyUnitsList;
       // private List<Units> UnMoveUnitsList;
        private List<Vector2> EnemyStartPosList;
        //private States.UnitLevelType level;
        private int spawTime = 0;
        private Vector2 playerStartPos;
        private Vector2 enemyStartPos;
        private KeyboardState lastKeyState;
        public KeyboardState currKeyState;
        private ButtonComponent gate1;
        private ButtonComponent gate2;
        private ButtonComponent gate3;
        private ButtonComponent gate4;
        private SpriteFont font;
        private float esllapseTime;
        public GameScreen(ScreenManager scrManager, Game game, SpriteBatch spriteBatch)
            : base(scrManager, game, spriteBatch)
        {
            this.state = States.ScreenState.GS_MAIN_GAME;
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            
           
        }

        protected override void LoadContent()
        {
            
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            

        }

        public override void Draw(GameTime gameTime)
        {
         
          
     
        }
    }
}
