using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using webapi.Repository.Entity;

namespace webapi.Repository
{
    public class webapiDbContext : DbContext
    {
        public webapiDbContext(DbContextOptions<webapiDbContext> options) : base(options) { }

        public DbSet<Good> Goods { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Goodtags> GoodTags { get; set; }
    }
}
