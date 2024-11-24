namespace Api.Data.Repositories
{
    public abstract class RepositoryBase
    {
        protected IDbContextAccessor _dbContexAccessor;

        public RepositoryBase(IDbContextAccessor dbContexAccessor)
        {
            _dbContexAccessor = dbContexAccessor;
        }

        protected DataBase DataComtext
        {
            get { return _dbContexAccessor.DataBase; }
        }
    }
}
