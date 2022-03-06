using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Common.Helpers;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Queries
{
    public static class GetMembersByKey
    {
        public class Query : IRequest<Result>
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
            public List<string> Members { get; set; }

            public Result(List<string> keys) 
            {
                Members = keys;
                HttpStatusCode = HttpStatusCode.OK;
            }
        }

        public class Handler : IRequestHandler<Query, Result>
        {
            public MultiValueDataDictionary dictionary = MultiValueDataDictionary.GetInstance();

            public const string ErrorMessage = "ERROR, key does not exist";

            public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
            {
                //Checks if the key exist
                if(dictionary.CheckIfKeyExist(query.Key))
                {
                    //Returns all the members of that key.
                    return new Result(dictionary.GetAllMembersOfAKey(query.Key));
                }

                return new Result(ErrorMessage);
            }
        }
    }
}
