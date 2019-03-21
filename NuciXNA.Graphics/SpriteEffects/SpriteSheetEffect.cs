﻿using Microsoft.Xna.Framework;
using NuciXNA.Graphics.Drawing;
using NuciXNA.Primitives;

namespace NuciXNA.Graphics.SpriteEffects
{
    public abstract class SpriteSheetEffect : NuciSpriteEffect<TextureSprite>
    {
        public int FrameCounter { get; set; }

        public int SwitchFrame { get; set; }

        public Point2D CurrentFrame { get; set; }

        public Size2D FrameAmount { get; set; }
        
        public Size2D FrameSize { get; private set; }

        public SpriteSheetEffect()
        {
            FrameCounter = 0;
            SwitchFrame = 100;
            CurrentFrame = Point2D.Empty;
            FrameAmount = Size2D.Empty;
            FrameSize = Size2D.Empty;
        }

        public override void LoadContent(TextureSprite sprite)
        {
            base.LoadContent(sprite);

            FrameSize = Sprite.TextureSize / FrameAmount;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Active)
            {
                UpdateFrame(gameTime);
            }
        }

        public abstract void UpdateFrame(GameTime gameTime);
    }
}
