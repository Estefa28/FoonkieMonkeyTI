using FM.Domain.Interfaces;
using FM.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FM.EntityFramework.Repositories
{
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
                throw new Exception($" El método {nameof(Add)} necesita un modelo");

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
                throw new Exception(" El id es necesario");

            var matched = DbSet.FirstOrDefault(x => x.Id == id);
            if (matched == null)
            {
                throw new Exception("No se encontraron registros");
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
