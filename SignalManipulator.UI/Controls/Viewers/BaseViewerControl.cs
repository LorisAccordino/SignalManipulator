using ScottPlot;
using ScottPlot.WinForms;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Misc;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls.Viewers
{
    public class BaseViewerControl : UserControl
    {
        // Common services and events
        protected IAudioEventDispatcher AudioEvents;
        protected UIUpdateService UIUpdate;

        // Other commons
        protected int SampleRate { get; private set; }
        protected readonly object RenderLock = new();
        protected volatile bool NeedsRender;

        // Navigator and plot
        protected virtual FormsPlot FormsPlot => throw new NotImplementedException();
        protected virtual AxisNavigator AxisNavigator => throw new NotImplementedException();
        protected Plot Plot => FormsPlot.Plot;

        protected BaseViewerControl() { }

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
            OnStopped(null, EventArgs.Empty);  // Init UI

            FormsPlot.UserInputProcessor.Disable();
            InitializePlot(); // Other defined plot settings
            InitializeEvents(); // Other defined events
        }

        private void OnLoad(object? s, AudioInfo info)
        {
            SampleRate = info.SampleRate;
            ResampleBuffers();
            UpdateDataPeriod();
        }

        private void OnStarted(object? s, EventArgs e)
        {
            AudioEvents.WaveformReady += ProcessFrameInternal;
            EnableUI(true);
        }

        private void OnStopped(object? s, EventArgs e)
        {
            AudioEvents.WaveformReady -= ProcessFrameInternal;
            ClearBuffers();
            ResetUI();
            EnableUI(false);
            UIUpdate.Enqueue(RenderPlot);
        }
        
        private void ProcessFrameInternal(object? s, WaveformFrame frame)
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
        protected virtual void ResampleBuffers() { }
        protected virtual void ClearBuffers() { }
        protected virtual void ResetUI() { }
        protected virtual void UpdateDataPeriod() { }
        protected virtual void ProcessFrame(WaveformFrame frame) { }
        protected virtual void EnableUI(bool enable) { }
    }
}