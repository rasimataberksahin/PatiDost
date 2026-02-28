using App.Api.Data;
using App.Api.DTOs;
using App.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinariansController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public VeterinariansController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/veterinarians
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vets = await _dbContext.Veterinarians
                .Select(v => new VeterinarianListDto
                {
                    Id = v.Id,
                    FullName = v.FullName,
                    Specialization = v.Specialization
                })
                .ToListAsync();

            return Ok(vets);
        }

        // GET: api/veterinarians/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vet = await _dbContext.Veterinarians
                .Where(v => v.Id == id)
                .Select(v => new VeterinarianListDto
                {
                    Id = v.Id,
                    FullName = v.FullName,
                    Specialization = v.Specialization
                })
                .FirstOrDefaultAsync();

            if (vet == null)
                return NotFound("Veteriner bulunamadı");

            return Ok(vet);
        }

        // POST: api/veterinarians
        [HttpPost]
        public async Task<IActionResult> Create(VeterinarianCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vet = new VeterinarianEntity
            {
                FullName = model.FullName,
                Specialization = model.Specialization
            };

            _dbContext.Veterinarians.Add(vet);
            await _dbContext.SaveChangesAsync();

            return Ok(vet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VeterinarianEntity updated)
        {
            var vet = await _dbContext.Veterinarians.FindAsync(id);
            if (vet == null) return NotFound();

            vet.FullName = updated.FullName;
            vet.Specialization = updated.Specialization;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/veterinarians/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vet = await _dbContext.Veterinarians.FindAsync(id);

            if (vet == null)
                return NotFound("Veteriner bulunamadı");

            _dbContext.Veterinarians.Remove(vet);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
