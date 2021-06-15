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
    public class ImageController : BaseApiController
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = await _imageService.GetAllAsync();

            var items = query.Select(x => new ImageViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                HotelId = x.HotelId
            });

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = await _imageService.Query()
                .Include(x => x.Hotel)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (query == null)
                return NotFound($"image with id {id} not found");

            var item = new ImageViewModel
            {
                Id = query.Id,
                ImageUrl = query.ImageUrl,
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

        [HttpPost]
        public async Task<IActionResult> Create(ImageViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = new Image
            {
                ImageUrl = model.ImageUrl,
                HotelId = model.HotelId
            };

            await _imageService.AddAsync(item);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ImageViewModel model)
        {
            var item = await _imageService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"image with id {id} not found");

            item.ImageUrl = model.ImageUrl;
            item.HotelId = model.HotelId;

            await _imageService.UpdateAsync(item);

            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _imageService.GetByIdAsync(id);

            if (item == null)
                return NotFound($"image with id {id} not found");

            await _imageService.DeleteAsync(item);

            return Accepted();
        }
    }
}
