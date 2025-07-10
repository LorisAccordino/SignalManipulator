using ScottPlot;
using ScottPlot.WinForms;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System.ComponentModel;
using System.Drawing;

namespace SignalManipulator.UI.Controls.User.Viewers
{
    public class BaseViewer : FloatableControl
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


        // Common services and events
        protected IAudioEventDispatcher AudioEvents;
        protected UIUpdateService UIUpdate;

        // Other commons
        protected int SampleRate { get; private set; } = AudioEngine.SAMPLE_RATE;
        protected readonly object RenderLock = new();
        protected volatile bool NeedsRender;

        // Navigator and plot
        protected virtual FormsPlot FormsPlot => throw new NotImplementedException();
        protected virtual AxisNavigator AxisNavigator => throw new NotImplementedException();
        protected Plot Plot => FormsPlot.Plot;

        protected BaseViewer() : base() { }

        public void InitializeViewer()
        {
            AudioEvents = AudioEngine.Instance.AudioEventDispatcher;
            UIUpdate = UIUpdateService.Instance;
            InitializeCommon();
        }


        private void InitializeCommon()
        {
            AudioEvents.OnLoad += OnLoad;
            AudioEvents.OnStarted += OnStarted;
            AudioEvents.OnStopped += OnStopped;
            UIUpdate.Register(RenderPlot);

            FormsPlot.UserInputProcessor.Disable();
            InitializePlot(); // Other defined plot settings
            InitializeEvents(); // Other defined events

            OnStopped(null, EventArgs.Empty);  // Init UI
        }

        private void OnLoad(object? s, AudioInfo info)
        {
            SampleRate = info.SampleRate;
            ResizeBuffers();
            UpdateDataPeriod();
        }

        private void OnStarted(object? s, EventArgs e)
        {
            AudioEvents.AudioFrameReady += ProcessFrameDispatch;
            EnableUI(true);
        }

        private void OnStopped(object? s, EventArgs e)
        {
            AudioEvents.AudioFrameReady -= ProcessFrameDispatch;
            ClearBuffers();
            ResetUI();
            EnableUI(false);
            UIUpdate.Enqueue(RenderPlot);
        }

        private void ProcessFrameDispatch(object? s, CompositeAudioFrame frame)
        {
            lock (RenderLock)
            {
                ProcessFrame(frame);
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
        protected virtual void ProcessFrame(CompositeAudioFrame frame) { }
        protected virtual void EnableUI(bool enable) { }
    }
}