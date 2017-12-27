using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Category Category { get; set; }
    }

    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Department Department { get; set; }
    }

    public class Department
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
