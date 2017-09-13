﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sia.Domain.ApiModels;
using Sia.Gateway.Authentication;
using Sia.Gateway.Requests;
using System.Threading.Tasks;

namespace Sia.Gateway.Controllers
{
    [Route("incidents/{incidentId}/engagements")]
    public class EngagementsController : BaseController
    {
        private const string notFoundMessage = "Incident or engagement not found";

        public EngagementsController(IMediator mediator, AzureActiveDirectoryAuthenticationInfo authConfig) 
            : base(mediator, authConfig)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]long incidentId, [FromRoute]long id)
        {
            var result = await _mediator.Send(new GetEngagementRequest(incidentId, id, new AuthenticatedUserContext(User, HttpContext.Session, _authConfig)));
            if (result == null)
            {
                return NotFound(notFoundMessage);
            }
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromRoute]long incidentId, [FromBody]NewEngagement newEngagement)
        {
            var result = await _mediator.Send(new PostEngagementRequest(incidentId, newEngagement, new AuthenticatedUserContext(User, HttpContext.Session, _authConfig)));
            if (result == null)
            {
                return NotFound(notFoundMessage);
            }
            return Created($"api/incidents/{result.IncidentId}/engagements/{result.Id}", result);
        }

        [HttpPut("{engagementId}")]
        public async Task<IActionResult> Put([FromRoute]long incidentId, [FromRoute]long engagementId, [FromBody]UpdateEngagement updatedEngagement)
        {
            await _mediator.Send(new PutEngagementRequest(incidentId, engagementId, updatedEngagement, new AuthenticatedUserContext(User, HttpContext.Session, _authConfig)));
            return Ok();
        }
    }
}