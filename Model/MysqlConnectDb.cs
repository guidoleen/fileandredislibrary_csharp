using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace FileAndRedisLibrary.Model
{
	class MysqlConnectDb
	{
		private string constring = ConfigurationManager.ConnectionStrings["MysqlDb"].ConnectionString;
		private MySqlConnection conn = new MySqlConnection();

		public MySqlConnection GetMysqlConnection()
		{
			this.conn.ConnectionString = constring;
			return this.conn;
		}
	}
}
