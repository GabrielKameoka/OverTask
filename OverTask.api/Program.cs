using Microsoft.EntityFrameworkCore;
using OverTask.api.Controllers;
using OverTask.api.Data;
using OverTask.api.Controllers;
using OverTask.api.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔐 CORS - permite chamadas do Blazor
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5204") // Porta correta do Blazor
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// OpenAPI/Swagger
builder.Services.AddOpenApi();

// Banco de dados
builder.Services.AddDbContext<OverTaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Autorização e controladores
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthorization();
builder.Services.AddControllers();


var app = builder.Build();

// 🧪 Swagger só em dev
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 🚀 Middleware
app.UseCors(); // ⬅️ Use CORS aqui!

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.Run();