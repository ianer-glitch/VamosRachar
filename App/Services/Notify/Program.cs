using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Notify.Repositories.NotificationRepository;
using Notify.Repositories.NotifyRepository;
using Notify.UseCases;

var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddGrpc();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

//configure serice port to listen
builder.WebHost.ConfigureKestrel(op =>
{
    op.ListenAnyIP(5002, li =>
    {
        li.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped(typeof(INotifyRepository<>),typeof(NotifyRepository<>));

var app = builder.Build();

app.MapGrpcService<NotificationUseCase>();

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}