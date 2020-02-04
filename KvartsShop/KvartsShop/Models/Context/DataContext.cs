using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KvartsShop.Models.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubCatalog> SubCatalogs { get; set; }
        public DbSet<SubSubCatalog> SubSubCatalogs { get; set; }
        public DbSet<ProductSlider> ProductSliders { get; set; }
    }

    public class CatalogInitializer : DropCreateDatabaseAlways<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            context.Catalogs.Add(new Catalog { Name = "Watch", ImagePath = "/Content/Images/ImageCategory/watch.png" });
            context.Catalogs.Add(new Catalog { Name = "Clock", ImagePath = "/Content/Images/ImageCategory/clock.png" });
            context.Catalogs.Add(new Catalog { Name = "Radio", ImagePath = "/Content/Images/ImageCategory/radio.png" });
            context.Catalogs.Add(new Catalog { Name = "Accessories", ImagePath = "/Content/Images/ImageCategory/accessories.png" });
            context.Catalogs.Add(new Catalog { Name = "Battery", ImagePath = "/Content/Images/ImageCategory/battery.png" });

            string[] SubCatalogsNameForWatch = { "чоловічі", "жіночі", "дитячі" };
            string startPath = "/Content/Images/ImageSubCategory/";
            string[] SubCatalogsImageForWatch = { "male.png", "female.png", "children.png" };

            for (int i = 0; i < SubCatalogsNameForWatch.Length; i++)
            {
                context.SubCatalogs.Add(new SubCatalog { Name = SubCatalogsNameForWatch[i], CatalogId = 1, ImagePath = startPath + SubCatalogsImageForWatch[i] });
            }

            string[] SubSubCatalogsNameForWatch = { "електроніка", "кварц", "механіка" };

            for (int i = 0; i < SubSubCatalogsNameForWatch.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (SubSubCatalogsNameForWatch[j] == "механіка" && i == 2)
                    {
                        continue;
                    }
                    context.SubSubCatalogs.Add(new SubSubCatalog { Name = SubSubCatalogsNameForWatch[j], SubCatalogId = i + 1, CatalogId = 1 });
                }
            }

            for (int i = 0; i < 200; i++)
            {
                context.Products.Add(
                new Product
                {
                    Name = "Product",
                    Brand = "Q&Q",
                    Description = "Description",
                    Price = i,
                    ImagePath = "/Content/Images/Layout/basket.png",
                    CatalogId = i / 40 + 1,
                    SubCatalogId = i / 20 + 1,
                    SubSubCatalogId = i / 20 + 1
                }
                );
            }
            context.Products.Add(
                new Product
                {
                    Name = "G-SHORT",
                    Brand = "Perfect",
                    Description = "Description",
                    Price = 23,
                    ImagePath = "/Content/Images/Layout/basket.png",
                    CatalogId = 1,
                    SubCatalogId = 1,
                    SubSubCatalogId = 1
                }
                );
            for (int i = 0; i < 10; i++)
            {
                context.ProductSliders.Add(
                new ProductSlider
                {
                    Name = "Product",
                    Description = "Description",
                    Brand = "Q&Q",
                    Price = i,
                    ImagePath = "/Content/Images/Layout/basket.png",
                    CatalogId = 1
                }
                );
            }
            context.ProductSliders.AddRange(context.ProductSliders);

            context.Users.Add(new User { FirstName = "Olecksandr", LastName = "Morozko", Email = "admin@gmail.com", Phone = "40432432", Status = Status.admin, Password = "40432432" });
            context.Users.Add(new User { FirstName = "Roman", LastName = "Morozko", Email = "rom@gmail.com", Phone = "00000000", Status = Status.user, Password = "333444" });
            base.Seed(context);
        }
    }
}