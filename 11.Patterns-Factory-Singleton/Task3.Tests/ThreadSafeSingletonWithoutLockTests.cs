using Task3;
using Xunit;

namespace Task3.Tests
{
    public class ThreadSafeSingletonWithoutLockTests
    {
        [Fact]
        public void Instance_ShouldReturnSameInstance()
        {
            // Act
            var instance1 = ThreadSafeSingletonWithoutLock.Instance;
            var instance2 = ThreadSafeSingletonWithoutLock.Instance;

            // Assert
            Assert.Same(instance1, instance2);
        }

        [Fact]
        public void Instance_ShouldNotBeNull()
        {
            // Act
            var instance = ThreadSafeSingletonWithoutLock.Instance;

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void Instance_ShouldBeThreadSafe()
        {
            ThreadSafeSingletonWithoutLock[] instances = new ThreadSafeSingletonWithoutLock[10];
            Parallel.For(0, 10, i =>
            {
                instances[i] = ThreadSafeSingletonWithoutLock.Instance;
            });

            for (int i = 1; i < instances.Length; i++)
            {
                Assert.Same(instances[0], instances[i]);
            }
        }
    }
}
