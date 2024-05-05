using System;
using System.IO;
using System.Linq;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.Web.Controllers
{
    [Authorize]
    public class JobApplyController : PayrollControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxDocumentLength = 5242880; //5MB
        private const string MaxDocumentLengthUserFriendlyValue = "5MB"; //5MB
        private readonly string[] DocumentAllowedFileTypes = { "jpeg", "jpg", "png" };

        public JobApplyController(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
          
        }

        public FileUploadCacheOutput UploadDocumentFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count == 0)
                {
                    throw new UserFriendlyException(L("NoFileFoundError"));
                }

                var file = Request.Form.Files.First();
                if (file.Length > MaxDocumentLength)
                {
                    throw new UserFriendlyException(L("Warn_File_SizeLimit", MaxDocumentLengthUserFriendlyValue));
                }

                var fileType = Path.GetExtension(file.FileName).Substring(1);
                if (DocumentAllowedFileTypes != null && DocumentAllowedFileTypes.Length > 0 && !DocumentAllowedFileTypes.Contains(fileType))
                {
                    throw new UserFriendlyException(L("FileNotInAllowedFileTypes", DocumentAllowedFileTypes));
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

        public string[] GetDocumentFileAllowedTypes()
        {
            return DocumentAllowedFileTypes;
        }

    }
}