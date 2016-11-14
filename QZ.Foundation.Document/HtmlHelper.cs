/* Copyright (c) 2016 Qianzhan Information Lim. Co. All rights reserved.
 * Contributor: Sha Jianjian
 * 2016
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QZ.Foundation.Document
{
    public class HtmlHelper
    {
        /// <summary>
        /// 字符编码-实体字符串映射
        /// </summary>
        private static Dictionary<int, string> _dic_val;
        /// <summary>
        /// 实体字符串-字符编码映射
        /// </summary>
        private static Dictionary<string, int> _dic_name;
        /// <summary>
        /// 最大实体字符串长度
        /// </summary>
        private static readonly int _maxEntLen;

        static HtmlHelper()
        {
            _maxEntLen = 9;
            _dic_name = new Dictionary<string, int>();
            _dic_val = new Dictionary<int, string>();
            _dic_val.Add(0x22, "quot");
            _dic_name.Add("quot", 0x22);
            _dic_val.Add(0x27, "apos");
            _dic_name.Add("apos", 0x27);
            _dic_val.Add(0x26, "amp");
            _dic_name.Add("amp", 0x26);
            _dic_val.Add(60, "lt");
            _dic_name.Add("lt", 60);
            _dic_val.Add(0x3e, "gt");
            _dic_name.Add("gt", 0x3e);
            _dic_val.Add(160, "nbsp");
            _dic_name.Add("nbsp", 160);
            _dic_val.Add(0xa1, "iexcl");
            _dic_name.Add("iexcl", 0xa1);
            _dic_val.Add(0xa2, "cent");
            _dic_name.Add("cent", 0xa2);
            _dic_val.Add(0xa3, "pound");
            _dic_name.Add("pound", 0xa3);
            _dic_val.Add(0xa4, "curren");
            _dic_name.Add("curren", 0xa4);
            _dic_val.Add(0xa5, "yen");
            _dic_name.Add("yen", 0xa5);
            _dic_val.Add(0xa6, "brvbar");
            _dic_name.Add("brvbar", 0xa6);
            _dic_val.Add(0xa7, "sect");
            _dic_name.Add("sect", 0xa7);
            _dic_val.Add(0xa8, "uml");
            _dic_name.Add("uml", 0xa8);
            _dic_val.Add(0xa9, "copy");
            _dic_name.Add("copy", 0xa9);
            _dic_val.Add(170, "ordf");
            _dic_name.Add("ordf", 170);
            _dic_val.Add(0xab, "laquo");
            _dic_name.Add("laquo", 0xab);
            _dic_val.Add(0xac, "not");
            _dic_name.Add("not", 0xac);
            _dic_val.Add(0xad, "shy");
            _dic_name.Add("shy", 0xad);
            _dic_val.Add(0xae, "reg");
            _dic_name.Add("reg", 0xae);
            _dic_val.Add(0xaf, "macr");
            _dic_name.Add("macr", 0xaf);
            _dic_val.Add(0xb0, "deg");
            _dic_name.Add("deg", 0xb0);
            _dic_val.Add(0xb1, "plusmn");
            _dic_name.Add("plusmn", 0xb1);
            _dic_val.Add(0xb2, "sup2");
            _dic_name.Add("sup2", 0xb2);
            _dic_val.Add(0xb3, "sup3");
            _dic_name.Add("sup3", 0xb3);
            _dic_val.Add(180, "acute");
            _dic_name.Add("acute", 180);
            _dic_val.Add(0xb5, "micro");
            _dic_name.Add("micro", 0xb5);
            _dic_val.Add(0xb6, "para");
            _dic_name.Add("para", 0xb6);
            _dic_val.Add(0xb7, "middot");
            _dic_name.Add("middot", 0xb7);
            _dic_val.Add(0xb8, "cedil");
            _dic_name.Add("cedil", 0xb8);
            _dic_val.Add(0xb9, "sup1");
            _dic_name.Add("sup1", 0xb9);
            _dic_val.Add(0xba, "ordm");
            _dic_name.Add("ordm", 0xba);
            _dic_val.Add(0xbb, "raquo");
            _dic_name.Add("raquo", 0xbb);
            _dic_val.Add(0xbc, "frac14");
            _dic_name.Add("frac14", 0xbc);
            _dic_val.Add(0xbd, "frac12");
            _dic_name.Add("frac12", 0xbd);
            _dic_val.Add(190, "frac34");
            _dic_name.Add("frac34", 190);
            _dic_val.Add(0xbf, "iquest");
            _dic_name.Add("iquest", 0xbf);
            _dic_val.Add(0xd7, "times");
            _dic_name.Add("times", 0xd7);
            _dic_val.Add(0xf7, "divide");
            _dic_name.Add("divide", 0xf7);
            _dic_val.Add(0xc0, "Agrave");
            _dic_name.Add("Agrave", 0xc0);
            _dic_val.Add(0xc1, "Aacute");
            _dic_name.Add("Aacute", 0xc1);
            _dic_val.Add(0xc2, "Acirc");
            _dic_name.Add("Acirc", 0xc2);
            _dic_val.Add(0xc3, "Atilde");
            _dic_name.Add("Atilde", 0xc3);
            _dic_val.Add(0xc4, "Auml");
            _dic_name.Add("Auml", 0xc4);
            _dic_val.Add(0xc5, "Aring");
            _dic_name.Add("Aring", 0xc5);
            _dic_val.Add(0xc6, "AElig");
            _dic_name.Add("AElig", 0xc6);
            _dic_val.Add(0xc7, "Ccedil");
            _dic_name.Add("Ccedil", 0xc7);
            _dic_val.Add(200, "Egrave");
            _dic_name.Add("Egrave", 200);
            _dic_val.Add(0xc9, "Eacute");
            _dic_name.Add("Eacute", 0xc9);
            _dic_val.Add(0xca, "Ecirc");
            _dic_name.Add("Ecirc", 0xca);
            _dic_val.Add(0xcb, "Euml");
            _dic_name.Add("Euml", 0xcb);
            _dic_val.Add(0xcc, "Igrave");
            _dic_name.Add("Igrave", 0xcc);
            _dic_val.Add(0xcd, "Iacute");
            _dic_name.Add("Iacute", 0xcd);
            _dic_val.Add(0xce, "Icirc");
            _dic_name.Add("Icirc", 0xce);
            _dic_val.Add(0xcf, "Iuml");
            _dic_name.Add("Iuml", 0xcf);
            _dic_val.Add(0xd0, "ETH");
            _dic_name.Add("ETH", 0xd0);
            _dic_val.Add(0xd1, "Ntilde");
            _dic_name.Add("Ntilde", 0xd1);
            _dic_val.Add(210, "Ograve");
            _dic_name.Add("Ograve", 210);
            _dic_val.Add(0xd3, "Oacute");
            _dic_name.Add("Oacute", 0xd3);
            _dic_val.Add(0xd4, "Ocirc");
            _dic_name.Add("Ocirc", 0xd4);
            _dic_val.Add(0xd5, "Otilde");
            _dic_name.Add("Otilde", 0xd5);
            _dic_val.Add(0xd6, "Ouml");
            _dic_name.Add("Ouml", 0xd6);
            _dic_val.Add(0xd8, "Oslash");
            _dic_name.Add("Oslash", 0xd8);
            _dic_val.Add(0xd9, "Ugrave");
            _dic_name.Add("Ugrave", 0xd9);
            _dic_val.Add(0xda, "Uacute");
            _dic_name.Add("Uacute", 0xda);
            _dic_val.Add(0xdb, "Ucirc");
            _dic_name.Add("Ucirc", 0xdb);
            _dic_val.Add(220, "Uuml");
            _dic_name.Add("Uuml", 220);
            _dic_val.Add(0xdd, "Yacute");
            _dic_name.Add("Yacute", 0xdd);
            _dic_val.Add(0xde, "THORN");
            _dic_name.Add("THORN", 0xde);
            _dic_val.Add(0xdf, "szlig");
            _dic_name.Add("szlig", 0xdf);
            _dic_val.Add(0xe0, "agrave");
            _dic_name.Add("agrave", 0xe0);
            _dic_val.Add(0xe1, "aacute");
            _dic_name.Add("aacute", 0xe1);
            _dic_val.Add(0xe2, "acirc");
            _dic_name.Add("acirc", 0xe2);
            _dic_val.Add(0xe3, "atilde");
            _dic_name.Add("atilde", 0xe3);
            _dic_val.Add(0xe4, "auml");
            _dic_name.Add("auml", 0xe4);
            _dic_val.Add(0xe5, "aring");
            _dic_name.Add("aring", 0xe5);
            _dic_val.Add(230, "aelig");
            _dic_name.Add("aelig", 230);
            _dic_val.Add(0xe7, "ccedil");
            _dic_name.Add("ccedil", 0xe7);
            _dic_val.Add(0xe8, "egrave");
            _dic_name.Add("egrave", 0xe8);
            _dic_val.Add(0xe9, "eacute");
            _dic_name.Add("eacute", 0xe9);
            _dic_val.Add(0xea, "ecirc");
            _dic_name.Add("ecirc", 0xea);
            _dic_val.Add(0xeb, "euml");
            _dic_name.Add("euml", 0xeb);
            _dic_val.Add(0xec, "igrave");
            _dic_name.Add("igrave", 0xec);
            _dic_val.Add(0xed, "iacute");
            _dic_name.Add("iacute", 0xed);
            _dic_val.Add(0xee, "icirc");
            _dic_name.Add("icirc", 0xee);
            _dic_val.Add(0xef, "iuml");
            _dic_name.Add("iuml", 0xef);
            _dic_val.Add(240, "eth");
            _dic_name.Add("eth", 240);
            _dic_val.Add(0xf1, "ntilde");
            _dic_name.Add("ntilde", 0xf1);
            _dic_val.Add(0xf2, "ograve");
            _dic_name.Add("ograve", 0xf2);
            _dic_val.Add(0xf3, "oacute");
            _dic_name.Add("oacute", 0xf3);
            _dic_val.Add(0xf4, "ocirc");
            _dic_name.Add("ocirc", 0xf4);
            _dic_val.Add(0xf5, "otilde");
            _dic_name.Add("otilde", 0xf5);
            _dic_val.Add(0xf6, "ouml");
            _dic_name.Add("ouml", 0xf6);
            _dic_val.Add(0xf8, "oslash");
            _dic_name.Add("oslash", 0xf8);
            _dic_val.Add(0xf9, "ugrave");
            _dic_name.Add("ugrave", 0xf9);
            _dic_val.Add(250, "uacute");
            _dic_name.Add("uacute", 250);
            _dic_val.Add(0xfb, "ucirc");
            _dic_name.Add("ucirc", 0xfb);
            _dic_val.Add(0xfc, "uuml");
            _dic_name.Add("uuml", 0xfc);
            _dic_val.Add(0xfd, "yacute");
            _dic_name.Add("yacute", 0xfd);
            _dic_val.Add(0xfe, "thorn");
            _dic_name.Add("thorn", 0xfe);
            _dic_val.Add(0xff, "yuml");
            _dic_name.Add("yuml", 0xff);
            _dic_val.Add(0x2200, "forall");
            _dic_name.Add("forall", 0x2200);
            _dic_val.Add(0x2202, "part");
            _dic_name.Add("part", 0x2202);
            _dic_val.Add(0x2203, "exist");
            _dic_name.Add("exist", 0x2203);
            _dic_val.Add(0x2205, "empty");
            _dic_name.Add("empty", 0x2205);
            _dic_val.Add(0x2207, "nabla");
            _dic_name.Add("nabla", 0x2207);
            _dic_val.Add(0x2208, "isin");
            _dic_name.Add("isin", 0x2208);
            _dic_val.Add(0x2209, "notin");
            _dic_name.Add("notin", 0x2209);
            _dic_val.Add(0x220b, "ni");
            _dic_name.Add("ni", 0x220b);
            _dic_val.Add(0x220f, "prod");
            _dic_name.Add("prod", 0x220f);
            _dic_val.Add(0x2211, "sum");
            _dic_name.Add("sum", 0x2211);
            _dic_val.Add(0x2212, "minus");
            _dic_name.Add("minus", 0x2212);
            _dic_val.Add(0x2217, "lowast");
            _dic_name.Add("lowast", 0x2217);
            _dic_val.Add(0x221a, "radic");
            _dic_name.Add("radic", 0x221a);
            _dic_val.Add(0x221d, "prop");
            _dic_name.Add("prop", 0x221d);
            _dic_val.Add(0x221e, "infin");
            _dic_name.Add("infin", 0x221e);
            _dic_val.Add(0x2220, "ang");
            _dic_name.Add("ang", 0x2220);
            _dic_val.Add(0x2227, "and");
            _dic_name.Add("and", 0x2227);
            _dic_val.Add(0x2228, "or");
            _dic_name.Add("or", 0x2228);
            _dic_val.Add(0x2229, "cap");
            _dic_name.Add("cap", 0x2229);
            _dic_val.Add(0x222a, "cup");
            _dic_name.Add("cup", 0x222a);
            _dic_val.Add(0x222b, "int");
            _dic_name.Add("int", 0x222b);
            _dic_val.Add(0x2234, "there4");
            _dic_name.Add("there4", 0x2234);
            _dic_val.Add(0x223c, "sim");
            _dic_name.Add("sim", 0x223c);
            _dic_val.Add(0x2245, "cong");
            _dic_name.Add("cong", 0x2245);
            _dic_val.Add(0x2248, "asymp");
            _dic_name.Add("asymp", 0x2248);
            _dic_val.Add(0x2260, "ne");
            _dic_name.Add("ne", 0x2260);
            _dic_val.Add(0x2261, "equiv");
            _dic_name.Add("equiv", 0x2261);
            _dic_val.Add(0x2264, "le");
            _dic_name.Add("le", 0x2264);
            _dic_val.Add(0x2265, "ge");
            _dic_name.Add("ge", 0x2265);
            _dic_val.Add(0x2282, "sub");
            _dic_name.Add("sub", 0x2282);
            _dic_val.Add(0x2283, "sup");
            _dic_name.Add("sup", 0x2283);
            _dic_val.Add(0x2284, "nsub");
            _dic_name.Add("nsub", 0x2284);
            _dic_val.Add(0x2286, "sube");
            _dic_name.Add("sube", 0x2286);
            _dic_val.Add(0x2287, "supe");
            _dic_name.Add("supe", 0x2287);
            _dic_val.Add(0x2295, "oplus");
            _dic_name.Add("oplus", 0x2295);
            _dic_val.Add(0x2297, "otimes");
            _dic_name.Add("otimes", 0x2297);
            _dic_val.Add(0x22a5, "perp");
            _dic_name.Add("perp", 0x22a5);
            _dic_val.Add(0x22c5, "sdot");
            _dic_name.Add("sdot", 0x22c5);
            _dic_val.Add(0x391, "Alpha");
            _dic_name.Add("Alpha", 0x391);
            _dic_val.Add(0x392, "Beta");
            _dic_name.Add("Beta", 0x392);
            _dic_val.Add(0x393, "Gamma");
            _dic_name.Add("Gamma", 0x393);
            _dic_val.Add(0x394, "Delta");
            _dic_name.Add("Delta", 0x394);
            _dic_val.Add(0x395, "Epsilon");
            _dic_name.Add("Epsilon", 0x395);
            _dic_val.Add(0x396, "Zeta");
            _dic_name.Add("Zeta", 0x396);
            _dic_val.Add(0x397, "Eta");
            _dic_name.Add("Eta", 0x397);
            _dic_val.Add(920, "Theta");
            _dic_name.Add("Theta", 920);
            _dic_val.Add(0x399, "Iota");
            _dic_name.Add("Iota", 0x399);
            _dic_val.Add(0x39a, "Kappa");
            _dic_name.Add("Kappa", 0x39a);
            _dic_val.Add(0x39b, "Lambda");
            _dic_name.Add("Lambda", 0x39b);
            _dic_val.Add(0x39c, "Mu");
            _dic_name.Add("Mu", 0x39c);
            _dic_val.Add(0x39d, "Nu");
            _dic_name.Add("Nu", 0x39d);
            _dic_val.Add(0x39e, "Xi");
            _dic_name.Add("Xi", 0x39e);
            _dic_val.Add(0x39f, "Omicron");
            _dic_name.Add("Omicron", 0x39f);
            _dic_val.Add(0x3a0, "Pi");
            _dic_name.Add("Pi", 0x3a0);
            _dic_val.Add(0x3a1, "Rho");
            _dic_name.Add("Rho", 0x3a1);
            _dic_val.Add(0x3a3, "Sigma");
            _dic_name.Add("Sigma", 0x3a3);
            _dic_val.Add(0x3a4, "Tau");
            _dic_name.Add("Tau", 0x3a4);
            _dic_val.Add(0x3a5, "Upsilon");
            _dic_name.Add("Upsilon", 0x3a5);
            _dic_val.Add(0x3a6, "Phi");
            _dic_name.Add("Phi", 0x3a6);
            _dic_val.Add(0x3a7, "Chi");
            _dic_name.Add("Chi", 0x3a7);
            _dic_val.Add(0x3a8, "Psi");
            _dic_name.Add("Psi", 0x3a8);
            _dic_val.Add(0x3a9, "Omega");
            _dic_name.Add("Omega", 0x3a9);
            _dic_val.Add(0x3b1, "alpha");
            _dic_name.Add("alpha", 0x3b1);
            _dic_val.Add(0x3b2, "beta");
            _dic_name.Add("beta", 0x3b2);
            _dic_val.Add(0x3b3, "gamma");
            _dic_name.Add("gamma", 0x3b3);
            _dic_val.Add(0x3b4, "delta");
            _dic_name.Add("delta", 0x3b4);
            _dic_val.Add(0x3b5, "epsilon");
            _dic_name.Add("epsilon", 0x3b5);
            _dic_val.Add(950, "zeta");
            _dic_name.Add("zeta", 950);
            _dic_val.Add(0x3b7, "eta");
            _dic_name.Add("eta", 0x3b7);
            _dic_val.Add(0x3b8, "theta");
            _dic_name.Add("theta", 0x3b8);
            _dic_val.Add(0x3b9, "iota");
            _dic_name.Add("iota", 0x3b9);
            _dic_val.Add(0x3ba, "kappa");
            _dic_name.Add("kappa", 0x3ba);
            _dic_val.Add(0x3bb, "lambda");
            _dic_name.Add("lambda", 0x3bb);
            _dic_val.Add(0x3bc, "mu");
            _dic_name.Add("mu", 0x3bc);
            _dic_val.Add(0x3bd, "nu");
            _dic_name.Add("nu", 0x3bd);
            _dic_val.Add(0x3be, "xi");
            _dic_name.Add("xi", 0x3be);
            _dic_val.Add(0x3bf, "omicron");
            _dic_name.Add("omicron", 0x3bf);
            _dic_val.Add(960, "pi");
            _dic_name.Add("pi", 960);
            _dic_val.Add(0x3c1, "rho");
            _dic_name.Add("rho", 0x3c1);
            _dic_val.Add(0x3c2, "sigmaf");
            _dic_name.Add("sigmaf", 0x3c2);
            _dic_val.Add(0x3c3, "sigma");
            _dic_name.Add("sigma", 0x3c3);
            _dic_val.Add(0x3c4, "tau");
            _dic_name.Add("tau", 0x3c4);
            _dic_val.Add(0x3c5, "upsilon");
            _dic_name.Add("upsilon", 0x3c5);
            _dic_val.Add(0x3c6, "phi");
            _dic_name.Add("phi", 0x3c6);
            _dic_val.Add(0x3c7, "chi");
            _dic_name.Add("chi", 0x3c7);
            _dic_val.Add(0x3c8, "psi");
            _dic_name.Add("psi", 0x3c8);
            _dic_val.Add(0x3c9, "omega");
            _dic_name.Add("omega", 0x3c9);
            _dic_val.Add(0x3d1, "thetasym");
            _dic_name.Add("thetasym", 0x3d1);
            _dic_val.Add(0x3d2, "upsih");
            _dic_name.Add("upsih", 0x3d2);
            _dic_val.Add(0x3d6, "piv");
            _dic_name.Add("piv", 0x3d6);
            _dic_val.Add(0x152, "OElig");
            _dic_name.Add("OElig", 0x152);
            _dic_val.Add(0x153, "oelig");
            _dic_name.Add("oelig", 0x153);
            _dic_val.Add(0x160, "Scaron");
            _dic_name.Add("Scaron", 0x160);
            _dic_val.Add(0x161, "scaron");
            _dic_name.Add("scaron", 0x161);
            _dic_val.Add(0x178, "Yuml");
            _dic_name.Add("Yuml", 0x178);
            _dic_val.Add(0x192, "fnof");
            _dic_name.Add("fnof", 0x192);
            _dic_val.Add(710, "circ");
            _dic_name.Add("circ", 710);
            _dic_val.Add(0x2dc, "tilde");
            _dic_name.Add("tilde", 0x2dc);
            _dic_val.Add(0x2002, "ensp");
            _dic_name.Add("ensp", 0x2002);
            _dic_val.Add(0x2003, "emsp");
            _dic_name.Add("emsp", 0x2003);
            _dic_val.Add(0x2009, "thinsp");
            _dic_name.Add("thinsp", 0x2009);
            _dic_val.Add(0x200c, "zwnj");
            _dic_name.Add("zwnj", 0x200c);
            _dic_val.Add(0x200d, "zwj");
            _dic_name.Add("zwj", 0x200d);
            _dic_val.Add(0x200e, "lrm");
            _dic_name.Add("lrm", 0x200e);
            _dic_val.Add(0x200f, "rlm");
            _dic_name.Add("rlm", 0x200f);
            _dic_val.Add(0x2013, "ndash");
            _dic_name.Add("ndash", 0x2013);
            _dic_val.Add(0x2014, "mdash");
            _dic_name.Add("mdash", 0x2014);
            _dic_val.Add(0x2018, "lsquo");
            _dic_name.Add("lsquo", 0x2018);
            _dic_val.Add(0x2019, "rsquo");
            _dic_name.Add("rsquo", 0x2019);
            _dic_val.Add(0x201a, "sbquo");
            _dic_name.Add("sbquo", 0x201a);
            _dic_val.Add(0x201c, "ldquo");
            _dic_name.Add("ldquo", 0x201c);
            _dic_val.Add(0x201d, "rdquo");
            _dic_name.Add("rdquo", 0x201d);
            _dic_val.Add(0x201e, "bdquo");
            _dic_name.Add("bdquo", 0x201e);
            _dic_val.Add(0x2020, "dagger");
            _dic_name.Add("dagger", 0x2020);
            _dic_val.Add(0x2021, "Dagger");
            _dic_name.Add("Dagger", 0x2021);
            _dic_val.Add(0x2022, "bull");
            _dic_name.Add("bull", 0x2022);
            _dic_val.Add(0x2026, "hellip");
            _dic_name.Add("hellip", 0x2026);
            _dic_val.Add(0x2030, "permil");
            _dic_name.Add("permil", 0x2030);
            _dic_val.Add(0x2032, "prime");
            _dic_name.Add("prime", 0x2032);
            _dic_val.Add(0x2033, "Prime");
            _dic_name.Add("Prime", 0x2033);
            _dic_val.Add(0x2039, "lsaquo");
            _dic_name.Add("lsaquo", 0x2039);
            _dic_val.Add(0x203a, "rsaquo");
            _dic_name.Add("rsaquo", 0x203a);
            _dic_val.Add(0x203e, "oline");
            _dic_name.Add("oline", 0x203e);
            _dic_val.Add(0x20ac, "euro");
            _dic_name.Add("euro", 0x20ac);
            _dic_val.Add(0x2122, "trade");
            _dic_name.Add("trade", 0x2122);
            _dic_val.Add(0x2190, "larr");
            _dic_name.Add("larr", 0x2190);
            _dic_val.Add(0x2191, "uarr");
            _dic_name.Add("uarr", 0x2191);
            _dic_val.Add(0x2192, "rarr");
            _dic_name.Add("rarr", 0x2192);
            _dic_val.Add(0x2193, "darr");
            _dic_name.Add("darr", 0x2193);
            _dic_val.Add(0x2194, "harr");
            _dic_name.Add("harr", 0x2194);
            _dic_val.Add(0x21b5, "crarr");
            _dic_name.Add("crarr", 0x21b5);
            _dic_val.Add(0x2308, "lceil");
            _dic_name.Add("lceil", 0x2308);
            _dic_val.Add(0x2309, "rceil");
            _dic_name.Add("rceil", 0x2309);
            _dic_val.Add(0x230a, "lfloor");
            _dic_name.Add("lfloor", 0x230a);
            _dic_val.Add(0x230b, "rfloor");
            _dic_name.Add("rfloor", 0x230b);
            _dic_val.Add(0x25ca, "loz");
            _dic_name.Add("loz", 0x25ca);
            _dic_val.Add(0x2660, "spades");
            _dic_name.Add("spades", 0x2660);
            _dic_val.Add(0x2663, "clubs");
            _dic_name.Add("clubs", 0x2663);
            _dic_val.Add(0x2665, "hearts");
            _dic_name.Add("hearts", 0x2665);
            _dic_val.Add(0x2666, "diams");
            _dic_name.Add("diams", 0x2666);
        }
        /// <summary>
        /// 将HTML实体字符串解码
        /// </summary>
        /// <param name="entity">实体名 如 nbsp 返回空格 gt返回&gt;</param>
        /// <returns>解码后的字符</returns>
        public static char DecodeEntity(string entity)
        {
            if (entity[0] == '#')
            {
                entity = entity.Remove(0, 1);
                ushort ch;
                if (UInt16.TryParse(entity, out ch))
                    return (char)ch;
                return HtmlConst.CHEMPTY;
            }
            if (_dic_name.ContainsKey(entity))
                return (char)_dic_name[entity];
            return HtmlConst.CHEMPTY;
        }

        /// <summary>
        /// 将一个字符转换为 HTML实体字符 如 空格返回 &nbsp;
        /// </summary>
        /// <param name="c">要转换的字符</param>
        /// <returns>HTML实体字符串&*;</returns>
        public static string SpcialChar2Entity(char c)
        {
            if (_dic_val.ContainsKey(c))
                return string.Format("&{0};", _dic_val[c]);
            return char.ToString(c);
        }

        /// <summary>
        /// 将含有HTML实体字符的字符串解码
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static string DecodeHTMLString(string input)
        {
            return DecodeHTMLString(input, true);
        }
        /// <summary>
        /// 将含有HTML实体字符的字符串解码
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns></returns>
        public static string DecodeHTMLString(string input, bool skipWiteSpace)
        {
            char c;
            int i = 0;
            StringBuilder sb = new StringBuilder(input.Length);
            while (i < input.Length)
            {
                c = input[i];
                if (c == '&')
                {
                    i++;
                    int j = i;
                    while (j - i < _maxEntLen && j < input.Length)
                    {

                        if (input[j] == ';')
                        {
                            if (j - i > 0)
                            {
                                char e = DecodeEntity(new string(input.ToCharArray(i, j - i)));
                                if (e == HtmlConst.CHEMPTY)
                                    break;
                                sb.Append(e);
                            }
                            else
                                sb.Append("&;");
                            j++;
                            i = j;
                            break;
                        }
                        j++;
                    }
                    if (i != j)
                        sb.Append('&');
                    continue;
                }
                else
                {
                    if (skipWiteSpace)
                    {
                        if (!HtmlConst.IsWhiteSpace(c))
                            sb.Append(c);
                    }
                    else
                        sb.Append(c);


                }
                i++;
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 一些 char和string类型的常量
    /// </summary>
    public struct HtmlConst
    {

        /// <summary>
        /// &lt;
        /// </summary>
        public const char CHLT = '<';

        /// <summary>
        /// &gt;
        /// </summary>
        public const char CHGT = '>';

        /// <summary>
        /// 双引号 "
        /// </summary>
        public const char CHD = '"';

        /// <summary>
        /// 单引号 '
        /// </summary>
        public const char CHS = '\'';

        /// <summary>
        /// 叹号 !
        /// </summary>
        public const char CHT = '!';

        /// <summary>
        /// 问号
        /// </summary>
        public const char CHQ = '?';

        /// <summary>
        /// 斜杠 /
        /// </summary>
        public const char CHCL = '/';

        /// <summary>
        /// 反斜杠 \
        /// </summary>
        public const char CHTCL = '\\';

        /// <summary>
        /// 等号 =
        /// </summary>
        public const char CHE = '=';

        /// <summary>
        /// *
        /// </summary>
        public const char CHA = '*';

        /// <summary>
        /// -
        /// </summary>
        public const char CHM = '-';

        /// <summary>
        /// \r
        /// </summary>
        public const char CHNL = '\r';

        /// <summary>
        /// \n
        /// </summary>
        public const char CHLB = '\n';

        /// <summary>
        /// 空白符
        /// </summary>
        public const char CHEMPTY = '\0';

        /// <summary>
        /// 大写 SCRIPT
        /// </summary>
        public const string SCRIPT = "SCRIPT";

        /// <summary>
        /// 小写SCRIPT
        /// </summary>
        public const string SCRIPTL = "script";

        /// <summary>
        /// 注释开始 !--
        /// </summary>
        public const string COMMENTSSTART = "!--";

        /// <summary>
        /// 注释结束 --&gt;
        /// </summary>
        public const string COMMENTSEND = "-->";

        /// <summary>
        /// 叹号 !
        /// </summary>
        public const string T = "!";

        /// <summary>
        /// ID
        /// </summary>
        public const string ID = "ID";

        /// <summary>
        /// 检查是否是特殊标记
        /// </summary>
        /// <param name="elementTagName">大写标签名</param>
        /// <returns>如果是特殊标记返回True，不是特殊标记返回False</returns>
        public static bool IsSelfBackTag(string elementTagName)
        {

            switch (elementTagName)
            {
                case "IMG":
                case "INPUT":
                case "BR":
                case "HR":
                case "BASE":
                case "LINK":
                case "META":
                case "PARAM":
                case "AREA":
                case "ISINDEX":
                    return true;
                default:
                    return false;
            }

        }

        /// <summary>
        /// 检查是否是可以不写结束标记的元素
        /// </summary>
        /// <param name="elementTagName"></param>
        /// <returns></returns>
        internal static bool IsNoEndTag(string elementTagName)
        {
            return elementTagName == "P" || elementTagName == "LI";
        }

        /// <summary>
        /// 是否是标签结束符号
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static bool IsTagNameEndSymbol(char c)
        {
            switch (c)
            {
                case ' ':
                case '>':
                case '\n':
                case '\t':
                case '\r':
                case '/':
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 检查过滤
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static bool IsWhiteSpace(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case '\r':
                case '\t':
                    return true;
            }
            return false;
        }

        /*
         是否是空白符，排除掉换行符
         */
        internal static bool IsWhiteSpaceWithoutlineBreak(char c)
        {
            switch (c)
            {
                case ' ':
                case '　':
                case '\f':
                case '\t':
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 是否是属性结尾
        /// </summary>
        /// <param name="c">字符</param>
        /// <returns></returns>
        internal static bool IsAttrEndSymbol(char c)
        {
            switch (c)
            {
                case ' ':
                case '\n':
                case '\r':
                case '\t':
                case '>':
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 是否为空字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static bool IsNullOrEmpty(string input)
        {
            return input == null || input.Length == 0;
        }

        /// <summary>
        /// 转大写
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char ToUpper(char c)
        {

            return (c < 0x7b && c > 0x60) ? (char)(c - 0x20) : c;

        }

        /// <summary>
        /// 转小写
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static char ToLower(char c)
        {

            return (c < 0x5b && c > 0x40) ? (char)(c + 0x20) : c;

        }

    }
}
