using App.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Controllers
builder.Services.AddControllers();

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// 🔹 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 DbContext (ASIL EKSİK OLAN)
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

// 🔹 Swagger pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<App.Api.Middlewares.ExceptionMiddleware>();

app.UseHttpsRedirection();



app.UseCors("AllowAll");
//

app.UseAuthorization();

app.MapControllers();

app.Run();

