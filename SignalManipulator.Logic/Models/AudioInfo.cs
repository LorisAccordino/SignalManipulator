using NAudio.Wave;
using SignalManipulator.Logic.Providers;
using System.Drawing;
namespace SignalManipulator.Logic.Models
{
    public struct AudioInfo
    {
        private const string DEFAULT_MESSAGE = "No info available";


        /*** TECHNICAL DATA ***/
        public ISampleProvider SourceProvider { get; } = new DefaultSampleProvider();
        public WaveStream? WaveStream { get; } = null;

        public string FileName { get; } = DEFAULT_MESSAGE;
        public TimeSpan CurrentTime => WaveStream?.CurrentTime ?? TimeSpan.Zero;
        public TimeSpan TotalTime => WaveStream?.TotalTime ?? TimeSpan.Zero;
        public long Length => WaveStream?.Length ?? 0;

        public WaveFormat WaveFormat => SourceProvider.WaveFormat;
        public int SampleRate => WaveFormat.SampleRate;
        public int Channels => WaveFormat.Channels;
        public int BitsPerSample => WaveFormat.BitsPerSample;
        public int BytesPerSample => BitsPerSample / 8;
        public string WaveFormatDescription => WaveFormat?.ToString() ?? DEFAULT_MESSAGE;
        public WaveFormatEncoding Encoding => WaveFormat.Encoding;

        public int ByteRate => WaveFormat.AverageBytesPerSecond;
        public int BitRate => ByteRate * 8;
        public int KyloBitRate => BitRate / 1000;
        public int BlockAlign => WaveFormat.BlockAlign;
        public int TotalSamples => (int)(Length / BytesPerSample);
        public int TotalFrames => (int)(Length / BlockAlign);
        /**********************/


        /*******METADATA*******/
        public TagLib.File? TagFile
        {
            get
            {
                if (File.Exists(FileName))
                    return TagLib.File.Create(FileName);
                return null;
            }
        }

        public TagLib.Tag? Tag => TagFile?.Tag;
        public string Title => Tag?.Title ?? DEFAULT_MESSAGE;
        public string Artist => Tag?.FirstPerformer ?? DEFAULT_MESSAGE;
        public string Album => Tag?.Album ?? DEFAULT_MESSAGE;
        public string Genre => Tag?.FirstGenre ?? DEFAULT_MESSAGE;
        public string Composer => Tag?.FirstComposer ?? DEFAULT_MESSAGE;
        public string Comment => Tag?.Comment ?? DEFAULT_MESSAGE;
        public uint Year => Tag?.Year ?? 0;
        public uint TrackNumber => Tag?.Track ?? 0;
        public byte[]? CoverImageBytes
        {
            get
            {
                var cover = Tag?.Pictures.FirstOrDefault(p => p.Type == TagLib.PictureType.FrontCover);

                if (cover != null)
                    return cover.Data.Data;

                return null;
            }
        }
        public Image? CoverImage
        {
            get
            {
                if (CoverImageBytes != null)
                {
                    using var ms = new MemoryStream(CoverImageBytes);
                    return Image.FromStream(ms);
                }

                return null;
            }
        }
        /**********************/


        /******STATS DATA******/


        /**********************/


        public static AudioInfo Default => new AudioInfo();

        public AudioInfo() { }
        public AudioInfo(AudioFileReader? reader)
        {
            if (reader == null) return;

            // General info
            SourceProvider = reader;
            WaveStream = reader;
            FileName = reader.FileName;
        }

        public AudioInfo(ISampleProvider? provider) => SourceProvider = provider ?? new DefaultSampleProvider();
    }
}