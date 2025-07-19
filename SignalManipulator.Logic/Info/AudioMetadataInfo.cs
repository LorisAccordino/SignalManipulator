using NAudio.Wave;
using System.Drawing;
using File = TagLib.File;

namespace SignalManipulator.Logic.Info
{
    public class AudioMetadataInfo
    {
        private readonly string DEFAULT_MESSAGE = AudioInfo.DEFAULT_MESSAGE;
        private readonly string filePath;
        private File? tagFile;

        public static AudioMetadataInfo Default => new AudioMetadataInfo("");
        public AudioMetadataInfo(string filePath)
        {
            this.filePath = filePath;
        }

        private File? TagFile
        {
            get
            {
                if (tagFile == null && System.IO.File.Exists(filePath)) 
                    tagFile = File.Create(filePath);
                return tagFile;
            }
        }

        public string FilePath => filePath;
        public string Title => TagFile?.Tag?.Title ?? DEFAULT_MESSAGE;
        public string Artist => TagFile?.Tag?.FirstPerformer ?? DEFAULT_MESSAGE;
        public string Album => TagFile?.Tag?.Album ?? DEFAULT_MESSAGE;
        public string Genre => TagFile?.Tag?.FirstGenre ?? DEFAULT_MESSAGE;
        public string Composer => TagFile?.Tag?.FirstComposer ?? DEFAULT_MESSAGE;
        public string Comment => TagFile?.Tag?.Comment ?? DEFAULT_MESSAGE;
        public uint Year => TagFile?.Tag?.Year ?? 0;
        public uint TrackNumber => TagFile?.Tag?.Track ?? 0;

        public byte[]? CoverImageBytes
        {
            get
            {
                var cover = TagFile?.Tag?.Pictures.FirstOrDefault(p => p.Type == TagLib.PictureType.FrontCover);
                return cover?.Data.Data;
            }
        }

        public Image? CoverImage
        {
            get
            {
                var bytes = CoverImageBytes;
                if (bytes != null)
                {
                    using var ms = new MemoryStream(bytes);
                    return Image.FromStream(ms);
                }
                return null;
            }
        }
    }
}