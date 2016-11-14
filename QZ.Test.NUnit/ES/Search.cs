using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using QZ.Test.Client;

namespace QZ.Test.NUnit.ES
{
    [TestFixture]
    public class Search
    {
        [Test]
        public void Company_GenericFilterByTrade() => ES_Search.Company_GeneraiFilterByTrade();

        [Test]
        public void Company_NestByTrade() => ES_Search.Company_NestByTrade();

        [Test]
        public void Company_Script_Search() => ES_Search.Company_Script_Search();

    }
}
