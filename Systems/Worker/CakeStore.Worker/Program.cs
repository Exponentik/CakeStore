using CakeStore.Services.Logger;
using CakeStore.Services.Settings;
using CakeStore.Settings;
using CakeStore.Worker;
using CakeStore.Worker.Configuration;

var logSettings = Settings.Load<LogSettings>("Log");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppLogger(logSettings);

// Configure services
var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppHealthChecks();

services.RegisterAppServices();


// Configure the HTTP request pipeline.

var app = builder.Build();

var logger = app.Services.GetRequiredService<IAppLogger>();


app.UseAppHealthChecks();


logger.Information("Worker has started");


// Start task executor

logger.Information("Try to connect to RabbitMq");

app.Services.GetRequiredService<ITaskExecutor>().Start();

logger.Information("RabbitMq connected");


// Run app

logger.Information("Worker started");

app.Run();

logger.Information("Worker has stopped");
