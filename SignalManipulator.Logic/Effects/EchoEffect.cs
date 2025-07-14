using NAudio.Wave;

namespace SignalManipulator.Logic.Effects
{
    public class EchoEffect : AudioEffect
    {
        public override string Name => "Echo";

        // Customizable parameters
        public int DelayMs { get; set; } = 300;           // Delay in ms
        public float Feedback { get; set; } = 0.5f;       // Amount of "feedback"
        public float WetMix { get; set; } = 0.5f;         // Wet quantity
        public float DryMix { get; set; } = 1.0f;         // Dry quantity

        private float[] echoBuffer = Array.Empty<float>();
        private int echoIndex = 0;

        public EchoEffect(ISampleProvider sourceProvider) : base(sourceProvider) { }

        public override int Process(float[] samples, int offset, int count)
        {
            // Read the original data
            int samplesRead = sourceProvider.Read(samples, offset, count);
            if (samplesRead == 0)
                return 0;

            // Apply effect
            int sampleRate = WaveFormat.SampleRate;
            int delaySamples = (DelayMs * sampleRate) / 1000;

            if (echoBuffer.Length != delaySamples)
            {
                echoBuffer = new float[delaySamples];
                echoIndex = 0;
            }

            for (int i = 0; i < samples.Length; i++)
            {
                float dry = samples[i];
                float delayedSample = echoBuffer[echoIndex];

                float wet = delayedSample * WetMix;
                samples[i] = dry * DryMix + wet;

                // Write in the echo fftBuffer
                echoBuffer[echoIndex] = dry + delayedSample * Feedback;

                // Increment and loop
                echoIndex = (echoIndex + 1) % echoBuffer.Length;
            }

            return samplesRead;
        }
    }
}