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

    public static PathSet Diamond = new PathSet(new Path[] { new Path(new PathPoint[] { new PathPoint(new Vector2(0.7917228f, 0.015294552f)), new PathPoint(new Vector2(1f, 0.3063426f)), new PathPoint(new Vector2(0.5006747f, 0.98470545f)), new PathPoint(new Vector2(0f, 0.30634308f)), new PathPoint(new Vector2(0.20782709f, 0.015294552f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.67206454f, 0.32253695f)), new PathPoint(new Vector2(0.32928467f, 0.32253695f)), new PathPoint(new Vector2(0.5006747f, 0.83670664f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.112460375f, 0.32253695f)), new PathPoint(new Vector2(0.4516418f, 0.7926221f)), new PathPoint(new Vector2(0.2950964f, 0.32253695f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.5497074f, 0.7926221f)), new PathPoint(new Vector2(0.8888886f, 0.32253695f)), new PathPoint(new Vector2(0.70625234f, 0.32253695f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.11201072f, 0.2896986f)), new PathPoint(new Vector2(0.29914522f, 0.2896986f)), new PathPoint(new Vector2(0.4655869f, 0.09626627f)), new PathPoint(new Vector2(0.2501123f, 0.09626627f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.8893385f, 0.29014826f)), new PathPoint(new Vector2(0.750787f, 0.09626579f)), new PathPoint(new Vector2(0.5357623f, 0.09626579f)), new PathPoint(new Vector2(0.7017541f, 0.28969812f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.6594689f, 0.29014826f)), new PathPoint(new Vector2(0.5006745f, 0.1048131f)), new PathPoint(new Vector2(0.34232998f, 0.2896986f)) }) { Closed = true } });

    public static PathSet Money = new PathSet(new Path[] { new Path(new PathPoint[] { new PathPoint(new Vector2(1.1920929E-07f, 0.14043474f)), new PathPoint(new Vector2(1f, 0.14043474f)), new PathPoint(new Vector2(1f, 0.85956526f)), new PathPoint(new Vector2(0f, 0.85956526f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.40608716f, 0.35956526f)), new PathPoint(new Vector2(0.40608716f, 0.26565194f)), new PathPoint(null, new Vector2(0.18739152f, 0.26565194f), new Vector2(0.18739152f, 0.27434778f)), new PathPoint(new Vector2(0.18565238f, 0.2824638f), new Vector2(0.18217409f, 0.28999996f), new Vector2(0.17898571f, 0.29753613f)), new PathPoint(new Vector2(0.17449296f, 0.3042028f), new Vector2(0.16869581f, 0.30999994f), new Vector2(0.16318858f, 0.3155074f)), new PathPoint(new Vector2(0.15666687f, 0.32000017f), new Vector2(0.1491307f, 0.32347846f), new Vector2(0.14159441f, 0.32666683f)), new PathPoint(new Vector2(0.13347852f, 0.32826114f), new Vector2(0.1247828f, 0.32826114f), null), new PathPoint(null, new Vector2(0.1247828f, 0.5782609f), new Vector2(0.13347852f, 0.5782609f)), new PathPoint(new Vector2(0.14159441f, 0.5798552f), new Vector2(0.1491307f, 0.5830436f), new Vector2(0.15666687f, 0.58623195f)), new PathPoint(new Vector2(0.16318858f, 0.5907247f), new Vector2(0.16869581f, 0.5965216f), new Vector2(0.17449296f, 0.60202885f)), new PathPoint(new Vector2(0.17898571f, 0.608551f), new Vector2(0.18217409f, 0.6160872f), new Vector2(0.18565238f, 0.6236234f)), new PathPoint(new Vector2(0.18739152f, 0.63173914f), new Vector2(0.18739152f, 0.64043474f), null), new PathPoint(new Vector2(0.31260896f, 0.64043474f)), new PathPoint(new Vector2(0.31260896f, 0.60956526f)), new PathPoint(null, new Vector2(0.40608728f, 0.60956526f), new Vector2(0.38637722f, 0.59478235f)), new PathPoint(new Vector2(0.37101483f, 0.5763769f), new Vector2(0.36000037f, 0.554348f), new Vector2(0.3492757f, 0.53231907f)), new PathPoint(new Vector2(0.34391344f, 0.5089855f), new Vector2(0.34391344f, 0.48434806f), new Vector2(0.34391344f, 0.45971036f)), new PathPoint(new Vector2(0.3492757f, 0.436522f), new Vector2(0.36000037f, 0.41478276f), new Vector2(0.37072492f, 0.3927536f)), new PathPoint(new Vector2(0.3860873f, 0.37434793f), new Vector2(0.40608728f, 0.3595655f), null) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.5939131f, 0.60956526f)), new PathPoint(new Vector2(0.6873915f, 0.60956526f)), new PathPoint(new Vector2(0.6873915f, 0.64043474f)), new PathPoint(null, new Vector2(0.81260896f, 0.64043474f), new Vector2(0.81260896f, 0.63173914f)), new PathPoint(new Vector2(0.814203f, 0.6236234f), new Vector2(0.8173914f, 0.6160872f), new Vector2(0.82057977f, 0.608551f)), new PathPoint(new Vector2(0.82492757f, 0.6020293f), new Vector2(0.8304348f, 0.5965216f), new Vector2(0.83623195f, 0.5907247f)), new PathPoint(new Vector2(0.8428986f, 0.58623195f), new Vector2(0.8504348f, 0.5830436f), new Vector2(0.8579712f, 0.5798552f)), new PathPoint(new Vector2(0.86608696f, 0.5782609f), new Vector2(0.8747828f, 0.5782609f), null), new PathPoint(null, new Vector2(0.8747828f, 0.32826114f), new Vector2(0.8660872f, 0.32826114f)), new PathPoint(new Vector2(0.8579712f, 0.32666683f), new Vector2(0.8504348f, 0.32347846f), new Vector2(0.8428986f, 0.32000017f)), new PathPoint(new Vector2(0.83623195f, 0.3155074f), new Vector2(0.8304348f, 0.30999994f), new Vector2(0.82492757f, 0.3042028f)), new PathPoint(new Vector2(0.82057977f, 0.29753613f), new Vector2(0.8173914f, 0.28999996f), new Vector2(0.814203f, 0.2824638f)), new PathPoint(new Vector2(0.81260896f, 0.27434802f), new Vector2(0.81260896f, 0.26565194f), null), new PathPoint(new Vector2(0.5939131f, 0.26565194f)), new PathPoint(null, new Vector2(0.5939131f, 0.35956526f), new Vector2(0.61391306f, 0.37434793f)), new PathPoint(new Vector2(0.62927556f, 0.3927536f), new Vector2(0.6400001f, 0.41478252f), new Vector2(0.65072465f, 0.43652153f)), new PathPoint(new Vector2(0.6560869f, 0.45971012f), new Vector2(0.6560869f, 0.48434782f), new Vector2(0.6560869f, 0.5089853f)), new PathPoint(new Vector2(0.6505797f, 0.53231883f), new Vector2(0.6395652f, 0.55434775f), new Vector2(0.6288407f, 0.5763767f)), new PathPoint(new Vector2(0.61362314f, 0.59478235f), new Vector2(0.5939131f, 0.6095648f), null) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.56260896f, 0.69782615f)), new PathPoint(new Vector2(0.56260896f, 0.20304346f)), new PathPoint(new Vector2(0.4373914f, 0.20304346f)), new PathPoint(new Vector2(0.4373914f, 0.69782615f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.9373915f, 0.70304346f)), new PathPoint(new Vector2(0.9373915f, 0.20304346f)), new PathPoint(new Vector2(0.5939131f, 0.20304346f)), new PathPoint(new Vector2(0.5939131f, 0.23434782f)), new PathPoint(new Vector2(0.90608716f, 0.23434782f)), new PathPoint(new Vector2(0.90608716f, 0.6717391f)), new PathPoint(new Vector2(0.5939131f, 0.6717391f)), new PathPoint(new Vector2(0.5939131f, 0.70304346f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.06260884f, 0.20304346f)), new PathPoint(new Vector2(0.06260884f, 0.70304346f)), new PathPoint(new Vector2(0.40608716f, 0.70304346f)), new PathPoint(new Vector2(0.40608716f, 0.6717391f)), new PathPoint(new Vector2(0.0939132f, 0.6717391f)), new PathPoint(new Vector2(0.0939132f, 0.23434806f)), new PathPoint(new Vector2(0.40608716f, 0.23434806f)), new PathPoint(new Vector2(0.40608716f, 0.2030437f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.06260884f, 0.73434806f)), new PathPoint(new Vector2(0.06260884f, 0.76565194f)), new PathPoint(new Vector2(0.40608716f, 0.76565194f)), new PathPoint(new Vector2(0.40608716f, 0.73434806f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.9373915f, 0.76565194f)), new PathPoint(new Vector2(0.9373915f, 0.73434806f)), new PathPoint(new Vector2(0.5939131f, 0.73434806f)), new PathPoint(new Vector2(0.5939131f, 0.76565194f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(null, new Vector2(0.25000012f, 0.5f), new Vector2(0.25000012f, 0.49565244f)), new PathPoint(new Vector2(0.24840593f, 0.4920292f), new Vector2(0.24521756f, 0.4891305f), new Vector2(0.24231899f, 0.48594213f)), new PathPoint(new Vector2(0.23869586f, 0.48434806f), new Vector2(0.23434806f, 0.48434806f), new Vector2(0.22797108f, 0.48434806f)), new PathPoint(new Vector2(0.22188413f, 0.48318863f), new Vector2(0.2160871f, 0.48086977f), new Vector2(0.21057975f, 0.47826123f)), new PathPoint(new Vector2(0.20565224f, 0.4747827f), new Vector2(0.20130444f, 0.4704349f), new Vector2(0.19695663f, 0.4660871f)), new PathPoint(new Vector2(0.19347823f, 0.4611597f), new Vector2(0.19086957f, 0.45565248f), new Vector2(0.18855071f, 0.44985533f)), new PathPoint(new Vector2(0.18739128f, 0.4437685f), new Vector2(0.18739128f, 0.43739176f), new Vector2(0.18739128f, 0.4275365f)), new PathPoint(new Vector2(0.19028986f, 0.4185512f), new Vector2(0.196087f, 0.4104352f), new Vector2(0.20188403f, 0.4023192f)), new PathPoint(new Vector2(0.2094202f, 0.39666724f), new Vector2(0.21869564f, 0.39347887f), null), new PathPoint(new Vector2(0.21869564f, 0.35956573f)), new PathPoint(new Vector2(0.24999988f, 0.35956573f)), new PathPoint(null, new Vector2(0.24999988f, 0.39347887f), new Vector2(0.25666654f, 0.39579773f)), new PathPoint(new Vector2(0.2624637f, 0.39956594f), new Vector2(0.2673912f, 0.40478325f), new Vector2(0.27260864f, 0.41000056f)), new PathPoint(new Vector2(0.27637672f, 0.41594267f), new Vector2(0.27869546f, 0.42260933f), null), new PathPoint(null, new Vector2(0.24782598f, 0.43000078f), new Vector2(0.2449274f, 0.4244933f)), new PathPoint(new Vector2(0.24043477f, 0.42173982f), new Vector2(0.2343477f, 0.42173982f), new Vector2(0.2299999f, 0.42173982f)), new PathPoint(new Vector2(0.22623181f, 0.42333388f), new Vector2(0.22304344f, 0.4265225f), new Vector2(0.22014487f, 0.42942095f)), new PathPoint(new Vector2(0.21869564f, 0.4330442f), new Vector2(0.21869564f, 0.437392f), new Vector2(0.21869564f, 0.4417398f)), new PathPoint(new Vector2(0.22014487f, 0.445508f), new Vector2(0.22304344f, 0.44869637f), new Vector2(0.22623181f, 0.45159483f)), new PathPoint(new Vector2(0.2299999f, 0.45304418f), new Vector2(0.2343477f, 0.45304418f), new Vector2(0.24072456f, 0.45304418f)), new PathPoint(new Vector2(0.24666655f, 0.45434856f), new Vector2(0.25217378f, 0.4569571f), new Vector2(0.2579708f, 0.45927596f)), new PathPoint(new Vector2(0.2630434f, 0.4626093f), new Vector2(0.2673912f, 0.4669571f), new Vector2(0.271739f, 0.47130513f)), new PathPoint(new Vector2(0.27507234f, 0.4763775f), new Vector2(0.2773912f, 0.48217463f), new Vector2(0.27999997f, 0.48768187f)), new PathPoint(new Vector2(0.28130436f, 0.49362373f), new Vector2(0.28130436f, 0.5000005f), new Vector2(0.28130436f, 0.5101454f)), new PathPoint(new Vector2(0.27840567f, 0.5192759f), new Vector2(0.27260864f, 0.5273919f), new Vector2(0.2668116f, 0.5355079f)), new PathPoint(new Vector2(0.25927544f, 0.54115987f), new Vector2(0.25f, 0.54434824f), null), new PathPoint(new Vector2(0.25f, 0.57826114f)), new PathPoint(new Vector2(0.21869576f, 0.57826114f)), new PathPoint(null, new Vector2(0.21869576f, 0.54434824f), new Vector2(0.21202898f, 0.54202986f)), new PathPoint(new Vector2(0.206087f, 0.5382619f), new Vector2(0.20086968f, 0.5330441f), new Vector2(0.19594216f, 0.52753687f)), new PathPoint(new Vector2(0.19231904f, 0.52145004f), new Vector2(0.19000006f, 0.5147834f), null), new PathPoint(null, new Vector2(0.22086966f, 0.5073919f), new Vector2(0.22347832f, 0.5128994f)), new PathPoint(new Vector2(0.22797108f, 0.51565313f), new Vector2(0.23434782f, 0.51565313f), new Vector2(0.23869574f, 0.51565313f)), new PathPoint(new Vector2(0.24231887f, 0.5142038f), new Vector2(0.24521744f, 0.5113051f), new Vector2(0.24840581f, 0.5081165f)), new PathPoint(new Vector2(0.25f, 0.5043485f), new Vector2(0.25f, 0.5000007f), null) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.56260896f, 0.79695654f)), new PathPoint(new Vector2(0.56260896f, 0.72913074f)), new PathPoint(new Vector2(0.4373914f, 0.72913074f)), new PathPoint(new Vector2(0.4373914f, 0.79695654f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(null, new Vector2(0.7656522f, 0.51565266f), new Vector2(0.7589855f, 0.51565266f)), new PathPoint(new Vector2(0.75275373f, 0.51449347f), new Vector2(0.7469566f, 0.5121751f), new Vector2(0.74144936f, 0.50956655f)), new PathPoint(new Vector2(0.7365217f, 0.506233f), new Vector2(0.7321739f, 0.5021746f), new Vector2(0.72811604f, 0.49782705f)), new PathPoint(new Vector2(0.72478247f, 0.49289942f), new Vector2(0.7221739f, 0.4873922f), new Vector2(0.71985507f, 0.48159528f)), new PathPoint(new Vector2(0.71869564f, 0.4753635f), new Vector2(0.71869564f, 0.46869683f), new Vector2(0.71869564f, 0.4623201f)), new PathPoint(new Vector2(0.71985507f, 0.45637774f), new Vector2(0.7221739f, 0.4508705f), new Vector2(0.72478247f, 0.44507337f)), new PathPoint(new Vector2(0.728261f, 0.440001f), new Vector2(0.7326088f, 0.4356532f), new Vector2(0.7369566f, 0.4313054f)), new PathPoint(new Vector2(0.741884f, 0.42797208f), new Vector2(0.7473912f, 0.42565322f), new Vector2(0.7531884f, 0.42304468f)), new PathPoint(new Vector2(0.7592752f, 0.4217403f), new Vector2(0.76565194f, 0.4217403f), new Vector2(0.7720287f, 0.4217403f)), new PathPoint(new Vector2(0.777971f, 0.42304468f), new Vector2(0.78347826f, 0.42565322f), new Vector2(0.7892754f, 0.42797208f)), new PathPoint(new Vector2(0.79434776f, 0.4313054f), new Vector2(0.79869556f, 0.4356532f), new Vector2(0.80304337f, 0.440001f)), new PathPoint(new Vector2(0.8063767f, 0.44507337f), new Vector2(0.80869555f, 0.4508705f), new Vector2(0.8113041f, 0.45637774f)), new PathPoint(new Vector2(0.8126085f, 0.4623196f), new Vector2(0.8126085f, 0.46869683f), new Vector2(0.8126085f, 0.47507358f)), new PathPoint(new Vector2(0.8113041f, 0.4811604f), new Vector2(0.80869555f, 0.48695755f), new Vector2(0.8063767f, 0.49246478f)), new PathPoint(new Vector2(0.80304337f, 0.49739242f), new Vector2(0.79869556f, 0.50174f), new Vector2(0.79434776f, 0.50608826f)), new PathPoint(new Vector2(0.7892754f, 0.50956655f), new Vector2(0.78347826f, 0.5121751f), new Vector2(0.777971f, 0.51449347f)), new PathPoint(new Vector2(0.77202916f, 0.51565266f), new Vector2(0.76565194f, 0.51565266f), null) }) { Closed = true } });

    public static PathSet Discord = new PathSet(new Path[]
    {
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.964224f, 0.345491f), new Vector2(0.85007143f, 0.17502052f),
                new Vector2(0.784785f, 0.14447458f)),
            new PathPoint(new Vector2(0.7149775f, 0.12227507f), new Vector2(0.6419958f, 0.10963447f),
                new Vector2(0.63303274f, 0.12583926f)),
            new PathPoint(new Vector2(0.62256134f, 0.14763507f), new Vector2(0.61534166f, 0.16497357f),
                new Vector2(0.53776026f, 0.15330628f)),
            new PathPoint(new Vector2(0.4608921f, 0.15330628f), new Vector2(0.38473743f, 0.16497357f),
                new Vector2(0.37751916f, 0.14763507f)),
            new PathPoint(new Vector2(0.36680993f, 0.12583926f), new Vector2(0.35776672f, 0.10963447f),
                new Vector2(0.28470618f, 0.12227507f)),
            new PathPoint(new Vector2(0.21481857f, 0.14455615f), new Vector2(0.14953205f, 0.17518228f),
                new Vector2(0.017848577f, 0.37417325f)),
            new PathPoint(new Vector2(-0.017848648f, 0.5682216f), new Vector2(0f, 0.75951463f),
                new Vector2(0.08733941f, 0.82473755f)),
            new PathPoint(new Vector2(0.17198181f, 0.86435854f), new Vector2(0.25519577f, 0.8902854f),
                new Vector2(0.27574176f, 0.8620082f)),
            new PathPoint(new Vector2(0.294066f, 0.83194894f), new Vector2(0.30985188f, 0.8002693f),
                new Vector2(0.2797871f, 0.7888453f)),
            new PathPoint(new Vector2(0.25099146f, 0.7747475f), new Vector2(0.22378297f, 0.75838095f),
                new Vector2(0.23100124f, 0.7530333f)),
            new PathPoint(new Vector2(0.23806189f, 0.74744225f), new Vector2(0.24488337f, 0.7416895f),
                new Vector2(0.41083568f, 0.8193097f)),
            new PathPoint(new Vector2(0.5911472f, 0.8193097f), new Vector2(0.7551169f, 0.7416895f),
                new Vector2(0.76201856f, 0.74744225f)),
            new PathPoint(new Vector2(0.76907784f, 0.7530333f), new Vector2(0.7762173f, 0.75838095f),
                new Vector2(0.74892867f, 0.7748277f)),
            new PathPoint(new Vector2(0.7200542f, 0.7889255f), new Vector2(0.6899894f, 0.80035084f),
                new Vector2(0.70577526f, 0.83194894f)),
            new PathPoint(new Vector2(0.7240207f, 0.86208975f), new Vector2(0.74464554f, 0.89036554f),
                new Vector2(0.8279396f, 0.8644387f)),
            new PathPoint(new Vector2(0.9126606f, 0.8248191f), new Vector2(1f, 0.75951463f),
                new Vector2(1.0209428f, 0.5377573f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.38315162f, 0.6418712f), new Vector2(0.33246064f, 0.6418712f),
                new Vector2(0.2826434f, 0.6418712f)),
            new PathPoint(new Vector2(0.24178924f, 0.5953638f), new Vector2(0.24178924f, 0.53872925f),
                new Vector2(0.24178924f, 0.48209468f)),
            new PathPoint(new Vector2(0.28177103f, 0.4355071f), new Vector2(0.33246064f, 0.4355071f),
                new Vector2(0.38315162f, 0.4355071f)),
            new PathPoint(new Vector2(0.42400444f, 0.4820131f), new Vector2(0.42313203f, 0.53872925f),
                new Vector2(0.42321086f, 0.5953638f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.7182293f, 0.6418712f), new Vector2(0.66753966f, 0.6418712f),
                new Vector2(0.6177224f, 0.6418712f)),
            new PathPoint(new Vector2(0.57686824f, 0.5953638f), new Vector2(0.57686824f, 0.53872925f),
                new Vector2(0.57686824f, 0.48209468f)),
            new PathPoint(new Vector2(0.61684865f, 0.4355071f), new Vector2(0.66753966f, 0.4355071f),
                new Vector2(0.7182293f, 0.4355071f)),
            new PathPoint(new Vector2(0.75908345f, 0.4820131f), new Vector2(0.758211f, 0.53872925f),
                new Vector2(0.758211f, 0.5953638f))
        }) { Closed = true }
    });
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
        themeBuilder.WriteCssLine($"icon.{spriteName}");
        themeBuilder.WriteCssLine("{");
        themeBuilder.WriteCssLine($"    decorator: image( {spriteName} );");
        themeBuilder.WriteCssLine("}");
    }
}