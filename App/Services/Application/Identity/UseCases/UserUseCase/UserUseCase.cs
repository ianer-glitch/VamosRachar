using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using ProtoServer;

namespace Identity.UseCases.UserUseCase
{
    public class UserUseCase : payloadExampleSerivce.payloadExampleSerivceBase
    {
        public override async Task<PayloadMessage> GetPayloadMessage(PayloadMessage request, ServerCallContext context)
        {
            return await Task.FromResult(request);
        }
    }
}