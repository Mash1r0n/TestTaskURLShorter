﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Authorization.Registration
{
    public class RegistrationModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
