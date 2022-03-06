using AutoFixture;
using FluentAssertions;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Infrastructure.Queries;
using SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Tests.Queries
{
    public class GetAllKeysFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Get_All_Keys()
        {
            var keyList = new List<string>() { "key1", "key2", "key3", "key4", };

            dictionary.Add("key1", "val1");
            dictionary.Add("key2", "val2");
            dictionary.Add("key3", "val3");
            dictionary.Add("key4", "val4");

            var query = _container.FakeBuild<GetAllKeys.Query>().Create();
            var handler = _container.Get<GetAllKeys.Handler>();

            var result = await handler.Handle(query, CancellationToken.None);

            keyList.All(x => result.Keys.Contains(x)).Should().BeTrue();
        }
    }
}
