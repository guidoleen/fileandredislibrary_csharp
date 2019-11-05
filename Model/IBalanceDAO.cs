
namespace FileAndRedisLibrary.Model
{
	interface IBalanceDAO
	{
		decimal GetSumBalanceFromObject(int Id);
	}
}
