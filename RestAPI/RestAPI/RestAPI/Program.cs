var builder = WebApplication.CreateBuilder(args);

ILogger logger = SetupLogger();
logger.LogInformation("# WAVEFORM API - REST - Start #");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();
logger.LogInformation($"Environment -- {app.Environment}");
app.MapHealthChecks("/health");

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

ILogger SetupLogger()
{
    using var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
        .AddConsole();
    });

    ILogger logger = loggerFactory.CreateLogger<Program>();
    return logger;
}