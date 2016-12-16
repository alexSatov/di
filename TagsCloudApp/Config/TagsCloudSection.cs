using System.Configuration;

namespace TagsCloudApp.Config
{
    public class TagsCloudSection : ConfigurationSection
    {
        [ConfigurationProperty("maxTagsCount", DefaultValue = "100")]
        public int MaxTagsCount => (int)this["maxTagsCount"];

        [ConfigurationProperty("font")]
        public FontElement Font => (FontElement)this["font"];

        [ConfigurationProperty("color")]
        public ColorElement Color => (ColorElement)this["color"];

        [ConfigurationProperty("imageSize")]
        public ImageElement ImageSize => (ImageElement)this["imageSize"];

        [ConfigurationProperty("centerPoint")]
        public CenterElement CenterPoint => (CenterElement)this["centerPoint"];
    }

    public class FontElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "TimesNewRoman")]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public string Name => (string) this["name"];

        [ConfigurationProperty("minsize", DefaultValue = "10")]
        [IntegerValidator(MinValue = 1, MaxValue = 49)]
        public int MinSize => (int)this["minsize"];

        [ConfigurationProperty("maxsize", DefaultValue = "50")]
        [IntegerValidator(MinValue = 50, MaxValue = 100)]
        public int MaxSize => (int)this["maxsize"];
    }

    public class ColorElement : ConfigurationElement
    {
        [ConfigurationProperty("background", DefaultValue = "FFFFFF")]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6)]
        public string Background => (string)this["background"];

        [ConfigurationProperty("tag", DefaultValue = "000000")]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6)]
        public string Tag => (string)this["tag"];
    }

    public class ImageElement : ConfigurationElement
    {
        [ConfigurationProperty("width", DefaultValue = "800")]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int Width => (int)this["width"];

        [ConfigurationProperty("height", DefaultValue = "800")]
        [IntegerValidator(MinValue = 1, MaxValue = 9999)]
        public int Height => (int)this["height"];
    }

    public class CenterElement : ConfigurationElement
    {
        [ConfigurationProperty("x", DefaultValue = "0")]
        [IntegerValidator(MinValue = 0, MaxValue = 9998)]
        public int X => (int)this["x"];

        [ConfigurationProperty("y", DefaultValue = "0")]
        [IntegerValidator(MinValue = 0, MaxValue = 9998)]
        public int Y => (int)this["y"];
    }
}