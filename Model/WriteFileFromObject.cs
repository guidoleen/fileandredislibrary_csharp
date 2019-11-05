using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileAndRedisLibrary.Model
{
	public class WriteFileFromObject<T> : FileHandler<T>
	{
		public void SetFileListObj(T[] objs)
		{
			base.objs = objs;
		}
		private string WriteFile()
		{
			string result = "";
			byte[] bytes;

			using (FileStream fstream = File.Open(base.fileName, FileMode.OpenOrCreate ) )
			{
				try
				{
					foreach (T item in base.objs)
					{
						bytes = ConvertObjectToByte(item);
						fstream.Write(bytes, 0, bytes.Length);
					}
				}
				catch (IOException ee)
				{
					result = ee.ToString();
				}
			}
			
			return result;
		}

		private byte[] ConvertObjectToByte(T obj)
		{
			if (obj == null) return null;

			byte[] bytes = null;
			BinaryFormatter bf = new BinaryFormatter();

			using (MemoryStream stream = new MemoryStream())
			{
				bf.Serialize(stream, obj);
				bytes = stream.ToArray();

				return bytes;
			}
		}

		public override string HandleFile()
		{
			return this.WriteFile();
		}
	}
}
