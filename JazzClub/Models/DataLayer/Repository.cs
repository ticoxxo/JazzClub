using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using JazzClub.Models.Helpers;

namespace JazzClub.Models.DataLayer
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected JazzClubContext context { get; set; }
		private DbSet<T> dbSet { get; set; }

		public Repository(JazzClubContext ctx)
		{
			context = ctx;
			dbSet = context.Set<T>();
		}
		public void Delete(T entity) => dbSet.Remove(entity);

		public T? Get(int id) => dbSet.Find(id);

		public T? Get(QueryOptions<T> options)
		{
			IQueryable<T> query = dbSet;
			foreach (string include in options.GetIncludes())
			{
				query = query.Include(include);
			}
			if (options.HasWhere)
				query = query.Where(options.Where);
			return query.FirstOrDefault();
		}

		public async Task<T?> GetAsync(int id) => await dbSet.FindAsync(id);

		public async Task<T?> GetAsync(QueryOptions<T> options)
		{
			TokensHelper tkh = new TokensHelper();
			var a = tkh.ManualCancellationHandler();
			
			IQueryable<T> query = dbSet;
			foreach (string include in options.GetIncludes())
			{
				query = query.Include(include);
			}
			if (options.HasWhere)
				query = query.Where(options.Where);
			return await query.FirstAsync(a.Token);
		}

		public void Insert(T entity) => dbSet.Add(entity);

		public IEnumerable<T> List(QueryOptions<T> options)
		{
			IQueryable<T> query = dbSet;
			foreach (string include in options.GetIncludes())
			{
				query = query.Include(include);
			}
			if (options.HasWhere) query = query.Where(options.Where);

			if (options.HasOrderBy)
			{
				if (options.HasThenOrderBy)
				{
					query = query.OrderBy(options.OrderBy).ThenBy(options.ThenOrderBy);
				}
				else
				{
					query = query.OrderBy(options.OrderBy);
				}
			}

			return query.ToList();
		}

		public async Task<IEnumerable<T>> ListAsync(QueryOptions<T> options)
		{
            IQueryable<T> query = dbSet;

            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere) query = query.Where(options.Where);

            if (options.HasOrderBy)
            {
                if (options.HasThenOrderBy)
                {
                    query = query.OrderBy(options.OrderBy).ThenBy(options.ThenOrderBy);
                }
                else
                {
                    query = query.OrderBy(options.OrderBy);
                }
            }


            return await query.ToListAsync();
			
		}

		public void Save() => context.SaveChanges();

		public async Task SaveAsyncrono() => await context.SaveChangesAsync();

		public void Update(T entity) => dbSet.Update(entity);
	}
}
