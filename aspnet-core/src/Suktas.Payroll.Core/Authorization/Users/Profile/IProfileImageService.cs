using Abp;
using Abp.Domain.Services;
using System.Threading.Tasks;

namespace Suktas.Payroll.Authorization.Users.Profile
{
    public interface IProfileImageService : IDomainService
    {
        Task<string> GetProfilePictureContentForUser(UserIdentifier userIdentifier);
    }
}