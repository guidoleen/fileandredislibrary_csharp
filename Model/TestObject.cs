using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileAndRedisLibrary.Model
{
	[Serializable]
	public class TestObject
	{
		public int Id { get; set; }
		public string testObjectName { get; set; }

		override
		public string ToString()
		{
			return $"TestObject Id: {Id} TestObject Name: {testObjectName}";
		}
	}
}
