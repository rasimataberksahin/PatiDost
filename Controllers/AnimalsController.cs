using App.Api.Data;
using App.Api.DTOs;
using App.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public AnimalsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var animals = await _dbContext.Animals
                .Include(a => a.Owner)
                .Select(a => new AnimalListDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Type = a.Type,
                    Breed = a.Breed,
                    Age = a.Age,
                    OwnerName = a.Owner != null
                        ? a.Owner.FullName
                        : "-"
                })
                .ToListAsync();

            return Ok(animals);
        }

        // GET: api/animals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var animal = await _dbContext.Animals
                .Include(a => a.Owner)
                .Where(a => a.Id == id)
                .Select(a => new AnimalListDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Type = a.Type,
                    Age = a.Age,
                    OwnerName = a.Owner.FullName
                })
                .FirstOrDefaultAsync();

            if (animal == null)
                return NotFound("Hayvan bulunamadı");

            return Ok(animal);
        }

        // POST: api/animals
        [HttpPost]
        public async Task<IActionResult> Create(AnimalCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = await _dbContext.Owners
                .FirstOrDefaultAsync(o => o.Id == model.OwnerId);

            if (owner == null)
                return BadRequest("Geçersiz OwnerId");

            var animal = new AnimalEntity
            {
                Name = model.Name,
                Type = model.Type,
                Breed = model.Breed,
                Age = model.Age,
                OwnerId = model.OwnerId
            };

            _dbContext.Animals.Add(animal);
            await _dbContext.SaveChangesAsync();

            return Ok(animal);
        }

        // PUT: api/animals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AnimalUpdateDto model)
        {
            var animal = await _dbContext.Animals.FindAsync(id);
            if (animal == null)
                return NotFound();

            animal.Name = model.Name;
            animal.Type = model.Type;
            animal.Breed = model.Breed;
            animal.Age = model.Age;
            animal.OwnerId = model.OwnerId;

            await _dbContext.SaveChangesAsync();
            return NoContent(); // 204
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _dbContext.Animals.FindAsync(id);
            if (animal == null) return NotFound();

            _dbContext.Animals.Remove(animal);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
