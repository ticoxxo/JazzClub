namespace JazzClub.Models.Helpers
{
	public static class StatusHelper
	{
		public static string ChooseColor(int value)
		{
			var result = (value) switch
			{
				0 => "green",
				1 => "red",
				2 => "yellow",
				_ => throw new ArgumentException("No existe ese estatus")
			};
			return result;
		}

		public static string ShowStatus(int value)
		{
			var result = (value) switch
			{
				0 => "Activo",
				1 => "Inactivo",
				2 => "Cancelado",
				_ => throw new ArgumentException("No existe ese estatus")
			};
			return result;
		}
	}
}
