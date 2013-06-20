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
            //PlayerUnitsList = new List<Units>();
            //EnemyUnitsList = new List<Units>();
            //UnMoveUnitsList = new List<Units>();
            //EnemyStartPosList = new List<Vector2>();
            //tile = new TileMap(this.Game, "tini2_b.json", Global.Map);
            //this.Game.Components.Add(tile);
           // camera = new Camera(this.Game, Vector2.Zero, new Vector2(800, 600), this.tile);
            //this.Game.Components.Add(camera);
            //foreach (TileMap.ObjectLayer item in tile._objectLayer)
            //{
            //    if (item._classname == "Wall")
            //    {
            //        Castle c = new Castle(this.Game,item._location,item._type);
            //        c.isMovable = false;
            //        UnMoveUnitsList.Add(c);
            //    }
            //    if (item._classname == "Gate")
            //    {
            //        Button b = null;
            //        if (item._type == Global.CharacterType.Enemy)
            //        {
                         
            //           // enemyStartPos = item._location;
            //            EnemyStartPosList.Add(item._location);
            //        }
            //        else
            //        {
            //            b = new Button(this.Game, "gate", item._location, 32, 32);
            //            playerStartPos = item._location;
            //        }
            //        switch (item._name)
            //        {
            //            case "gate1":
            //                gate1 = b;
            //                break;
            //            case "gate2":
            //                gate2 = b;
            //                break;
            //            case "gate3":
            //                gate3 =  b;
            //                break;
            //            case "gate4":
            //                gate4 = b;
            //                break;
            //            default:
            //                break;
            //        }
            //    }

            //}
            //gate1.DrawOrder = 99;
            //gate2.DrawOrder = 99;
            //gate3.DrawOrder = 99;
            //gate4.DrawOrder = 99;
            //this.Game.Components.Add(gate1);
            //this.Game.Components.Add(gate2);
            //this.Game.Components.Add(gate3);
            //this.Game.Components.Add(gate4);
            //level = Global.UnitLevelType.level1;

            //gate1.OnMouseDown = delegate()
            //{
            //    playerStartPos = gate1.location;
            //    gate1.isActive = true;
            //    gate2.isActive = false;
            //    gate3.isActive = false;
            //    gate4.isActive = false;
            //};
            //gate2.OnMouseDown = delegate()
            //{
            //    playerStartPos = gate2.location;
            //    gate1.isActive = false;
            //    gate2.isActive = true;
            //    gate3.isActive = false;
            //    gate4.isActive = false;
            //};
            //gate3.OnMouseDown = delegate()
            //{
            //    playerStartPos = gate3.location;
            //    gate1.isActive = false;
            //    gate2.isActive = false;
            //    gate3.isActive = true;
            //    gate4.isActive = false;
            //};
            //gate4.OnMouseDown = delegate()
            //{
            //    playerStartPos = gate4.location;
            //    gate1.isActive = false;
            //    gate2.isActive = false;
            //    gate3.isActive = false;
            //    gate4.isActive = true;
            //};


            //myChar = new Character(this.Game, this.spriteBatch, new Vector2(400, 300), 100, 10,Global.CharacterType.Player);
            //this.Game.Components.Add(myChar);
            //myChar.DrawOrder = this.DrawOrder+1;
            //Random rand = new Random();
            //int enemyNo = rand.Next(5);
            //for (int i = 0; i < enemyNo; i++)
            //{
            //    Character enemy = new Character(this.Game, this.spriteBatch,new Vector2(100,100) /*new Vector2(rand.Next(800), rand.Next(600))*/, 100, 10, Global.CharacterType.Enemy);
            //    //this.Game.Components.Add(enemy);
            //    //enemy.DrawOrder = this.DrawOrder + 1;
            //    charList.Add(enemy);
            //}
            
           // font = Game.Content.Load<SpriteFont>("Arial");

           
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
            // TODO: Add your update code here
            //#region Spam Units
            //if (spawTime < 15000)
            //{
            //    spawTime += gameTime.ElapsedGameTime.Milliseconds;
            //}
            //else
            //{
            //    spawTime = 0;
            //   // for (int i = 0; i < 2; i++)
            //    //{
            //    int pos = Global.getRandomNumber(0,EnemyStartPosList.Count-1);
            //    enemyStartPos = EnemyStartPosList[pos];
            //    Units u = new Units(this.Game, playerStartPos, Global.CharacterType.Player, this.level,Global.UnitType.unit, Global.ID++);
            //    Units u2 = new Units(this.Game, enemyStartPos, Global.CharacterType.Enemy, this.level, Global.UnitType.unit, Global.ID++);
            //    u.isMovable = true;
            //    u2.isMovable = true;
            //    PlayerUnitsList.Add(u);
            //    EnemyUnitsList.Add(u2);
            //    //}
            //}
            //#endregion

           

            //#region Update
            //esllapseTime += gameTime.ElapsedGameTime.Milliseconds;
            //if (esllapseTime > 1000.0f/Game1.FPS)
            //{
            //    #region Update Units
            //    Random rand = new Random(System.DateTime.Now.Millisecond);
            //    int r = rand.Next() % 2;
            //    if (r == 0)
            //    {
                    
            //        for (int i = 0; i < PlayerUnitsList.Count; i++)
            //        {
            //            PlayerUnitsList[i].Update(gameTime, PlayerUnitsList, EnemyUnitsList, tile);


            //        }
            //        for (int i = 0; i < EnemyUnitsList.Count; i++)
            //        {
            //            if (EnemyUnitsList[i].Hp <= 0)
            //            {
            //                EnemyUnitsList.RemoveAt(i);
            //            }

            //        }
            //        for (int i = 0; i < EnemyUnitsList.Count; i++)
            //        {

            //            EnemyUnitsList[i].Update(gameTime, PlayerUnitsList, EnemyUnitsList, tile);


            //        }

            //        for (int i = 0; i < PlayerUnitsList.Count; i++)
            //        {
            //            if (PlayerUnitsList[i].Hp <= 0)
            //            {

            //                PlayerUnitsList.RemoveAt(i);
            //            }
            //        }
            //    }
            //    else
            //    {
                    
            //        for (int i = 0; i < EnemyUnitsList.Count; i++)
            //        {

            //            EnemyUnitsList[i].Update(gameTime, PlayerUnitsList, EnemyUnitsList, tile);


            //        }

            //        for (int i = 0; i < PlayerUnitsList.Count; i++)
            //        {
            //            if (PlayerUnitsList[i].Hp <= 0)
            //            {

            //                PlayerUnitsList.RemoveAt(i);
            //            }
            //        }
            //        for (int i = 0; i < PlayerUnitsList.Count; i++)
            //        {
            //            PlayerUnitsList[i].Update(gameTime, PlayerUnitsList, EnemyUnitsList, tile);


            //        }
            //        for (int i = 0; i < EnemyUnitsList.Count; i++)
            //        {
            //            if (EnemyUnitsList[i].Hp <= 0)
            //            {
            //                EnemyUnitsList.RemoveAt(i);
            //            }

            //        }
            //    }
            //    #endregion

            //    #region Move Camera
            //    lastKeyState = currKeyState;

            //    currKeyState = Keyboard.GetState();

            //    if (currKeyState.IsKeyDown(Keys.A) && lastKeyState.IsKeyUp(Keys.A))// && lastKeyState != state)
            //    {
            //        //lastKeyState = state;
            //        this.camera.MoveLeft();
            //    }
            //    if (currKeyState.IsKeyDown(Keys.D) && lastKeyState.IsKeyUp(Keys.D))// && lastKeyState != state)
            //    {
            //        //lastKeyState = state;
            //        this.camera.MoveRight();
                    
                   
            //    }
            //    #endregion
            //    base.Update(gameTime);
            //    esllapseTime = 0;
            //}
            //#endregion

        }

        public override void Draw(GameTime gameTime)
        {
         
          
            //tile.Draw(gameTime);
            //foreach (Units c in this.PlayerUnitsList)
            //{
                
            //    //if (camera.sourceRect.Contains(new Point((int)c.location.X,(int)c.location.Y)))
            //    //{
            //         c.Draw(gameTime);
            //    //}
               
            //}
            //foreach (Units c in this.EnemyUnitsList)
            //{
            //   //if (camera.sourceRect.Contains(new Point((int)c.location.X,(int)c.location.Y)))
            //   //{
            //         c.Draw(gameTime);
            //   //}
            //}
            //Game1.spriteBatch.DrawString(font, PlayerUnitsList.Count.ToString(), new Vector2(10, 10), Color.Blue);
            //Game1.spriteBatch.DrawString(font, EnemyUnitsList.Count.ToString(), new Vector2(780, 10), Color.Red);
            
        }
    }
}
