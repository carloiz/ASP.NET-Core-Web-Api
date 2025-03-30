using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP.NetCoreWebApi.src.Users
{
    public class UsersDto
    {
        public required string UserNumber { get; set; }
        public string Username { get; set; } = string.Empty;
        public string UserLevel { get; set; } = string.Empty;
        public bool Status { get; set; }
        public bool IsSessionActive { get; set; }
        public DateTime RecordDate { get; set; }
    }

    public class CreateUserDto
    {

        [Required(ErrorMessage = "Username is required.")]
        [DefaultValue("")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [DefaultValue("")]
        //[MinLength(8, ErrorMessage = "Password is too short.")]
        [MaxLength(64, ErrorMessage = "Password is too long.")]
        [RegularExpression(@"^\S+$", ErrorMessage = "The password cannot contain spaces.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserLevel is required.")]
        [DefaultValue("")]
        public string UserLevel { get; set; } = string.Empty;

    }


    public class UpdateUserDto
    {
        //[Required(ErrorMessage = "Username is required.")]
        [DefaultValue("")]
        public string? Username { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Role is required.")]
        [DefaultValue("")]
        public string? UserLevel { get; set; } = string.Empty;
    }


    public class LoginInputModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [DefaultValue("")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [DefaultValue("")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "User Level is required.")]
        [DefaultValue("")]
        public string UserLevel { get; set; } = string.Empty;
    }

    public class LogoutRequest
    {
        [DefaultValue("admin")]
        public string Username { get; set; }
        [DefaultValue("")]
        public string Token { get; set; }
    }

    public class UserRegistInfo
    {
        public string AuthToken { get; set; }
        public string UserNumber { get; set; }
        public string UserLevel { get; set; }
    }


    public class ForgotPassword
    {
        [Required(ErrorMessage = "Username is required.")]
        [DefaultValue("")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Old Password is required.")]
        [DataType(DataType.Password)]
        [DefaultValue("")]
        public required string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required.")]
        [DataType(DataType.Password)]
        [DefaultValue("")]
        [MinLength(8, ErrorMessage = "Password is too short.")]
        [MaxLength(64, ErrorMessage = "Password is too long.")]
        [RegularExpression(@"^\S+$", ErrorMessage = "The password cannot contain spaces.")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Repeat Password is required.")]
        [DataType(DataType.Password)]
        [DefaultValue("")]
        [MinLength(8, ErrorMessage = "Password is too short.")]
        [MaxLength(64, ErrorMessage = "Password is too long.")]
        [RegularExpression(@"^\S+$", ErrorMessage = "The password cannot contain spaces.")]
        public required string RepeatPassword { get; set; }
    }

}
