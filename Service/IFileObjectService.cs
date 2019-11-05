namespace FileAndRedisLibrary.Service
{
	public interface IFileObjectService<T>
	{
		void Read(string fileName);
		void Write(string fileName, T[] objects);
	}
}