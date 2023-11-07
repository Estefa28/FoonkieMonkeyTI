using FM.Domain.Interfaces;
using FM.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FM.EntityFramework.Repositories
{
    /// <summary>
    /// Implementation of Repository Pattern for use of Entity Framework
    /// </summary>
    /// <typeparam name="Model">EntityBase</typeparam>
    public abstract class EFRepository<Model> : IRepository<Model> where Model : EntityBase
    {
        private readonly ApplicationContext Context;
        private readonly DbSet<Model> DbSet;

        public EFRepository(ApplicationContext context)
        {
            Context = context;
            DbSet = context.Set<Model>();
        }

        public virtual Model Add(Model model)
        {
            if (model == null)
                throw new Exception($" The method {nameof(Add)} needs a model");

            DbSet.Add(model);
            Complete();
            return model;
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public List<Model> GetAll(int page, int pageSize)
        {
            return DbSet.Skip(page * pageSize - pageSize).Take(pageSize).ToList();
        }

        public Model Search(int id)
        {
            if (id == 0)
                throw new Exception("The id is necessary");

            var matched = DbSet.FirstOrDefault(x => x.Id == id);
            if (matched == null)
            {
                throw new Exception("No records found");
            }

            return matched;
        }

        public Model Update(Model model)
        {
            DbSet.Update(model);
            Complete();

            return model;
        }
    }
}
