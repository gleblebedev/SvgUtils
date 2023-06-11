using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Text;
using Svg;

namespace SvgUtils;

public class ThemeBuilder
{
    private readonly DefinitionListHelper _defs;
    private readonly SvgGroup _group;
    private readonly SvgDocument _doc;
    private readonly StringBuilder _theme = new StringBuilder();
    private readonly StringBuilder _css = new StringBuilder();
    public ThemeBuilder(Vector2 atlasSize)
    {
        var definitionList = new SvgDefinitionList();
        _group = new SvgGroup();
        _defs = new DefinitionListHelper(definitionList);

        _doc = new SvgDocument()
        {
            Width = atlasSize.X,
            Height = atlasSize.Y,
            ViewBox = new SvgViewBox(0, 0, atlasSize.X, atlasSize.Y)
        };
        _doc.Children.Add(definitionList);
        _doc.Children.Add(_group);

    }

    public void Save(string name = "casual")
    {
        _doc.Write($"{name}.svg");
        _doc.Draw()?.Save($"{name}.png");
        using (var file = File.Create($"{name}.rcss"))
        {
            using (var writer = new StreamWriter(file, new UTF8Encoding(false)))
            {
                writer.WriteLine("@spritesheet theme");
                writer.WriteLine("{");
                writer.WriteLine($"    src: {name}.png;");
                writer.WriteLine(_theme.ToString());
                writer.WriteLine("}");
                writer.WriteLine(_css);
            }
        }
    }

    public void Add(SvgElement path)
    {
        _group.Children.Add(path);
    }

    public void AddThemeSprite(string name, Rect rect)
    {
        _theme.AppendLine($"    {name}: {rect.Position.X}px {rect.Position.Y}px {rect.Size.X}px {rect.Size.Y}px;");
    }

    public ThemeBuilder WriteCssLine(string str)
    {
        _css.AppendLine(str);
        return this;
    }
    public ThemeBuilder WriteCss(string str)
    {
        _css.Append(str);
        return this;
    }

    public ThemeBuilder WriteCssFormat([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string format,
        params object?[] args)
    {
        _css.AppendFormat(format, args);
        return this;
    }
}