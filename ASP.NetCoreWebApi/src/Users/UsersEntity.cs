using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASP.NetCoreWebApi.src.Users
{
    [Table("Users")]
    public class UsersEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Prevents auto-increment
        [Required]
        public required string UserNumber { get; set; } 

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string UserLevel { get; set; } = string.Empty;

        [Required]
        public bool Status { get; set; } = true;

        // New property to manage session state
        public bool IsSessionActive { get; set; } = false;

        // New field to store the current active session token
        public string? CurrentToken { get; set; } = string.Empty;

        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

    }
}