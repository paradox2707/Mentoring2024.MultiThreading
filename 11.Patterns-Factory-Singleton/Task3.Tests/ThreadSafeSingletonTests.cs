using Task3;
using Xunit;

namespace Task3.Tests
{
    public class ThreadSafeSingletonTests
    {
        [Fact]
        public void Instance_ShouldReturnSameInstance()
        {
            // Act
            var instance1 = ThreadSafeSingleton.Instance;
            var instance2 = ThreadSafeSingleton.Instance;

            // Assert
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void Instance_ShouldNotBeNull()
        {
            // Act
            var instance = ThreadSafeSingleton.Instance;

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Instance_ShouldBeThreadSafe()
        {
            ThreadSafeSingleton[] instances = new ThreadSafeSingleton[10];
            Parallel.For(0, 10, i =>
            {
                instances[i] = ThreadSafeSingleton.Instance;
            });

            for (int i = 1; i < instances.Length; i++)
            {
                Assert.Same(instances[0], instances[i]);
            }
        }
    }
}
