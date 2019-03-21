﻿using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.DataAccess.Content;
using NuciXNA.Primitives;

namespace NuciXNA.Graphics.Drawing
{
    public class TextSprite : Sprite
    {
        SpriteFont font;

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public string FontName { get; set; }

        public Colour OutlineColour { get; set; }

        // TODO: Make this a number (Outline size)
        /// <summary>
        /// Gets or sets a value indicating whether the text of the <see cref="Sprite"/> will be outlined.
        /// </summary>
        /// <value><c>true</c> if the text is outlined; otherwise, <c>false</c>.</value>
        public FontOutline FontOutline { get; set; }

        /// <summary>
        /// Gets or sets the text horizontal alignment.
        /// </summary>
        /// <value>The text horizontal alignment.</value>
        public HorizontalAlignment TextHorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the text vertical alignment.
        /// </summary>
        /// <value>The text vertical alignment.</value>
        public VerticalAlignment TextVerticalAlignment { get; set; }

        /// <summary>
        /// Gets the covered screen area.
        /// </summary>
        /// <value>The covered screen area.</value>
        public override Rectangle2D ClientRectangle
            => new Rectangle2D(Location, SpriteSize);

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        public TextSprite() : base()
        {
            OutlineColour = Colour.Black;
            Text = string.Empty;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            if (string.IsNullOrWhiteSpace(Text))
            {
                Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(FontName))
            {
                font = NuciContentManager.Instance.LoadSpriteFont("Fonts/" + FontName);
            }

            if (SpriteSize == Size2D.Empty)
            {
                Size2D size;
                
                if (!string.IsNullOrEmpty(Text))
                {
                    size = new Size2D(
                        (int)font.MeasureString(Text).X,
                        (int)font.MeasureString(Text).Y);
                }
                else
                {
                    size = new Size2D(1, 1);
                }

                SpriteSize = size;
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            Text = string.Empty;
            font = null;
        }

        /// <summary>
        /// Draws the content.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return;
            }
            
            StringDrawer.Draw(
                spriteBatch,
                font,
                WrapText(font, Text, SpriteSize.Width),
                ClientRectangle,
                Tint,
                OutlineColour,
                Opacity,
                TextHorizontalAlignment,
                TextVerticalAlignment,
                FontOutline);
        }

        /// <summary>
        /// Wraps the text on multiple lines.
        /// </summary>
        /// <returns>The text.</returns>
        /// <param name="font">Font.</param>
        /// <param name="text">Text.</param>
        /// <param name="maxLineWidth">Maximum line width.</param>
        string WrapText(SpriteFont font, string text, float maxLineWidth)
        {
            if (font.MeasureString(text).X <= maxLineWidth)
            {
                return text;
            }

            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = font.MeasureString(word);

                if (word.Contains("\r"))
                {
                    lineWidth = 0f;
                    sb.Append("\r \r");
                }

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }

                else
                {
                    if (size.X > maxLineWidth)
                    {
                        if (sb.ToString() == " ")
                        {
                            sb.Append(WrapText(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                        else
                        {
                            sb.Append("\n" + WrapText(font, word.Insert(word.Length / 2, " ") + " ", maxLineWidth));
                        }
                    }
                    else
                    {
                        sb.Append("\n" + word + " ");
                        lineWidth = size.X + spaceWidth;
                    }
                }
            }

            return sb.ToString();
        }
    }
}
