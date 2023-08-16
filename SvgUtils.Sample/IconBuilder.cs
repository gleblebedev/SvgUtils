using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using Svg;
using SvgUtils;

namespace SvgUtils;

public class IconBuilder : SvgAtlasItem
{
    private readonly UIConfiguration _config;
    private readonly Color _color;
    private readonly Color _fontColor;
    private readonly string _name;
    private readonly IconLayout _layout;

    private readonly PathSet _icon;

    public static PathSet XBox = new PathSet(new Path[]
    {
        new Path(new PathPoint[]
        {
            new PathPoint(null, new Vector2(0.45188716f, 0.99941605f), new Vector2(0.37423265f, 0.99197865f)),
            new PathPoint(new Vector2(0.2956123f, 0.9640909f), new Vector2(0.22807433f, 0.92002636f),
                new Vector2(0.17147954f, 0.8831016f)),
            new PathPoint(new Vector2(0.15869915f, 0.8679225f), new Vector2(0.15869915f, 0.8376301f),
                new Vector2(0.15869915f, 0.7767823f)),
            new PathPoint(new Vector2(0.22560231f, 0.6702088f), new Vector2(0.34006953f, 0.5487158f),
                new Vector2(0.40507904f, 0.479716f)),
            new PathPoint(new Vector2(0.49563295f, 0.39883828f), new Vector2(0.5054261f, 0.4010284f),
                new Vector2(0.52446103f, 0.4052853f)),
            new PathPoint(new Vector2(0.6766656f, 0.5537499f), new Vector2(0.7336441f, 0.6236386f),
                new Vector2(0.8237459f, 0.7341555f)),
            new PathPoint(new Vector2(0.8651685f, 0.8246448f), new Vector2(0.84412456f, 0.86498785f),
                new Vector2(0.8281278f, 0.8956551f)),
            new PathPoint(new Vector2(0.7288677f, 0.9555935f), new Vector2(0.6559446f, 0.97862077f),
                new Vector2(0.5958424f, 0.99759936f)),
            new PathPoint(new Vector2(0.51690894f, 1.0056434f), new Vector2(0.45188713f, 0.99941593f), null)
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.10155419f, 0.8039636f), new Vector2(0.082261406f, 0.7743659f),
                new Vector2(0.03523238f, 0.7022169f)),
            new PathPoint(new Vector2(0.011471769f, 0.6311872f), new Vector2(0f, 0.5284544f),
                new Vector2(-0.0037880058f, 0.49453163f)),
            new PathPoint(new Vector2(-0.0024305808f, 0.47512823f), new Vector2(0.008601572f, 0.4055009f),
                new Vector2(0.022351583f, 0.31872022f)),
            new PathPoint(new Vector2(0.071771584f, 0.21832633f), new Vector2(0.13115434f, 0.15654187f),
                new Vector2(0.15644592f, 0.1302274f)),
            new PathPoint(new Vector2(0.15870471f, 0.12958628f), new Vector2(0.18953347f, 0.13997173f),
                new Vector2(0.2269713f, 0.15258363f)),
            new PathPoint(new Vector2(0.26695192f, 0.18019567f), new Vector2(0.32895625f, 0.23626207f), null),
            new PathPoint(new Vector2(0.36513293f, 0.2689743f)),
            new PathPoint(null, new Vector2(0.34537786f, 0.2932436f), new Vector2(0.25367418f, 0.40590245f)),
            new PathPoint(new Vector2(0.15686779f, 0.56559247f), new Vector2(0.12038442f, 0.6643887f),
                new Vector2(0.1005506f, 0.7180983f)),
            new PathPoint(new Vector2(0.09255086f, 0.7720124f), new Vector2(0.101084776f, 0.7944584f),
                new Vector2(0.10684645f, 0.80961275f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(null, new Vector2(0.90776616f, 0.786637f), new Vector2(0.9124121f, 0.7639572f)),
            new PathPoint(new Vector2(0.9065359f, 0.72230405f), new Vector2(0.8927641f, 0.68029326f),
                new Vector2(0.8629384f, 0.58931094f)),
            new PathPoint(new Vector2(0.7632467f, 0.4200539f), new Vector2(0.6717035f, 0.3049755f), null),
            new PathPoint(new Vector2(0.6428858f, 0.268749f)),
            new PathPoint(null, new Vector2(0.67406297f, 0.24012151f), new Vector2(0.7147711f, 0.20274249f)),
            new PathPoint(new Vector2(0.74303484f, 0.1803605f), new Vector2(0.7735307f, 0.16135326f),
                new Vector2(0.7975956f, 0.14635426f)),
            new PathPoint(new Vector2(0.8319848f, 0.13307646f), new Vector2(0.8467668f, 0.13307646f),
                new Vector2(0.85588044f, 0.13307646f)),
            new PathPoint(new Vector2(0.8879648f, 0.16637309f), new Vector2(0.91386575f, 0.20271048f),
                new Vector2(0.95398086f, 0.25898954f)),
            new PathPoint(new Vector2(0.9834922f, 0.3272121f), new Vector2(0.9984437f, 0.39823258f),
                new Vector2(1.0081043f, 0.44412112f)),
            new PathPoint(new Vector2(1.0089093f, 0.54234684f), new Vector2(1f, 0.5881225f),
                new Vector2(0.9926884f, 0.6256888f)),
            new PathPoint(new Vector2(0.97725004f, 0.674418f), new Vector2(0.9621893f, 0.7074666f),
                new Vector2(0.95090455f, 0.7322293f)),
            new PathPoint(new Vector2(0.9228378f, 0.78032184f), new Vector2(0.91053814f, 0.79597104f),
                new Vector2(0.9042147f, 0.8040165f)),
            new PathPoint(new Vector2(0.9042095f, 0.80399895f), new Vector2(0.90776604f, 0.786637f), null)
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(null, new Vector2(0.46020153f, 0.11589605f), new Vector2(0.4179633f, 0.094446816f)),
            new PathPoint(new Vector2(0.35280308f, 0.0714224f), new Vector2(0.3168083f, 0.065227926f),
                new Vector2(0.30418953f, 0.06305632f)),
            new PathPoint(new Vector2(0.28266373f, 0.0618452f), new Vector2(0.26897314f, 0.06253654f),
                new Vector2(0.23927484f, 0.064036235f)),
            new PathPoint(new Vector2(0.2406013f, 0.062483206f), new Vector2(0.2882434f, 0.039974615f),
                new Vector2(0.32785216f, 0.021261374f)),
            new PathPoint(new Vector2(0.36089134f, 0.010257145f), new Vector2(0.40574247f, 0.00083965994f),
                new Vector2(0.45619667f, -0.00975433f)),
            new PathPoint(new Vector2(0.55103254f, -0.009878658f), new Vector2(0.60068345f, 0.00058408827f),
                new Vector2(0.65431064f, 0.0118847415f)),
            new PathPoint(new Vector2(0.71745837f, 0.035384253f), new Vector2(0.7530414f, 0.057281904f), null),
            new PathPoint(new Vector2(0.7636171f, 0.06379011f)),
            new PathPoint(null, new Vector2(0.7393528f, 0.06256482f), new Vector2(0.69113535f, 0.060129944f)),
            new PathPoint(new Vector2(0.6208648f, 0.0796098f), new Vector2(0.54541993f, 0.11632538f),
                new Vector2(0.5226639f, 0.12739974f)),
            new PathPoint(new Vector2(0.50286657f, 0.13624454f), new Vector2(0.5014259f, 0.13598047f),
                new Vector2(0.4999854f, 0.1357164f)),
            new PathPoint(new Vector2(0.48143438f, 0.1266784f), new Vector2(0.46020153f, 0.11589604f), null)
        }) { Closed = true }
    });
    public static PathSet Settings = new PathSet(new Path[]
    {
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.62580216f, 0.9999999f), new Vector2(0.5896658f, 0.9999999f), null),
            new PathPoint(null, new Vector2(0.40898362f, 0.9999999f), new Vector2(0.37284717f, 0.9999999f)),
            new PathPoint(new Vector2(0.34245202f, 0.9736575f), new Vector2(0.3380616f, 0.9378587f), null),
            new PathPoint(null, new Vector2(0.3248904f, 0.8456602f), new Vector2(0.3117192f, 0.8389057f)),
            new PathPoint(new Vector2(0.29888573f, 0.83147573f), new Vector2(0.28638992f, 0.82337046f), null),
            new PathPoint(null, new Vector2(0.19858178f, 0.8584937f), new Vector2(0.16447166f, 0.87132716f)),
            new PathPoint(new Vector2(0.12664664f, 0.8571428f), new Vector2(0.11009818f, 0.82674766f), null),
            new PathPoint(null, new Vector2(0.021276772f, 0.67207015f), new Vector2(0.0040528774f, 0.6399864f)),
            new PathPoint(new Vector2(0.011482775f, 0.6018237f), new Vector2(0.038838387f, 0.58020926f), null),
            new PathPoint(null, new Vector2(0.11347532f, 0.52212083f), new Vector2(0.11313757f, 0.5146909f)),
            new PathPoint(new Vector2(0.11246213f, 0.50759876f), new Vector2(0.11246213f, 0.499831f),
                new Vector2(0.11246213f, 0.4924011f)),
            new PathPoint(new Vector2(0.11279988f, 0.48463342f), new Vector2(0.11347532f, 0.47720352f), null),
            new PathPoint(null, new Vector2(0.039176106f, 0.41911504f), new Vector2(0.010469586f, 0.397163f)),
            new PathPoint(new Vector2(0.0030396879f, 0.35764936f), new Vector2(0.021276742f, 0.3272542f), null),
            new PathPoint(null, new Vector2(0.11144897f, 0.17156366f), new Vector2(0.1279974f, 0.1411685f)),
            new PathPoint(new Vector2(0.16548476f, 0.12765956f), new Vector2(0.19891939f, 0.14083079f), null),
            new PathPoint(null, new Vector2(0.2870653f, 0.17662951f), new Vector2(0.29989877f, 0.1681864f)),
            new PathPoint(new Vector2(0.31239453f, 0.16109416f), new Vector2(0.32522807f, 0.1543397f), null),
            new PathPoint(null, new Vector2(0.33839926f, 0.06112799f), new Vector2(0.34278968f, 0.027017891f)),
            new PathPoint(new Vector2(0.37318483f, 0f), new Vector2(0.40864578f, 0f), null),
            new PathPoint(null, new Vector2(0.58932793f, 0f), new Vector2(0.62546444f, 0f)),
            new PathPoint(new Vector2(0.65552175f, 0.026342452f), new Vector2(0.6602498f, 0.06214115f), null),
            new PathPoint(null, new Vector2(0.673421f, 0.1543397f), new Vector2(0.6865922f, 0.16109416f)),
            new PathPoint(new Vector2(0.6994258f, 0.16852412f), new Vector2(0.7119216f, 0.17662951f), null),
            new PathPoint(null, new Vector2(0.7997297f, 0.14150628f), new Vector2(0.8345152f, 0.12867275f)),
            new PathPoint(new Vector2(0.8720026f, 0.1428571f), new Vector2(0.888551f, 0.17325225f), null),
            new PathPoint(null, new Vector2(0.97838557f, 0.32860515f), new Vector2(0.99594724f, 0.3606889f)),
            new PathPoint(new Vector2(0.98817945f, 0.39885172f), new Vector2(0.9608239f, 0.42046598f), null),
            new PathPoint(null, new Vector2(0.8865247f, 0.47855446f), new Vector2(0.8868624f, 0.48598436f)),
            new PathPoint(new Vector2(0.8875377f, 0.4930766f), new Vector2(0.8875377f, 0.50084424f),
                new Vector2(0.8875377f, 0.50084424f)),
            new PathPoint(new Vector2(0.8872f, 0.51604176f), new Vector2(0.8865247f, 0.523134f), null),
            new PathPoint(null, new Vector2(0.9608239f, 0.58122253f), new Vector2(0.98817945f, 0.60317457f)),
            new PathPoint(new Vector2(0.9959471f, 0.6413373f), new Vector2(0.97872317f, 0.67207015f), null),
            new PathPoint(null, new Vector2(0.8878757f, 0.8291117f), new Vector2(0.8713273f, 0.85950685f)),
            new PathPoint(new Vector2(0.8338399f, 0.87301576f), new Vector2(0.80006754f, 0.85984457f), null),
            new PathPoint(null, new Vector2(0.7122594f, 0.82472134f), new Vector2(0.69942594f, 0.83316445f)),
            new PathPoint(new Vector2(0.6869302f, 0.8402566f), new Vector2(0.6740967f, 0.8470111f), null),
            new PathPoint(null, new Vector2(0.6609255f, 0.94022286f), new Vector2(0.6561973f, 0.97365737f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.43228653f, 0.90239775f)), new PathPoint(new Vector2(0.5670383f, 0.90239775f)),
            new PathPoint(new Vector2(0.5849376f, 0.7777778f)),
            new PathPoint(null, new Vector2(0.61094236f, 0.7669705f), new Vector2(0.6325567f, 0.7581897f)),
            new PathPoint(new Vector2(0.65383327f, 0.7453562f), new Vector2(0.6764606f, 0.7288078f), null),
            new PathPoint(new Vector2(0.69841266f, 0.7122594f)), new PathPoint(new Vector2(0.8145896f, 0.75920296f)),
            new PathPoint(new Vector2(0.8817966f, 0.6420127f)), new PathPoint(new Vector2(0.7828436f, 0.56501186f)),
            new PathPoint(null, new Vector2(0.7862209f, 0.5376562f), new Vector2(0.7875718f, 0.5248227f)),
            new PathPoint(new Vector2(0.7892604f, 0.5126647f), new Vector2(0.7892604f, 0.4994934f),
                new Vector2(0.7892604f, 0.4863222f)),
            new PathPoint(new Vector2(0.7879095f, 0.47348872f), new Vector2(0.7862209f, 0.46133062f), null),
            new PathPoint(new Vector2(0.7828436f, 0.433975f)), new PathPoint(new Vector2(0.8817966f, 0.35697398f)),
            new PathPoint(new Vector2(0.8139142f, 0.23978385f)), new PathPoint(new Vector2(0.6970619f, 0.28706518f)),
            new PathPoint(null, new Vector2(0.67510974f, 0.26984128f), new Vector2(0.6545086f, 0.25430593f)),
            new PathPoint(new Vector2(0.6325567f, 0.24147251f), new Vector2(0.6102668f, 0.2323539f), null),
            new PathPoint(new Vector2(0.5849376f, 0.22154674f)), new PathPoint(new Vector2(0.5670383f, 0.09692669f)),
            new PathPoint(new Vector2(0.43228653f, 0.09692669f)), new PathPoint(new Vector2(0.41404948f, 0.22154668f)),
            new PathPoint(null, new Vector2(0.38804474f, 0.2316784f), new Vector2(0.36643043f, 0.24079695f)),
            new PathPoint(new Vector2(0.34515384f, 0.2532927f), new Vector2(0.32252637f, 0.27017888f), null),
            new PathPoint(new Vector2(0.30057433f, 0.28638962f)), new PathPoint(new Vector2(0.18439737f, 0.24012151f)),
            new PathPoint(new Vector2(0.11651486f, 0.3566362f)), new PathPoint(new Vector2(0.2154679f, 0.43363717f)),
            new PathPoint(null, new Vector2(0.2120907f, 0.46099284f), new Vector2(0.21073982f, 0.47382632f)),
            new PathPoint(new Vector2(0.20905116f, 0.48699757f), new Vector2(0.20905116f, 0.49949333f),
                new Vector2(0.20905116f, 0.51232684f)),
            new PathPoint(new Vector2(0.21006438f, 0.52549803f), new Vector2(0.2120907f, 0.53765607f), null),
            new PathPoint(new Vector2(0.2154679f, 0.56501174f)), new PathPoint(new Vector2(0.11651486f, 0.6420127f)),
            new PathPoint(new Vector2(0.18372187f, 0.75920284f)), new PathPoint(new Vector2(0.30023655f, 0.7122593f)),
            new PathPoint(null, new Vector2(0.3221886f, 0.7291454f), new Vector2(0.34312746f, 0.7453561f)),
            new PathPoint(new Vector2(0.36406627f, 0.75751424f), new Vector2(0.38703153f, 0.7666328f), null),
            new PathPoint(new Vector2(0.4130362f, 0.77743995f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.40526864f, 0.67071927f), new Vector2(0.4994935f, 0.67071927f),
                new Vector2(0.5937184f, 0.67071927f)),
            new PathPoint(new Vector2(0.67038167f, 0.59439373f), new Vector2(0.67038167f, 0.4998311f),
                new Vector2(0.67038167f, 0.40560624f)),
            new PathPoint(new Vector2(0.5940561f, 0.32894292f), new Vector2(0.4994935f, 0.32894292f),
                new Vector2(0.40526864f, 0.32894292f)),
            new PathPoint(new Vector2(0.32860532f, 0.40526846f), new Vector2(0.32860532f, 0.4998311f),
                new Vector2(0.32860532f, 0.594056f))
        }) { Closed = true }
    });


    public IconBuilder(UIConfiguration config, Color color, PathSet icon, string name)
    {
        _config = config;
        _color = color;
        _fontColor = config.FontColor;
        _icon = icon;
        _name = name;

        _layout = new IconLayout(config);
        Rect = _layout.AtlasRect.Rect;
    }

    public override void Build(ThemeBuilder themeBuilder)
    {
        _layout.AtlasRect.MoveBy(Rect.Position);

        {
            SvgPath path = _icon.ScaleToFit(_layout.InnerAreaRect.Rect);
            path.WithFill(_fontColor);
            themeBuilder.Add(path);
        }

        var spriteName = _name;

        themeBuilder.AddThemeSprite(spriteName, _layout.InnerAreaRect.Rect);
    }
}