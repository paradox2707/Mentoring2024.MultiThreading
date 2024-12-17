using Task3;
using Xunit;

namespace Task3.Tests
{
    public class SingletonTests
    {
        [Fact]
        public void Instance_ShouldReturnSameInstance()
        {
            // Act
            var instance1 = Singleton.Instance;
            var instance2 = Singleton.Instance;

            // Assert
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void Instance_ShouldNotBeNull()
        {
            // Act
            var instance = Singleton.Instance;

            // Assert
            Assert.NotNull(instance);
        }

        // This test is for demonstration purposes and will not compile
        // because the constructor is private.
        // Uncommenting the following code will result in a compilation error.
        /*
        [Fact]
        public void Constructor_ShouldBeInaccessible()
        {
            // Act
            var instance = new Singleton(); // This line should cause a compilation error

            // Assert
            Assert.Null(instance); // This line will never be reached
        }
        */
    }
}
