using CORE.Prevail.Model;
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
    /// EntityFramework codefirst ile db konfigurasyonu icin FluentApi temel yapisi
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T>
        where T : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();
            builder.Property(x => x.DeleteDate).IsRequired(false);
        }
    }
}
