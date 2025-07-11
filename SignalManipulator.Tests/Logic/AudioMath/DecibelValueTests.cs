using SignalManipulator.Logic.AudioMath.Objects;
using System.Diagnostics.CodeAnalysis;

namespace SignalManipulator.Tests.Logic.AudioMath
{
    [ExcludeFromCodeCoverage]
    public class DecibelValueTests
    {
        [Fact]
        public void Constructor_Sets_Linear_And_Reference()
        {
            var db = new DecibelValue(0.5, 2.0);
            Assert.Equal(0.5, db.Linear);
            Assert.Equal(2.0, db.Reference);
        }

        [Fact]
        public void Constructor_Throws_On_Negative_Linear()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DecibelValue(-1.0));
        }

        [Fact]
        public void LinearToDb_Zero_Returns_NegativeInfinity()
        {
            double result = DecibelValue.LinearToDb(0);
            Assert.Equal(double.NegativeInfinity, result);
        }

        [Fact]
        public void LinearToDb_And_Back_Yields_Same_Value()
        {
            double linear = 0.123;
            double db = DecibelValue.LinearToDb(linear);
            double linearBack = DecibelValue.DbToLinear(db);
            Assert.Equal(linear, linearBack, 6);
        }

        [Theory]
        [InlineData(1.0, 0.0)]
        [InlineData(10.0, 20.0)]
        [InlineData(0.1, -20.0)]
        public void Known_Linear_To_Db_Conversion(double linear, double expectedDb)
        {
            double db = DecibelValue.LinearToDb(linear);
            Assert.Equal(expectedDb, db, 4);
        }

        [Fact]
        public void dB_Property_Is_Correct()
        {
            var db = new DecibelValue(1.0);
            Assert.Equal(0.0, db.dB, 6);
        }

        [Fact]
        public void FromDb_Recreates_Correct_Instance()
        {
            var db = new DecibelValue(1.0);
            var newDb = db.FromDb(-6.0);
            Assert.Equal(-6.0, newDb.dB, 2);
        }

        [Fact]
        public void ToString_Returns_dB_Formatted()
        {
            var db = new DecibelValue(1.0);
            Assert.Equal("0,00 dB", db.ToString());
        }

        [Fact]
        public void NamedConstructors_Create_CorrectValues()
        {
            var dbFS = DecibelValue.FromDbFS(-12.0);
            Assert.Equal(-12.0, dbFS.AsDbFS(), 4);

            var dbV = DecibelValue.FromDbV(6.0);
            Assert.Equal(6.0, dbV.AsDbV(), 4);

            var dbU = DecibelValue.FromDbU(0.0);
            Assert.Equal(0.0, dbU.AsDbU(), 4);

            var dbW = DecibelValue.FromDbW(-3.0);
            Assert.Equal(-3.0, dbW.AsDbW(), 4);
        }

        [Fact]
        public void ToStringDb_Formats_Are_Correct()
        {
            var db = new DecibelValue(1.0);
            Assert.Equal("0,00 dBFS", db.ToStringDbFS());
            Assert.Equal("0,00 dBV", db.ToStringDbV());
            Assert.Equal($"{DecibelValue.LinearToDb(1.0, 0.775):F2} dBu", db.ToStringDbU());
            Assert.Equal("0,00 dBW", db.ToStringDbW());
        }
    }
}