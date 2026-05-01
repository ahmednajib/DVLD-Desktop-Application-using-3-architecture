using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DVLD_Project.Global_Classes
{
    public static class clsModernTheme
    {
        private static readonly Color PageBackground = Color.FromArgb(248, 250, 252);
        private static readonly Color Surface = Color.White;
        private static readonly Color Border = Color.FromArgb(226, 232, 240);
        private static readonly Color Text = Color.FromArgb(15, 23, 42);
        private static readonly Color MutedText = Color.FromArgb(100, 116, 139);
        private static readonly Color Primary = Color.FromArgb(37, 99, 235);
        private static readonly Color PrimaryHover = Color.FromArgb(29, 78, 216);
        private static readonly Color Danger = Color.FromArgb(220, 38, 38);
        private static readonly Color DangerHover = Color.FromArgb(185, 28, 28);
        private static readonly Color Header = Color.FromArgb(15, 23, 42);

        private static readonly HashSet<Control> StyledControls = new HashSet<Control>();
        private static readonly Dictionary<Control, Guna2Elipse> RoundedControls = new Dictionary<Control, Guna2Elipse>();
        private static bool _enabled;

        public static void Enable()
        {
            if (_enabled)
                return;

            _enabled = true;
            Application.Idle += ApplyToOpenForms;
        }

        private static void ApplyToOpenForms(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
                Apply(form);
        }

        private static void Apply(Control root)
        {
            ApplyControl(root);

            if (ShouldSkipChildStyling(root))
                return;

            foreach (Control child in root.Controls)
                Apply(child);
        }

        private static void ApplyControl(Control control)
        {
            if (StyledControls.Contains(control))
                return;

            StyledControls.Add(control);
            control.ControlAdded += Control_ControlAdded;
            control.Disposed += Control_Disposed;

            if (control is Form form)
                StyleForm(form);
            else if (control is Guna2Button gunaButton)
                StyleGunaButton(gunaButton);
            else if (control is Button button)
                StyleButton(button);
            else if (control is Guna2TextBox gunaTextBox)
                StyleGunaTextBox(gunaTextBox);
            else if (control is TextBox textBox)
                StyleTextBox(textBox);
            else if (control is Guna2ComboBox gunaComboBox)
                StyleGunaComboBox(gunaComboBox);
            else if (control is ComboBox comboBox)
                StyleComboBox(comboBox);
            else if (control is Guna2DateTimePicker dateTimePicker)
                StyleDateTimePicker(dateTimePicker);
            else if (control is Guna2DataGridView gunaGrid)
                StyleGunaGrid(gunaGrid);
            else if (control is DataGridView grid)
                StyleGrid(grid);
            else if (control is Guna2GroupBox gunaGroupBox)
                StyleGunaGroupBox(gunaGroupBox);
            else if (control is GroupBox groupBox)
                StyleGroupBox(groupBox);
            else if (control is Guna2Panel gunaPanel)
                StyleGunaPanel(gunaPanel);
            else if (control is Panel panel)
                StylePanel(panel);
            else if (control is MenuStrip menuStrip)
                StyleMenuStrip(menuStrip);
            else if (control is Label label)
                StyleLabel(label);
            else if (control is CheckBox checkBox)
                StyleCheckBox(checkBox);
            else if (control is RadioButton radioButton)
                StyleRadioButton(radioButton);
        }

        private static void Control_ControlAdded(object sender, ControlEventArgs e)
        {
            Apply(e.Control);
        }

        private static void Control_Disposed(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null)
                return;

            StyledControls.Remove(control);

            Guna2Elipse elipse;
            if (RoundedControls.TryGetValue(control, out elipse))
            {
                elipse.Dispose();
                RoundedControls.Remove(control);
            }
        }

        private static void StyleForm(Form form)
        {
            form.Font = ModernFont(form.Font, form.Font.Style);
            if (IsDefaultBackColor(form.BackColor))
                form.BackColor = PageBackground;
        }

        private static void StyleGunaButton(Guna2Button button)
        {
            button.Animated = true;
            button.BorderRadius = RadiusFor(button.Height);
            button.Cursor = Cursors.Hand;
            button.Font = ModernFont(button.Font, FontStyle.Bold);

            if (IsSecondaryAction(button.Name, button.Text))
            {
                button.FillColor = Surface;
                button.ForeColor = Text;
                button.BorderColor = Border;
                button.BorderThickness = 1;
                button.HoverState.FillColor = Color.FromArgb(241, 245, 249);
                button.HoverState.ForeColor = Text;
                return;
            }

            if (IsDangerAction(button.Name, button.Text))
            {
                button.FillColor = Danger;
                button.ForeColor = Color.White;
                button.BorderThickness = 0;
                button.HoverState.FillColor = DangerHover;
                return;
            }

            if (IsDefaultFill(button.FillColor))
            {
                button.FillColor = Primary;
                button.ForeColor = Color.White;
                button.HoverState.FillColor = PrimaryHover;
            }
        }

        private static void StyleButton(Button button)
        {
            button.Cursor = Cursors.Hand;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = ModernFont(button.Font, FontStyle.Bold);

            if (IsSecondaryAction(button.Name, button.Text))
            {
                button.BackColor = Surface;
                button.ForeColor = Text;
                button.FlatAppearance.BorderColor = Border;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            }
            else if (IsDangerAction(button.Name, button.Text))
            {
                button.BackColor = Danger;
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = DangerHover;
            }
            else
            {
                button.BackColor = Primary;
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = PrimaryHover;
            }

            ApplyRadius(button, RadiusFor(button.Height));
        }

        private static void StyleGunaTextBox(Guna2TextBox textBox)
        {
            textBox.Animated = true;
            textBox.BorderRadius = TextBoxRadiusFor(textBox.Height);
            textBox.BorderColor = Color.FromArgb(203, 213, 225);
            textBox.BorderThickness = 1;
            textBox.FillColor = Surface;
            textBox.FocusedState.BorderColor = Color.FromArgb(203, 213, 225);
            textBox.HoverState.BorderColor = Color.FromArgb(203, 213, 225);
            textBox.FocusedState.FillColor = Surface;
            textBox.HoverState.FillColor = Surface;
            textBox.Font = ModernFont(textBox.Font, textBox.Font.Style);
            textBox.ForeColor = Text;
            textBox.PlaceholderForeColor = MutedText;
        }

        private static void StyleTextBox(TextBox textBox)
        {
            if (IsInsideGunaInput(textBox))
                return;

            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Surface;
            textBox.ForeColor = Text;
            textBox.Font = ModernFont(textBox.Font, textBox.Font.Style);
            ApplyRadius(textBox, TextBoxRadiusFor(textBox.Height));
        }

        private static void StyleGunaComboBox(Guna2ComboBox comboBox)
        {
            comboBox.BorderRadius = RadiusFor(comboBox.Height);
            comboBox.BorderColor = Border;
            comboBox.FillColor = Surface;
            comboBox.FocusedState.BorderColor = Primary;
            comboBox.HoverState.BorderColor = Color.FromArgb(191, 219, 254);
            comboBox.Font = ModernFont(comboBox.Font, comboBox.Font.Style);
            comboBox.ForeColor = Text;
            comboBox.ItemsAppearance.SelectedBackColor = Color.FromArgb(239, 246, 255);
            comboBox.ItemsAppearance.SelectedForeColor = Text;
        }

        private static void StyleComboBox(ComboBox comboBox)
        {
            if (IsInsideGunaInput(comboBox))
                return;

            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = Surface;
            comboBox.ForeColor = Text;
            comboBox.Font = ModernFont(comboBox.Font, comboBox.Font.Style);
            ApplyRadius(comboBox, RadiusFor(comboBox.Height));
        }

        private static void StyleDateTimePicker(Guna2DateTimePicker dateTimePicker)
        {
            dateTimePicker.Animated = true;
            dateTimePicker.BorderRadius = RadiusFor(dateTimePicker.Height);
            dateTimePicker.BorderColor = Border;
            dateTimePicker.BorderThickness = 1;
            dateTimePicker.FillColor = Surface;
            dateTimePicker.FocusedColor = Primary;
            dateTimePicker.Font = ModernFont(dateTimePicker.Font, dateTimePicker.Font.Style);
            dateTimePicker.ForeColor = Text;
        }

        private static void StyleGunaGrid(Guna2DataGridView grid)
        {
            grid.Theme = DataGridViewPresetThemes.WhiteGrid;
            StyleGrid(grid);
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Border;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grid.ColumnHeadersHeight = Math.Max(grid.ColumnHeadersHeight, 42);
            grid.RowTemplate.Height = Math.Max(grid.RowTemplate.Height, 36);

            grid.ColumnHeadersDefaultCellStyle.BackColor = Header;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = Header;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = ModernFont(grid.Font, FontStyle.Bold);

            grid.DefaultCellStyle.BackColor = Surface;
            grid.DefaultCellStyle.ForeColor = Text;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = Text;
            grid.DefaultCellStyle.Font = ModernFont(grid.Font, FontStyle.Regular);

            grid.AlternatingRowsDefaultCellStyle.BackColor = PageBackground;
            grid.AlternatingRowsDefaultCellStyle.ForeColor = Text;

            ApplyRadius(grid, 10);
        }

        private static void StyleGunaGroupBox(Guna2GroupBox groupBox)
        {
            groupBox.BorderColor = Border;
            groupBox.BorderRadius = 12;
            groupBox.BorderThickness = 1;
            groupBox.CustomBorderColor = Header;
            groupBox.FillColor = Surface;
            groupBox.Font = ModernFont(groupBox.Font, FontStyle.Bold);
            groupBox.ForeColor = Color.White;
        }

        private static void StyleGroupBox(GroupBox groupBox)
        {
            groupBox.BackColor = Surface;
            groupBox.ForeColor = Text;
            groupBox.Font = ModernFont(groupBox.Font, FontStyle.Bold);
            ApplyRadius(groupBox, 12);
        }

        private static void StyleGunaPanel(Guna2Panel panel)
        {
            panel.BorderRadius = panel.BorderRadius == 0 ? 12 : panel.BorderRadius;

            if (IsDefaultFill(panel.FillColor) && panel.Dock != DockStyle.Fill)
                panel.FillColor = Surface;

            if (panel.BorderThickness == 0 && panel.Dock != DockStyle.Fill)
            {
                panel.BorderColor = Border;
                panel.BorderThickness = 1;
            }
        }

        private static void StylePanel(Panel panel)
        {
            if (IsDefaultBackColor(panel.BackColor))
                panel.BackColor = panel.Dock == DockStyle.Fill ? PageBackground : Surface;

            if (panel.Dock != DockStyle.Fill)
                ApplyRadius(panel, 12);
        }

        private static void StyleMenuStrip(MenuStrip menuStrip)
        {
            menuStrip.BackColor = Header;
            menuStrip.ForeColor = Color.White;
            menuStrip.Font = ModernFont(menuStrip.Font, FontStyle.Bold);
            menuStrip.Renderer = new ToolStripProfessionalRenderer(new ModernMenuColorTable());

            foreach (ToolStripItem item in menuStrip.Items)
                StyleToolStripItem(item);
        }

        private static void StyleToolStripItem(ToolStripItem item)
        {
            item.Font = ModernFont(item.Font, item.Font.Style);
            item.ForeColor = item.OwnerItem == null ? Color.White : Text;

            ToolStripMenuItem menuItem = item as ToolStripMenuItem;
            if (menuItem == null)
                return;

            menuItem.DropDown.BackColor = Surface;
            menuItem.DropDown.ForeColor = Text;
            menuItem.DropDown.Padding = new Padding(6);

            foreach (ToolStripItem dropDownItem in menuItem.DropDownItems)
                StyleToolStripItem(dropDownItem);
        }

        private static void StyleLabel(Label label)
        {
            label.Font = ModernFont(label.Font, label.Font.Style);

            if (label.ForeColor == SystemColors.ControlText ||
                label.ForeColor == Color.Black ||
                (IsInsideGunaGroupBox(label) && label.ForeColor == Color.White))
            {
                label.ForeColor = Text;
            }
        }

        private static void StyleCheckBox(CheckBox checkBox)
        {
            checkBox.Font = ModernFont(checkBox.Font, checkBox.Font.Style);
            checkBox.ForeColor = Text;
        }

        private static void StyleRadioButton(RadioButton radioButton)
        {
            radioButton.Font = ModernFont(radioButton.Font, radioButton.Font.Style);
            radioButton.ForeColor = Text;
        }

        private static Font ModernFont(Font source, FontStyle style)
        {
            float size = source == null ? 9.75F : source.Size;
            return new Font("Segoe UI", size, style, GraphicsUnit.Point);
        }

        private static int RadiusFor(int height)
        {
            if (height >= 48)
                return 10;

            if (height >= 32)
                return 8;

            return 6;
        }

        private static int TextBoxRadiusFor(int height)
        {
            return height >= 40 ? 6 : 5;
        }

        private static bool IsSecondaryAction(string name, string text)
        {
            string value = ((name ?? string.Empty) + " " + (text ?? string.Empty)).ToLowerInvariant();
            return value.Contains("close") ||
                   value.Contains("cancel") ||
                   value.Contains("back") ||
                   value.Contains("previous") ||
                   value.Contains("prev");
        }

        private static bool IsDangerAction(string name, string text)
        {
            string value = ((name ?? string.Empty) + " " + (text ?? string.Empty)).ToLowerInvariant();
            return value.Contains("delete") ||
                   value.Contains("remove");
        }

        private static bool IsDefaultFill(Color color)
        {
            return color == Color.Empty ||
                   color == Color.Transparent ||
                   color == SystemColors.Control ||
                   color == SystemColors.ButtonFace ||
                   color == Color.FromArgb(94, 148, 255);
        }

        private static bool IsDefaultBackColor(Color color)
        {
            return color == Color.Empty ||
                   color == Color.Transparent ||
                   color == SystemColors.Control ||
                   color == SystemColors.ControlLight ||
                   color == SystemColors.ControlLightLight;
        }

        private static bool ShouldSkipChildStyling(Control control)
        {
            if (control is Form form && form.Name == "frmLogin")
                return true;

            return control is Guna2TextBox ||
                   control is Guna2ComboBox ||
                   control is Guna2DateTimePicker ||
                   control is Guna2Button ||
                   control is Guna2DataGridView;
        }

        private static bool IsInsideGunaInput(Control control)
        {
            Control parent = control.Parent;

            while (parent != null)
            {
                if (ShouldSkipChildStyling(parent))
                    return true;

                parent = parent.Parent;
            }

            return false;
        }

        private static bool IsInsideGunaGroupBox(Control control)
        {
            Control parent = control.Parent;

            while (parent != null)
            {
                if (parent is Guna2GroupBox)
                    return true;

                parent = parent.Parent;
            }

            return false;
        }

        private static void ApplyRadius(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
                return;

            Guna2Elipse elipse;
            if (!RoundedControls.TryGetValue(control, out elipse))
            {
                elipse = new Guna2Elipse();
                elipse.TargetControl = control;
                RoundedControls[control] = elipse;
            }

            elipse.BorderRadius = radius;
        }

        private sealed class ModernMenuColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground { get { return Surface; } }
            public override Color ToolStripBorder { get { return Border; } }
            public override Color MenuBorder { get { return Border; } }
            public override Color MenuItemBorder { get { return Color.FromArgb(191, 219, 254); } }
            public override Color MenuItemSelected { get { return Color.FromArgb(239, 246, 255); } }
            public override Color MenuItemSelectedGradientBegin { get { return Color.FromArgb(239, 246, 255); } }
            public override Color MenuItemSelectedGradientEnd { get { return Color.FromArgb(239, 246, 255); } }
            public override Color MenuItemPressedGradientBegin { get { return Color.FromArgb(30, 41, 59); } }
            public override Color MenuItemPressedGradientMiddle { get { return Color.FromArgb(30, 41, 59); } }
            public override Color MenuItemPressedGradientEnd { get { return Color.FromArgb(30, 41, 59); } }
            public override Color ImageMarginGradientBegin { get { return Surface; } }
            public override Color ImageMarginGradientMiddle { get { return Surface; } }
            public override Color ImageMarginGradientEnd { get { return Surface; } }
            public override Color SeparatorDark { get { return Border; } }
            public override Color SeparatorLight { get { return Surface; } }
        }
    }
}
