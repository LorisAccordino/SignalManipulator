namespace SignalManipulator.Logic.Core.Routing.Outputs
{
    public static class AudioOutputFactory
    {
        public static IAudioOutput Create(AudioDriverType driver)
        {
            return driver switch
            {
                AudioDriverType.WaveOut => new MultiDeviceWaveOut(),
                AudioDriverType.DirectSound => new MultiDeviceDirectSoundOut(),
                AudioDriverType.Wasapi => new MultiDeviceWasapiOut(),
                AudioDriverType.Asio => new MultiDeviceAsioOut(),
                _ => throw new NotSupportedException($"Driver {driver} not supported")
            };
        }

        public static IAudioOutput CreateWithFallback(params AudioDriverType[] fallbackDrivers)
        {
            Exception? lastException = null;

            foreach (var driver in fallbackDrivers)
            {
                try
                {
                    return Create(driver);
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }
            }

            throw new InvalidOperationException("No suitable audio driver could be created.", lastException);
        }
    }
}