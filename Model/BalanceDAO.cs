using System;
using MySql.Data.MySqlClient;

namespace FileAndRedisLibrary.Model
{
	class BalanceDAO : IBalanceDAO
	{
		private MySqlConnection conn = new MysqlConnectDb().GetMysqlConnection();
		private MySqlCommand comm;
		public decimal GetSumBalanceFromObject(int Id)
		{
			decimal result = 0;
			MySqlDataReader rdr;
			string balance_available = "balance_available";

			string sql = $"select sum({balance_available}) as balancesum FROM balance WHERE account_number IN" +
				" (SELECT account_number FROM acct_entrusted_funds_accounts WHERE cust_id = @objectID)";
			try
			{
				using (comm = this.conn.CreateCommand())
				{
					comm.CommandText = sql;
					comm.Parameters.AddWithValue("@objectID", Id);

					using (conn)
					{
						conn.Open();

						rdr = comm.ExecuteReader();
						while (rdr.Read())
							result += rdr.GetDecimal("balancesum");
						conn.Close();
					}
				}
			}
			catch(Exception ee)
			{
				Console.WriteLine(ee.ToString());
			}
			
			return result;
		}
	}
}
