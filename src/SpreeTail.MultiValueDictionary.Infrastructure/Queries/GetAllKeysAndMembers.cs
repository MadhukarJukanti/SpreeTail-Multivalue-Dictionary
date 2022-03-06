using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Common.Helpers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Queries
{
    public static class GetAllKeysAndMembers
    {
        public class Query : IRequest<Result>
        {
        }

        public class Result : BasicResult
        {
            public Result(string error) : base(error)
            {
            }

            public Result(HttpStatusCode status) : base(status)
            {
            }
            public List<string> Keys { get; set; }

            public Result(List<string> keys) : base()
            {
                Keys = keys;
                HttpStatusCode = HttpStatusCode.OK;
            }
        }
        public class Handler : IRequestHandler<Query, Result>
        {
            public MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                //Returns all the keys and values of the dictionary.
                return new Result(dictionary.GetAllKeysAndValues());
            }
        }
    }
}
