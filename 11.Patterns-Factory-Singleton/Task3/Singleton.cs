namespace Task3;

public class Singleton
{
    private static Singleton _instance;

    private Singleton()
    {
        // Private constructor to prevent instantiation from outside
    }

    public static Singleton Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }
}
