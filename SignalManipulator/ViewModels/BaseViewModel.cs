using ScottPlot;
using ScottPlot.WinForms;
using SignalManipulator.Controls;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Playback;
using SignalManipulator.Logic.Data;
using SignalManipulator.Logic.Info;
using SignalManipulator.Logic.Providers;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System.ComponentModel;

namespace SignalManipulator.ViewModels
{
    public class BaseViewModel : FloatableControl
    {
        public static readonly int MIN_WIDTH = 680;
        public static readonly int MIN_HEIGHT = 370;

        // Min size
        private Size MinSize => IsSquaredControl ? new Size(MIN_HEIGHT, MIN_HEIGHT) : new Size(MIN_WIDTH, MIN_HEIGHT);
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Size MinimumSize { get => MinSize; set => base.MinimumSize = MinSize; }
        public override Size GetPreferredSize(Size proposedSize) => MinSize;

        private bool isSquaredControl = false;
        public bool IsSquaredControl
        {
            get => isSquaredControl;
            set
            {
                isSquaredControl = value;

                // Keep a square aspect ratio
                if (isSquaredControl) SquareControlHelper.Attach(this, FormsPlot);
            }
        }



        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text { get; set; }


        // Common services exposed
        private AudioPlayer AudioPlayer;
        private AudioDataProvider AudioDataProvider;
        private UIUpdateService UIUpdate;

        // Other commons
        protected int SampleRate { get; private set; } = AudioEngine.SAMPLE_RATE;
        protected readonly object RenderLock = new();
        protected volatile bool NeedsRender;

        // Navigator and plot
        protected virtual FormsPlot FormsPlot => throw new NotImplementedException();
        protected virtual AxisNavigator AxisNavigator => throw new NotImplementedException();
        protected Plot Plot => FormsPlot.Plot;

        protected BaseViewModel() : base() { }

        public void InitializeViewer()
        {
            AudioPlayer = AudioEngine.Instance.AudioPlayer;
            AudioDataProvider = AudioEngine.Instance.AudioDataProvider;
            UIUpdate = UIUpdateService.Instance;
            InitializeCommon();
        }


        private void InitializeCommon()
        {
            AudioPlayer.OnLoad += OnLoad;
            AudioPlayer.OnStarted += OnStarted;
            AudioPlayer.OnStopped += OnStopped;
            UIUpdate.Register(RenderPlot);

            FormsPlot.UserInputProcessor.Disable();
            InitializePlot(); // Other defined plot settings
            InitializeEvents(); // Other defined events

            OnStopped(null, EventArgs.Empty);  // Init UI
        }

        private void OnLoad(object? s, AudioInfo info)
        {
            SampleRate = info.Technical.SampleRate;
            ResizeBuffers();
            UpdateDataPeriod();
        }

        private void OnStarted(object? s, EventArgs e)
        {
            AudioDataProvider.AudioDataReady += ProcessDataDispatch;
            EnableUI(true);
        }

        private void OnStopped(object? s, EventArgs e)
        {
            AudioDataProvider.AudioDataReady -= ProcessDataDispatch;

            this.SafeInvoke(() =>
            {
                ClearBuffers();
                ResetUI();
                EnableUI(false);
                UIUpdate.Enqueue(RenderPlot);
            });
        }

        private void ProcessDataDispatch(object? s, AnalyzedAudioSlice data)
        {
            lock (RenderLock)
            {
                ProcessData(data);
                NeedsRender = true;
            }
        }

        protected void RenderPlot()
        {
            lock (RenderLock)
            {
                if (AxisNavigator.NeedsUpdate)
                {
                    AxisNavigator.Recalculate();
                    Plot.Axes.SetLimitsX(AxisNavigator.Start, AxisNavigator.End);
                    NeedsRender = true;
                }
                if (NeedsRender)
                    FormsPlot.Refresh();
                NeedsRender = false;
            }
        }

        // Template methods to implement in derived classes
        protected virtual void InitializePlot() { }
        protected virtual void InitializeEvents() { }
        protected virtual void ResizeBuffers() { }
        protected virtual void ClearBuffers() { }
        protected virtual void ResetUI() { }
        protected virtual void UpdateDataPeriod() { }
        protected virtual void ProcessData(AnalyzedAudioSlice frame) { }
        protected virtual void EnableUI(bool enable) { }
    }
}