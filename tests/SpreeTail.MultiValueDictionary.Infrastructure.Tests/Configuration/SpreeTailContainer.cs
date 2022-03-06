using AutoFixture;
using AutoFixture.Dsl;
using Lamar;
using SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration.Registry;
using System;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Tests.Configuration
{
    public class SpreeTailContainer : IDisposable
    {
        public readonly IContainer _container;
        public SpreeTailContainer()
        {
            _container = new Container(container =>
            {
                container.IncludeRegistry<MediatrRegistry>();
            });
        }

        public ICustomizationComposer<T> FakeBuild<T>()
        {
            var fixture = Get<Fixture>();
            return fixture.Build<T>();
        }

        public T Get<T>()
        {
            return _container.GetInstance<T>();
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}
