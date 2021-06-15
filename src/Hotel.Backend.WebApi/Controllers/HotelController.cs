using Hotel.Backend.Domain.Enums;
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
    public class HotelController : BaseApiController
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = await _hotelService.GetAllAsync();

            var items = query.Select(x => new HotelViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category,
                Price = x.Price
            });

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = await _hotelService.Query()
                .Include(x => x.ClientRatings)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (query == null)
                return NotFound($"hotel with id {id} not found");

            var item = new HotelViewModel
            {
                Id = query.Id,
                Name = query.Name,
                Price = query.Price,
                Category = query.Category,
                Images = query.Images.Select(x => new ImageViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    HotelId = x.HotelId
                }).ToList(),
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

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetAllByCategory(int categoryId)
        {
            var query = await _hotelService.QueryNoTracking()
                .Include(x => x.ClientRatings)
                .Include(x => x.Images)
                .Where(x => x.Category == (Category)categoryId)
                .ToListAsync();

            if (query == null)
                return Ok(new List<HotelViewModel>());

            var items = query.Select(x => new HotelViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Category = x.Category,
                Images = x.Images.Select(x => new ImageViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    HotelId = x.HotelId
                }).ToList(),
                ClientRatings = x.ClientRatings.Select(y => new ClientRatingViewModel
                {
                    Id = y.Id,
                    Comment = y.Comment,
                    Score = y.Score,
                    ClientId = y.ClientId,
                    HotelId = y.HotelId
                }).ToList()
            }).ToList();

            return Ok(items);
        }

        [HttpGet("by-price/{price}")]
        public async Task<IActionResult> GetAllByPrice(double price)
        {
            var query = await _hotelService.QueryNoTracking()
                .Include(x => x.ClientRatings)
                .Include(x => x.Images)
                .Where(x => x.Price == price)
                .OrderByDescending(x => x.Price) // ordena por precio de mayor a menor
                .ToListAsync();

            if (query == null)
                return Ok(new List<HotelViewModel>());

            var items = query.Select(x => new HotelViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Category = x.Category,
                Images = x.Images.Select(x => new ImageViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    HotelId = x.HotelId
                }).ToList(),
                ClientRatings = x.ClientRatings.Select(y => new ClientRatingViewModel
                {
                    Id = y.Id,
                    Comment = y.Comment,
                    Score = y.Score,
                    ClientId = y.ClientId,
                    HotelId = y.HotelId
                }).ToList()
            }).ToList();

            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HotelViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new Domain.Models.Hotel
            {
                Name = model.Name,
                Price = model.Price,
                Category = model.Category
            };

            await _hotelService.AddAsync(item);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HotelViewModel model)
        {
            var item = await _hotelService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"hotel with id {id} not found");

            item.Name = model.Name;
            item.Price = model.Price;
            item.Category = model.Category;

            await _hotelService.UpdateAsync(item);

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _hotelService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"hotel with id {id} not found");

            await _hotelService.DeleteAsync(item);

            return Accepted();
        }
    }
}
