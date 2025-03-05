public interface IGameRepo
{
    public List<Games> GetAll();

    public Games GetById(int id);

    public void Create(GameFormViewModel game);

    public void Delete(int id);

    public void Update(Games games);

    public void Save();
}

