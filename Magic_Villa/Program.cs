using Magic_Villa;
using Magic_Villa.Data;
using Magic_Villa.Repo;
using Magic_Villa.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"));
});
//
builder.Services.AddScoped<IVillaRepository, VillaRepostory>();
//
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepostory>();
//
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
