using AutoFixture;
using FluentAssertions;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Infrastructure.Queries;
using SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Tests.Queries
{
    public class CheckIfMemberExistFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Contain_Member()
        {
            dictionary.Add("CheckIfMemberExist", "value1");
            dictionary.Add("CheckIfMemberExist", "value2");

            var query = _container.FakeBuild<CheckIfMemberExist.Query>()
                .With(x => x.Member, "value1")
                .With(x => x.Key, "CheckIfMemberExist")
                .Create();

            var handler = _container.Get<CheckIfMemberExist.Handler>();
            var result = await handler.Handle(query, CancellationToken.None);

            result.IsMemberExist.Should().BeTrue();
            dictionary.GetAllMembersOfAKey("CheckIfMemberExist").Contains("value1").Should().BeTrue();
        }

        [Fact]
        public async Task Should_Contain_Not_Member()
        {
            dictionary.Add("CheckIfMemberExist12", "value1");
            dictionary.Add("CheckIfMemberExist12", "value2");
            var query = _container.FakeBuild<CheckIfMemberExist.Query>()
                .With(x => x.Member, "value5")
                .With(x => x.Key, "CheckIfMemberExist12")
                .Create();

            var handler = _container.Get<CheckIfMemberExist.Handler>();
            var result = await handler.Handle(query, CancellationToken.None);

            result.IsMemberExist.Should().BeFalse();
            dictionary.GetAllMembersOfAKey("CheckIfMemberExist12").Contains("value5").Should().BeFalse();
        }
    }
}
