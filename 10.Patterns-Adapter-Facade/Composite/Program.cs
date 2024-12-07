namespace Composite;

class Program
{
    static void Main(string[] args)
    {
        var form = new Form("myForm");
        var label = new LabelText("myLabel");
        var input = new InputText("myInput", "myInputValue");

        form.AddComponent(label);
        form.AddComponent(input);

        string xml = form.ConvertToString();
        Console.WriteLine(xml);
    }
}
