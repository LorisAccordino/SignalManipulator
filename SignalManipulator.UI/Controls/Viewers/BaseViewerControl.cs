using ScottPlot;
using ScottPlot.WinForms;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Events;
using SignalManipulator.Logic.Models;
using SignalManipulator.UI.Helpers;
using SignalManipulator.UI.Misc;
using System.Windows.Forms;

namespace SignalManipulator.UI.Controls.Viewers
{
    public abstract partial class BaseViewerControl : UserControl
    {
        // Common services and events
        protected IAudioEventDispatcher AudioEvents;
        protected UIUpdateService UIUpdate;

        // Other commons
        protected int SampleRate { get; private set; }
        protected readonly object RenderLock = new();
        protected volatile bool NeedsRender;

        // Navigator and plot
        protected abstract AxisNavigator AxisNavigator { get; }
        protected abstract FormsPlot FormsPlot { get; }
        protected Plot Plot => FormsPlot.Plot;

        protected BaseViewerControl()
        {
            InitializeComponent();
            if (!DesignModeHelper.IsDesignMode)
            {
                AudioEvents = AudioEngine.Instance.AudioEventDispatcher;
                UIUpdate = UIUpdateService.Instance;
                InitCommon();
            }
        }

        private void InitCommon()
        {
            AudioEvents.OnLoad += OnLoad;
            AudioEvents.OnStarted += OnStarted;
            AudioEvents.OnStopped += OnStopped;
            UIUpdate.Register(RenderPlot);
            OnStopped(null, EventArgs.Empty);  // Init UI

            FormsPlot.UserInputProcessor.Disable();
            InitializeLogic(); // Other defined logic
        }

        private void OnLoad(object? s, AudioInfo info)
        {
            SampleRate = info.SampleRate;
            ConfigureBuffers();
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
        }

        private void ProcessFrameInternal(object? s, WaveformFrame frame)
        {
            lock (RenderLock)
            {
                ProcessFrame(frame);
                NeedsRender = true;
            }
        }

        private void RenderPlot()
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
                    formsPlot.Refresh();
                NeedsRender = false;
            }
        }

        // Template methods to implement in derived classes
        protected abstract void InitializeLogic();
        protected abstract void ConfigureBuffers();
        protected abstract void ClearBuffers();
        protected abstract void ResetUI();
        protected abstract void UpdateDataPeriod();
        protected abstract void ProcessFrame(WaveformFrame frame);
        protected abstract void EnableUI(bool enable);
    }
}