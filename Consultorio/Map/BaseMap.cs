using Consultorio.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consultorio.Map
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : Base
    {
        private readonly string _tableName; 
        public BaseMap(string tablename)
        {
            _tableName = tablename;
        } public virtual void Configure(EntityTypeBuilder<T> builder)
        {
           builder.HasKey(x => x.Id);
           builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
        }
       
    }
}
