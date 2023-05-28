using System;
using System.ComponentModel.DataAnnotations;

namespace Bizland.ViewModels.Account;

public class RegisterVM
{
    [Required,MaxLength(100)]
	public string UserName { get; set; }

    [Required, MaxLength(100),DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required, MaxLength(100), DataType(DataType.Password)]
    public string Password { get; set; }

    [Required, MaxLength(100), DataType(DataType.Password),Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

}

