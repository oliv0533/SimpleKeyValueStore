using FastEndpoints;
using FastEndpoints.Swagger;
using SimpleKeyValueStore;
using SimpleKeyValueStore.Interfaces;
using SimpleKeyValueStore.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IKeyValueStore, KeyValueStore>();

builder.Services.AddMediatR(cfg =>
{
    cfg.AddOpenBehavior(typeof(ExclusiveCommandBehavior<,>));
    cfg.RegisterServicesFromAssemblyContaining<ISimpleKeyValueStoreMarker>();
});

builder.Services.AddMemoryCache();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument();

var app = builder.Build();

app
    .UseFastEndpoints()
    .UseSwaggerGen();

app.Run();