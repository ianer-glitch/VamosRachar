using Grpc.Core;
using Notify.Repositories.NotificationRepository;
using Notify.Repositories.NotifyRepository;
using ProtoServer.ProtoFiles;

namespace Notify.UseCases;

public class NotificationUseCase: NotificationUseCaseProtoService.NotificationUseCaseProtoServiceBase 
{
    private readonly INotificationRepository  _notificationRepo;

    public NotificationUseCase(INotificationRepository  notificationRepo)
    {
        _notificationRepo = notificationRepo;
    }
    public override async Task<PNotification> CreateNotification(PNotification request, ServerCallContext context)
    {
        return await _notificationRepo.CreateNotification(request);
    }
}