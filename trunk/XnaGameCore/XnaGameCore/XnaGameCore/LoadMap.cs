using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    public class LoadMap
    {
        GraphicsDevice graphicDevice;
        int WIDTH, HEIGHT;
        int[,] heightData;
        VertexPositionTexture[] vertices;
        VertexBuffer vertexB;
        IndexBuffer indexB;
        Texture2D texture;

        public LoadMap(string heightDataAsset, string textureAsset,Game game)
        {
            this.graphicDevice = game.GraphicsDevice;
            texture = game.Content.Load<Texture2D>(textureAsset);

            int offset;
            FileStream fs = new FileStream(heightDataAsset, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            
            reader.BaseStream.Seek(10, SeekOrigin.Current);
            offset = (int)reader.ReadUInt32();

            reader.BaseStream.Seek(4, SeekOrigin.Current);
            WIDTH = (int)reader.ReadUInt32();
            HEIGHT = (int)reader.ReadUInt32();

            reader.BaseStream.Seek(offset - 26, SeekOrigin.Current);
            heightData = new int[WIDTH, HEIGHT];
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH ; j++)
                {
                    int height = (int)(reader.ReadByte());
                    //height += (int)(reader.ReadByte());
                    //height += (int)(reader.ReadByte());
                 //   height /= 15;
                    heightData[WIDTH - 1 - j, HEIGHT - 1 - i] = height;
                }
            }

            AddVertices();
            IndexSetup();
        }

        private void AddVertices()
        {
            vertices = new VertexPositionTexture[WIDTH * HEIGHT];

            for (int i = 0; i < WIDTH; i++)
            {
                for (int j = 0; j < HEIGHT; j++)
                {
                    vertices[i + j * WIDTH].Position = new Vector3(i*2, heightData[i, j],j*2 );
                    if ((i % 2 == 0) && (j % 2 == 0))
                    {
                        vertices[i + j * WIDTH].TextureCoordinate = new Vector2(0f, 0f);
                    }
                    else if ((i % 2 == 0) && (j % 2 != 0))
                    {
                        vertices[i + j * WIDTH].TextureCoordinate = new Vector2(0f, 1f);
                    }
                    else if ((i % 2 != 0) && (j % 2 == 0))
                    {
                        vertices[i + j * WIDTH].TextureCoordinate = new Vector2(1f, 0f);
                    }
                    else if ((i % 2 != 0) && (j % 2 != 0))
                    {
                        vertices[i + j * WIDTH].TextureCoordinate = new Vector2(1f, 1f);
                    } 
                }
            }
            vertexB = new VertexBuffer(graphicDevice, VertexPositionTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vertexB.SetData<VertexPositionTexture>(vertices);
        }
        private void IndexSetup()
        {
            short[] index = new short[(WIDTH - 1) * (HEIGHT - 1) * 6];
            for (int i = 0; i < WIDTH - 1; i++)
            {
                for (int j = 0; j < HEIGHT - 1; j++)
                {

                    index[(i + j * (WIDTH - 1)) * 6] = (short)((i + 1) + (j + 1) * WIDTH);
                    index[(i + j * (WIDTH - 1)) * 6 + 1] = (short)((i + 1) + j * WIDTH);
                    index[(i + j * (WIDTH - 1)) * 6 + 2] = (short)((i + j * WIDTH));
                    index[(i + j * (WIDTH - 1)) * 6 + 3] = (short)((i + 1) + (j + 1) * WIDTH);
                    index[(i + j * (WIDTH - 1)) * 6 + 4] = (short)(i + j * WIDTH);
                    index[(i + j * (WIDTH - 1)) * 6 + 5] = (short)(i + (j + 1) * WIDTH);
                }
            }
            indexB = new IndexBuffer(graphicDevice, IndexElementSize.SixteenBits, index.Length, BufferUsage.WriteOnly);
            indexB.SetData(index);
        }
        public void DrawMap(Effect mapEffect, string technique, Matrix view, Matrix projection, Matrix world)
        {
            graphicDevice.SetVertexBuffer(vertexB);
            graphicDevice.Indices = indexB;
            mapEffect.CurrentTechnique = mapEffect.Techniques[technique];
            mapEffect.Parameters["xView"].SetValue(view);
            mapEffect.Parameters["xProjection"].SetValue(projection);
            mapEffect.Parameters["xWorld"].SetValue(world);
            mapEffect.Parameters["xTexture"].SetValue(texture);
          //  mapEffect.Parameters["WorldInverseTranspose"].SetValue(Matrix.Transpose(Matrix.Invert(world)));
            foreach (EffectPass pass in mapEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, WIDTH * HEIGHT, 0, (WIDTH - 1) * (HEIGHT - 1) * 2);
            }
        }
    }
}
