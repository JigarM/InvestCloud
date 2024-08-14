using System.Reflection;

namespace InvestCloud.Utils.FontHelpers;

    public enum IconSourceType
    {
        FontAwesomeRegular,
        FontAwesomeSolid
    }

    public static class FontSource
    {
        public static FontImageSource GetFontImageSource(IconSourceType source, string glyph, Color color, double size = 30)
        {
            switch (source)
            {
                case IconSourceType.FontAwesomeRegular:
                    return new FontAwesomeRegularIconSource { Color = color, Glyph = glyph, Size = size };

                case IconSourceType.FontAwesomeSolid:
                    return new FontAwesomeSolidIconSource { Color = color, Glyph = glyph, Size = size };

                default:
                    return null;
            }
        }
    }

    public class FontImageSourceExtended : FontImageSource
    {
        protected string m_Icon;

        protected void SetIcon(string icon, Type enumeration)
        {
            m_Icon = icon;

            var fields = enumeration.GetFields(BindingFlags.Public | BindingFlags.Static);
            var field = fields.FirstOrDefault(p => p.Name == icon);
            if (field != null)
            {
                Glyph = (string)field.GetValue(null);
            }

            Size = 30;
        }
    }

    public class FontAwesomeIconSource : FontImageSourceExtended
    {
        public enum FontAwesomeType
        {
            FontAwesomeSolid,
            FontAwesomeRegular,
            FontAwesomeBrands
        }

        public FontAwesomeIconSource(FontAwesomeType type)
        {
            FontFamily = type.ToString();
        }

        public string Icon
        {
            get => m_Icon;
            set
            {
                SetIcon(value, typeof(FontAwesomeIcons));

                if (Glyph == null && value != null)
                {
                    Glyph = value;
                }
            }
        }
    }

    public class FontAwesomeSolidIconSource : FontAwesomeIconSource
    {
        public FontAwesomeSolidIconSource() : base(FontAwesomeType.FontAwesomeSolid) { }
    }

    public class FontAwesomeRegularIconSource : FontAwesomeIconSource
    {
        public FontAwesomeRegularIconSource() : base(FontAwesomeType.FontAwesomeRegular) { }
    }
