namespace Task3;

public class ThreadSafeSingleton
{
    private static ThreadSafeSingleton _instance;
    private static readonly object _lock = new object();

    private ThreadSafeSingleton()
    {
        // Private constructor to prevent instantiation from outside
    }

    public static ThreadSafeSingleton Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ThreadSafeSingleton();
                }
            }
            return _instance;
        }
    }
}
