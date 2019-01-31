﻿/**
 * MetroFramework - Modern UI for WinForms
 * 
 * The MIT License (MIT)
 * Copyright (c) 2011 Sven Walter, http://github.com/viperneo
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using MetroFramework.Components;
using MetroFramework.Interfaces;
using MetroFramework.Drawing;
using System.Collections.Generic;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace MetroFramework.Controls
{
    [ToolboxBitmap(typeof(ComboBox))]
    public class MetroComboBox : ComboBox, IMetroControl
    {

        #region Edit Control
        private MetroTextBox textBox = new MetroTextBox();
        #endregion

        #region Interface

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        private MetroColorStyle metroStyle = MetroColorStyle.Default;
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroColorStyle.Default)]
        public MetroColorStyle Style
        {
            get
            {
                if (DesignMode || metroStyle != MetroColorStyle.Default)
                {
                    return metroStyle;
                }

                if (StyleManager != null && metroStyle == MetroColorStyle.Default)
                {
                    return StyleManager.Style;
                }
                if (StyleManager == null && metroStyle == MetroColorStyle.Default)
                {
                    return MetroDefaults.Style;
                }

                return metroStyle;
            }
            set { metroStyle = value; }
        }

        private MetroThemeStyle metroTheme = MetroThemeStyle.Default;
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [DefaultValue(MetroThemeStyle.Default)]
        public MetroThemeStyle Theme
        {
            get
            {
                if (DesignMode || metroTheme != MetroThemeStyle.Default)
                {
                    return metroTheme;
                }

                if (StyleManager != null && metroTheme == MetroThemeStyle.Default)
                {
                    return StyleManager.Theme;
                }
                if (StyleManager == null && metroTheme == MetroThemeStyle.Default)
                {
                    return MetroDefaults.Theme;
                }

                return metroTheme;
            }
            set { metroTheme = value; }
        }

        private MetroStyleManager metroStyleManager = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroStyleManager StyleManager
        {
            get { return metroStyleManager; }
            set { metroStyleManager = value; }
        }

        private bool useCustomBackColor = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set { useCustomBackColor = value; }
        }

        private bool useCustomForeColor = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseCustomForeColor
        {
            get { return useCustomForeColor; }
            set { useCustomForeColor = value; }
        }

        private bool useStyleColors = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool UseStyleColors
        {
            get { return useStyleColors; }
            set { useStyleColors = value; }
        }

        [Browsable(false)]
        [Category(MetroDefaults.PropertyCategory.Behaviour)]
        [DefaultValue(false)]
        public bool UseSelectable
        {
            get { return GetStyle(ControlStyles.Selectable); }
            set { SetStyle(ControlStyles.Selectable, value); }
        }

        [Browsable(true)]
        [Category(MetroDefaults.PropertyCategory.Behaviour)]
        [DefaultValue("")]
        [Description("Gets or sets the text associated with this control.")]
        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; base.Text = textBox.Text;  OnTextChanged(EventArgs.Empty);  }
        }

        #endregion

        #region Fields

        private bool displayFocusRectangle = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool DisplayFocus
        {
            get { return displayFocusRectangle; }
            set { displayFocusRectangle = value; }
        }

        [DefaultValue(DrawMode.OwnerDrawFixed)]
        [Browsable(false)]
        public new DrawMode DrawMode
        {
            get { return DrawMode.OwnerDrawFixed; }
            set { base.DrawMode = DrawMode.OwnerDrawFixed; }
        }

        private ComboBoxStyle dropDownStyle = ComboBoxStyle.DropDownList;
        [DefaultValue(ComboBoxStyle.DropDownList)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [Browsable(true)]
        public new ComboBoxStyle DropDownStyle
        {
            get { return dropDownStyle; }
            set
            {
                // we don't support the Simple style
                if (value == ComboBoxStyle.Simple)
                    value = ComboBoxStyle.DropDownList;
                dropDownStyle = value;
                // fake out the base
                base.DropDownStyle = ComboBoxStyle.DropDownList;
                // if we are a dropdown and have focus, then show the edit box
                if ((value == ComboBoxStyle.DropDown) && (this.Focused))
                    textBox.Visible = true;
                else
                    textBox.Visible = false;
                Invalidate();
            }
        }

        private MetroComboBoxSize metroComboBoxSize = MetroComboBoxSize.Medium;
        [DefaultValue(MetroComboBoxSize.Medium)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public MetroComboBoxSize FontSize
        {
            get { return metroComboBoxSize; }
            set { metroComboBoxSize = value; }
        }

        private MetroComboBoxWeight metroComboBoxWeight = MetroComboBoxWeight.Regular;
        [DefaultValue(MetroComboBoxWeight.Regular)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public MetroComboBoxWeight FontWeight
        {
            get { return metroComboBoxWeight; }
            set { metroComboBoxWeight = value; }
        }

        private string promptText = "";
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("")]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public string PromptText
        {
            get { return promptText; }
            set
            {
                promptText = value.Trim();
                // when we are drop down, use the watermark property of the textbox to show the prompt text
                if (DropDownStyle == ComboBoxStyle.DropDown)
                    textBox.WaterMark = promptText;
                Invalidate();
            }
        }

        private bool drawPrompt = false;

        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        private AutoCompleteMode autoCompleteMode = AutoCompleteMode.None;
        public new AutoCompleteMode AutoCompleteMode
        {
            get { return autoCompleteMode; }
            set
            {
                autoCompleteMode = value;
                textBox.AutoCompleteMode = value;
                if (value != AutoCompleteMode.None)
                {
                    // if using autocomplete, then the source will be the item list.
                    textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    textBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                    foreach (object item in this.Items)
                    {
                        textBox.AutoCompleteCustomSource.Add(item.ToString());
                    }
                }
                else
                {
                    textBox.AutoCompleteSource = AutoCompleteSource.None;
                    textBox.AutoCompleteCustomSource = null;
                }
            }
        }

        [Browsable(false)]
        public new AutoCompleteSource AutoCompleteSource
        {
            get { return base.AutoCompleteSource; } set { base.AutoCompleteSource = value; }
        }

        [Browsable(false)]
        public new AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return base.AutoCompleteCustomSource; } set { base.AutoCompleteCustomSource = value; }
        }

        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;

        #endregion

        #region Constructor

        public MetroComboBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
            DropDownStyle = ComboBoxStyle.DropDownList;

            base.DrawMode = DrawMode.OwnerDrawFixed;

            drawPrompt = (SelectedIndex == -1);

            // setup the textbox
            this.SuspendLayout();
            textBox.Location = new System.Drawing.Point(0, 0);
            textBox.FontSize = (MetroTextBoxSize)metroComboBoxSize;
            textBox.FontWeight = (MetroTextBoxWeight)metroComboBoxWeight;
            textBox.WaterMarkFont = MetroFonts.ComboBox(metroComboBoxSize, metroComboBoxWeight);
            textBox.Size = this.Size;
            textBox.TabIndex = 0;
            textBox.Margin = new Padding(0);
            textBox.Padding = new Padding(0);
            textBox.TextAlign = HorizontalAlignment.Left;
            textBox.Resize += TextBox_Resize;
            textBox.TextChanged +=  TextBox_TextChanged;
            textBox.UseSelectable = true;
            textBox.Enter += TextBox_Enter;
            textBox.Visible = false;


            this.Controls.Add(textBox);
            this.ResumeLayout(true);
            this.AdjustControls();

                        
        }


        #endregion

        #region TextBox Methods

        private void TextBox_Enter(object sender, EventArgs e)
        {
            // if the list changes and we have autocomplete enabled, then we have to re-read the item list into the autocompletesource
            // we have to rebuild the autocomplete source here because we are running .net 2.0 and 
            // cannot use and observable on Items to know when it has changed
            if (this.autoCompleteMode != AutoCompleteMode.None)
            {
                textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
               
                for (int i = 0; i < Items.Count; i++)
                    textBox.AutoCompleteCustomSource.Add(GetItemText(Items[i]));
            }
        }

        private void TextBox_Resize(object sender, EventArgs e)
        {
            AdjustControls();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.promptText = textBox.Text;
            OnTextChanged(e);
        }
        #endregion

        #region Paint Methods

        protected override void OnSizeChanged(EventArgs e)
        {
            AdjustControls();
            base.OnSizeChanged(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (!useCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Form(Theme);
                }

                if (backColor.A == 255 && BackgroundImage == null)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

                OnCustomPaintBackground(new MetroPaintEventArgs(backColor, Color.Empty, e.Graphics));
            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnCustomPaint(new MetroPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                

                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            base.ItemHeight = GetPreferredSize(Size.Empty).Height;

            Color borderColor, foreColor;

            if (isHovered && !isPressed && Enabled)
            {
                foreColor = MetroPaint.ForeColor.ComboBox.Hover(Theme);
                borderColor = MetroPaint.BorderColor.ComboBox.Hover(Theme);
            }
            else if (isHovered && isPressed && Enabled)
            {
                foreColor = MetroPaint.ForeColor.ComboBox.Press(Theme);
                borderColor = MetroPaint.BorderColor.ComboBox.Press(Theme);
            }
            else if (!Enabled)
            {
                foreColor = MetroPaint.ForeColor.ComboBox.Disabled(Theme);
                borderColor = MetroPaint.BorderColor.ComboBox.Disabled(Theme);
            }
            else
            {
                foreColor = MetroPaint.ForeColor.ComboBox.Normal(Theme);
                borderColor = MetroPaint.BorderColor.ComboBox.Normal(Theme);
            }

            using (Pen p = new Pen(borderColor))
            {
                Rectangle boxRect = new Rectangle(0, 0, Width - 1, Height - 1);
                e.Graphics.DrawRectangle(p, boxRect);
            }

  
            if (DropDownStyle != ComboBoxStyle.Simple)
            {
                using (SolidBrush b = new SolidBrush(foreColor))
                {
                    e.Graphics.FillPolygon(b, new Point[] { new Point(Width - 20, (Height / 2) - 2), new Point(Width - 9, (Height / 2) - 2), new Point(Width - 15, (Height / 2) + 4) });
                }

                Rectangle textRect = new Rectangle(2, 2, Width - 40, Height - 4);

                if (Enabled)
                    TextRenderer.DrawText(e.Graphics, Text, MetroFonts.ComboBox(metroComboBoxSize, metroComboBoxWeight), textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                else
                    ControlPaint.DrawStringDisabled(e.Graphics, Text, MetroFonts.ComboBox(metroComboBoxSize, metroComboBoxWeight), MetroPaint.ForeColor.ComboBox.Disabled(Theme), textRect, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                OnCustomPaintForeground(new MetroPaintEventArgs(Color.Empty, foreColor, e.Graphics));
                if (displayFocusRectangle && isFocused)
                    ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);

                if (drawPrompt)
                {
                    DrawTextPrompt(e.Graphics);
                }
            }
            


        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Color foreColor = MetroPaint.ForeColor.Link.Normal(Theme);
                Color backColor = BackColor;

                if (!useCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Form(Theme);
                }

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    using (SolidBrush b = new SolidBrush(MetroPaint.GetStyleColor(Style)))
                    {
                        e.Graphics.FillRectangle(b, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }

                    foreColor = MetroPaint.ForeColor.Tile.Normal(Theme);
                }
                else
                {
                    using (SolidBrush b = new SolidBrush(backColor))
                    {
                        e.Graphics.FillRectangle(b, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }
                }

                if (this.DropDownStyle != ComboBoxStyle.DropDown)
                {
                    Rectangle textRect = new Rectangle(0, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
                    TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), MetroFonts.ComboBox(metroComboBoxSize, metroComboBoxWeight), textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    Rectangle textRect = new Rectangle(0, e.Bounds.Top, this.textBox.Width, e.Bounds.Height);
                    TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), MetroFonts.ComboBox(metroComboBoxSize, metroComboBoxWeight), textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
                }
            }
            else
            {
                base.OnDrawItem(e);
            }
        }

        private void DrawTextPrompt()
        {

            using (Graphics graphics = CreateGraphics())
            {
                DrawTextPrompt(graphics);
            }
        }

        private void DrawTextPrompt(Graphics g)
        {
            Color backColor = BackColor;

            if (!useCustomBackColor)
            {
                backColor = MetroPaint.BackColor.Form(Theme);
            }
                Rectangle textRect = new Rectangle(2, 2, Width - 20, Height - 4);
                TextRenderer.DrawText(g, promptText, MetroFonts.ComboBox(metroComboBoxSize, metroComboBoxWeight), textRect, SystemColors.GrayText, backColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        #endregion

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            isHovered = true;
            
            Invalidate();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            
            Invalidate();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isFocused = true;
            isHovered = true;
            Invalidate();

            base.OnEnter(e);

            

        }

        protected override void OnLeave(EventArgs e)
        {
            isFocused = false;
            isHovered = false;
            isPressed = false;
            if (DropDownStyle == ComboBoxStyle.DropDown)
                textBox.Visible = false;
            Invalidate();

            base.OnLeave(e);
        }

        #endregion

        #region Keyboard Methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isHovered = true;
                isPressed = true;
                Invalidate();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            //isHovered = false;
            //isPressed = false;
            Invalidate();

            base.OnKeyUp(e);
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (DropDownStyle == ComboBoxStyle.DropDown)
                    textBox.Visible = true;
                isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //This will check if control got the focus
            //If not thats the only it will remove the focus color
            if (!isFocused)
            {
                isHovered = false;
            }

            Invalidate();

            base.OnMouseLeave(e);
        }

        #endregion

        #region Overridden Methods

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size preferredSize;
            base.GetPreferredSize(proposedSize);

            using (var g = CreateGraphics())
            {
                string measureText = Text.Length > 0 ? Text : "MeasureText";
                proposedSize = new Size(int.MaxValue, int.MaxValue);
                preferredSize = TextRenderer.MeasureText(g, measureText, MetroFonts.ComboBox(metroComboBoxSize, metroComboBoxWeight), proposedSize, TextFormatFlags.Left | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.VerticalCenter);
                preferredSize.Height += 4;
            }

            return preferredSize;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            drawPrompt = (SelectedIndex == -1);
            Invalidate();
        }

        private const int OCM_COMMAND = 0x2111;
        private const int WM_PAINT = 15;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (((m.Msg == WM_PAINT) || (m.Msg == OCM_COMMAND)) && (drawPrompt))
            {
                DrawTextPrompt();
            }
        }

        #endregion

        #region Private Methods
        private void AdjustControls()
        {
            if (DropDownStyle == ComboBoxStyle.DropDownList)
                return;

            this.SuspendLayout();
            textBox.Width = ClientRectangle.Width - 26;
            textBox.Height = ClientRectangle.Height;
            this.ResumeLayout();
            this.Invalidate(false);
        }

       
        #endregion
    }

   
}
