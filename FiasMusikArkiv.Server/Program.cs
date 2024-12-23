using AutoMapper;
using FiasMusikArkiv.Server.Configuration;
using FiasMusikArkiv.Server.Data;
using FiasMusikArkiv.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<FiasMusikArkivDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Application"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://localhost:5173") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IDbContextSeeder, FiasMusikArkivDbContext>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

using (var seedScope = app.Services.CreateScope())
{
    var seeder = seedScope.ServiceProvider.GetService<IDbContextSeeder>();
    seeder.EnsureSeedData();
}

app.Run();
