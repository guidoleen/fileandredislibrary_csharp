using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BuckarooDevLibrary.Utils;

namespace FileAndRedisLibrary.Model
{
	public class ReadFileFromObject<T> : FileHandler<T>
	{
		private T ReadFileToObject()
		{
			T resultObject = default(T); // = (T) new object();
			Stream stream;

			using (stream = new FileStream(base.fileName, FileMode.Open, FileAccess.Read))
			{
				// Actual Conversion
				BinaryFormatter bf = new BinaryFormatter();
				try
				{
					resultObject = (T)bf.Deserialize(stream);
				}
				catch(Exception ee)
				{
					Console.WriteLine(ee.ToString());
				}
			}

			return resultObject;
		}

		private T[] ReadFilesToObjects()
		{
			T[] resultObjects = new T[0]; // default(T[]);
			Stream stream;

			using (stream = new FileStream(base.fileName, FileMode.Open, FileAccess.Read))
			{
				resultObjects = new BasicUtils<T>().DeserializerToArrayObjects(stream); 
			}

			return resultObjects;
		}
		
		public override string HandleFile()
		{
			return new BasicUtils<T>().FromArrayObjectsToString( this.ReadFilesToObjects() );
		}
	}
}
