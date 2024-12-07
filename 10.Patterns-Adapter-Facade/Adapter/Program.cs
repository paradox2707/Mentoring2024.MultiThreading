namespace Adapter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var elements = new Elements<string>(new List<string> { "Element1", "Element2", "Element3" });

            var containerAdapter = new ElementsAdapter<string>(elements);

            var printer = new Printer();
            printer.Print(containerAdapter);
        }
    }
}
