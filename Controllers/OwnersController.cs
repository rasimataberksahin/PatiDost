using App.Api.Data;
using App.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public OwnersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/owners
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var owners = await _dbContext.Owners.ToListAsync();
            return Ok(owners);
        }

        // GET: api/owners/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var owner = await _dbContext.Owners.FindAsync(id);

            if (owner == null)
                return NotFound("Owner bulunamadı");

            return Ok(owner);
        }

        // POST: api/owners
        [HttpPost]
        public async Task<IActionResult> Create(OwnerEntity owner)
        {
            _dbContext.Owners.Add(owner);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = owner.Id }, owner);
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OwnerEntity updated)
        {
            var owner = await _dbContext.Owners.FindAsync(id);
            if (owner == null)
                return NotFound();

            owner.FullName = updated.FullName;
            owner.Phone = updated.Phone;
            owner.Email = updated.Email;

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var owner = await _dbContext.Owners.FindAsync(id);
            if (owner == null)
                return NotFound();

            _dbContext.Owners.Remove(owner);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
