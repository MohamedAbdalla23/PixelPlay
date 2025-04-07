public interface IGameRepo
{
    public IQueryable<Games> GetAll();

    public Games GetById(int id);

    public Task Create(CreateGameFormViewModel model);

    public bool Delete(int id);

    public Task<Games?> UpdateGameAsync(EditGameFormViewModel model);

    public Task Save();
}

