namespace JazzClub.Models.Helpers
{
	public static class EditHelper
	{
		public static bool IsModelDifferent(Object entity, Object entityTwo)
		{

			var result = entity.Equals(entityTwo);
			return true;
		}
	}
}
