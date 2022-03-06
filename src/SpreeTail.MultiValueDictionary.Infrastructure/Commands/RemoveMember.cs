using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Common.Helpers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Commands
{
    public static class RemoveMember
    {
        public class Command : IRequest<Result>
        {
            public string Key { get; set; }
            
            public string Value { get; set; }
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
            public const string MemberErrorMessage = "ERROR, member does not exist";

            public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
            {

                //Check if the key exist 
                if(dictionary.CheckIfKeyExist(command.Key))
                {
                    //Check if the member exist.
                    if(dictionary.CheckIfMemberExist(command.Key, command.Value))
                    {
                        //Remove the member 
                        //If this is the last key it removes the key-value pair.
                        dictionary.RemoveMember(command.Key, command.Value);
                        return new Result(HttpStatusCode.OK);
                    }
                    else
                    {
                        //Returns error if the values doesn't exist.
                        return new Result(MemberErrorMessage);
                    }
                }

                //Returns error if the key doesn't exist.
                return new Result(KeyErrorMessage);
            }
        }
    }
}
