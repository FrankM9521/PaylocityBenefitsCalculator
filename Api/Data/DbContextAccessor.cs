using Api.Data.Repositories;

namespace Api.Data
{
    public interface IDbContextAccessor
    {
        public DataBase DataBase { get; }
    }
    public class DbContextAccessor : IDbContextAccessor
    {
        public DataBase DataBase
        {
            get
            {
                return DataBase.Instance;
            }
        }
    }
}
