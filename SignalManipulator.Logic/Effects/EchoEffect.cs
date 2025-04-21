using NAudio.Wave;
using SignalManipulator.Logic.Utils;
using System;

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

        private double[] echoBuffer = Array.Empty<double>();
        private int echoIndex = 0;

        public EchoEffect(IWaveProvider sourceProvider) : base(sourceProvider) { }

        public void Process(double[] buffer, int sampleRate)
        {
            int delaySamples = (DelayMs * sampleRate) / 1000;

            if (echoBuffer.Length != delaySamples)
            {
                echoBuffer = new double[delaySamples];
                echoIndex = 0;
            }

            for (int i = 0; i < buffer.Length; i++)
            {
                double dry = buffer[i];
                double delayedSample = echoBuffer[echoIndex];

                double wet = delayedSample * WetMix;
                buffer[i] = dry * DryMix + wet;

                // Write in the echo buffer
                echoBuffer[echoIndex] = dry + delayedSample * Feedback;

                // Increment and loop
                echoIndex = (echoIndex + 1) % echoBuffer.Length;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // Read the original data
            int bytesRead = sourceProvider.Read(buffer, offset, count);
            if (bytesRead == 0)
                return 0;

            // Convert to double
            double[] doubleBuffer = AudioMathHelper.ConvertPcmToDouble(buffer, WaveFormat);

            // Apply effect
            Process(doubleBuffer, WaveFormat.SampleRate);

            // Convert back to byte[]
            byte[] processedBytes = AudioMathHelper.ConvertDoubleToPcm(doubleBuffer, WaveFormat);

            // Copy into the original buffer
            Array.Copy(processedBytes, 0, buffer, offset, bytesRead);

            return bytesRead;
        }
    }
}
