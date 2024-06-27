namespace JazzClub.Models.Helpers
{
	public static class EnumsHelper
	{

	public enum StatusEnum
	{
		Activo = 0,
		Inactivo = 1,
		PagoExpirado = 2
	}

	public enum DaysEnum
	{
		Monday = 1,
		Tuesday = 2,
		Wednesday = 3,
		Thursday = 4,
		Friday = 5,
		Saturday = 6,
		Sunday = 7
	}

	public static int GetIntDaysEnum(string day)
		{
			return (int)Enum.Parse(typeof(DaysEnum), day);
		} 
}
}
