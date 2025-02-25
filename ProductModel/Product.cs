using ProductModel;
using System.ComponentModel.DataAnnotations;
using System;

public class Product
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
    public string CategoryCode { get; set; }

    public Category Category { get; set; }

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
