using Adapter.Interfaces;

namespace Adapter
{
    public class Elements<T> : IElements<T>
    {
        private readonly List<T> _elements;

        public Elements(IEnumerable<T> elements)
        {
            _elements = new List<T>(elements);
        }

        public IEnumerable<T> GetElements()
        {
            return _elements;
        }
    }
}
