﻿using System.Threading.Tasks;
using Abp.Application.Services;
using Suktas.Payroll.MultiTenancy.Payments.PayPal.Dto;

namespace Suktas.Payroll.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
