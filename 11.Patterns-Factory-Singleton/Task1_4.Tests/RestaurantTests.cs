using Moq;

namespace Task1_4.Tests
{
    public class RestaurantTests
    {
        [Fact]
        public void CookMasala_ShouldFollowIndiaRecipe()
        {
            var restaurant = new Restaurant();
            var mockOutputer = new Mock<IOutputer>();
            var cooker = new ExampleCooker(mockOutputer.Object);

            restaurant.CookMasala(cooker, Country.India);

            var expectedMessages = new List<string>
                {
                    "Frying 200 grams of rice at Strong level.",
                    "Frying 100 grams of chicken at Strong level.",
                    "Salting rice at Strong level.",
                    "Salting chicken at Strong level.",
                    "Peppering rice at Strong level.",
                    "Peppering chicken at Strong level."
                };

            foreach (var message in expectedMessages)
            {
                mockOutputer.Verify(o => o.Print(message), Times.Once);
            }
        }

        [Fact]
        public void CookMasala_ShouldFollowUkraineRecipe()
        {
            var restaurant = new Restaurant();
            var mockOutputer = new Mock<IOutputer>();
            var cooker = new ExampleCooker(mockOutputer.Object);

            restaurant.CookMasala(cooker, Country.Ukraine);

            var expectedMessages = new List<string>
                {
                    "Frying 500 grams of rice at Strong level.",
                    "Frying 300 grams of chicken at Medium level.",
                    "Salting rice at Strong level.",
                    "Salting chicken at Medium level.",
                    "Peppering rice at Low level.",
                    "Peppering chicken at Low level."
                };

            foreach (var message in expectedMessages)
            {
                mockOutputer.Verify(o => o.Print(message), Times.Once);
            }
        }

        [Fact]
        public void CookMasala_ShouldFollowEnglandRecipe()
        {
            var restaurant = new Restaurant();
            var mockOutputer = new Mock<IOutputer>();
            var cooker = new ExampleCooker(mockOutputer.Object);

            restaurant.CookMasala(cooker, Country.England);

            var expectedMessages = new List<string>
                {
                    "Frying 100 grams of rice at Low level.",
                    "Frying 100 grams of chicken at Low level."
                };

            foreach (var message in expectedMessages)
            {
                mockOutputer.Verify(o => o.Print(message), Times.Once);
            }
        }
    }
}