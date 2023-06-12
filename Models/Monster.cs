#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace Monsters.Models;
public class Monster
{
    [Key]
    public int MonsterId { get; set; }
    [Required]
    [MinLength(5)]
    public string Name { get; set; } 
    [Required]
    public string LocatedAt { get; set; }
    public int LastSeen {get;set;}
    [Required]
    [Range(10,50)]
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
                