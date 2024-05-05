using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Suktas.Payroll.Dto;

namespace Suktas.Payroll.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
