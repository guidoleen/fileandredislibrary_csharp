using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BuckarooDevLibrary.Utils
{
	public class BasicUtils<T>
	{
		/// <summary>
		/// Adding a new object to an Array. Looks similair to adding an object to a list (List<T>)
		/// </summary>
		/// <param name="objsOld"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public T[] AddNewObjectFromArray(T[] objsOld, T obj)
		{
			T[] objsFlag = objsOld;
			T[] objsNew = new T[objsOld.Length + 1];

			for (int i = 0; i < objsNew.Length; i++)
			{
				if (i == objsOld.Length)
					objsNew[i] = obj;
				else
					objsNew[i] = objsFlag[i];
			}
			return objsNew;
		}

		/// <summary>
		/// Deserialize objects from a given stream (file or memorystream) and place the found objects in an array of T[]'s
		/// used with the binary formatter.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public T[] DeserializerToArrayObjects(Stream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			bool stopStream = false;
			T singleObject = default(T);
			T[] resultObjects = new T[0];

			try
			{
				while (stopStream == false)
				{
					singleObject = (T)bf.Deserialize(stream);
					if (singleObject.GetType().Name.Equals(singleObject.GetType().Name))
					{
						resultObjects = this.AddNewObjectFromArray(resultObjects, singleObject);
					}
				}
			}
			catch (SerializationException eee)
			{
				stopStream = true;
				stream.Close();
			}
			catch (Exception ee)
			{
				Console.WriteLine(ee.ToString());
			}
			finally
			{
				stream.Close();
			}
			return resultObjects;
		}

		/// <summary>
		/// Read the objects from an object array and read out tostring()
		/// used with the binary formatter.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public string FromArrayObjectsToString(T[] obj)
		{
			string strObj = "";

			if (obj == null) return strObj;

			foreach (var item in obj)
			{
				strObj += item.ToString();
			}
			return strObj.ToString();
		}
	}
}
