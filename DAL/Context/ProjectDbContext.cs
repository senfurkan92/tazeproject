using DAL.FluentApiMap;
using DAL.Helper;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    /// <summary>
    /// entity framework yapısı identity kullanilarak cookie bazlı authentication icin kullanilmistir
    /// ayrica database codefirst yaklasimi ile entity framework kullanılarak konfigure edilmektedir
    /// </summary>
    public class ProjectDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CstrHelper.GetConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new ArticleMap());
        }

        public DbSet<Category> Categories;
        public DbSet<Article> Articles;
    }
}
