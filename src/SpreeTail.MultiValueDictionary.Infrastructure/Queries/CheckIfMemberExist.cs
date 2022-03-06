using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Queries
{
    public class CheckIfMemberExist
    {
        public class Query : IRequest<Result>
        {
            public string Key { get; set; }
            public string Member { get; set; }
        }

        public class Result 
        {
            public bool IsMemberExist { get; set; }

            public Result(bool isMemberExist)
            {
                IsMemberExist = isMemberExist;
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            public MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                //Checks if the key exist first if so it will check if the value exist.
                return new Result(dictionary.CheckIfMemberExist(query.Key, query.Member));
            }
        }
    }
}
