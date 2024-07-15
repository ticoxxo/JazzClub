namespace JazzClub.Models.DataLayer
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> List(QueryOptions<T> options);

		Task<IEnumerable<T>> ListAsync(QueryOptions<T> options);

		//Get type by id
		T? Get(int id);
		Task<T?> GetAsync(int id);

		//Get type with LINQ query
		T? Get(QueryOptions<T> options);
		Task<T?> GetAsync(QueryOptions<T> options);


		//Create
		void Insert(T entity);
		//update
		void Update(T entity);
		//Delete
		void Delete(T entity);
		//Save
		void Save();

		Task SaveAsyncrono();


	}
}
