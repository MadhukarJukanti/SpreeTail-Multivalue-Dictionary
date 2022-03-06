using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Common.Helpers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Commands
{
    public static class RemoveAll
    {
        public class Command : IRequest<Result>
        {
            public string Key { get; set; }
        }

        public class Result : BasicResult
        {
            public Result(string error) : base(error)
            {
            }

            public Result(HttpStatusCode status) : base(status)
            {
            }
        }
        public class Handler : IRequestHandler<Command, Result>
        {
            public MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

            public const string KeyErrorMessage = "ERROR, key does not exist";

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                //Checks if the key exist if so it will remove the key and value.
                if (dictionary.RemoveKey(command.Key))
                {
                    return new Result(HttpStatusCode.OK);
                }
                
                return new Result(KeyErrorMessage);
            }
        }
    }
}
