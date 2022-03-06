using AutoFixture;
using FluentAssertions;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Infrastructure.Commands;
using SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Tests.Commands
{
    public class RemoveAllFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Remove_All()
        {
            dictionary.Add("RemoveAllKey", "RemoveAllMem1");
            dictionary.Add("RemoveAllKey", "RemoveAllMem2");

            var command = _container.FakeBuild<RemoveAll.Command>()
                .With(x => x.Key, "RemoveAllKey")
                .Create();

            var handler = _container.Get<RemoveAll.Handler>();
            var result = await handler.Handle(command, CancellationToken.None);

            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            dictionary.CheckIfKeyExist("RemoveAllKey").Should().BeFalse();
        }

        [Fact]
        public async Task Should_Not_RemoveAll()
        {
            var command = _container.FakeBuild<RemoveAll.Command>()
                .With(x => x.Key, "RemoveAllKey1")
                .Create();

            var handler = _container.Get<RemoveAll.Handler>();
            var result = await handler.Handle(command, CancellationToken.None);

            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            result.ErrorMessage.Should().Be(RemoveAll.Handler.KeyErrorMessage);
        }
    }
}
