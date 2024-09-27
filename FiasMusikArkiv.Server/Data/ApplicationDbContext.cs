using FiasMusikArkiv.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;


namespace FiasMusikArkiv.Server.Data
{
    public interface IDbContextSeeder
    {
        void EnsureSeedData();
    }
    public class FiasMusikArkivDbContext : DbContext, IDbContextSeeder
    {
        public FiasMusikArkivDbContext(DbContextOptions<FiasMusikArkivDbContext> options) : base(options) { }
        private const string DefaultCreatedBy = "(System)";
        private const string DefaultModifiedBy = "(System)";
        private const string DefaultDeletedBy = "(System)";

        public DbSet<Song> Songs { get; set;}
        public DbSet<CodeGenre> CodeGenre { get; set;}


        public void EnsureSeedData()
        {
            this.Database.Migrate(); // Run any pending migrations

            AddOrUpdateEnumValues(this.CodeGenre, typeof(Data.GenreCode), removeUnusedEnums: true);

            this.SaveChanges();
        }
        private void AddOrUpdateEnumValues<T>(IQueryable<T> entities, Type enumType, bool removeUnusedEnums = false) where T : CodeBaseModel
        {
            var enumValues = System.Enum.GetValues(enumType);

            //
            // Add or Update
            // 
            int sortOrder = 0;
            foreach (var value in enumValues)
            {
                sortOrder++;
                var valueString = value.ToString();
                var valueMember = enumType.GetMember(valueString)[0];
                var attribute = valueMember.GetCustomAttributes().SingleOrDefault(a => a.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;
                var code = valueString;
                var description = attribute != null ? attribute.Description : valueString;

                if (!entities.Any(r => r.Code == code))
                {
                    var newEntity = Activator.CreateInstance<T>() as T;
                    newEntity.Code = code;
                    newEntity.Description = description;
                    newEntity.SortOrder = sortOrder;
                    this.Add<T>(newEntity);
                }
                else
                {
                    var existingEntity = entities.Single(r => r.Code == code);
                    existingEntity.Description = description;
                    existingEntity.SortOrder = sortOrder;
                }
            }

            //
            // Remove no longer used enum values
            //
            if (removeUnusedEnums)
            {
                var allExistingCodes = entities.Select(e => e.Code).ToList();
                var allEnumValues = enumValues.Cast<object>().Select(x => x.ToString()).ToList();
                var codesToRemove = allExistingCodes.Except(allEnumValues).ToList();
                foreach (var code in codesToRemove)
                {
                    var entityToRemove = entities.Single(r => r.Code == code);
                    this.Remove<T>(entityToRemove);
                }
            }
        }
    }

    
}
