namespace JazzClub.Models.DataLayer
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> List(QueryOptions<T> options);

        //Get type by id
        T? Get(int id);

        //Get type with LINQ query
        T? Get(QueryOptions<T> options);

        //Create
        void Insert(T entity);
        //update
        void Update(T entity);
        //Delete
        void Delete(T entity);
        //Save
        void Save();

        void SaveAsyncrono();


	}
}
