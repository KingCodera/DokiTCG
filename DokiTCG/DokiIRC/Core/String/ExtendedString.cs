using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class ExtendedString
    {
        public enum COLOURS
        {
            WHITE,
            BLACK,
            NAVY,
            GREEN,
            RED,
            MAROON,
            PURPLE,
            ORANGE,
            YELLOW,
            LIME,
            TEAL,
            CYAN,
            BLUE,
            PINK,
            GREY,
            SILVER,
            NONE
        }

        public const char BOLD = '\x02';
        public const char ITALIC = '\x1F';
        public const char COLOUR = '\x03';
        public const char RESET = '\x0F'; // I'm not sure if I'll use this :D
        public const char REVERSE = '\x16';

        // This is where the magic happens.

        public static string CustomColour(this String arg, int foreground, int background = -1)
        {
            if (background < 0)
            {
                return arg.CustomColour(foreground.ToString());
            }
            else
            {
                return arg.CustomColour(foreground.ToString(), background.ToString());
            }
        }

        public static string CustomColour(this String arg, string foreground, string background = null)
        {
            if (background == null)
            {
                return COLOUR + foreground + arg + COLOUR;
            }
            else
            {
                return COLOUR + foreground + "," + background + arg + COLOUR;
            }
        }

        private static string ApplyCode(this String arg, char code)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}{1}{0}", code, arg, code);
            return sb.ToString();
        }

        public static string Bold(this String arg)
        {
            return arg.ApplyCode(BOLD);
        }

        public static string Italic(this String arg)
        {
            return arg.ApplyCode(ITALIC);
        }

        public static string Reverse(this String arg)
        {
            return arg.ApplyCode(REVERSE);
        }

        public static string Colour(this String arg, COLOURS foreground, COLOURS background = COLOURS.NONE)
        {
            StringBuilder sb = new StringBuilder();
            if (background == COLOURS.NONE)
            {
                sb.AppendFormat("{0}{1}{2}{0}", COLOUR, ColoursToString(foreground), arg);
                return sb.ToString();
            }
            else
            {
                sb.AppendFormat("{0}{1},{2}{3}{0}", COLOUR, ColoursToString(foreground), ColoursToString(background), arg);
                return sb.ToString();
            }
        }

        public static string White(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.WHITE, background); }

        public static string Black(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.BLACK, background); }

        public static string Navy(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.NAVY, background); }

        public static string Green(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.GREEN, background); }

        public static string Red(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.RED, background); }

        public static string Maroon(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.MAROON, background); }

        public static string Purple(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.PURPLE, background); }

        public static string Orange(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.ORANGE, background); }

        public static string Yellow(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.YELLOW, background); }

        public static string Lime(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.LIME, background); }

        public static string Teal(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.TEAL, background); }

        public static string Cyan(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.CYAN, background); }

        public static string Blue(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.BLUE, background); }

        public static string Pink(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.PINK, background); }

        public static string Grey(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.GREY, background); }

        public static string Silver(this String arg, COLOURS background = COLOURS.NONE)
        { return arg.Colour(COLOURS.SILVER, background); }

        private static string ColoursToString(COLOURS arg)
        {
            switch (arg)
            {
                case COLOURS.WHITE: return "00";
                case COLOURS.BLACK: return "01";
                case COLOURS.NAVY: return "02";
                case COLOURS.GREEN: return "03";
                case COLOURS.RED: return "04";
                case COLOURS.MAROON: return "05";
                case COLOURS.PURPLE: return "06";
                case COLOURS.ORANGE: return "07";
                case COLOURS.YELLOW: return "08";
                case COLOURS.LIME: return "09";
                case COLOURS.TEAL: return "10";
                case COLOURS.CYAN: return "11";
                case COLOURS.BLUE: return "12";
                case COLOURS.PINK: return "13";
                case COLOURS.GREY: return "14";
                case COLOURS.SILVER: return "15";
                default: return "00";
            }
        }
    }
}