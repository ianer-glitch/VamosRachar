using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Notify.Repositories.NotificationRepository;
using Notify.Repositories.NotifyRepository;
using Notify.UseCases;

var builder = WebApplication.CreateSlimBuilder(args);





builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton(typeof(INotifyRepository<>),typeof(NotifyRepository<>));

//use the host builder to create background services
builder.Services.AddHostedService<NotificationServiceBus>();



var app = builder.Build();



app.Run();



