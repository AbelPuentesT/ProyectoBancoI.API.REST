using ProyectoBanco.Infrastructure.Extensiones;

var builder = WebApplication.CreateBuilder(args);

// Agregar services to the container.
builder.AddDbContexts();

builder.AddOptions();

builder.AddServices();

builder.AddJwtAuthentication();

builder.AddMVCFilters();

builder.AutoMapper();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
