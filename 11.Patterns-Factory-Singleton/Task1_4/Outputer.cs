using Task1_4.Interfaces;

namespace Task1_4;

public class Outputer : IOutputer
{
    public void Print(string message)
    {
        Console.WriteLine(message);
    }
}
