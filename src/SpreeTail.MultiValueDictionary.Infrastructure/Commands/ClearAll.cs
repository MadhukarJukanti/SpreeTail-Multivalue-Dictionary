using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Common.Helpers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Commands
{
    public static class ClearAll
    {
        public class Command : IRequest<Result>
        {
        }

        public class Result : BasicResult
        {
            public Result(HttpStatusCode status) : base(status)
            {
            }
        }
        public class Handler : IRequestHandler<Command, Result>
        {
            public MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {
                //Clears all the keys and values in the dictionary
                dictionary.ClearDictionary();
                return new Result(HttpStatusCode.OK);
            }
        }
    }
}
