//using AutoFixture;
//using FluentAssertions;
//using SpreeTail.MultiValueDictionary.Common;
//using SpreeTail.MultiValueDictionary.Infrastructure.Commands;
//using SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Xunit;

//namespace SpreeTail.MultiValueDictionary.Infrastructure.Tests.Commands
//{
//    public class ClearAllFacts
//    {
//        private SpreeTailContainer _container = new SpreeTailContainer();

//        [Fact]
//        [TestCollectionOrderer(1)]
//        public async Task Should_Clear_Dictionary()
//        {
//            var dictionary = Global.GetDictionary();
//            dictionary.TryAdd("ClearKey1", new List<string> { "ClearMem1" });
//            dictionary.TryAdd("ClearKey12", new List<string> { "ClearMem12" });

//            var command = _container.FakeBuild<ClearAll.Command>().Create();

//            var handler = _container.Get<ClearAll.Handler>();

//            var result = await handler.Handle(command, CancellationToken.None);

//            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.OK);
//            dictionary.ContainsKey("ClearKey1").Should().BeFalse();
//            dictionary.ContainsKey("ClearKey12").Should().BeFalse();
//        }

//        [Fact]
//        public async Task Should_Not_Clear_Dictionary()
//        {
//            var dictionary = Global.GetDictionary();
//            var command = _container.FakeBuild<ClearAll.Command>().Create();

//            var handler = _container.Get<ClearAll.Handler>();

//            var result = await handler.Handle(command, CancellationToken.None);
//            result.HttpStatusCode.Should().Be(System.Net.HttpStatusCode.OK);
//        }
//    }
//}
