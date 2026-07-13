using System.ComponentModel.DataAnnotations.Schema;

namespace web_hello_world.Dto
{
    [Table("users")]
    public class Users
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
