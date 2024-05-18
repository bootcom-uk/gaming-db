using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
    public class IGDBTests
    {

        internal IGDB.IGDB _igdb;

        [SetUp]
        public void Setup()
        {
            _igdb = new IGDB.IGDB("clientId", "clientSecret");
        }

    }
}
