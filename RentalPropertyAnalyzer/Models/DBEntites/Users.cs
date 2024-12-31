using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace RentalPropertyAnalyzer.Models.DBEntites
{
    public class Users
    {
        public Users()
        {
            Username = string.Empty;
            Firstname = string.Empty;
            Lastname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            PasswordHash = [];
            PasswordSalt = [];

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
