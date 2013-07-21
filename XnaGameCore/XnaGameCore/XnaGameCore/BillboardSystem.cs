using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace XnaGameCore
{
    public class BillboardSystem
    {
        VertexBuffer vertexB;
        IndexBuffer indexB;
        VertexPositionTexture[] vertices;
        short[] indices;

        int numBillboards;
        Vector2 billboardSize;
        Texture2D texture;

        GraphicsDevice graphicDevice;
        Effect effect;

        public bool ensureOcclusion = true;
        public enum BillboardMode { Cylindrical, Spherical };
        public BillboardMode mode = BillboardMode.Spherical;

        public BillboardSystem(GraphicsDevice graphicDevice, ContentManager content,
            Texture2D texture, Vector2 billboardSize, Vector3[] billboardsPosition)
        {
            this.graphicDevice = graphicDevice;
            this.billboardSize = billboardSize;
            this.texture = texture;
            this.numBillboards = billboardsPosition.Length;

            this.effect = content.Load<Effect>("BillboardEffect");

            GeneratePactices(billboardsPosition);
        }

        private void GeneratePactices(Vector3[] billboardPosition)
        {
            vertices = new VertexPositionTexture[numBillboards * 4];
            indices = new short[numBillboards * 6];
            int x = 0;

            for (int i = 0; i < vertices.Length; i += 4)
            {
                Vector3 position = billboardPosition[i / 4];

                vertices[i + 0] = new VertexPositionTexture(position, new Vector2(0, 0));
                vertices[i + 1] = new VertexPositionTexture(position, new Vector2(0, 1));
                vertices[i + 2] = new VertexPositionTexture(position, new Vector2(1, 1));
                vertices[i + 3] = new VertexPositionTexture(position, new Vector2(1, 0));

                indices[x++] = (short)(i + 0);
                indices[x++] = (short)(i + 3);
                indices[x++] = (short)(i + 2);
                indices[x++] = (short)(i + 2);
                indices[x++] = (short)(i + 1);
                indices[x++] = (short)(i + 0);
            }
            vertexB = new VertexBuffer(graphicDevice, typeof(VertexPositionTexture), vertices.Length, BufferUsage.WriteOnly);
            vertexB.SetData<VertexPositionTexture>(vertices);

            indexB = new IndexBuffer(graphicDevice, IndexElementSize.SixteenBits, indices.Length, BufferUsage.WriteOnly);
            indexB.SetData<short>(indices);
        }

        public void Draw(Matrix view, Matrix projection, Vector3 up, Vector3 right)
        {
            graphicDevice.SetVertexBuffer(vertexB);
            graphicDevice.Indices = indexB;
            graphicDevice.BlendState = BlendState.AlphaBlend;

            SetEffectParameters(view, projection, up, right);
            if (ensureOcclusion)
            {
                DrawOpaquePixel();
                DrawTransparentPixel();
            }
            else
            {
                graphicDevice.DepthStencilState = DepthStencilState.DepthRead;


                effect.Parameters["AlphaTest"].SetValue(false);
                DrawBillboard();
            }

            // Reset render states
            graphicDevice.BlendState = BlendState.Opaque;
            graphicDevice.DepthStencilState = DepthStencilState.Default;

            // Un-set the vertex and index buffer
            graphicDevice.SetVertexBuffer(null);
            graphicDevice.Indices = null;

        }

        private void DrawOpaquePixel()
        {
            graphicDevice.DepthStencilState = DepthStencilState.Default;
            effect.Parameters["AlphaTest"].SetValue(true);
            effect.Parameters["AlphaTestGreater"].SetValue(true);
            DrawBillboard();
        }
        private void DrawTransparentPixel()
        {
            graphicDevice.DepthStencilState = DepthStencilState.DepthRead;
            effect.Parameters["AlphaTest"].SetValue(true);
            effect.Parameters["AlphaTestGreater"].SetValue(false);
            DrawBillboard();
        }
        private void DrawBillboard()
        {
            graphicDevice.BlendState = BlendState.AlphaBlend;

            effect.CurrentTechnique.Passes[0].Apply();
            graphicDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                4 * numBillboards, 0, numBillboards * 2);

            graphicDevice.BlendState = BlendState.Opaque;
        }
        void SetEffectParameters(Matrix View, Matrix Projection, Vector3 Up, Vector3 Right)
        {
            effect.Parameters["ParticleTexture"].SetValue(texture);
            effect.Parameters["View"].SetValue(View);
            effect.Parameters["Projection"].SetValue(Projection);
            effect.Parameters["Size"].SetValue(billboardSize / 2f);
            effect.Parameters["Up"].SetValue(mode == BillboardMode.Spherical ? Up : Vector3.Up);
            effect.Parameters["Side"].SetValue(Right);
        }
    }
}
