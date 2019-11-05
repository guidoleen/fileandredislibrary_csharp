using BuckarooDevLibrary.Utils;
using StackExchange.Redis;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FileAndRedisLibrary.Model
{
	// Redis Settings after download and installation >> localhost:6379,ssl=false,connectTimeout=5000,syncTimeout=5000,defaultDatabase=1,abortConnect=false,keepAlive=180
	// TODO >> Set multiple objects with a single Redis key
	public class RedisHandler<T>
	{
		/// <summary>
		/// Redis Connector. After installing Redis this handler will handle the key value 
		/// storage (in-memory)
		/// </summary>
		private string strRedisSettings { get; }
		private int iDbNumber { get; }
		private BinaryFormatter bf = new BinaryFormatter();

		public RedisHandler(string strRedisSettings, int iDbNumber)
		{
			this.strRedisSettings = strRedisSettings;
			this.iDbNumber = iDbNumber;
		}

		// Redis Connection
		private ConnectionMultiplexer RedisConnector()
		{
			string configRedis = this.strRedisSettings;

			ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(configRedis);

			return conn;
		}

		// Redis Db
		private IDatabase RedisDb()
		{
			return this.RedisConnector().GetDatabase(this.iDbNumber);
		}

		// Redis Checking
		private bool IsValidRedisConnection()
		{
			if (this.RedisConnector().IsConnected) return true;
			else return false;
		}

		// Use Redis Get DataObject from key
		public T[] GetDataObjectsFromRedis(string key)
		{
			if (!IsValidRedisConnection())
				return null;

			var conn = this.RedisConnector();
			var Db = conn.GetDatabase(this.iDbNumber);
			T[] resultObjects = new T[0];

			string result = Db.StringGet(key);
			using (MemoryStream stream = new MemoryStream(this.ConvertStringToByte(result)))
			{
				resultObjects = new BasicUtils<T>().DeserializerToArrayObjects(stream);
			}

			return resultObjects;
		}

		// Read data from redis and return to string value
		public string GetDataObjectsFromRedisToString(string key)
		{
			return new BasicUtils<T>().FromArrayObjectsToString(this.GetDataObjectsFromRedis(key));
		}

		// Convert a String to byte
		private byte[] ConvertStringToByte(string input)
		{
			return Encoding.ASCII.GetBytes(input);
		}

		// Set object data from a key to Redis in-memory
		public void SetDataToRedis(string key, object obj)
		{
			var Db = this.RedisDb();
			byte[] buffer = null;

			try
			{
				using (MemoryStream stream = new MemoryStream())
				{
					this.bf.Serialize(stream, obj);
					buffer = stream.ToArray();
				}
			}
			catch (Exception ee)
			{
				Console.WriteLine(ee.ToString());
			}

			if (buffer != null)
				Db.StringSet(key, buffer);
		}
	}
}
