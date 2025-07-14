using SignalManipulator.Forms;
using SignalManipulator.Logic.Core;
using SignalManipulator.Logic.Core.Effects;
using SignalManipulator.Logic.Core.Effects.Loaders;
using SignalManipulator.Logic.Effects;
using SignalManipulator.UI.Helpers;

namespace SignalManipulator.Controls
{
    public partial class EffectChainControl: UserControl
    {
        private readonly Dictionary<IAudioEffect, Form> openEffectUIs = new();
        private bool suppressCheckToggle = false;
        private EffectChain effectChain;

        public EffectChainControl()
        {
            InitializeComponent();

            if (!DesignModeHelper.IsDesignMode)
            {
                effectChain = AudioEngine.Instance.EffectChain;
            }

            // UI events
            effectList.ItemCheck += OnEffectItemCheck;
            effectList.MouseDown += OnEffectMouseDown;
            effectList.MouseDoubleClick += OnEffectItemMouseDoubleClick;
        }

        private void OnAddEffect(object sender, EventArgs e)
        {
            var dialog = new AddEffectDialog();
            if (dialog.ShowDialog() == DialogResult.OK && dialog.SelectedEffect != null)
            {
                effectChain.AddEffect(dialog.SelectedEffect);
                suppressCheckToggle = false;
                effectList.Items.Add(effectChain.GetLastEffect(), true);
            }

        }

        private void OnRemoveEffect(object sender, EventArgs e)
        {
            int selectedIndex = effectList.SelectedIndex;
            if (selectedIndex >= 0)
            {
                effectChain.RemoveAt(selectedIndex);
                effectList.Items.RemoveAt(selectedIndex);
            }
        }

        private void OnEffectItemCheck(object? sender, ItemCheckEventArgs e)
        {
            if (suppressCheckToggle)
            {
                // Cancel the change of the check state
                e.NewValue = e.CurrentValue;
                return;
            }

            BeginInvoke(() =>
            {
                if (e.Index < 0 || e.Index >= effectChain.EffectList.Count) return;

                var effect = effectChain.EffectList[e.Index];
                effect.Bypass = !effectList.GetItemChecked(e.Index);
            });
        }

        private void OnEffectMouseDown(object? sender, MouseEventArgs e)
        {
            int index = effectList.IndexFromPoint(e.Location);

            if (index >= 0)
            {
                Rectangle itemRect = effectList.GetItemRectangle(index);
                int checkWidth = 16; // Checkbox area approx.

                // If click is near left edge (checkbox)
                if (e.X < itemRect.Left + checkWidth)
                {
                    suppressCheckToggle = false; // Normal toggle
                }
                else
                {
                    // Outside the checkbox → suppose to double click and not uncheck
                    suppressCheckToggle = true;
                }
            }
        }

        private void OnEffectItemMouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (!suppressCheckToggle) return;

            int index = effectList.SelectedIndex;
            if (index >= 0 && effectList.Items[index] is IAudioEffect audioEffect)
            {
                if (openEffectUIs.TryGetValue(audioEffect, out var existingForm))
                {
                    // Bring to front the existing form
                    if (existingForm.WindowState == FormWindowState.Minimized)
                        existingForm.WindowState = FormWindowState.Normal;

                    existingForm.BringToFront();
                    return;
                }

                // Otherwise, create a new one
                var uiForm = EffectUILoader.CreateUIForEffect(audioEffect);
                if (uiForm != null)
                {
                    openEffectUIs[audioEffect] = uiForm;
                    uiForm.FormClosed += (s, args) => openEffectUIs.Remove(audioEffect);
                    uiForm.Show();
                }
            }
        }
    }
}