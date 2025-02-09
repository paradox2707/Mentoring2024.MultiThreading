namespace Task3;

public class ThreadSafeSingletonWithoutLock
{
    private static readonly Lazy<ThreadSafeSingletonWithoutLock> _instance =
        new Lazy<ThreadSafeSingletonWithoutLock>(() => new ThreadSafeSingletonWithoutLock());

    private ThreadSafeSingletonWithoutLock()
    {
        // Private constructor to prevent instantiation from outside
    }

    public static ThreadSafeSingletonWithoutLock Instance
    {
        get
        {
            return _instance.Value;
        }
    }
}
