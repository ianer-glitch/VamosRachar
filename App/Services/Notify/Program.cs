using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Notify.Repositories.NotificationRepository;
using Notify.Repositories.NotifyRepository;
using Notify.UseCases;

var builder = WebApplication.CreateSlimBuilder(args);





builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped(typeof(INotifyRepository<>),typeof(NotifyRepository<>));
builder.Services.AddSingleton<NotificationUseCase>();


var not = builder.Services.BuildServiceProvider().GetRequiredService<NotificationUseCase>();
await not.CreateNotificationRabitMq();


var app = builder.Build();



app.Run();



