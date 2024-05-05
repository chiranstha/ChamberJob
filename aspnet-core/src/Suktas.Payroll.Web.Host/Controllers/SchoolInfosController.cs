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
    public class SchoolInfosController : PayrollControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxImageLength = 5242880; //5MB
        private const string MaxImageLengthUserFriendlyValue = "5MB"; //5MB
        private readonly object[] _imageAllowedFileTypes = { "jpeg", "jpg", "png" };

        public SchoolInfosController(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
           
        }

        public FileUploadCacheOutput UploadImageFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count == 0)
                {
                    throw new UserFriendlyException(L("NoFileFoundError"));
                }

                var file = Request.Form.Files.First();
                if (file.Length > MaxImageLength)
                {
                    throw new UserFriendlyException(L("Warn_File_SizeLimit", MaxImageLengthUserFriendlyValue));
                }

                var fileType = Path.GetExtension(file.FileName)[1..];
                if (_imageAllowedFileTypes is { Length: > 0 } && !_imageAllowedFileTypes.Contains(fileType))
                {
                    throw new UserFriendlyException(L("FileNotInAllowedFileTypes", _imageAllowedFileTypes));
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

       

    }
}