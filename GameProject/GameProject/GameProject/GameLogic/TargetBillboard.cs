using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaGameCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Configuration;

namespace GameProject.GameLogic
{
   public  class TargetBillboard:BillboardSystem
    {
        public Vector3 beginPosition;
        Random rand = new Random();
        float eslapsedTime;
        public BoundingBox[] targetBox;

        public TargetBillboard(GraphicsDevice graphicDevice, ContentManager content,
            Texture2D texture, Vector2 billboardSize, Vector3[] billboardsPosition) :
            base(graphicDevice, content, texture, billboardSize, billboardsPosition)
        {
            beginPosition = billboardPosition[0];
            targetBox = new BoundingBox[this.billboardPosition.Length];


        }
        public void Update(GameTime gameTime)
        {
            eslapsedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (eslapsedTime >= 10000)
            {
                for (int i = 0; i < this.billboardPosition.Length; i++)
                {
                    billboardPosition[i] = new Vector3(rand.Next(20, 100), rand.Next(20, 50), 230);
                    this.GeneratePactices(billboardPosition);

                }
                for (int i = 0; i < this.billboardPosition.Length; i++)
                {
                    if (billboardPosition[i] != beginPosition)
                    {
                        targetBox[i] = new BoundingBox(new Vector3(billboardPosition[i].X - billboardSize.X / 2, billboardPosition[i].Y - billboardSize.Y / 2, billboardPosition[i].Z),
                        new Vector3(billboardPosition[i].X + billboardSize.X / 2, billboardPosition[i].Y + billboardSize.Y / 2, billboardPosition[i].Z + 10f));
                    }

                }
                //Console.WriteLine("targetPos: {0}", billboardPosition[0]);
                //Console.WriteLine("targetBox: {0}", targetBox[0]);
 
                eslapsedTime = 0;
            }
            
        }

        public void GotHit()
        {
            this.GeneratePactices(billboardPosition);
        }
    }
}
    