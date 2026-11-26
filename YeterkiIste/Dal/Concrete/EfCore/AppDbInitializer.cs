using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dal.Concrete.EfCore
{
    public static class AppDbInitializer
    {
        public static void Seed(AppDbContext dbContext)
        {
            if (!dbContext.Category.Any())
            {
                dbContext.AddRange(
                    new  Category
                    {
                        Name = "Choco Chips",
                        IsActive=true,
                        AddedTime= DateTime.Now, 
                        ImageUrl = "\\Images\\Chocochip.png"
                    },
                    new Category
                    {
                        Name = "2 Chips",
                        IsActive = true,
                        AddedTime = DateTime.Now,
                        ImageUrl = "\\Images\\Chocochip.png"
                    },
                    new Category
                    {
                        Name = "3 Chips",
                        IsActive = true,
                        AddedTime = DateTime.Now,
                        ImageUrl = "\\Images\\Chocochip.png"
                    },
                    new Category
                    {
                        Name = "4 Chips",
                        IsActive = true,
                        AddedTime = DateTime.Now,
                        ImageUrl = "\\Images\\Chocochip.png"
                    }
                    );
            }
            dbContext.SaveChanges();
        }
    }
}
