#region File Description
//-----------------------------------------------------------------------------
// AnimatedTexture.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AnimatedSprite
{
    public class AnimatedTexture
    {
        private int columnframecount;
        private int rowframecount;
        private Texture2D myTexture;
        private float TimePerFrame;
        private int columnFrame;
        private int rowFrame;
        private float TotalElapsed;
        private bool Paused;

        public float Rotation, Scale, Depth;
        public Vector2 Origin;
        public AnimatedTexture(Vector2 origin, float rotation, float scale, float depth)
        {
            this.Origin = origin;
            this.Rotation = rotation;
            this.Scale = scale;
            this.Depth = depth;
        }
        public void Load(ContentManager content, string asset, int columnframeCount, int rowframeCount, int framesPerSec)
        {
            this.columnframecount = columnframeCount;
            this.rowframecount = rowframeCount;
            myTexture = content.Load<Texture2D>("Character/Ant-Man/Ant-Man_Ant-Man-Greatest_Allies");
            TimePerFrame = (float)1 / framesPerSec;
            columnFrame = 0;
            rowFrame = 0;
            TotalElapsed = 0;
            Paused = false;
        }

        // class AnimatedTexture
        public void UpdateFrame(float elapsed)
        {
            if (Paused)
                return;
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                columnFrame++;
                if(columnFrame == columnframecount) rowFrame++;
                // Keep the Frame between 0 and the total frames, minus one.
                columnFrame = columnFrame % columnframecount;
                rowFrame = rowFrame % rowframecount;
                TotalElapsed -= TimePerFrame;
            }
        }

        // class AnimatedTexture
        public void DrawFrame(SpriteBatch batch, Vector2 screenPos)
        {
            DrawFrame(batch, columnFrame, rowFrame, screenPos);
        }
        public void DrawFrame(SpriteBatch batch, int columnframe, int rowframe, Vector2 screenPos)
        {
            int framewidth = myTexture.Width / columnframecount;
            int framehigth = myTexture.Height / rowframecount;
            Rectangle sourcerect = new Rectangle(framewidth * columnframe, framehigth * rowframe, framewidth, framehigth);
            batch.Draw(myTexture, screenPos, sourcerect, Color.White, Rotation, Origin, Scale, SpriteEffects.None, Depth);
        }

        public bool IsPaused
        {
            get { return Paused; }
        }
        public void Reset()
        {
            columnFrame = 0;
            TotalElapsed = 0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            Paused = false;
        }
        public void Pause()
        {
            Paused = true;
        }

    }
}
