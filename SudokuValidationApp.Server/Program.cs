using SudokuValidationInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
RegisterModules.Register(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Inside Program.cs or Startup.cs

// Configured CORS in backend to allow requests from frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CustomCorsPolicy",
        policy => policy.WithOrigins("https://localhost:55561").AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

//app.UseDefaultFiles();
//app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("CustomCorsPolicy");
app.UseAuthorization();

app.MapControllers();

//app.MapFallbackToFile("/index.html");



app.Run();
