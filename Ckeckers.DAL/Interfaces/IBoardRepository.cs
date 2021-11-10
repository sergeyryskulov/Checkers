namespace Ckeckers.DAL.Repositories
{
    public interface IBoardRepository
    {
        string Load(string key);

        void Save(string key, string state);
    }
}