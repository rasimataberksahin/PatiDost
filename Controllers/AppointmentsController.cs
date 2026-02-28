using App.Api.Data;
using App.Api.DTOs;
using App.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AppointmentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _db.Appointments
            .Include(a => a.Animal)
            .Include(a => a.Veterinarian)
            .Select(a => new AppointmentListDto
            {
                Id = a.Id,
                AnimalName = a.Animal.Name,
                VeterinarianName = a.Veterinarian.FullName,
                AppointmentDate = a.AppointmentDate,
                Description = a.Description
            }).ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var a = await _db.Appointments.FindAsync(id);
        if (a == null) return NotFound();

        return Ok(new
        {
            id = a.Id,
            animalId = a.AnimalId,
            veterinarianId = a.VeterinarianId,
            appointmentDate = a.AppointmentDate,
            description = a.Description
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppointmentCreateDto dto)
    {
        var a = new AppointmentEntity
        {
            AnimalId = dto.AnimalId,
            VeterinarianId = dto.VeterinarianId,
            AppointmentDate = dto.AppointmentDate,
            Description = dto.Description
        };

        _db.Appointments.Add(a);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AppointmentCreateDto dto)
    {
        var a = await _db.Appointments.FindAsync(id);
        if (a == null) return NotFound();

        a.AnimalId = dto.AnimalId;
        a.VeterinarianId = dto.VeterinarianId;
        a.AppointmentDate = dto.AppointmentDate;
        a.Description = dto.Description;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var a = await _db.Appointments.FindAsync(id);
        if (a == null) return NotFound();

        _db.Appointments.Remove(a);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
