using CakeStore.Api;
using CakeStore.Api.Configuration;
using CakeStore.Services.Settings;
using CakeStore.Services.Logger;
using CakeStore.Settings;
var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);


var services = builder.Services;

var b = builder.Services;


b.AddAppAutoMappers();
b.AddAppValidator();

b.AddAppHealthChecks();


b.RegisterServices();
b.AddAppVersioning();
b.AddAppCors ();
b.AddAppControllerAndViews();
b.AddAppSwagger(mainSettings, swaggerSettings);

var app = builder.Build();
app.UseAppHealthChecks();

app.UseAppCors ();
app.UseAppControllerAndViews();
app.UseAppSwagger();

app.Run();



