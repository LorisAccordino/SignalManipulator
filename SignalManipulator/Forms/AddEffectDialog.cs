using SignalManipulator.Logic.Core.Effects.Loaders;
using SignalManipulator.Logic.Effects;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Forms
{
    public partial class AddEffectDialog : Form
    {
        private ToolTip tooltip = new ToolTip();

        public EffectInfo? SelectedEffect { get; private set; } = null;

        public AddEffectDialog()
        {
            InitializeComponent();
            LoadEffects();
        }

        private void LoadEffects()
        {
            var effects = EffectLoader.GetAvailableEffects();

            foreach (var group in effects.GroupBy(e => e.Category))
            {
                var categoryNode = new TreeNode(group.Key);
                foreach (var effect in group)
                {
                    var node = new TreeNode(effect.Name) { Tag = effect };
                    categoryNode.Nodes.Add(node);
                }
                treeViewEffects.Nodes.Add(categoryNode);
            }

            treeViewEffects.ExpandAll();
        }

        private void treeViewEffects_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag is EffectInfo effect)
            {
                SelectedEffect = effect;
                btnAdd.Enabled = true;
            }
            else
            {
                SelectedEffect = null;
                btnAdd.Enabled = false;
            }
        }

        private void treeViewEffects_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (e.Node?.Tag is EffectInfo effect)
            {
                tooltip.SetToolTip(treeViewEffects, effect.Description);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (SelectedEffect != null)
                DialogResult = DialogResult.OK;
        }
    }
}