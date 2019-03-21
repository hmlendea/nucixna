﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using NuciXNA.Graphics.SpriteEffects;
using NuciXNA.Input;
using NuciXNA.Primitives;

namespace NuciXNA.Gui.GuiElements
{
    /// <summary>
    /// Menu item GUI element.
    /// </summary>
    public abstract class GuiMenuItem : GuiElement
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the selected text colour.
        /// </summary>
        /// <value>The selected text colour.</value>
        public Colour SelectedTextColour { get; set; }

        public virtual bool Selectable { get; }

        // TODO: Maybe implement my own handler and args
        /// <summary>
        /// Occurs when activated.
        /// </summary>
        public event EventHandler Activated;

        /// <summary>
        /// The text GUI element.
        /// </summary>
        protected GuiText text;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiMenuItem"/> class.
        /// </summary>
        public GuiMenuItem()
        {
            ForegroundColour = Colour.White;
            SelectedTextColour = Colour.Gold;

            Selectable = true;
            Size = new Size2D(512, 48);
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void DoLoadContent()
        {
            text = new GuiText
            {
                FontName = "MenuFont",
                AreEffectsActive = true,
                FadeEffect = new FadeEffect
                {
                    Speed = 2,
                    MinimumMultiplier = 0.25f
                }
            };
            text.FadeEffect.Activate();

            AddChild(text);

            SetChildrenProperties();
            RegisterEvents();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        protected override void DoUnloadContent()
        {
            UnregisterEvents();
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        protected override void DoUpdate(GameTime gameTime)
        {
            SetChildrenProperties();
        }

        /// <summary>
        /// Draws the content on the specified spriteBatch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        protected override void DoDraw(SpriteBatch spriteBatch)
        {

        }

        void SetChildrenProperties()
        {
            text.Text = Text;
            text.Size = Size;

            if (IsFocused)
            {
                text.AreEffectsActive = true;
                text.ForegroundColour = SelectedTextColour;
            }
            else
            {
                text.AreEffectsActive = false;
                text.ForegroundColour = ForegroundColour;
            }
        }

        /// <summary>
        /// Registers the events.
        /// </summary>
        void RegisterEvents()
        {
            Clicked += OnClicked;
            KeyPressed += OnKeyPressed;
            MouseEntered += OnMouseEntered;
        }

        /// <summary>
        /// Unregisters the events.
        /// </summary>
        void UnregisterEvents()
        {
            Clicked -= OnClicked;
            KeyPressed -= OnKeyPressed;
            MouseEntered -= OnMouseEntered;
        }

        void OnClicked(object sender, MouseButtonEventArgs e)
        {
            Activated?.Invoke(this, EventArgs.Empty);
        }

        void OnKeyPressed(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Keys.Enter || e.Key == Keys.E)
            {
                Activated?.Invoke(this, EventArgs.Empty);
            }
        }

        void OnMouseEntered(object sender, MouseEventArgs e)
        {
            // TODO: Play selection sound
            GuiManager.Instance.FocusElement(this);
        }
    }
}
