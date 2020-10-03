namespace Utils.Patterns.Singleton
{
	static class Singleton<T> where T : new()
	{
		private static T _instance = new T();

		public static T Instance => _instance;
	}
}
