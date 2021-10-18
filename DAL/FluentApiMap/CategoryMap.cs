using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FluentApiMap
{
    public class CategoryMap : BaseMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.ToTable("Categories");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            // relations
            builder.HasOne(x => x.AppUser).WithMany(x => x.Categories).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
