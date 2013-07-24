using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using XnaGameCore;
using GameProject.GameLogic;

namespace GameProject
{
    class Bullet:BillboardSystem
    {
        public int damage;
        public float speed = 2f;
        Vector3[] directionVector;
        Vector3 beginPosition;
        float timeLife;

        public Bullet(GraphicsDevice graphicDevice, ContentManager content,
            Texture2D texture, Vector2 billboardSize, Vector3[] billboardsPosition):
            base(graphicDevice,content,texture,billboardSize,billboardsPosition)
        {
            beginPosition = billboardPosition[0];
            directionVector = new Vector3[billboardPosition.Length];
        }
        public void Update(GameTime gameTime,TargetBillboard target)
        {
            this.Move(this.billboardPosition,target);
        }
        private void Move(Vector3[] position, TargetBillboard target)
        {
            for (int i = 0; i < position.Length; i++)
            {
                if (position[i] != beginPosition)
                {
                    position[i] += directionVector[i] * speed;
                    //Console.WriteLine("bullet: {0}",position[i]);
                    if (isHit(position[i], ref target))
                    {
                        Console.WriteLine("bullet: {0} hit target", i);
                        position[i] = beginPosition;
                    }


                    if (position[i].X >= 260 || position[i].Y >= 260 || position[i].Z >= 260
                        || position[i].X <= -20 || position[i].Y <= -20 || position[i].Z <= -20)
                        position[i] = beginPosition;

                }
            }
            GeneratePactices(position);
        }
        public void SetFirePosition(Vector3 firePosition,Vector3 fireDirection, int index)
        {
            this.billboardPosition[index] = firePosition;
            this.directionVector[index] = fireDirection;
            GeneratePactices(billboardPosition);
        }
        public bool isHit(Vector3 bulletPosition ,ref TargetBillboard target)
        {
            BoundingBox box1 = new BoundingBox(new Vector3(bulletPosition.X - this.billboardSize.X / 2, bulletPosition.Y - this.billboardSize.Y / 2, bulletPosition.Z),
                new Vector3 (bulletPosition.X + this.billboardSize.X / 2, bulletPosition.Y + this.billboardSize.Y / 2, bulletPosition.Z + 0.5f));
            
           // Console.WriteLine("bulletBox: {0}",box1);

            for (int i = 0; i < target.billboardPosition.Length; i++)
            {
                if(box1.Intersects(target.targetBox[i]))
                {
                    target.billboardPosition[i] = target.beginPosition;
                    target.GotHit();
                    Console.WriteLine("target: {0} got hit", i);
                    return true;
                }
            }
            return false;
        }
    }
}
