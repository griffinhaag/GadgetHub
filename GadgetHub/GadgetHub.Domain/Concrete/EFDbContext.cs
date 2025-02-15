using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products {  get; set; }
    }
}
