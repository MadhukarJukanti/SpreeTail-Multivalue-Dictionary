using AutoFixture;
using FluentAssertions;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Infrastructure.Queries;
using SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Tests.Queries
{
    public class GetMembersByKeyFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Get_Members_By_Key()
        {
            var memberList = new List<string> { "mem1", "mem2", "mem3", "mem4" };
            dictionary.Add("MembersByKey", "mem1");
            dictionary.Add("MembersByKey", "mem2");
            dictionary.Add("MembersByKey", "mem3");
            dictionary.Add("MembersByKey", "mem4");

            var query = _container.FakeBuild<GetMembersByKey.Query>()
                .With(x => x.Key, "MembersByKey")
                .Create();
            var handler = _container.Get<GetMembersByKey.Handler>();

            var result = await handler.Handle(query, CancellationToken.None);

            memberList.All(x => result.Members.Contains(x)).Should().BeTrue();
        }

        [Fact]
        public async Task Should_Not_Get_Members_By_Key()
        {

            dictionary.Add("MembersByKey1", "test");

            var query = _container.FakeBuild<GetMembersByKey.Query>()
               .With(x => x.Key, "MembersByKey12")
               .Create();
            var handler = _container.Get<GetMembersByKey.Handler>();

            var result = await handler.Handle(query, CancellationToken.None);

            result.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            result.ErrorMessage.Should().Be(GetMembersByKey.Handler.ErrorMessage);
        }
    }
}
