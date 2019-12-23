namespace Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Game
    {
        private string name;
        private decimal price;
        private string category;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game(){ }
        public Game(string name, decimal price, decimal discount, string image, string description, Category category, string producer, string profileImage, string backgroundImage) : this()
        {
            Name = name;
            Price = price;
            Image = image;
            Description = description;
            Discount = discount;
            CategoryId = category.CategoryId;
            Category = category.CategoryName;
            Producer = producer;
            ProfileImage = profileImage;
            BackgroundImage = backgroundImage;
        }
        public int GameId { get; set; }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name", "Name must be neither null nor empty");
                }
                name = value;
            }
        }

        public string Description { get; set; }

        public decimal Price
        {
            get => price;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Price", "Price must be neither null nor empty");
                }
                price = value;
            }
        }
        public string Image { get; set; }
        public string ProfileImage { get; set; }
        public string BackgroundImage { get; set; }
        public string Producer { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public decimal Discount { get; set; }
    }
}
