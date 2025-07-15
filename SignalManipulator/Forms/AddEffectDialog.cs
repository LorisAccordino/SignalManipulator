using SignalManipulator.Logic.Core.Effects.Loaders;
using SignalManipulator.Logic.Info;

namespace SignalManipulator.Forms
{
    public partial class AddEffectDialog : Form
    {
        private ToolTip tooltip = new ToolTip();
        private List<EffectInfo> allEffects = new();

        public EffectInfo? SelectedEffect { get; private set; } = null;

        public AddEffectDialog()
        {
            InitializeComponent();
            searchTxt.TextChanged += OnSearchTextChanged;
            LoadEffects();
        }

        private void LoadEffects()
        {
            allEffects = EffectLoader.GetAvailableEffects().ToList();
            PopulateTree(allEffects);
        }

        private void PopulateTree(IEnumerable<EffectInfo> effects)
        {
            treeViewEffects.BeginUpdate();
            treeViewEffects.Nodes.Clear();

            foreach (var group in effects.GroupBy(e => e.Category).OrderBy(g => g.Key))
            {
                var categoryNode = new TreeNode(group.Key);
                foreach (var effect in group.OrderBy(e => e.Name))
                {
                    var node = new TreeNode(effect.Name) { Tag = effect };
                    categoryNode.Nodes.Add(node);
                }

                if (categoryNode.Nodes.Count > 0)
                    treeViewEffects.Nodes.Add(categoryNode);
            }

            treeViewEffects.ExpandAll();
            treeViewEffects.SelectedNode = null;
            btnAdd.Enabled = false;
            SelectedEffect = null;
            treeViewEffects.EndUpdate();
        }

        private void OnSearchTextChanged(object? sender, EventArgs e)
        {
            string query = searchTxt.Text.Trim().ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(query))
            {
                PopulateTree(allEffects);
            }
            else
            {
                var filtered = allEffects
                    .Where(e => e.Name.ToLowerInvariant().Contains(query)
                             || e.Description?.ToLowerInvariant().Contains(query) == true
                             || e.Category?.ToLowerInvariant().Contains(query) == true);
                PopulateTree(filtered);
            }
        }

        private void OnEffects_AfterSelect(object sender, TreeViewEventArgs e)
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

        private void OnEffects_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (e.Node?.Tag is EffectInfo effect)
            {
                tooltip.SetToolTip(treeViewEffects, effect.Description);
            }
        }

        private void OnAdd_Click(object sender, EventArgs e)
        {
            if (SelectedEffect != null)
                DialogResult = DialogResult.OK;
        }
    }
}