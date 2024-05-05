using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Suktas.Payroll.MultiTenancy.Accounting.Dto;

namespace Suktas.Payroll.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
