using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpreeTail.MultiValueDictionary.Infrastructure.Commands;
using SpreeTail.MultiValueDictionary.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.SystemWeb
{
    public class ConsoleApplication : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public ConsoleApplication(ILogger<ConsoleApplication> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Spreetail MultiValue Dictionary API");
            var input = new List<string>() { "start" };
            while (input.Any())
            {
                Console.WriteLine("Please select one of the option. Enter STOP to exit");
                Console.WriteLine("KEYS, MEMBERS, ADD, REMOVE, REMOVEALL, CLEAR, KEYIFEXISTS, MEMBEREXISTS, ITEMS");

                try
                {
                    //If user enters more than one space ignore those.
                    input = Console.ReadLine().Split(' ').Where(x => x != string.Empty).ToList();
                }
                catch (FormatException)
                {
                    input = new List<string>();
                }

                switch (input.First())
                {
                    case "KEYS":
                        var getKeysQuery = new GetAllKeys.Query();
                        var getKeysResult = await _mediator.Send(getKeysQuery, cancellationToken);

                        Console.WriteLine($"{string.Join("\r\n", getKeysResult.Keys)} \r\n");
                        break;
                    case "MEMBERS":
                        if (input.Count < 2)
                        {
                            Console.WriteLine("Please enter valid input \r\n");
                        }
                        else
                        {
                            var getMembersQuery = new GetMembersByKey.Query
                            {
                                Key = input[1]
                            };

                            var getMembersResult = await _mediator.Send(getMembersQuery, cancellationToken);

                            if (getMembersResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Console.WriteLine($"{string.Join("\r\n", getMembersResult.Members)}\r\n");
                            }
                            else
                            {
                                Console.WriteLine($"{ getMembersResult.ErrorMessage }\r\n");
                            }
                        }
                        break;
                    case "ADD":
                        if (input.Count < 3)
                        {
                            Console.WriteLine("Please enter valid input \r\n");
                        }
                        else
                        {
                            var addToDictionaryquery = new AddToDictionary.Command
                            {
                                Key = input[1],
                                Value = input[2]
                            };

                            var addToDictionaryResult = await _mediator.Send(addToDictionaryquery, cancellationToken);

                            if (addToDictionaryResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Console.WriteLine("Added \r\n");
                            }
                            else
                            {
                                Console.WriteLine($"{ addToDictionaryResult.ErrorMessage }\r\n");
                            }
                        }
                        break;
                    case "REMOVE":
                        if (input.Count < 3)
                        {
                            Console.WriteLine("Please enter valid input \r\n");
                        }
                        else
                        {
                            var removeMemberQuery = new RemoveMember.Command
                            {
                                Key = input[1],
                                Value = input[2]
                            };

                            var removeMemberResult = await _mediator.Send(removeMemberQuery, cancellationToken);

                            if (removeMemberResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Console.WriteLine("Removed \r\n");
                            }
                            else
                            {
                                Console.WriteLine($"{ removeMemberResult.ErrorMessage}\r\n");
                            }
                        }
                        break;

                    case "REMOVEALL":
                        if (input.Count < 2)
                        {
                            Console.WriteLine("Please enter valid input \r\n");
                        }
                        else
                        {
                            var removeAllQuery = new RemoveAll.Command
                            {
                                Key = input[1]
                            };

                            var removeAllResult = await _mediator.Send(removeAllQuery, cancellationToken);

                            if (removeAllResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Console.WriteLine("Removed All \r\n");
                            }
                            else
                            {
                                Console.WriteLine($"{ removeAllResult.ErrorMessage}\r\n");
                            }
                        }
                        break;
                    case "CLEAR":
                        var clearAllQuery = new ClearAll.Command();
                        await _mediator.Send(clearAllQuery, cancellationToken);

                        Console.WriteLine("Cleared \r\n");
                        break;
                    case "KEYIFEXISTS":
                        if (input.Count < 2)
                        {
                            Console.WriteLine("Please enter valid input \r\n");
                        }
                        else
                        {
                            var keyExistQuery = new CheckIfKeyExist.Query
                            {
                                Key = input[1]
                            };

                            var keyExistResult = await _mediator.Send(keyExistQuery, cancellationToken);
                            if (keyExistResult.IsKeyExist)
                            {
                                Console.WriteLine("true \r\n");
                            }
                            else
                            {
                                Console.WriteLine("false \r\n");
                            }
                        }
                        break;

                    case "MEMBERIFEXISTS":
                        if (input.Count < 3)
                        {
                            Console.WriteLine("Please enter valid input \r\n");
                        }
                        else
                        {
                            var memberExistQuery = new CheckIfMemberExist.Query
                            {
                                Key = input[1],
                                Member = input[2]
                            };

                            var memberExistResult = await _mediator.Send(memberExistQuery, cancellationToken);

                            if (memberExistResult.IsMemberExist)
                            {
                                Console.WriteLine("true \r\n");
                            }
                            else
                            {
                                Console.WriteLine("false \r\n");
                            }
                        }
                        break;
                    case "ALLMEMBERS":
                        var getAllMemberQuery = new GetAllMembers.Query();

                        var getAllMemberResult = await _mediator.Send(getAllMemberQuery, cancellationToken);

                        Console.WriteLine($"{string.Join("\r\n", getAllMemberResult.Members)} \r\n");
                        break;
                    case "ITEMS":
                        var getAllKeyAndMemberQuery = new GetAllKeysAndMembers.Query();

                        var getAllKeyAndMemberResult = await _mediator.Send(getAllKeyAndMemberQuery, cancellationToken);
                        Console.WriteLine($"{string.Join("\r\n", getAllKeyAndMemberResult.Keys)}\r\n");
                        break;
                    case "STOP":
                        input = default;
                        break;
                }
            }
            return;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            return;
        }
    }
}
