using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FM.Domain.Models;
using FM.EntityFramework.Constants;

namespace FM.EntityFramework.Configurations
{
    /// <summary>
    /// User Entity Configuration
    /// </summary>
    public class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> entity)
        {
            entity.ToTable("Users", ConfigConstants.Schema);

            entity.HasIndex(x => x.Id);
            
        }
    }
}
