﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Localization;
using SteelSeries.GameSense;

namespace Fargowiltas.Common.Systems
{
    public class InstaDrawPlayer : ModPlayer
    {
        public bool Draw = false;
        public Vector2 DrawPosition = Vector2.Zero;
        public Vector2 Scale = Vector2.Zero;

        public override void ResetEffects()
        {
            Draw = false;
            DrawPosition = Vector2.Zero;
            Scale = Vector2.Zero;
        }
        public override void UpdateDead()
        {
            ResetEffects();
        }
    }
    public class InstaVisual : ModSystem
    {
        public enum DrawOrigin
        {
            Center,
            TopLeft,
            Top,
            TopRight,
            Left,
            Right,
            BottomLeft,
            Bottom,
            BottomRight
        }
        public static void DrawInstaVisual(Player player, Vector2 drawPosition, Vector2 scale, DrawOrigin drawOrigin = DrawOrigin.Center)
        {
            InstaDrawPlayer drawPlayer = player.GetModPlayer<InstaDrawPlayer>();
            drawPlayer.Draw = true;
            drawPlayer.DrawPosition = drawPosition;
            Vector2 right = Vector2.UnitX * (scale.X * 8 - 16);
            Vector2 left = -Vector2.UnitX * scale.X * 8;
            Vector2 y = Vector2.UnitY * scale.Y * 8;
            drawPlayer.DrawPosition -= drawOrigin switch
            {
                DrawOrigin.TopLeft => left - y,
                DrawOrigin.Top => -y,
                DrawOrigin.TopRight => right - y,
                DrawOrigin.Left => left,
                DrawOrigin.Right => right,
                DrawOrigin.BottomLeft => left + y,
                DrawOrigin.Bottom => y,
                DrawOrigin.BottomRight => right + y,
                _ => Vector2.Zero
            };
            drawPlayer.Scale = scale;
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 1"));
            if (index != -1)
            {
                layers.Insert(index, new LegacyGameInterfaceLayer(
                    "Fargowiltas: Insta Item Visual",
                    delegate {
                        DrawVisual(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.Game)
                );
            }
        }
        public static void DrawVisual(SpriteBatch spriteBatch)
        {
            InstaDrawPlayer drawPlayer = Main.LocalPlayer.GetModPlayer<InstaDrawPlayer>();
            if (drawPlayer.Draw)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Fargowiltas/Assets/InstaVisualSquare").Value;
                Vector2 drawPos = drawPlayer.DrawPosition.ToTileCoordinates().ToWorldCoordinates() - Main.screenPosition - Vector2.One * 8;
                drawPos -= drawPlayer.Scale * 8f;
                if (drawPlayer.Scale.X % 2 != 0)
                    drawPos.X += 8f;
                if (drawPlayer.Scale.Y % 2 != 0)
                    drawPos.Y += 8f;
                spriteBatch.Draw(texture, drawPos, null, Color.Black with { A = 100 }, 0f, Vector2.Zero, drawPlayer.Scale, SpriteEffects.None, 0f);
            }
        }
    }
    /*
    internal class InstaItemVisual : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;

        public override void OnInitialize()
        {
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding.
            area = new UIElement();
            area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
            area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
            area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("ExampleMod/Common/UI/ExampleResourceUI/ExampleResourceFrame")); // Frame of our resource bar
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            text = new UIText("0/0", 0.8f); // text to show stat
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(40, 0f);
            text.Left.Set(0, 0f);

            gradientA = new Color(123, 25, 138); // A dark purple
            gradientB = new Color(187, 91, 201); // A light purple

            area.Append(text);
            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon
            if (Main.LocalPlayer.HeldItem.ModItem is not ExampleCustomResourceWeapon)
                return;

            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<ExampleResourcePlayer>();
            // Calculate quotient
            float quotient = (float)modPlayer.exampleResourceCurrent / modPlayer.exampleResourceMax2; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 8;
            hitbox.Height -= 16;

            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                // float percent = (float)i / steps; // Alternate Gradient Approach
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is not ExampleCustomResourceWeapon)
                return;

            var modPlayer = Main.LocalPlayer.GetModPlayer<ExampleResourcePlayer>();
            // Setting the text per tick to update and show our resource values.
            text.SetText(ExampleResourceUISystem.ExampleResourceText.Format(modPlayer.exampleResourceCurrent, modPlayer.exampleResourceMax2));
            base.Update(gameTime);
        }
    }
    */
}
