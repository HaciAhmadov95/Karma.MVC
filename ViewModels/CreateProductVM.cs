﻿using Karma.MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karma.MVC.ViewModels;

public class CreateProductVM
{
    [Required, MaxLength(100)]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required, Range(0, 100000000)]
    public double Price { get; set; }
    [Required, Range(0, 10000)]
    public int Width { get; set; }
    [Required, Range(0, 10000)]
    public int Height { get; set; }
    [Required, Range(0, 10000)]
    public int Depth { get; set; }
    [Required, Range(0, 10000)]
    public int Weight { get; set; }
    [Required]
    public bool QualityChecking { get; set; }

    [NotMapped, Required]
    public IFormFile MainImage { get; set; }
    [NotMapped, Required]
    public List<IFormFile> ImageFile { get; set; }

    public ICollection<Image> Images { get; set; }
    public List<Color> Colors { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public int BrandId { get; set; }
    public Brand Brand { get; set; }
}
