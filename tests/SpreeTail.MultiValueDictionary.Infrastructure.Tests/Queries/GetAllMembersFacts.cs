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
    public class GetAllMembersFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Get_All_Members()
        {
            var memberList = new List<string>() { "mem1", "mem2", "mem3", "mem4"};

            dictionary.Add("AllMembers", "mem1");
            dictionary.Add("AllMembers", "mem2");
            dictionary.Add("AllMembers", "mem3");
            dictionary.Add("AllMembers", "mem4");

            var query = _container.FakeBuild<GetAllMembers.Query>().Create();
            var handler = _container.Get<GetAllMembers.Handler>();

            var result = await handler.Handle(query, CancellationToken.None);

            memberList.All(x => result.Members.Contains(x)).Should().BeTrue();
        }
    }
}
