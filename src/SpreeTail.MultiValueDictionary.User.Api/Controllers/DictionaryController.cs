using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpreeTail.MultiValueDictionary.Infrastructure.Commands;
using SpreeTail.MultiValueDictionary.Infrastructure.Queries;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.User.Api.Controllers
{
    [Route("api/v1/dictionary")]
    public class DictionaryController : ControllerBase
    {
        private IMediator _mediator;
        public DictionaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all the keys from the dictionary
        /// Key: KEYS
        /// </summary>
        /// <returns></returns>
        [HttpGet("keys")]
        public async Task<IActionResult> GetAllKeys()
        {
            var query = new GetAllKeys.Query();
            var result = await _mediator.Send(query);
            return Ok(result.Keys);
        }

        /// <summary>
        /// Gets all the members of the key from the dictionary
        /// Key: MEMBERS
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("key/{key}/members")]
        public async Task<IActionResult> GetAllMembers([FromRoute] string key)
        {
            var query = new GetMembersByKey.Query
            {
                Key = key
            };
            var result = await _mediator.Send(query);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(result.Members);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        /// <summary>
        /// Adds key value pair to dictionary. 
        /// If the key exist it will update the values.
        /// Key: ADD
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddToDictionary(AddToDictionary.Command command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Tries to remove the member from the key value pair
        /// If it is the last key it will remove the set.
        /// Key: REMOVE
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("remove/{key}/Member/{value}")]
        public async Task<IActionResult> RemoveMember([FromRoute] string key, [FromRoute] string value)
        {
            var query = new RemoveMember.Command
            {
                Key = key,
                Value = value
            };
            var result = await _mediator.Send(query);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok("Removed Member");
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        /// <summary>
        /// Removes the key and value pair from dictionary
        /// Key: REMOVEALL
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPut("remove/key/{key}/all")]
        public async Task<IActionResult> RemoveKey([FromRoute] string key)
        {
            var query = new RemoveAll.Command
            {
                Key = key
            };

            var result = await _mediator.Send(query);
            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok("Removed key-value pair");
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }


        /// <summary>
        /// clears all the data from the dictionary.
        /// Key: CLEAR
        /// </summary>
        /// <returns></returns>
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearDictionary()
        {
            var query = new ClearAll.Command();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Checks if the key exist in the dictionary.
        /// Key: CHECKIFKEYEXIST
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("check-if-key-exist/key/{key}")]
        public async Task<IActionResult> CheckIfKeyExist([FromRoute] string key)
        {
            var query = new CheckIfKeyExist.Query
            {
                Key = key
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Checks if the member exist in any of the values.
        /// Key: CHECKIFMEMBEREXIST
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpGet("check-if-member-exist/key/{key}/member/{member}")]
        public async Task<IActionResult> CheckIfMemberExist([FromRoute] string key, [FromRoute] string member)
        {
            var query = new CheckIfMemberExist.Query
            {
                Key = key,
                Member = member
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets all the Members from the dictionary
        /// Key: MEMBERS
        /// </summary>
        /// <returns></returns>
        [HttpGet("members")]
        public async Task<IActionResult> GetAllMembers()
        {
            var query = new GetAllMembers.Query();
            var result = await _mediator.Send(query);
            return Ok(result.Members);
        }

        /// <summary>
        /// Gets all the keys and the values from dictionary
        /// Key: Items
        /// </summary>
        /// <returns></returns>
        [HttpGet("items")]
        public async Task<IActionResult> GetAllKeysAndValues()
        {
            var query = new GetAllKeysAndMembers.Query();
            var result = await _mediator.Send(query);
            return Ok(result.Keys);
        }
    }
}
