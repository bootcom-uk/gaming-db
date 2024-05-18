using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tests
{
    
    public class Search
    {

        internal CeX.Search _search;

        [SetUp]
        public void Setup()
        {
            _search = new CeX.Search();
        }

        [Test]
        public async Task Search_Get_NonNull_Value_IsNot_Null()
        {

            var result = await _search.Get("1");
            Assert.That(result is not null);
        }

        [Test]
        public async Task Search_Get_NonNull_Value_Is_Null()
        {

            var result = await _search.Get(null);
            Assert.That(result is null);
        }

    }
}
