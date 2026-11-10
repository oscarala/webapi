using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Services registrieren
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<ITodoService, InMemoryTodoService>();

// CORS: erlaubt Aufrufe von einem Frontend (z.B. Angular/React) während der Entwicklung
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// HTTP-Pipeline konfigurieren
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();                       // /openapi/v1.json
    app.UseSwaggerUI(options =>             // /swagger
        options.SwaggerEndpoint("/openapi/v1.json", "WebApi v1"));
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
