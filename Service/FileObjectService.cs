using FileAndRedisLibrary.Model;
using System;

namespace FileAndRedisLibrary.Service
{
	public class FileObjectService<T> : IFileObjectService<T>
	{
		/// <summary>
		/// Read from file with serialized object(s)
		/// </summary>
		/// <param name="fileName"></param>
		public void Read(string fileName)
		{
			FileHandler<T> readHandler = new ReadFileFromObject<T>();
			readHandler.fileName = fileName;

			Console.WriteLine(
				readHandler.HandleFile()
				);
		}

		/// <summary>
		/// Write to file with serialized object(s)
		/// </summary>
		/// <param name="fileName"></param>
		public void Write(string fileName, T[] objects)
		{
			WriteFileFromObject<T> handler = new WriteFileFromObject<T>();
			handler.fileName = fileName;

			handler.SetFileListObj(objects);
			
			// Call the handler to handle the file
			Console.WriteLine(
				handler.HandleFile()
				);
		}
	}
}
