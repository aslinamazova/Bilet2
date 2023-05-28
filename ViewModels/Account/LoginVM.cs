using System;
using System.ComponentModel.DataAnnotations;

namespace Bizland.ViewModels.Account
{
    public class LoginVM
    {
        [Required, MaxLength(100)]
        public string UserName { get; set; }

        [Required, MaxLength(100), DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}

