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
    public class AddToDictionaryFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Add_To_Dictionary()
        {
            var command = _container.FakeBuild<AddToDictionary.Command>()
                .With(x => x.Key, "Key")
                .With(x => x.Value, "Value")
                .Create();

            var handler = _container.Get<AddToDictionary.Handler>();
            var result = await handler.Handle(command, CancellationToken.None);
            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Not_Add_To_Dictionary()
        {
            dictionary.Add("Key1", "Value1");
            var command = _container.FakeBuild<AddToDictionary.Command>()
                .With(x => x.Key, "Key1")
                .With(x => x.Value, "Value1")
                .Create();
            var handler = _container.Get<AddToDictionary.Handler>();
            var result = await handler.Handle(command, CancellationToken.None);
            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            result.ErrorMessage.Should().Be(AddToDictionary.Handler.ErrorMessage);
        }
    }
}
