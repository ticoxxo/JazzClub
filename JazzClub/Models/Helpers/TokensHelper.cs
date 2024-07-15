namespace JazzClub.Models.Helpers
{
	public class TokensHelper
	{

		public TokensHelper() { }

		public CancellationTokenSource ManualCancellationHandler() {

			return new CancellationTokenSource();
		}


		public  EventWaitHandle ManualWaitHandler()
		{
			return new EventWaitHandle(false, EventResetMode.ManualReset);
		}
	}
}
