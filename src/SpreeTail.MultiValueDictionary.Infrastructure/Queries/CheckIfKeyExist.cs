using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Queries
{
    public static class CheckIfKeyExist
    {
        public class Query : IRequest<Result>
        {
            public string Key { get; set; }
        }

        public class Result
        {
            public bool IsKeyExist { get; set; }

            public Result(bool isKeyExist)
            {
                IsKeyExist = isKeyExist;
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            public MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                //Checks if the key exist in the dictionary.
                return new Result(dictionary.CheckIfKeyExist(query.Key));
            }
        }
    }
}
