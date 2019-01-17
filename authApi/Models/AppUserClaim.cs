using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authApi.Models
{
  [Table("UserClaims", Schema = "sec")]
  public class AppUserClaim
  {
    [Key]
    [Required]
    public Guid ClaimId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string ClaimType { get; set; }
    [Required]
    public string ClaimValue { get; set; }
  }
}