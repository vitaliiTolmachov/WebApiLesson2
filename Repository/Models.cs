using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Repository
{
    public class Product
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int? CategoryId { get; set; }
        //[JsonIgnore]
        public Category Category { get; set; }
    }

    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public int? DepartmentId { get; set; }
        //[JsonIgnore]
        public Department Department { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; }
    }

    public class Department
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public List<Category> Categories { get; set; }
    }
}
