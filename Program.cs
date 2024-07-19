using Microsoft.EntityFrameworkCore;
using Siace.Data.Models;
using Siace.Data.Repository; 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<SiaceDbContext>

builder.Services.AddDbContext<SiaceDbContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("dbSiaceConnection")));    

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().AllowAnyOrigin().WithOrigins("http://localhost:4200");
}));

builder.Services.AddScoped<IPreguntasRepository, PreguntasRepository>(); 

var app = builder.Build();
app.UseCors("corsapp");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
