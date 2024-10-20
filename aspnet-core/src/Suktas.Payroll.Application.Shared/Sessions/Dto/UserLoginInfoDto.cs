﻿using Abp.Application.Services.Dto;
using Suktas.Payroll.Authorization.Users;

namespace Suktas.Payroll.Sessions.Dto
{
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string ProfilePictureId { get; set; }

        public UserTypeEnum UserType { get; set; }
    }
}
