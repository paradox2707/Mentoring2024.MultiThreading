namespace Task1_4
{
    public class ExampleCooker(IOutputer outputer) : ICooker
    {
        private IOutputer _outputer = outputer;

        public void FryRice(int amount, Level level)
        {
            _outputer.Print($"Frying {amount} grams of rice at {level} level.");
        }

        public void FryChicken(int amount, Level level)
        {
            _outputer.Print($"Frying {amount} grams of chicken at {level} level.");
        }

        public void SaltRice(Level level)
        {
            _outputer.Print($"Salting rice at {level} level.");
        }

        public void SaltChicken(Level level)
        {
            _outputer.Print($"Salting chicken at {level} level.");
        }

        public void PepperRice(Level level)
        {
            _outputer.Print($"Peppering rice at {level} level.");
        }

        public void PepperChicken(Level level)
        {
            _outputer.Print($"Peppering chicken at {level} level.");
        }
    }
}
