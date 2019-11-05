
using MySql.Data.MySqlClient;

namespace FileAndRedisLibrary.Model
{
	public class TestObjectDAO
	{
		private MySqlConnection conn = new MysqlConnectDb().GetMysqlConnection();
		private MySqlCommand comm;
		private string TestObject_Id = "cust_id";
		private string TestObject_Name = "cust_name";

		public TestObject GetMerchantFromId(int Id)
		{
			TestObject testObject = new TestObject();
			string sql = "select * from customers where cust_id = @custId limit 1";
			MySqlDataReader rdr;

			using (comm = this.conn.CreateCommand())
			{
				comm.CommandText = sql;
				comm.Parameters.AddWithValue("@custId", Id);

				using(conn)
				{
					conn.Open();

					rdr = comm.ExecuteReader();
					while(rdr.Read())
					{
						testObject.Id = rdr.GetInt32(this.TestObject_Id);
						testObject.testObjectName = rdr.GetString(this.TestObject_Name);
					}
					rdr.Close();
					conn.Close();
				}
			}

			return testObject;
		}
	}
}
