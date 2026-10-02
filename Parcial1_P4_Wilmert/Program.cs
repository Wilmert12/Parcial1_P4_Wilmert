using Parcial1_P4_Wilmert.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();          // Genera el documento OpenAPI

// Registro del servicio de acceso a datos
builder.Services.AddScoped<NumbersService>();

var app = builder.Build();

// Crear la tabla al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var numbersService = scope.ServiceProvider.GetRequiredService<NumbersService>();
    await numbersService.InitializeAsync();
}

{
    app.MapOpenApi();                   // /openapi/v1.json
    app.MapScalarApiReference();        // /scalar/v1
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();