using Lamar;
using MediatR;

namespace SpreeTail.MultiValueDictionary.Host.Lamar.Registry
{
    public class MediatrRegistry : ServiceRegistry
    {
        public MediatrRegistry()
        {
            Scan(scanner =>
            {
                scanner.AssembliesAndExecutablesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith("SpreeTail.MultiValueDictionary"));
                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
            });
           
            For<IMediator>().Use<Mediator>();
            For<ServiceFactory>().Use(ctx => ctx.GetInstance);
        }
    }
}
