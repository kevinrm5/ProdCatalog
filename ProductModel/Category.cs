using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

public class Category
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MinLength(2)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Code { get; set; }

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public ICollection<Product> Products { get; set; }
}
