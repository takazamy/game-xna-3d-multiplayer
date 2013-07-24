using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using XnaGameCore;

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
        public void Update(GameTime gameTime)
        {
            this.Move(this.billboardPosition);
        }
        private void Move(Vector3[] position)
        {
            for (int i = 0; i < position.Length; i++)
            {
                if(position[i] != beginPosition)
                position[i] += directionVector[i] * speed;

                if (position[i].X >= 260 || position[i].Y >= 260 || position[i].Z >= 260
                    || position[i].X <= -20 || position[i].Y <= -20 || position[i].Z <= -20)
                    position[i] = beginPosition;
            }
            GeneratePactices(position);
        }
        public void SetFirePosition(Vector3 firePosition,Vector3 fireDirection, int index)
        {
            this.billboardPosition[index] = firePosition;
            this.directionVector[index] = fireDirection;
            GeneratePactices(billboardPosition);
        }
    }
}
