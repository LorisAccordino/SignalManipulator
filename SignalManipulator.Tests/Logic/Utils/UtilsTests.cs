using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Utils;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.Utils
{
    [ExcludeFromCodeCoverage]
    public class CircularBufferTests
    {
        [Fact]
        public void Constructor_SetsCapacityCorrectly()
        {
            var buffer = new CircularBuffer<int>(5);
            Assert.Equal(5, buffer.Capacity);
            Assert.False(buffer.IsFull);
        }

        [Fact]
        public void Constructor_InvalidCapacity_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CircularBuffer<int>(0));
            Assert.Contains("Capacity must be greater than 0", ex.Message);
        }

        [Fact]
        public void Add_SingleItem_StoredCorrectly()
        {
            var buffer = new CircularBuffer<string>(3);
            buffer.Add("A");

            var result = buffer.ToArray();
            Assert.Single(result);
            Assert.Equal("A", result[0]);
            Assert.False(buffer.IsFull);
        }

        [Fact]
        public void Add_OverCapacity_RemovesOldest()
        {
            var buffer = new CircularBuffer<int>(3);
            buffer.Add(1);
            buffer.Add(2);
            buffer.Add(3);
            buffer.Add(4); // Should remove 1

            Assert.True(buffer.IsFull);
            Assert.Equal([2, 3, 4], buffer.ToArray());
        }

        [Fact]
        public void AddRange_WrapsCorrectly()
        {
            var buffer = new CircularBuffer<int>(4);
            buffer.AddRange([10, 20, 30, 40]);
            Assert.Equal([10, 20, 30, 40], buffer.ToArray());

            buffer.AddRange([50, 60]);
            Assert.Equal([30, 40, 50, 60], buffer.ToArray());
        }

        [Fact]
        public void Clear_RemovesAllElements()
        {
            var buffer = new CircularBuffer<double>(3);
            buffer.Add(1.1);
            buffer.Add(2.2);
            buffer.Clear();

            Assert.Empty(buffer.ToArray());
            Assert.False(buffer.IsFull);
        }

        [Fact]
        public void CopyTo_CopiesToTargetArray()
        {
            var buffer = new CircularBuffer<char>(3);
            buffer.AddRange(['a', 'b', 'c']);

            char[] target = new char[5];
            buffer.CopyTo(target, 1); // copy to index 1

            Assert.Equal('\0', target[0]);        // untouched
            Assert.Equal('a', target[1]);
            Assert.Equal('b', target[2]);
            Assert.Equal('c', target[3]);
            Assert.Equal('\0', target[4]);        // untouched
        }

        [Fact]
        public void ChangingCapacity_RemovesExcessElements()
        {
            var buffer = new CircularBuffer<int>(5);
            buffer.AddRange([1, 2, 3, 4, 5]);
            buffer.Capacity = 3;

            Assert.Equal(3, buffer.Capacity);
            Assert.Equal([3, 4, 5], buffer.ToArray());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void SetCapacity_Invalid_Throws(int newCapacity)
        {
            var buffer = new CircularBuffer<int>(2);
            var ex = Assert.Throws<ArgumentException>(() => buffer.Capacity = newCapacity);
            Assert.Contains("Capacity must be greater than 0", ex.Message);
        }
    }

    [ExcludeFromCodeCoverage]
    public class EffectFactoryTests
    {
        private class DummyEffect : IAudioEffect
        {
            public string Name => "Dummy Effect";
            public ISampleProvider Source { get; }
            public WaveFormat WaveFormat => Source.WaveFormat;

            public DummyEffect(ISampleProvider source)
            {
                Source = source;
            }

            public void SetSource(ISampleProvider newSourceProvider) { }
            public int Read(float[] buffer, int offset, int count) => Source.Read(buffer, offset, count);
            public void Reset() { }
        }

        private class InvalidEffect : IAudioEffect
        {
            public string Name => "Invalid Effect";
            public WaveFormat WaveFormat => WaveFormat.CreateIeeeFloatWaveFormat(44100, 2);

            // Missing constructor with ISampleProvider

            public void SetSource(ISampleProvider newSourceProvider) { }
            public int Read(float[] buffer, int offset, int count) => 0;
            public void Reset() { }
        }

        [Fact]
        public void Create_WithValidConstructor_ReturnsWorkingFactory()
        {
            // Arrange
            var sampleProvider = new SignalGenerator(); // from NAudio

            // Act
            var factory = EffectFactory.Create<DummyEffect>();
            var effect = factory(sampleProvider);

            // Assert
            Assert.NotNull(effect);
            Assert.IsType<DummyEffect>(effect);
            Assert.Equal(sampleProvider, ((DummyEffect)effect).Source);
        }

        [Fact]
        public void Create_MissingConstructor_ThrowsInvalidOperationException()
        {
            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                var factory = EffectFactory.Create<InvalidEffect>();
                var sampleProvider = new SignalGenerator();
                _ = factory(sampleProvider);
            });

            Assert.Contains("constructor with the parameter ISampleProvider", ex.Message);
        }
    }
}