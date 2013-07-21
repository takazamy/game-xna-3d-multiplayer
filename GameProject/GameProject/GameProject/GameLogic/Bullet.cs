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
        public float speed;
        Vector3 directionVector;
        float timeLife;

        public Bullet(GraphicsDevice graphicDevice, ContentManager content,
            Texture2D texture, Vector2 billboardSize, Vector3[] billboardsPosition):
            base(graphicDevice,content,texture,billboardSize,billboardsPosition)
        {

        }
        public void Update(GameTime gameTime, Vector3 directionVector)
        {

        }
    }
}
