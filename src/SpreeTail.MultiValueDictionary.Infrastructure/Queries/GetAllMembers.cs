using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Queries
{
    public static class GetAllMembers
    {
        public class Query : IRequest<Result>
        {
        }

        public class Result 
        {
            public List<string> Members { get; set; }
            public Result(List<string> members)
            {
                Members = members;
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            public MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                //Gets all the memebers of dictionary.
                return new Result(dictionary.GetAllMembers());
            }
        }
    }
}
