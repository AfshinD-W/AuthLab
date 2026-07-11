using System;
using System.Collections.Generic;
using System.Text;

namespace AuthLab.Application.DTO.Password
{
    public class ForgotPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
    }
}
