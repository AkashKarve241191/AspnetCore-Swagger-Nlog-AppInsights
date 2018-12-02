using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDemo.Model
{
    public class UserDto
    {
        [JsonIgnore]
        public Guid Id
        {
            get;
            set;
        }
        [Required]
        public string FirstName
        {
            get;
            set;
        }
        [Required]
        public string LastName
        {
            get;
            set;
        }
        [Required]
        [JsonProperty(PropertyName = "mobilePhone")]
        public string Phone
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
    }

    // Book
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public Address Location { get; set; }
        public Press Press { get; set; }
    }

    // Press
    public class Press
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Category Category { get; set; }
    }

    // Category
    public enum Category
    {
        Book,
        Magazine,
        EBook
    }

    // Address
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}
