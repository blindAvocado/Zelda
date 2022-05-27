using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Zelda
{
    public class Resources
    {
        public static Dictionary<string, Texture2D> Images;
        public static Dictionary<string, SoundEffect> Sounds;
        public static Dictionary<string, SpriteFont> Fonts;

        public static void LoadImages(ContentManager content)
        {
            Images = new Dictionary<string, Texture2D>();

            List<string> imagesName = new List<string>()
            {
                "link",
                "room_border",
                "tiles",
                "gui",
                "keyboard",
                "heart",
                "items",
                "projectiles",
                "logo",
                "select",
                "enemy_octorok",
                "enemy_moblin",
                "doors"
            };

            foreach (string img in imagesName)
            {
                Images.Add(img, content.Load<Texture2D>("graphics/" + img));
            }
        }

        public static void LoadSounds(ContentManager content)
        {
            Sounds = new Dictionary<string, SoundEffect>();

            List<string> soundsName = new List<string>()
            {

            };

            foreach (string sfx in soundsName)
            {
                Images.Add(sfx, content.Load<Texture2D>("sounds/" + sfx));
            }
        }

        public static void LoadFonts(ContentManager content)
        {
            Fonts = new Dictionary<string, SpriteFont>();

            List<string> fontsName = new List<string>()
            {
                "Font",
                "Bold",
                "Normal"
            };

            foreach (string font in fontsName)
            {
                Fonts.Add(font, content.Load<SpriteFont>("fonts/" + font));
            }
        }
    }
}
