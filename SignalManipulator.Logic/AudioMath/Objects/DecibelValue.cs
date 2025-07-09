namespace SignalManipulator.Logic.AudioMath.Objects
{
    public class DecibelValue
    {
        public double Linear { get; }
        public double dB => LinearToDb(Linear);

        // Optional reference level (e.g., for dBV, dBu, dBW)
        public double Reference { get; } = 1.0;

        public DecibelValue(double linear, double reference = 1.0)
        {
            if (linear < 0)
                throw new ArgumentOutOfRangeException(nameof(linear), "Linear value must be non-negative.");

            Linear = linear;
            Reference = reference;
        }

        public DecibelValue FromDb(double db)
        {
            return new DecibelValue(DbToLinear(db, Reference), Reference);
        }

        public override string ToString() => $"{dB:F2} dB";

        // --- Static conversion utilities ---
        public static double LinearToDb(double value, double reference = 1.0)
        {
            if (value <= 0)
                return double.NegativeInfinity; // Often used to represent silence
            return 20.0 * Math.Log10(value / reference);
        }

        public static double DbToLinear(double db, double reference = 1.0)
        {
            return reference * Math.Pow(10, db / 20.0);
        }

        // --- Named constructors for specific dB types ---
        public static DecibelValue FromDbFS(double dbFS) => new(DbToLinear(dbFS, 1.0), 1.0);
        public static DecibelValue FromDbV(double dbV) => new(DbToLinear(dbV, 1.0), 1.0); // Volt ref 1V
        public static DecibelValue FromDbU(double dbU) => new(DbToLinear(dbU, 0.775), 0.775); // 0.775V
        public static DecibelValue FromDbW(double dbW) => new(DbToLinear(dbW, 1.0), 1.0); // Watt ref 1W

        // Get value in specific units
        public double AsDbFS() => LinearToDb(Linear, 1.0);
        public double AsDbV() => LinearToDb(Linear, 1.0);
        public double AsDbU() => LinearToDb(Linear, 0.775);
        public double AsDbW() => LinearToDb(Linear, 1.0);

        public string ToStringDbFS() => $"{AsDbFS():F2} dBFS";
        public string ToStringDbV() => $"{AsDbV():F2} dBV";
        public string ToStringDbU() => $"{AsDbU():F2} dBu";
        public string ToStringDbW() => $"{AsDbW():F2} dBW";
    }
}