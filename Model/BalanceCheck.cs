
namespace FileAndRedisLibrary.Model
{
	public class BalanceCheck
	{
		private decimal availableBalance;
		private int TestObjectID;

		public BalanceCheck() { }

		public void GetAvailableBalance(int TestObjectId)
		{
			this.TestObjectID = TestObjectId;
			this.availableBalance = new BalanceDAO().GetSumBalanceFromObject(TestObjectId);
		}

		override
		public string ToString()
		{
			return $"Available balance: {availableBalance.ToString()} from testObject {TestObjectID} ";
		}

	}
}
