using AutoFixture;
using FluentAssertions;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Infrastructure.Commands;
using SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Tests.Commands
{
    public class RemoveMemberFacts
    {
        private SpreeTailContainer _container = new SpreeTailContainer();
        private MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

        [Fact]
        public async Task Should_Remove_Member()
        {
            dictionary.Add("RemoveMember","RemoveAllMem1");
            dictionary.Add("RemoveMember", "RemoveAllMem2");

            var command = _container.FakeBuild<RemoveMember.Command>()
                .With(x => x.Key, "RemoveMember")
                .With(x => x.Value, "RemoveAllMem2")
                .Create();

            var handler = _container.Get<RemoveMember.Handler>();
            var result = await handler.Handle(command, CancellationToken.None);

            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            dictionary.GetAllMembersOfAKey("RemoveMember").Contains("RemoveAllMem2").Should().BeFalse();
        }
        
        [Fact]
        public async Task Should_Not_Remove_When_key_Doesnot_Exist()
        {
            var command = _container.FakeBuild<RemoveMember.Command>()
                .With(x => x.Key, "RemoveMember12")
                .With(x => x.Value, "RemoveMember12")
                .Create();

            var handler = _container.Get<RemoveMember.Handler>();
            var result = await handler.Handle(command, CancellationToken.None);

            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            result.ErrorMessage.Should().Be(RemoveMember.Handler.KeyErrorMessage);
        }

        [Fact]
        public async Task Should_Not_Remove_When_Member_Doesnot_Exist()
        {
            dictionary.Add("RemoveMember123", "RemoveAllMem1");
            dictionary.Add("RemoveMember123", "RemoveAllMem2");

            var command = _container.FakeBuild<RemoveMember.Command>()
                .With(x => x.Key, "RemoveMember123")
                .With(x => x.Value, "RemoveAllMem3")
                .Create();

            var handler = _container.Get<RemoveMember.Handler>();
            var result = await handler.Handle(command, CancellationToken.None);

            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            result.ErrorMessage.Should().Be(RemoveMember.Handler.MemberErrorMessage);
        }
    }
}
