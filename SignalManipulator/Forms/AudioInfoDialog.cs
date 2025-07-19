using SignalManipulator.Logic.Info;

namespace SignalManipulator.Forms
{
    public partial class AudioInfoDialog : Form
    {
        public AudioInfoDialog() : this(AudioInfo.Default) { }
        public AudioInfoDialog(AudioInfo info)
        {
            InitializeComponent();
            SetInfo(info);
        }

        public void SetInfo(AudioInfo info)
        {
            // Tech data
            var tech = info.Technical;
            sampleRateLbl.Value = tech.SampleRate.ToString("N0");
            bitDepthLbl.Value = tech.BitsPerSample.ToString();
            channelsLbl.Value = tech.Channels.ToString();
            encodingLbl.Value = tech.Encoding.ToString();
            bitRateLbl.Value = tech.KyloBitRate.ToString("N0");
            blockAlignLbl.Value = tech.BlockAlign.ToString();
            samplesLbl.Value = tech.TotalSamples.ToString("N0");
            framesLbl.Value = tech.TotalFrames.ToString("N0");

            // Metadata
            var meta = info.Metadata;
            titleLbl.Value = meta.Title;
            artistLbl.Value = meta.Artist;
            albumLbl.Value = meta.Album;
            genreLbl.Value = meta.Genre;
            yearLbl.Value = meta.Year.ToString();
            trackNumberLbl.Value = meta.TrackNumber.ToString();
            durationLbl.Value = info.TotalTime.ToString("hh\\:mm\\:ss\\.fff");
            if (meta.CoverImage != null) coverImageBox.Image = meta.CoverImage;
        }
    }
}