using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RodosApi.Contract;
using RodosApi.Contract.V1.Request;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Contract.V1.Response;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using RodosApi.Helpers;
using RodosApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Controllers.V1
{
    public class ClientController :  Controller
    {
        private IClientService _clientService;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;


        public ClientController(IClientService clientService, IMapper mapper, IUriService uriService )
        {
            _clientService = clientService;
            _mapper = mapper;
            _uriService = uriService;
        }
        [HttpGet(ApiRoutes.Client.GetClients)]
        public async Task<IActionResult> GetClients(PaginationQuery paginationQuery,string name,ClientSortingQuery clientSorting)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var sorting = _mapper.Map<ClientSorting>(clientSorting);

            var clients = await _clientService.GetClients(pagination, name,sorting);
            var clientResponse = _mapper.Map<List<ClientResponse>>(clients);
            if(pagination == null|| pagination.PageNumber<1|| pagination.PageSize < 1)
            {
                return Ok(clientResponse);
            }

           var result =  PaginationHelpers.CreatePaginationResponse(_uriService, pagination, clientResponse);

            return Ok(result);
        }

        [HttpGet(ApiRoutes.Client.GetClient)]
        public async Task<IActionResult> GetClient(long clientId)
        {
            var client = await _clientService.GetClient(clientId);
            if(client == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ClientResponse>(client));
        } 

        [HttpPost(ApiRoutes.Client.CreateClient)]
        public async Task<IActionResult> CreateClient([FromBody]ClientToCreate clientToCreate)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var clientValidation = await _clientService.ClientValidation(clientToCreate.Email);
            if(clientValidation == false)
            {
                ModelState.AddModelError("", "Client with same the same email is already exists");
                return Conflict(ModelState);
            }

            var client = _mapper.Map<Client>(clientToCreate);

            if(await _clientService.CreateClient(client) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }

            var baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Client.GetClient).Replace("{clientId}", client.ClientId.ToString());
            return Created(new Uri(baseUri),$"{client.Name} was exists with this Id - {client.ClientId}");
        }
        [HttpPut(ApiRoutes.Client.UpdateClient)]

        public async Task<IActionResult> UpdateClient(long clientId,[FromBody] ClientToUpdate clientToUpdate)
        {
            var client = await _clientService.GetClient(clientId);

            if(client == null)
            {
                return NotFound();
            }

            var clientValidation = await _clientService.ClientValidation(clientToUpdate.Email,clientId);

            if(clientValidation == false)
            {
                ModelState.AddModelError("", "Client with same the same email is already exists");
                return Conflict(ModelState);
            }

            client.Address = clientToUpdate.Adress;
            client.Email = clientToUpdate.Email;
            client.LastName = clientToUpdate.LastName;
            client.Name = clientToUpdate.Name;
            client.Phone = clientToUpdate.Phone;

            if(await _clientService.UpdateClient(client) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok(_mapper.Map<ClientResponse>(client));
        }
        [HttpDelete(ApiRoutes.Client.DeleteClient)]

        public async Task<IActionResult> DeleteClient(long clientId)
        {
            var client = await _clientService.GetClient(clientId);
            if(client is null)
            {
                return NotFound();
            }

            if(await _clientService.DeleteClient(client) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        
    }
}
