﻿using Elk.Core;
using GolPooch.Domain.Dto;
using GolPooch.Domain.Entity;
using System.Threading.Tasks;

namespace GolPooch.Service.Interfaces
{
    public interface IPurchaseService
    {
        Task<IResponse<PagingListDetails<PurchaseDto>>> GetTopPurchases(int userId, PagingParameter pagingParameter);
        Task<IResponse<PagingListDetails<PurchaseDto>>> GetAllPurchases(int userId, PagingParameter pagingParameter);
    }
}