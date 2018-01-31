﻿using System;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;

namespace NuciXNA.Gui.GuiElements
{
    /// <summary>
    /// Menu toggle GUI element.
    /// </summary>
    public class GuiMenuToggle : GuiMenuItem
    {
        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>The type of the property.</value>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the toggle state.
        /// </summary>
        /// <value>The type of the toggle state.</value>
        [XmlIgnore]
        public bool ToggleState { get; set; }

        string originalText;

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            originalText = Text;
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
            if (ToggleState)
            {
                Text = originalText + " : On";
            }
            else
            {
                Text = originalText + " : Off";
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Fired by the Activated event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        protected override void OnActivated(object sender, EventArgs e)
        {
            base.OnActivated(sender, e);

            ToggleState = !ToggleState;
        }
    }
}
