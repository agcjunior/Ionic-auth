using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authApi.Models
{
  [Table("Users", Schema = "sec")]
  public class AppUser
  {
    [Required]
    [Key]
    public Guid UserId { get; set; }
    [Required]
    [StringLength(100)]
    public string UserName { get; set; }
    [Required]
    [StringLength(100)]
    public string Password { get; set; }
  }
}
