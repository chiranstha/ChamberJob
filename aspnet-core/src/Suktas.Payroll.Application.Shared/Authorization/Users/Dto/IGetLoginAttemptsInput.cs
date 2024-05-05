﻿using Abp.Application.Services.Dto;

namespace Suktas.Payroll.Authorization.Users.Dto
{
    public interface IGetLoginAttemptsInput: ISortedResultRequest
    {
        string Filter { get; set; }
    }
}