using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public static class DBInitializerSeedData
    {
        public static void InitializeDatabase(HotelDbContext blogDbContext)
        {

            if (!blogDbContext.Hotels.Any())
            {
                var blogs = new Hotel[]
               {


            new Hotel { Name = "Hotel 1", Address = "123 Main St" },
             new Hotel { Name = "Hotel 2", Address = "456 Elm St" },

           };
                blogDbContext.Hotels.AddRangeAsync(blogs);
                blogDbContext.SaveChanges();
            }
        }
    }
}
