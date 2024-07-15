using System.Linq.Expressions;

namespace JazzClub.Models.DataLayer
{
	public class QueryOptions<T>
	{
		//Sorting and filtering
		public Expression<Func<T, Object>> OrderBy { get; set; } = null!;
		public Expression<Func<T, Object>> ThenOrderBy { get; set; } = null!;
		public Expression<Func<T, bool>> Where { get; set; } = null!;
		// private string array for include statements
		private string[] includes = Array.Empty<string>();

		// public write-only property for include statements - converts string to array
		public string Includes
		{
			set => includes = value.Replace(" ", ", ").Split(',');
		}

		public string[] GetIncludes() => includes;

		public bool HasWhere => Where != null;
		public bool HasOrderBy => OrderBy != null;
		public bool HasThenOrderBy => ThenOrderBy != null;
	}
}
