using System;
using System.IO;
using System.Linq;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using NPOI.SS.Formula.Functions;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Web.Controllers
{
    [Authorize]
    public class CompanyController : PayrollControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxLogoLength = 5242880; //5MB
        private const string MaxLogoLengthUserFriendlyValue = "5MB"; //5MB
        private readonly string[] LogoAllowedFileTypes = { "jpeg", "jpg", "png" };

        public CompanyController(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
        }

        public FileUploadCacheOutput UploadLogoFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count == 0)
                {
                    throw new UserFriendlyException(L("NoFileFoundError"));
                }

                var file = Request.Form.Files.First();
                if (file.Length > MaxLogoLength)
                {
                    throw new UserFriendlyException(L("Warn_File_SizeLimit", MaxLogoLengthUserFriendlyValue));
                }

                var fileType = Path.GetExtension(file.FileName).Substring(1);
                if (LogoAllowedFileTypes != null && LogoAllowedFileTypes.Length > 0 && !LogoAllowedFileTypes.Contains(fileType))
                {
                    throw new UserFriendlyException(L("FileNotInAllowedFileTypes", LogoAllowedFileTypes));
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var fileToken = Guid.NewGuid().ToString("N");
                _tempFileCacheManager.SetFile(fileToken, new TempFileInfo(file.FileName, fileType, fileBytes));

                return new FileUploadCacheOutput(fileToken);
            }
            catch (UserFriendlyException ex)
            {
                return new FileUploadCacheOutput(new ErrorInfo(ex.Message));
            }
        }

        public string[] GetLogoFileAllowedTypes()
        {
            return LogoAllowedFileTypes;
        }

    }
}