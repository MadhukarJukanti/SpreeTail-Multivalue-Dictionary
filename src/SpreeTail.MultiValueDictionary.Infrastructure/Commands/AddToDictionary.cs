using MediatR;
using SpreeTail.MultiValueDictionary.Common;
using SpreeTail.MultiValueDictionary.Common.Helpers;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.Infrastructure.Commands
{
    public static class AddToDictionary
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
            public const string ErrorMessage = "ERROR, member already exists for key";

            public async Task<Result> Handle(Command command, CancellationToken token)
            {
                //If the Key exist we check if the member already exist
                if (dictionary.CheckIfKeyExist(command.Key))
                {
                    //If the member exist we return the exception message.
                    if(dictionary.CheckIfMemberExist(command.Key, command.Value))
                    {
                        return new Result(ErrorMessage);
                    }
                    //update the list of values.
                    dictionary.Add(command.Key, command.Value);
                }
                else
                {
                    //If the key doesn't exist we add that to the dictionary.
                    dictionary.Add(command.Key, command.Value);
                }

                return new Result(HttpStatusCode.OK);
            }
        }
    }
}
