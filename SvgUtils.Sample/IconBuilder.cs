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

    public static PathSet Diamond = new PathSet(new Path[] { new Path(new PathPoint[] { new PathPoint(new Vector2(0.87322015f, 0.127738f)), new PathPoint(new Vector2(1f, 0.30490136f)), new PathPoint(new Vector2(0.696057f, 0.7178259f)), new PathPoint(new Vector2(0.3912924f, 0.3049016f)), new PathPoint(new Vector2(0.5177985f, 0.12773824f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.4449616f, 0.31475902f)), new PathPoint(new Vector2(0.23630887f, 0.31475902f)), new PathPoint(new Vector2(0.34063524f, 0.62773824f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0f, 0.62773824f)), new PathPoint(new Vector2(0.2064622f, 0.91388273f)), new PathPoint(new Vector2(0.11117196f, 0.62773824f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.37732738f, 0.91388273f)), new PathPoint(new Vector2(0.5837895f, 0.62773824f)), new PathPoint(new Vector2(0.47261757f, 0.62773824f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.20618838f, 0.3216045f)), new PathPoint(new Vector2(0.32009846f, 0.3216045f)), new PathPoint(new Vector2(0.4214129f, 0.203861f)), new PathPoint(new Vector2(0.2902518f, 0.203861f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.7634172f, 0.2041347f)), new PathPoint(new Vector2(0.67907983f, 0.08611703f)), new PathPoint(new Vector2(0.5481927f, 0.08611703f)), new PathPoint(new Vector2(0.64923304f, 0.20386076f)) }) { Closed = true }, new Path(new PathPoint[] { new PathPoint(new Vector2(0.5093098f, 0.20386076f)), new PathPoint(new Vector2(0.4126504f, 0.091046095f)), new PathPoint(new Vector2(0.31626493f, 0.20358706f)) }) { Closed = true } });

    public static PathSet Money = new PathSet(new Path[]
    {
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(1.1920929E-07f, 0.10706639f)),
            new PathPoint(new Vector2(0.61563164f, 0.10706639f)), new PathPoint(new Vector2(0.61563164f, 0.54978573f)),
            new PathPoint(new Vector2(0f, 0.54978573f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.24999994f, 0.6846894f)), new PathPoint(new Vector2(0.24999994f, 0.6268734f)),
            new PathPoint(null, new Vector2(0.115364015f, 0.6268734f), new Vector2(0.115364015f, 0.6322268f)),
            new PathPoint(new Vector2(0.11429328f, 0.6372231f), new Vector2(0.11215192f, 0.64186275f),
                new Vector2(0.11018914f, 0.6465024f)),
            new PathPoint(new Vector2(0.107423246f, 0.6506065f), new Vector2(0.10385436f, 0.6541754f),
                new Vector2(0.10046393f, 0.65756595f)),
            new PathPoint(new Vector2(0.09644896f, 0.66033185f), new Vector2(0.09180933f, 0.6624731f),
                new Vector2(0.087169826f, 0.664436f)),
            new PathPoint(new Vector2(0.08217341f, 0.66541755f), new Vector2(0.076820076f, 0.66541755f), null),
            new PathPoint(null, new Vector2(0.076820076f, 0.8193253f), new Vector2(0.08217341f, 0.8193253f)),
            new PathPoint(new Vector2(0.087169826f, 0.8203069f), new Vector2(0.09180933f, 0.8222698f),
                new Vector2(0.09644896f, 0.82423246f)),
            new PathPoint(new Vector2(0.10046393f, 0.82699835f), new Vector2(0.10385436f, 0.83056724f),
                new Vector2(0.107423246f, 0.8339578f)),
            new PathPoint(new Vector2(0.11018914f, 0.83797276f), new Vector2(0.11215192f, 0.8426124f),
                new Vector2(0.11429328f, 0.847252f)),
            new PathPoint(new Vector2(0.115364015f, 0.8522483f), new Vector2(0.115364015f, 0.8576015f), null),
            new PathPoint(new Vector2(0.1924519f, 0.8576015f)), new PathPoint(new Vector2(0.1924519f, 0.8385972f)),
            new PathPoint(null, new Vector2(0.25000006f, 0.8385972f), new Vector2(0.23786587f, 0.8294965f)),
            new PathPoint(new Vector2(0.22840828f, 0.8181654f), new Vector2(0.22162741f, 0.80460393f),
                new Vector2(0.21502501f, 0.791042f)),
            new PathPoint(new Vector2(0.21172386f, 0.77667725f), new Vector2(0.21172386f, 0.76150954f),
                new Vector2(0.21172386f, 0.7463418f)),
            new PathPoint(new Vector2(0.21502501f, 0.7320663f), new Vector2(0.22162741f, 0.7186831f),
                new Vector2(0.22822982f, 0.70512116f)),
            new PathPoint(new Vector2(0.23768741f, 0.6937901f), new Vector2(0.25000006f, 0.6846894f), null)
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.36563176f, 0.8385974f)), new PathPoint(new Vector2(0.42317992f, 0.8385974f)),
            new PathPoint(new Vector2(0.42317992f, 0.85760176f)),
            new PathPoint(null, new Vector2(0.5002677f, 0.85760176f), new Vector2(0.5002677f, 0.8522483f)),
            new PathPoint(new Vector2(0.50124913f, 0.847252f), new Vector2(0.5032119f, 0.8426126f),
                new Vector2(0.5051748f, 0.837973f)),
            new PathPoint(new Vector2(0.50785154f, 0.833958f), new Vector2(0.511242f, 0.8305675f),
                new Vector2(0.51481086f, 0.8269986f)),
            new PathPoint(new Vector2(0.5189151f, 0.8242327f), new Vector2(0.5235546f, 0.8222698f),
                new Vector2(0.5281941f, 0.8203069f)),
            new PathPoint(new Vector2(0.53319055f, 0.81932557f), new Vector2(0.538544f, 0.81932557f), null),
            new PathPoint(null, new Vector2(0.538544f, 0.6654178f), new Vector2(0.53319067f, 0.6654178f)),
            new PathPoint(new Vector2(0.5281941f, 0.6644362f), new Vector2(0.5235546f, 0.6624733f),
                new Vector2(0.5189151f, 0.6603321f)),
            new PathPoint(new Vector2(0.51481086f, 0.65756595f), new Vector2(0.511242f, 0.65417564f),
                new Vector2(0.50785154f, 0.65060675f)),
            new PathPoint(new Vector2(0.5051748f, 0.6465024f), new Vector2(0.5032119f, 0.641863f),
                new Vector2(0.50124913f, 0.63722336f)),
            new PathPoint(new Vector2(0.5002677f, 0.63222706f), new Vector2(0.5002677f, 0.6268736f), null),
            new PathPoint(new Vector2(0.36563176f, 0.6268736f)),
            new PathPoint(null, new Vector2(0.36563176f, 0.6846894f), new Vector2(0.3779444f, 0.6937903f)),
            new PathPoint(new Vector2(0.38740188f, 0.7051214f), new Vector2(0.3940043f, 0.7186831f),
                new Vector2(0.4006067f, 0.7320663f)),
            new PathPoint(new Vector2(0.40390795f, 0.7463418f), new Vector2(0.40390795f, 0.76150954f),
                new Vector2(0.40390795f, 0.77667725f)),
            new PathPoint(new Vector2(0.40051752f, 0.791042f), new Vector2(0.39373654f, 0.80460393f),
                new Vector2(0.38713413f, 0.8181654f)),
            new PathPoint(new Vector2(0.37776583f, 0.82949674f), new Vector2(0.36563164f, 0.8385972f), null)
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.3463598f, 0.8929335f)), new PathPoint(new Vector2(0.3463598f, 0.14561021f)),
            new PathPoint(new Vector2(0.2692719f, 0.14561021f)), new PathPoint(new Vector2(0.2692719f, 0.45021403f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.50000006f, 0.453426f)), new PathPoint(new Vector2(0.50000006f, 0.14561021f)),
            new PathPoint(new Vector2(0.28854388f, 0.14561021f)), new PathPoint(new Vector2(0.28854388f, 0.16488206f)),
            new PathPoint(new Vector2(0.4807281f, 0.16488206f)), new PathPoint(new Vector2(0.4807281f, 0.43415415f)),
            new PathPoint(new Vector2(0.28854388f, 0.43415415f)), new PathPoint(new Vector2(0.28854388f, 0.453426f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.038544f, 0.14561021f)), new PathPoint(new Vector2(0.038544f, 0.453426f)),
            new PathPoint(new Vector2(0.25000006f, 0.453426f)), new PathPoint(new Vector2(0.25000006f, 0.43415415f)),
            new PathPoint(new Vector2(0.05781591f, 0.43415415f)), new PathPoint(new Vector2(0.05781591f, 0.1648823f)),
            new PathPoint(new Vector2(0.25000006f, 0.1648823f)), new PathPoint(new Vector2(0.25000006f, 0.14561033f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.25000006f, 0.47269833f)), new PathPoint(new Vector2(0.25000006f, 0.49196994f)),
            new PathPoint(new Vector2(0.46145612f, 0.49196994f)), new PathPoint(new Vector2(0.46145612f, 0.47269833f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.99999994f, 0.49196994f)), new PathPoint(new Vector2(0.99999994f, 0.47269833f)),
            new PathPoint(new Vector2(0.7885439f, 0.47269833f)), new PathPoint(new Vector2(0.7885439f, 0.49196994f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(null, new Vector2(0.36536413f, 0.32842624f), new Vector2(0.36536413f, 0.32574975f)),
            new PathPoint(new Vector2(0.36438268f, 0.3235191f), new Vector2(0.36241978f, 0.32173455f),
                new Vector2(0.36063534f, 0.31977165f)),
            new PathPoint(new Vector2(0.35840482f, 0.31879032f), new Vector2(0.3557281f, 0.31879032f),
                new Vector2(0.3518023f, 0.31879032f)),
            new PathPoint(new Vector2(0.34805495f, 0.3180765f), new Vector2(0.34448606f, 0.31664908f),
                new Vector2(0.34109563f, 0.3150431f)),
            new PathPoint(new Vector2(0.3380621f, 0.31290162f), new Vector2(0.3353855f, 0.31022513f),
                new Vector2(0.33270878f, 0.3075484f)),
            new PathPoint(new Vector2(0.33056742f, 0.304515f), new Vector2(0.32896143f, 0.30112445f),
                new Vector2(0.3275339f, 0.29755557f)),
            new PathPoint(new Vector2(0.32682008f, 0.29380834f), new Vector2(0.32682008f, 0.28988254f),
                new Vector2(0.32682008f, 0.2838155f)),
            new PathPoint(new Vector2(0.32860452f, 0.27828372f), new Vector2(0.3321734f, 0.27328742f),
                new Vector2(0.3357423f, 0.26829088f)),
            new PathPoint(new Vector2(0.3403818f, 0.26481116f), new Vector2(0.34609205f, 0.2628485f), null),
            new PathPoint(new Vector2(0.34609205f, 0.24197042f)), new PathPoint(new Vector2(0.3653639f, 0.24197042f)),
            new PathPoint(null, new Vector2(0.3653639f, 0.2628485f), new Vector2(0.36946815f, 0.2642759f)),
            new PathPoint(new Vector2(0.37303704f, 0.26659572f), new Vector2(0.37607056f, 0.2698077f),
                new Vector2(0.37928265f, 0.27301967f)),
            new PathPoint(new Vector2(0.38160235f, 0.27667797f), new Vector2(0.38302988f, 0.2807821f), null),
            new PathPoint(null, new Vector2(0.36402565f, 0.28533232f), new Vector2(0.3622411f, 0.281942f)),
            new PathPoint(new Vector2(0.3594753f, 0.28024685f), new Vector2(0.35572797f, 0.28024685f),
                new Vector2(0.35305136f, 0.28024685f)),
            new PathPoint(new Vector2(0.35073155f, 0.28122818f), new Vector2(0.34876865f, 0.28319108f),
                new Vector2(0.3469842f, 0.2849754f)),
            new PathPoint(new Vector2(0.34609205f, 0.28720605f), new Vector2(0.34609205f, 0.28988278f),
                new Vector2(0.34609205f, 0.29255927f)),
            new PathPoint(new Vector2(0.3469842f, 0.29487932f), new Vector2(0.34876865f, 0.29684222f),
                new Vector2(0.35073155f, 0.29862654f)),
            new PathPoint(new Vector2(0.35305136f, 0.2995187f), new Vector2(0.35572797f, 0.2995187f),
                new Vector2(0.35965377f, 0.2995187f)),
            new PathPoint(new Vector2(0.36331183f, 0.3003217f), new Vector2(0.36670226f, 0.3019277f),
                new Vector2(0.37027115f, 0.30335534f)),
            new PathPoint(new Vector2(0.37339395f, 0.3054074f), new Vector2(0.37607056f, 0.30808413f),
                new Vector2(0.37874717f, 0.31076062f)),
            new PathPoint(new Vector2(0.38079935f, 0.31388342f), new Vector2(0.38222688f, 0.3174523f),
                new Vector2(0.38383287f, 0.32084286f)),
            new PathPoint(new Vector2(0.38463587f, 0.32450092f), new Vector2(0.38463587f, 0.32842648f),
                new Vector2(0.38463587f, 0.3346721f)),
            new PathPoint(new Vector2(0.38285142f, 0.34029305f), new Vector2(0.37928265f, 0.3452896f),
                new Vector2(0.37571377f, 0.35028613f)),
            new PathPoint(new Vector2(0.37107426f, 0.3537656f), new Vector2(0.36536402f, 0.3557285f), null),
            new PathPoint(new Vector2(0.36536402f, 0.37660635f)), new PathPoint(new Vector2(0.34609205f, 0.37660635f)),
            new PathPoint(null, new Vector2(0.34609205f, 0.3557285f), new Vector2(0.3419879f, 0.3543011f)),
            new PathPoint(new Vector2(0.33832973f, 0.35198152f), new Vector2(0.33511776f, 0.3487693f),
                new Vector2(0.33208424f, 0.34537876f)),
            new PathPoint(new Vector2(0.3298537f, 0.34163153f), new Vector2(0.32842618f, 0.3375274f), null),
            new PathPoint(null, new Vector2(0.3474304f, 0.33297694f), new Vector2(0.3490364f, 0.3363675f)),
            new PathPoint(new Vector2(0.3518023f, 0.33806264f), new Vector2(0.3557281f, 0.33806264f),
                new Vector2(0.3584047f, 0.33806264f)),
            new PathPoint(new Vector2(0.36063522f, 0.33717048f), new Vector2(0.36241966f, 0.33538592f),
                new Vector2(0.36438257f, 0.33342302f)),
            new PathPoint(new Vector2(0.36536402f, 0.33110344f), new Vector2(0.36536402f, 0.32842672f), null)
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(new Vector2(0.55781573f, 0.5112425f)), new PathPoint(new Vector2(0.55781573f, 0.46948683f)),
            new PathPoint(new Vector2(0.48072797f, 0.46948683f)), new PathPoint(new Vector2(0.48072797f, 0.5112425f))
        }) { Closed = true },
        new Path(new PathPoint[]
        {
            new PathPoint(null, new Vector2(0.60572785f, 0.33806288f), new Vector2(0.6016236f, 0.33806288f)),
            new PathPoint(new Vector2(0.5977872f, 0.3373493f), new Vector2(0.5942182f, 0.33592212f),
                new Vector2(0.59082776f, 0.33431613f)),
            new PathPoint(new Vector2(0.58779424f, 0.33226383f), new Vector2(0.58511764f, 0.32976544f),
                new Vector2(0.5826195f, 0.32708895f)),
            new PathPoint(new Vector2(0.5805673f, 0.3240553f), new Vector2(0.5789613f, 0.320665f),
                new Vector2(0.57753366f, 0.3170961f)),
            new PathPoint(new Vector2(0.57681996f, 0.31325972f), new Vector2(0.57681996f, 0.30915534f),
                new Vector2(0.57681996f, 0.30522978f)),
            new PathPoint(new Vector2(0.57753366f, 0.3015715f), new Vector2(0.5789613f, 0.29818118f),
                new Vector2(0.5805672f, 0.29461205f)),
            new PathPoint(new Vector2(0.58270854f, 0.29148948f), new Vector2(0.58538514f, 0.28881276f),
                new Vector2(0.58806187f, 0.28613603f)),
            new PathPoint(new Vector2(0.5910954f, 0.28408396f), new Vector2(0.5944858f, 0.2826563f),
                new Vector2(0.5980547f, 0.28105056f)),
            new PathPoint(new Vector2(0.60180193f, 0.28024757f), new Vector2(0.60572773f, 0.28024757f),
                new Vector2(0.6096534f, 0.28024757f)),
            new PathPoint(new Vector2(0.6133116f, 0.28105056f), new Vector2(0.61670214f, 0.2826563f),
                new Vector2(0.620271f, 0.28408396f)),
            new PathPoint(new Vector2(0.6233937f, 0.28613603f), new Vector2(0.62607044f, 0.28881276f),
                new Vector2(0.62874705f, 0.29148948f)),
            new PathPoint(new Vector2(0.6307991f, 0.29461205f), new Vector2(0.63222677f, 0.29818118f),
                new Vector2(0.63383263f, 0.3015715f)),
            new PathPoint(new Vector2(0.6346356f, 0.30522954f), new Vector2(0.6346356f, 0.30915534f),
                new Vector2(0.6346356f, 0.31308115f)),
            new PathPoint(new Vector2(0.63383263f, 0.31682837f), new Vector2(0.63222677f, 0.3203975f),
                new Vector2(0.6307991f, 0.3237878f)),
            new PathPoint(new Vector2(0.62874705f, 0.32682145f), new Vector2(0.62607044f, 0.32949793f),
                new Vector2(0.6233937f, 0.3321749f)),
            new PathPoint(new Vector2(0.620271f, 0.33431613f), new Vector2(0.61670214f, 0.33592212f),
                new Vector2(0.6133116f, 0.3373493f)),
            new PathPoint(new Vector2(0.60965365f, 0.33806288f), new Vector2(0.60572773f, 0.33806288f), null)
        }) { Closed = true }
    });

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