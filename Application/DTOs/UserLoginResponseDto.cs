﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserLoginResponseDto
    {
        public string Token { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool IsUserAdmin { get; set; }
    }
}
