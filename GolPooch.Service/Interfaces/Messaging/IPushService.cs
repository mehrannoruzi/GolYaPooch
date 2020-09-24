﻿using Elk.Core;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Interfaces
{
    public interface IPushService
    {
        Task<IResponse<bool>> Subscribe(PushEndpoint model);
        Task SendPush();
    }
}