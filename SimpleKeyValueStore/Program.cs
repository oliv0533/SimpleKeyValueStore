using FastEndpoints;
using FastEndpoints.Swagger;
using SimpleKeyValueStore;
using SimpleKeyValueStore.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IKeyValueStore, KeyValueStore>();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument();

var app = builder.Build();

app
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();