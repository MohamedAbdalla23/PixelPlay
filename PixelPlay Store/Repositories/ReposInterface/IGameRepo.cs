public interface IGameRepo
{
    public IQueryable<Games> GetAll();

    public Games GetById(int id);

    public Task Create(CreateGameFormViewModel model);

    public void Delete(int id);

    public Task<Games?> Update(EditGameFormViewModel model);

    public Task Save();
}

