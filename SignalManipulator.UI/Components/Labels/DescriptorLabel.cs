﻿using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace SignalManipulator.UI.Components.Labels
{
    [ExcludeFromCodeCoverage]
    public partial class DescriptorLabel : Label
    {
        private string description = "Description";
        private string separator = ": ";
        private string value = "value";

        [DefaultValue("Description")]
        [ExcludeFromCodeCoverage]
        public string Description
        {
            get => description;
            set
            {
                description = value;
                FormatText();
            }
        }

        [DefaultValue(": ")]
        public string Separator
        {
            get => separator;
            set
            {
                separator = value;
                FormatText();
            }
        }

        [DefaultValue("value")]
        public string Value
        {
            get => value;
            set
            {
                this.value = value;
                FormatText();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string Text { get => Value; set => Value = value; }


        public DescriptorLabel()
        {
            InitializeComponent();
        }

        public DescriptorLabel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        protected override void OnCreateControl() => FormatText();
        private void FormatText() => base.Text = Description + Separator + Value;
    }
}