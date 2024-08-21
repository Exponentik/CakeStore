using CakeStore.Api;
using CakeStore.Api.Configuration;
using CakeStore.Services.Settings;
using CakeStore.Services.Logger;
using CakeStore.Settings;
using CakeStore.Context;
using CakeStore.Context.Seeder;
var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);


var services = builder.Services;

var b = builder.Services;

b.AddAppDbContext(builder.Configuration);

b.AddAppAutoMappers();

b.AddAppValidator();

b.AddAppHealthChecks();



b.AddAppVersioning();

b.AddAppCors ();

b.AddAppControllerAndViews();

b.AddAppSwagger(mainSettings, swaggerSettings,identitySettings);

b.AddAppAuth(identitySettings);

b.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseAppHealthChecks();

app.UseAppCors ();

app.UseAppControllerAndViews();

app.UseAppSwagger();

app.UseAppAuth();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services);

app.Run();



