using FM.Domain.Models;

namespace FM.Domain.Interfaces
{
    public interface IRepository<Model> where Model : EntityBase
    {
        List<Model> GetAll(int page, int pageSize);
        Model Search(int id);
        Model Update(Model model);
        Model Add(Model model);
        int Complete();
    }
}
