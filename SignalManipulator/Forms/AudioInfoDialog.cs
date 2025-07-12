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
            sampleRateLbl.Value = info.SampleRate.ToString("N0");
            bitDepthLbl.Value = info.BitsPerSample.ToString();
            channelsLbl.Value = info.Channels.ToString();
            encodingLbl.Value = info.Encoding.ToString();
            bitRateLbl.Value = info.KyloBitRate.ToString("N0");
            blockAlignLbl.Value = info.BlockAlign.ToString();
            samplesLbl.Value = info.TotalSamples.ToString("N0");
            framesLbl.Value = info.TotalFrames.ToString("N0");

            // Metadata
            titleLbl.Value = info.Title;
            artistLbl.Value = info.Artist;
            albumLbl.Value = info.Album;
            genreLbl.Value = info.Genre;
            yearLbl.Value = info.Year.ToString();
            trackNumberLbl.Value = info.TrackNumber.ToString();
            durationLbl.Value = info.TotalTime.ToString("hh\\:mm\\:ss\\.fff");
            if (info.CoverImage != null) coverImageBox.Image = info.CoverImage;
        }
    }
}