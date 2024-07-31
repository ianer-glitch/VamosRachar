using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Notify.Repositories.NotificationRepository;
using Notify.Repositories.NotifyRepository;
using Notify.UseCases;

var builder = WebApplication.CreateSlimBuilder(args);





builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton(typeof(INotifyRepository<>),typeof(NotifyRepository<>));
// builder.Services.AddSingleton<NotificationUseCase>();

//use the host builder to create background services
HostApplicationBuilder hBuilder = Host.CreateApplicationBuilder(args);
hBuilder.Services.AddHostedService<NotificationUseCase>();
IHost host = hBuilder.Build();
host.Run();

// var not = builder.Services.BuildServiceProvider().GetRequiredService<NotificationUseCase>();
// await not.CreateNotificationRabitMq();


var app = builder.Build();



app.Run();



