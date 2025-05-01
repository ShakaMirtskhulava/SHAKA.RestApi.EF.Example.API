using System.ComponentModel.DataAnnotations;

namespace SHAKA.RestApi.EF.Example.API.Models;

public class MyUser
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? FirstName { get; set; }
    
    [StringLength(100)]
    public string? LastName { get; set; }
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
}
