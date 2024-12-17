using Moq;
using Task1_4;
using Task1_4.Enums;
using Task1_4.Interfaces;

namespace Task1_4.Tests;

public class RestaurantTests
{
    [Fact]
    public void CookMasala_ShouldFollowBasicIndiaRecipe()
    {
        var restaurant = new Restaurant();
        var mockOutputer = new Mock<IOutputer>();
        var cooker = new ExampleCooker(mockOutputer.Object);
        var currentDate = new DateTime(2023, 1, 1); // Winter date

        restaurant.CookMasala(cooker, Country.India, currentDate);

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
    public void CookMasala_ShouldFollowSummerIndiaRecipe()
    {
        var restaurant = new Restaurant();
        var mockOutputer = new Mock<IOutputer>();
        var cooker = new ExampleCooker(mockOutputer.Object);
        var currentDate = new DateTime(2023, 7, 1); // Summer date

        restaurant.CookMasala(cooker, Country.India, currentDate);

        var expectedMessages = new List<string>
            {
                "Frying 100 grams of rice at Low level.",
                "Frying 100 grams of chicken at Low level.",
                "Peppering rice at Medium level.",
                "Peppering chicken at Medium level."
            };

        foreach (var message in expectedMessages)
        {
            mockOutputer.Verify(o => o.Print(message), Times.Once);
        }
    }

    [Fact]
    public void CookMasala_ShouldFollowBasicUkraineRecipe()
    {
        var restaurant = new Restaurant();
        var mockOutputer = new Mock<IOutputer>();
        var cooker = new ExampleCooker(mockOutputer.Object);
        var currentDate = new DateTime(2023, 1, 1); // Winter date

        restaurant.CookMasala(cooker, Country.Ukraine, currentDate);

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
    public void CookMasala_ShouldFollowSummerUkraineRecipe()
    {
        var restaurant = new Restaurant();
        var mockOutputer = new Mock<IOutputer>();
        var cooker = new ExampleCooker(mockOutputer.Object);
        var currentDate = new DateTime(2023, 7, 1); // Summer date

        restaurant.CookMasala(cooker, Country.Ukraine, currentDate);

        var expectedMessages = new List<string>
            {
                "Frying 150 grams of rice at Medium level.",
                "Frying 200 grams of chicken at Medium level.",
                "Salting rice at Low level.",
                "Salting chicken at Low level."
            };

        foreach (var message in expectedMessages)
        {
            mockOutputer.Verify(o => o.Print(message), Times.Once);
        }
    }

    [Fact]
    public void CookMasala_ShouldFollowBasicEnglandRecipe()
    {
        var restaurant = new Restaurant();
        var mockOutputer = new Mock<IOutputer>();
        var cooker = new ExampleCooker(mockOutputer.Object);
        var currentDate = new DateTime(2023, 1, 1); // Winter date

        restaurant.CookMasala(cooker, Country.England, currentDate);

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

    [Fact]
    public void CookMasala_ShouldFollowSummerEnglandRecipe()
    {
        var restaurant = new Restaurant();
        var mockOutputer = new Mock<IOutputer>();
        var cooker = new ExampleCooker(mockOutputer.Object);
        var currentDate = new DateTime(2023, 7, 1); // Summer date

        restaurant.CookMasala(cooker, Country.England, currentDate);

        var expectedMessages = new List<string>
            {
                "Frying 50 grams of rice at Low level.",
                "Frying 50 grams of chicken at Low level."
            };

        foreach (var message in expectedMessages)
        {
            mockOutputer.Verify(o => o.Print(message), Times.Once);
        }
    }
}