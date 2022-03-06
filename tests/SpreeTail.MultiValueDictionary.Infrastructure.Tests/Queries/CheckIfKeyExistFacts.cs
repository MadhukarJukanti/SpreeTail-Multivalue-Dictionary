using AutoFixture;
using FluentAssertions;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Infrastructure.Queries;
using SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Tests.Queries
{
    public class CheckIfKeyExistFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Contain_Key()
        {
            dictionary.Add("CheckIfKeyExist", "value1");
            dictionary.Add("CheckIfKeyExist", "value2");

            var query = _container.FakeBuild<CheckIfKeyExist.Query>()
                .With(x => x.Key, "CheckIfKeyExist")
                .Create();

            var handler = _container.Get<CheckIfKeyExist.Handler>();
            var result = await handler.Handle(query, CancellationToken.None);

            result.IsKeyExist.Should().BeTrue();
            dictionary.CheckIfKeyExist("CheckIfKeyExist").Should().BeTrue();
        }

        [Fact]
        public async Task Should_Contain_Not_Key()
        {
            var query = _container.FakeBuild<CheckIfKeyExist.Query>()
                .With(x => x.Key, "CheckIfKeyExist1")
                .Create();

            var handler = _container.Get<CheckIfKeyExist.Handler>();
            var result = await handler.Handle(query, CancellationToken.None);

            result.IsKeyExist.Should().BeFalse();
            dictionary.CheckIfKeyExist("CheckIfKeyExist1").Should().BeFalse();
        }
    }
}
