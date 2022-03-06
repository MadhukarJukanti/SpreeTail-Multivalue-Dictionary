using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Queries
{
    public static class GetAllKeys
    {
        public class Query : IRequest<Result>
        {
        }

        public class Result
        { 
            public List<string> Keys { get; set; }

            public Result(List<string> keys) 
            {
                Keys = keys;
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            public MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();
            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                //Returns all the keys of the dictionary.
                return new Result(dictionary.GetAllKeys());
            }
        }
    }
}
