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
    public class GetAllKeysAndMembersFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Get_Members_By_Key()
        {
            var memberList = new List<string>() { "mem1", "mem2", "mem3", "mem4" };
            var values = new List<string>() { "MembersByKey" };
            dictionary.Add("MembersByKey", "mem1");
            dictionary.Add("MembersByKey", "mem2");
            dictionary.Add("MembersByKey", "mem3");
            dictionary.Add("MembersByKey", "mem4");
            memberList.AddRange(values);

            var query = _container.FakeBuild<GetAllKeysAndMembers.Query>()
                .Create();
            var handler = _container.Get<GetAllKeysAndMembers.Handler>();

            var result = await handler.Handle(query, CancellationToken.None);

            memberList.All(x => result.Keys.Contains(x)).Should().BeTrue();
        }
    }
}
