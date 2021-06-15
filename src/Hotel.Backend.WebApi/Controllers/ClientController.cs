using Hotel.Backend.Domain.Models;
using Hotel.Backend.Domain.ViewModels;
using Hotel.Backend.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Backend.WebApi.Controllers
{
    public class ClientController : BaseApiController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = await _clientService.GetAllAsync();

            var items = query.Select(x => new ClientViewModel
            {
                Id = x.Id,
                Name = x.Name
            });

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = await _clientService.Query()
                .Include(x => x.ClientRatings)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (query == null)
                return NotFound($"client with id {id} not found");

            var item = new ClientViewModel
            {
                Id = query.Id,
                Name = query.Name,
                ClientRatings = query.ClientRatings.Select(x => new ClientRatingViewModel
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    Score = x.Score,
                    ClientId = x.ClientId,
                    HotelId = x.HotelId
                }).ToList()
            };

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new Client
            {
                Name = model.Name
            };

            await _clientService.AddAsync(item);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ClientViewModel model)
        {
            var item = await _clientService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"client with id {id} not found");

            item.Name = model.Name;

            await _clientService.UpdateAsync(item);

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _clientService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"client with id {id} not found");

            await _clientService.DeleteAsync(item);

            return Accepted();
        }
    }
}
