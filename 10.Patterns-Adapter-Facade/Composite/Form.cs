namespace Composite
{
    public class Form : IXmlElement
    {
        private readonly string _name;
        private readonly List<IXmlElement> _elements;

        public Form(string name)
        {
            _name = name;
            _elements = new List<IXmlElement>();
        }

        public void AddComponent(IXmlElement element)
        {
            _elements.Add(element);
        }

        public string ConvertToString()
        {
            var xml = $"<form name='{_name}'>\n";
            foreach (var element in _elements)
            {
                xml += $"  {element.ConvertToString()}\n";
            }
            xml += "</form>";
            return xml;
        }
    }
}
