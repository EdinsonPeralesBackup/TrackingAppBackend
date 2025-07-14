using Microsoft.AspNetCore.Mvc;
using Tracking.Api.Filter;
using Tracking.Application.TrustedContacts.Command.ChangeStatusConfidenceContact;
using Tracking.Application.TrustedContacts.Command.DeleteTrustedContact;
using Tracking.Application.TrustedContacts.Command.RegisterTrustedContact;
using Tracking.Application.TrustedContacts.Command.UpdateTrustedContact;
using Tracking.Application.TrustedContacts.Query.GetSpecificTrustedContact;
using Tracking.Application.TrustedContacts.Query.GetTrustedContact;

namespace Tracking.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizationFilter))]
    public class TrustedContactController : AbstractController
    {
        [HttpPost]
        [Route("registerTrustedContact")]
        [ProducesResponseType(typeof(RegisterTrustedContactCommand), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterTrustedContact(RegisterTrustedContactCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("updateTrustedContact")]
        [ProducesResponseType(typeof(UpdateTrustedContactCommand), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateTrustedContact(UpdateTrustedContactCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("changeStatusConfidenceContact/{idTrustedContact?}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeStatusConfidenceContact(int idTrustedContact)
        {
            var response = await Mediator.Send(new ChangeStatusConfidenceContactCommand()
            {
                Id = idTrustedContact
            });
            return Ok(response);
        }

        [HttpGet]
        [Route("getConfidenceContact")]
        public async Task<IActionResult> GetConfidenceContact()
        {
            var response = await Mediator.Send(new GetTrustedContactQuery()
            {
                IdUser = Convert.ToInt32(CurrentUser.Id)
            });
            return Ok(response);
        }

        [HttpGet]
        [Route("getSpecificConfidenceContact/{idTrustedContact}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpecificConfidenceContact(int idTrustedContact)
        {
            var response = await Mediator.Send(new GetSpecificTrustedContactQuery()
            {
                IdTrustedContact = idTrustedContact
            });
            return Ok(response);
        }

        [HttpDelete]
        [Route("deleteConfidenceContact/{idTrustedContact?}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteConfidenceContact(int idTrustedContact)
        {
            var response = await Mediator.Send(new DeleteTrustedContactCommand()
            {
                IdTrustedContact = idTrustedContact
            });
            return Ok(response);
        }
    }
}
