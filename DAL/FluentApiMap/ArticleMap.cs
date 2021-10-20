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
    /// <summary>
    /// EntityFramework codefirst ile Articles tablosu konfigurasyonu icin FluentApi yapisi
    /// </summary>
    public class ArticleMap : BaseMap<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            base.Configure(builder);
            builder.ToTable("Articles");
            builder.Property(x => x.Caption).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Content).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.ImgPath).IsRequired().HasMaxLength(1000);

            // relations
            builder.HasOne(x => x.AppUser).WithMany(x => x.Articles).HasForeignKey(x => x.AppUserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Category).WithMany(x => x.Articles).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
