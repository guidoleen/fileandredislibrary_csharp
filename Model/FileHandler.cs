
namespace FileAndRedisLibrary.Model
{
	public abstract class FileHandler<T>
	{
		public string fileName { get; set; }
		protected T[] objs { get; set; }

		public abstract string HandleFile();
	}
}
