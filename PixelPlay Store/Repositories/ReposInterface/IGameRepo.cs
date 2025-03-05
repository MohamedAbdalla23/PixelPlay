public interface IGameRepo
{
    public List<Games> GetAll();

    public Games GetById(int id);

    public Task Create(GameFormViewModel model);

    public void Delete(int id);

    public void Update(Games games);

    public Task Save();
}

