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
        public float speed = 0.1f;
        Vector3 directionVector;
        float timeLife;

        public Bullet(GraphicsDevice graphicDevice, ContentManager content,
            Texture2D texture, Vector2 billboardSize, Vector3[] billboardsPosition):
            base(graphicDevice,content,texture,billboardSize,billboardsPosition)
        {

        }
        public void Update(Vector3 directionVector)
        {
            this.Move(this.billboardPosition, directionVector);
        }
        private void Move(Vector3[] position, Vector3 directionVector)
        {
            for (int i = 0; i < position.Length; i++)
            {
                position[i] += directionVector * speed;
            }
            GeneratePactices(position);
        }
    }
}
