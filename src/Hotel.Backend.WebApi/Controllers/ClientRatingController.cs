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
    public class ClientRatingController : BaseApiController
    {
        private readonly IClientRatingService _clientRatingService;

        public ClientRatingController(IClientRatingService clientRatingService)
        {
            _clientRatingService = clientRatingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = await _clientRatingService.GetAllAsync();

            var items = query.Select(x => new ClientRatingViewModel
            {
                Id = x.Id,
                Comment = x.Comment,
                Score = x.Score,
                ClientId = x.ClientId,
                HotelId = x.HotelId
            });

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = await _clientRatingService.Query()
                .Include(x => x.Hotel)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (query == null)
                return NotFound($"client rating with id {id} not found");

            var item = new ClientRatingViewModel
            {
                Id = query.Id,
                Comment = query.Comment,
                Score = query.Score,
                ClientId = query.ClientId,
                Client = new ClientViewModel
                {
                    Id = query.Client.Id,
                    Name = query.Client.Name
                },
                HotelId = query.HotelId,
                Hotel = new HotelViewModel
                {
                    Id = query.Hotel.Id,
                    Name = query.Hotel.Name,
                    Price = query.Hotel.Price,
                    Category = query.Hotel.Category
                }
            };

            return Ok(item);
        }

        [HttpGet("by-score/{score}")]
        public async Task<IActionResult> GetAllByScore(int score)
        {
            var query = await _clientRatingService.QueryNoTracking()
                .Include(x => x.Client)
                .Include(x => x.Hotel)
                .Where(x => x.Score == score)
                .ToListAsync();

            if (query == null)
                return Ok(new List<ClientRatingViewModel>());

            var items = query.Select(x => new ClientRatingViewModel
            {
                Id = x.Id,
                Score = x.Score,
                Comment = x.Comment,
                HotelId = x.HotelId,
                Hotel = new HotelViewModel
                {
                    Id = x.Hotel.Id,
                    Name = x.Hotel.Name,
                    Price = x.Hotel.Price,
                    Category = x.Hotel.Category
                },
                ClientId = x.ClientId,
                Client = new ClientViewModel
                {
                    Id = x.Client.Id,
                    Name = x.Client.Name
                }
            }).ToList();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientRatingViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new ClientRating
            {
                Comment = model.Comment,
                Score = model.Score,
                ClientId = model.ClientId,
                HotelId = model.HotelId
            };

            await _clientRatingService.AddAsync(item);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ClientRatingViewModel model)
        {
            var item = await _clientRatingService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"client rating with id {id} not found");

            item.Comment = model.Comment;
            item.Score = model.Score;
            item.ClientId = model.ClientId;
            item.HotelId = model.HotelId;

            await _clientRatingService.UpdateAsync(item);

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _clientRatingService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"client rating with id {id} not found");

            await _clientRatingService.DeleteAsync(item);

            return Accepted();
        }
    }
}
