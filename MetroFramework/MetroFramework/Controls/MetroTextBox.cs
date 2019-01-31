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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;

namespace MetroFramework.Controls
{
    [Designer("MetroFramework.Design.Controls.MetroTextBoxDesigner, " + AssemblyRef.MetroFrameworkDesignSN)]
    public class MetroTextBox : Control, IMetroControl
    {
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

        #endregion

        #region Fields

        private PromptedTextBox baseTextBox;

        private MetroTextBoxSize metroTextBoxSize = MetroTextBoxSize.Small;
        [DefaultValue(MetroTextBoxSize.Small)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxSize FontSize
        {
            get { return metroTextBoxSize; }
            set { metroTextBoxSize = value; UpdateBaseTextBox(); }
        }

        private MetroTextBoxWeight metroTextBoxWeight = MetroTextBoxWeight.Regular;
        [DefaultValue(MetroTextBoxWeight.Regular)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public MetroTextBoxWeight FontWeight
        {
            get { return metroTextBoxWeight; }
            set { metroTextBoxWeight = value; UpdateBaseTextBox(); }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("")]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        [Obsolete("Use watermark")]
        public string PromptText
        {
            get { return baseTextBox.WaterMark; }
            set { baseTextBox.WaterMark = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue("")]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public string WaterMark
        {
            get { return baseTextBox.WaterMark; }
            set { baseTextBox.WaterMark = value; }
        }

        private Image textBoxIcon = null;
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(null)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public Image Icon
        {
            get { return textBoxIcon; }
            set
            {
                textBoxIcon = value;
                Refresh();
            }
        }

        private bool textBoxIconRight = false;
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool IconRight
        {
            get { return textBoxIconRight; }
            set
            {
                textBoxIconRight = value;
                Refresh();
            }
        }

        private bool displayIcon = false;
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool DisplayIcon
        {
            get { return displayIcon; }
            set
            {
                displayIcon = value;
                Refresh();
            }
        }

        protected Size iconSize
        {
            get
            {
                if (displayIcon && textBoxIcon != null)
                {
                    int _height = textBoxIcon.Height > ClientRectangle.Height ? ClientRectangle.Height : textBoxIcon.Height;

                    Size originalSize = textBoxIcon.Size;
                    double resizeFactor = _height / (double)originalSize.Height;

                    Point iconLocation = new Point(1, 1);
                    return new Size((int)(originalSize.Width * resizeFactor), (int)(originalSize.Height * resizeFactor));
                }

                return new Size(-1, -1);
            }
        }

        private MetroTextButton _button;
        private bool _showbutton = false;

        protected int ButtonWidth
        {
            get
            {
                int _butwidth = 0;
                if (_button != null)
                {
                    _butwidth = (_showbutton) ? _button.Width : 0;
                }

                return _butwidth;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool ShowButton
        {
            get { return _showbutton; }
            set
            {
                _showbutton = value;
                Refresh();
            }
        }

        private MetroLink lnkClear;
        private bool _showclear = false;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public bool ShowClearButton
        {
            get { return _showclear; }
            set
            {
                _showclear = value;
                Refresh();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public MetroTextButton CustomButton
        {
            get { return _button; }
            set
            {
                _button = value;
                Refresh();
            }
        }

        private bool _witherror = false;

        [DefaultValue(false)]
        [Browsable(false)]
        public bool WithError
        {
            get { return _witherror; }
            set
            {
                _witherror = value;
                Invalidate();
            }
        }
        #endregion

        #region Routing Fields

        public override ContextMenu ContextMenu
        {
            get { return baseTextBox.ContextMenu; }
            set
            {
                ContextMenu = value;
                baseTextBox.ContextMenu = value;
            }
        }

        public override ContextMenuStrip ContextMenuStrip
        {
            get { return baseTextBox.ContextMenuStrip; }
            set
            {
                ContextMenuStrip = value;
                baseTextBox.ContextMenuStrip = value;
            }
        }

        [DefaultValue(false)]
        public bool Multiline
        {
            get { return baseTextBox.Multiline; }
            set { baseTextBox.Multiline = value; }
        }

        public override string Text
        {
            get { return baseTextBox.Text; }
            set { baseTextBox.Text = value; }
        }

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public Color WaterMarkColor
        {
            get { return baseTextBox.WaterMarkColor; }
            set { baseTextBox.WaterMarkColor = value; }
        }

        [Category(MetroDefaults.PropertyCategory.Appearance)]
        public Font WaterMarkFont
        {
            get { return baseTextBox.WaterMarkFont; }
            set { baseTextBox.WaterMarkFont = value; }
        }

        public string[] Lines
        {
            get { return baseTextBox.Lines; }
            set { baseTextBox.Lines = value; }
        }

        [Browsable(false)]
        public string SelectedText
        {
            get { return baseTextBox.SelectedText; }
            set { baseTextBox.Text = value; }
        }

        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return baseTextBox.ReadOnly; }
            set
            {
                baseTextBox.ReadOnly = value;
            }
        }

        public char PasswordChar
        {
            get { return baseTextBox.PasswordChar; }
            set { baseTextBox.PasswordChar = value; }
        }

        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get { return baseTextBox.UseSystemPasswordChar; }
            set { baseTextBox.UseSystemPasswordChar = value; }
        }

        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment TextAlign
        {
            get { return baseTextBox.TextAlign; }
            set { baseTextBox.TextAlign = value; }
        }

        public int SelectionStart
        {
            get { return baseTextBox.SelectionStart; }
            set { baseTextBox.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return baseTextBox.SelectionLength; }
            set { baseTextBox.SelectionLength = value; }
        }

        [DefaultValue(true)]
        public new bool TabStop
        {
            get { return baseTextBox.TabStop; }
            set { baseTextBox.TabStop = value; }
        }

        public int MaxLength
        {
            get { return baseTextBox.MaxLength; }
            set { baseTextBox.MaxLength = value; }
        }

        public ScrollBars ScrollBars
        {
            get { return baseTextBox.ScrollBars; }
            set { baseTextBox.ScrollBars = value; }
        }

        [DefaultValue(AutoCompleteMode.None)]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return baseTextBox.AutoCompleteMode; }
            set { baseTextBox.AutoCompleteMode = value; }
        }

        [DefaultValue(AutoCompleteSource.None)]
        public AutoCompleteSource AutoCompleteSource
        {
            get { return baseTextBox.AutoCompleteSource;}
            set { baseTextBox.AutoCompleteSource = value; }
        }

        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return baseTextBox.AutoCompleteCustomSource; }
            set { baseTextBox.AutoCompleteCustomSource = value; }
        }

        public bool ShortcutsEnabled
        {
            get { return baseTextBox.ShortcutsEnabled; }
            set { baseTextBox.ShortcutsEnabled = value; }
        }

        #endregion

        #region Constructor

        public MetroTextBox()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            this.GotFocus += MetroTextBox_GotFocus;
            base.TabStop = false;

            CreateBaseTextBox();
            UpdateBaseTextBox();
            AddEventHandler();
        }

        #endregion

        #region Routing Methods

        public event EventHandler AcceptsTabChanged;
        private void BaseTextBoxAcceptsTabChanged(object sender, EventArgs e)
        {
            if (AcceptsTabChanged != null)
                AcceptsTabChanged(this, e);
        }

        private void BaseTextBoxSizeChanged(object sender, EventArgs e)
        {
            base.OnSizeChanged(e);
        }

        private void BaseTextBoxCursorChanged(object sender, EventArgs e)
        {
            base.OnCursorChanged(e);
        }

        private void BaseTextBoxContextMenuStripChanged(object sender, EventArgs e)
        {
            base.OnContextMenuStripChanged(e);
        }

        private void BaseTextBoxContextMenuChanged(object sender, EventArgs e)
        {
            base.OnContextMenuChanged(e);
        }

        private void BaseTextBoxClientSizeChanged(object sender, EventArgs e)
        {
            base.OnClientSizeChanged(e);
        }

        private void BaseTextBoxClick(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        private void BaseTextBoxChangeUiCues(object sender, UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
        }

        private void BaseTextBoxCausesValidationChanged(object sender, EventArgs e)
        {
            base.OnCausesValidationChanged(e);
        }

        private void BaseTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        private void BaseTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        private void BaseTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        bool _cleared = false;
        bool _withtext = false;

        private void BaseTextBoxTextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);

            if (baseTextBox.Text != "" && !_withtext)
            {
                _withtext = true;
                _cleared = false;
                Invalidate();
            }

            if (baseTextBox.Text == "" && !_cleared)
            {
                _withtext = false;
                _cleared = true;
                Invalidate();
            }
        }

        public void Select(int start, int length)
        {
            baseTextBox.Select(start, length);
        }

        public void SelectAll()
        {
            baseTextBox.SelectAll();
        }

        public void Clear()
        {
            baseTextBox.Clear();
        }

        void MetroTextBox_GotFocus(object sender, EventArgs e)
        {
            baseTextBox.Focus();
        }

        public void AppendText(string text)
        {
            baseTextBox.AppendText(text);
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;
                baseTextBox.BackColor = backColor;

                if (!useCustomBackColor)
                {
                    backColor = MetroPaint.BackColor.Form(Theme);
                    baseTextBox.BackColor = backColor;
                }

                if (backColor.A == 255)
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
            if (useCustomForeColor)
            {
                baseTextBox.ForeColor = ForeColor;
            }
            else
            {
                baseTextBox.ForeColor = MetroPaint.ForeColor.Button.Normal(Theme);
            }

            Color borderColor = MetroPaint.BorderColor.ComboBox.Normal(Theme);

            if (useStyleColors)
                borderColor = MetroPaint.GetStyleColor(Style);

            if (_witherror)
            {
                borderColor = MetroColors.Red;
                if (this.Style == MetroColorStyle.Red) borderColor = MetroColors.Orange;
            }

            using (Pen p = new Pen(borderColor))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width - 2, Height - 1));
            }

            DrawIcon(e.Graphics);
        }

        private void DrawIcon(Graphics g)
        {
            if (displayIcon && textBoxIcon != null)
            {
                Point iconLocation = new Point(5, 5);
                if (textBoxIconRight)
                {
                    iconLocation = new Point(ClientRectangle.Width - iconSize.Width - 1, 1);
                }

                g.DrawImage(textBoxIcon, new Rectangle(iconLocation, iconSize));

                UpdateBaseTextBox();
            }
            else
            {
                _button.Visible = _showbutton;
                if (_showbutton && _button != null) UpdateBaseTextBox();
            }

            OnCustomPaintForeground(new MetroPaintEventArgs(Color.Empty, baseTextBox.ForeColor, g));
        }

        #endregion

        #region Overridden Methods

        public override void Refresh()
        {
            base.Refresh();
            UpdateBaseTextBox();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateBaseTextBox();
        }

        [DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing
        {
            get { return baseTextBox.CharacterCasing; }
            set { baseTextBox.CharacterCasing = value; }
        }
        #endregion

        #region Private Methods

        private void CreateBaseTextBox()
        {
            if (baseTextBox != null) return;

            baseTextBox = new PromptedTextBox();

            baseTextBox.BorderStyle = BorderStyle.None;
            baseTextBox.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);
            baseTextBox.Location = new Point(3, 3);
            baseTextBox.Size = new Size(Width - 6, Height - 6);

            Size = new Size(baseTextBox.Width + 6, baseTextBox.Height + 6);

            baseTextBox.TabStop = true;
            //baseTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;

            Controls.Add(baseTextBox);

            if (_button != null) return;

            _button = new MetroTextButton();
            _button.Theme = Theme;
            _button.Style = Style;
            _button.Location = new Point(3, 1);
            _button.Size = new Size(Height - 4, Height - 4);
            _button.TextChanged += _button_TextChanged;
            _button.MouseEnter += _button_MouseEnter;
            _button.MouseLeave += _button_MouseLeave;
            _button.Click += _button_Click;

            if (!this.Controls.Contains(this._button)) this.Controls.Add(_button);

            if (lnkClear != null) return;

            InitializeComponent();
        }

        public delegate void ButClick(object sender, EventArgs e);
        public event ButClick ButtonClick;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        void _button_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null) ButtonClick(this, e);
        }

        void _button_MouseLeave(object sender, EventArgs e)
        {
            UseStyleColors = baseTextBox.Focused;
            Invalidate();
        }

        void _button_MouseEnter(object sender, EventArgs e)
        {
            UseStyleColors = true;
            Invalidate();
        }

        void _button_TextChanged(object sender, EventArgs e)
        {
            _button.Invalidate();
        }

        private void AddEventHandler()
        {
            baseTextBox.AcceptsTabChanged += BaseTextBoxAcceptsTabChanged;

            baseTextBox.CausesValidationChanged += BaseTextBoxCausesValidationChanged;
            baseTextBox.ChangeUICues += BaseTextBoxChangeUiCues;
            baseTextBox.Click += BaseTextBoxClick;
            baseTextBox.ClientSizeChanged += BaseTextBoxClientSizeChanged;
            baseTextBox.ContextMenuChanged += BaseTextBoxContextMenuChanged;
            baseTextBox.ContextMenuStripChanged += BaseTextBoxContextMenuStripChanged;
            baseTextBox.CursorChanged += BaseTextBoxCursorChanged;

            baseTextBox.KeyDown += BaseTextBoxKeyDown;
            baseTextBox.KeyPress += BaseTextBoxKeyPress;
            baseTextBox.KeyUp += BaseTextBoxKeyUp;

            baseTextBox.SizeChanged += BaseTextBoxSizeChanged;

            baseTextBox.TextChanged += BaseTextBoxTextChanged;
            baseTextBox.GotFocus += baseTextBox_GotFocus;
            baseTextBox.LostFocus += baseTextBox_LostFocus;
        }

        void baseTextBox_LostFocus(object sender, EventArgs e)
        {
            UseStyleColors = false;
            Invalidate();
            this.InvokeLostFocus(this, e);
        }

        void baseTextBox_GotFocus(object sender, EventArgs e)
        {
            _witherror = false;
            UseStyleColors = true;
            Invalidate();
            this.InvokeGotFocus(this, e);
        }

        private void UpdateBaseTextBox()
        {
            if (_button != null)
            {
                if ((Height % 2) > 0)
                {
                    _button.Size = new Size(Height - 2, Height - 2);
                    _button.Location = new Point(this.Width - (_button.Width + 1), 1);
                }
                else
                {
                    _button.Size = new Size(Height - 5, Height - 5);
                    _button.Location = new Point((this.Width - _button.Width) - 3, 2);
                }

                _button.Visible = _showbutton;
            }

            int _clearloc = 0;
            if (lnkClear != null)
            {
                lnkClear.Visible = false;
                if (_showclear && this.Text != "" && !this.ReadOnly && this.Enabled)
                {
                    _clearloc = 16;
                    lnkClear.Location = new Point(this.Width - (ButtonWidth + 17), (this.Height - 14) / 2);
                    lnkClear.Visible = true;
                }
            }


            if (baseTextBox == null) return;

            baseTextBox.Font = MetroFonts.TextBox(metroTextBoxSize, metroTextBoxWeight);

            if (displayIcon)
            {
                Point textBoxLocation = new Point(iconSize.Width + 10, 5);
                if (textBoxIconRight)
                {
                    textBoxLocation = new Point(3, 3);
                }

                baseTextBox.Location = textBoxLocation;
                baseTextBox.Size = new Size(Width - (20 + ButtonWidth + _clearloc) - iconSize.Width, Height - 6);
            }
            else
            {
                baseTextBox.Location = new Point(3, 3);
                baseTextBox.Size = new Size(Width - (6 + ButtonWidth + _clearloc), Height - 6);
            }
        }

        #endregion

        #region PromptedTextBox

        private class PromptedTextBox : TextBox
        {
            private const int OCM_COMMAND = 0x2111;
            private const int WM_PAINT = 15;

            private bool drawPrompt;

            private string promptText = "";
            [Browsable(true)]
            [EditorBrowsable(EditorBrowsableState.Always)]
            [DefaultValue("")]
            public string WaterMark
            {
                get { return promptText; }
                set
                {
                    promptText = value.Trim();
                    Invalidate();
                }
            }

            private Color _waterMarkColor = MetroPaint.ForeColor.Button.Disabled(MetroThemeStyle.Dark);
            public Color WaterMarkColor
            {
                get { return _waterMarkColor; }
                set
                {
                    _waterMarkColor = value; Invalidate();/*thanks to Bernhard Elbl
                                                              for Invalidate()*/
                }
            }

            private Font _waterMarkFont = MetroFramework.MetroFonts.WaterMark(MetroLabelSize.Small, MetroWaterMarkWeight.Italic);
            public Font WaterMarkFont
            {
                get { return _waterMarkFont; }
                set { _waterMarkFont = value; }
            }

            public PromptedTextBox()
            {
                SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
                drawPrompt = (Text.Trim().Length == 0);
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
                TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.EndEllipsis;
                Rectangle clientRectangle = ClientRectangle;

                switch (TextAlign)
                {
                    case HorizontalAlignment.Left:
                        clientRectangle.Offset(1, 0);
                        break;

                    case HorizontalAlignment.Right:
                        flags |= TextFormatFlags.Right;
                        clientRectangle.Offset(-2, 0);
                        break;

                    case HorizontalAlignment.Center:
                        flags |= TextFormatFlags.HorizontalCenter;
                        clientRectangle.Offset(1, 0);
                        break;
                }

                SolidBrush drawBrush = new SolidBrush(WaterMarkColor);

                TextRenderer.DrawText(g, promptText, _waterMarkFont, clientRectangle, _waterMarkColor, BackColor, flags);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (drawPrompt)
                {
                    DrawTextPrompt(e.Graphics);
                }
            }

            protected override void OnCreateControl()
            {
                base.OnCreateControl();
            }

            protected override void OnTextAlignChanged(EventArgs e)
            {
                base.OnTextAlignChanged(e);
                Invalidate();
            }

            protected override void OnTextChanged(EventArgs e)
            {
                base.OnTextChanged(e);
                drawPrompt = (Text.Trim().Length == 0);
                Invalidate();
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);
                if (((m.Msg == WM_PAINT) || (m.Msg == OCM_COMMAND)) && (drawPrompt && !GetStyle(ControlStyles.UserPaint)))
                {
                    DrawTextPrompt();
                }
            }

            protected override void OnLostFocus(EventArgs e)
            {
                base.OnLostFocus(e);
            }
        }

        #endregion

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetroTextBox));
            this.lnkClear = new MetroFramework.Controls.MetroLink();
            this.SuspendLayout();
            // 
            // lnkClear
            // 
            this.lnkClear.FontSize = MetroFramework.MetroLinkSize.Medium;
            this.lnkClear.FontWeight = MetroFramework.MetroLinkWeight.Regular;
            this.lnkClear.Image = ((System.Drawing.Image)(resources.GetObject("lnkClear.Image")));
            this.lnkClear.ImageSize = 10;
            this.lnkClear.Location = new System.Drawing.Point(654, 96);
            this.lnkClear.Name = "lnkClear";
            this.lnkClear.NoFocusImage = ((System.Drawing.Image)(resources.GetObject("lnkClear.NoFocusImage")));
            this.lnkClear.Size = new System.Drawing.Size(12, 12);
            this.lnkClear.TabIndex = 2;
            this.lnkClear.UseSelectable = true;
            this.lnkClear.Click += new EventHandler(lnkClear_Click);
            this.ResumeLayout(false);
            this.Controls.Add(lnkClear);
        }

        public delegate void LUClear();
        public event LUClear ClearClicked;

        void lnkClear_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.Clear();
            baseTextBox.Focus();

            if (ClearClicked != null) ClearClicked();
        }

        #region MetroTextButton
        [ToolboxItem(false)]
        public class MetroTextButton : Button, IMetroControl
        {
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
                set
                {
                    metroTheme = value;
                }
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

            #endregion

            #region Fields

            private bool isHovered = false;
            private bool isPressed = false;

            #endregion

            #region Constructor

            protected override void OnCreateControl()
            {
                base.OnCreateControl();
                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw |
                         ControlStyles.UserPaint, true);
            }

            #endregion

            #region Paint Methods

            protected override void OnPaint(PaintEventArgs e)
            {
                Color backColor, foreColor;

                MetroThemeStyle _Theme = Theme;
                MetroColorStyle _Style = Style;

                if (Parent != null)
                {
                    if (Parent is IMetroForm)
                    {
                        _Theme = ((IMetroForm)Parent).Theme;
                        _Style = ((IMetroForm)Parent).Style;
                        foreColor = MetroPaint.ForeColor.Button.Press(_Theme);
                        backColor = MetroPaint.GetStyleColor(_Style);
                    }
                    else if (Parent is IMetroControl)
                    {
                        _Theme = ((IMetroControl)Parent).Theme;
                        _Style = ((IMetroControl)Parent).Style;
                        foreColor = MetroPaint.ForeColor.Button.Press(_Theme);
                        backColor = MetroPaint.GetStyleColor(_Style);
                    }
                    else
                    {
                        foreColor = MetroPaint.ForeColor.Button.Press(_Theme);
                        backColor = MetroPaint.GetStyleColor(_Style);
                    }
                }
                else
                {
                    foreColor = MetroPaint.ForeColor.Button.Press(_Theme);
                    backColor = MetroPaint.BackColor.Form(_Theme);
                }

                if (isHovered && !isPressed && Enabled)
                {
                    int _r = backColor.R;
                    int _g = backColor.G;
                    int _b = backColor.B;

                    backColor = ControlPaint.Light(backColor, 0.25f);
                }
                else if (isHovered && isPressed && Enabled)
                {
                    foreColor = MetroPaint.ForeColor.Button.Press(_Theme);
                    backColor = MetroPaint.GetStyleColor(_Style);
                }
                else if (!Enabled)
                {
                    foreColor = MetroPaint.ForeColor.Button.Disabled(_Theme);
                    backColor = MetroPaint.BackColor.Button.Disabled(_Theme);
                }
                else
                {
                    foreColor = MetroPaint.ForeColor.Button.Press(_Theme);
                }

                e.Graphics.Clear(backColor);
                Font buttonFont = MetroFonts.Button(MetroButtonSize.Small, MetroButtonWeight.Bold);
                TextRenderer.DrawText(e.Graphics, Text, buttonFont, ClientRectangle, foreColor, backColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                DrawIcon(e.Graphics);
            }

            private Bitmap _image = null;

            public new Image Image
            {
                get { return base.Image; }
                set
                {
                    base.Image = value;
                    if (value == null) return;
                    _image = ApplyInvert(new Bitmap(value));
                }
            }

            public Bitmap ApplyInvert(Bitmap bitmapImage)
            {
                byte A, R, G, B;
                Color pixelColor;

                for (int y = 0; y < bitmapImage.Height; y++)
                {
                    for (int x = 0; x < bitmapImage.Width; x++)
                    {
                        pixelColor = bitmapImage.GetPixel(x, y);
                        A = pixelColor.A;
                        R = (byte)(255 - pixelColor.R);
                        G = (byte)(255 - pixelColor.G);
                        B = (byte)(255 - pixelColor.B);
                        bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                    }
                }

                return bitmapImage;
            }

            protected Size iconSize
            {
                get
                {
                    if (Image != null)
                    {
                        Size originalSize = Image.Size;
                        double resizeFactor = 14 / (double)originalSize.Height;

                        Point iconLocation = new Point(1, 1);
                        return new Size((int)(originalSize.Width * resizeFactor), (int)(originalSize.Height * resizeFactor));
                    }

                    return new Size(-1, -1);
                }
            }

            private void DrawIcon(Graphics g)
            {
                if (Image != null)
                {
                    Point iconLocation = new Point(2, (ClientRectangle.Height - iconSize.Height) / 2);
                    int _filler = 5;

                    switch (ImageAlign)
                    {
                        case ContentAlignment.BottomCenter:
                            iconLocation = new Point((ClientRectangle.Width - iconSize.Width) / 2, (ClientRectangle.Height - iconSize.Height) - _filler);
                            break;
                        case ContentAlignment.BottomLeft:
                            iconLocation = new Point(_filler, (ClientRectangle.Height - iconSize.Height) - _filler);
                            break;
                        case ContentAlignment.BottomRight:
                            iconLocation = new Point((ClientRectangle.Width - iconSize.Width) - _filler, (ClientRectangle.Height - iconSize.Height) - _filler);
                            break;
                        case ContentAlignment.MiddleCenter:
                            iconLocation = new Point((ClientRectangle.Width - iconSize.Width) / 2, (ClientRectangle.Height - iconSize.Height) / 2);
                            break;
                        case ContentAlignment.MiddleLeft:
                            iconLocation = new Point(_filler, (ClientRectangle.Height - iconSize.Height) / 2);
                            break;
                        case ContentAlignment.MiddleRight:
                            iconLocation = new Point((ClientRectangle.Width - iconSize.Width) - _filler, (ClientRectangle.Height - iconSize.Height) / 2);
                            break;
                        case ContentAlignment.TopCenter:
                            iconLocation = new Point((ClientRectangle.Width - iconSize.Width) / 2, _filler);
                            break;
                        case ContentAlignment.TopLeft:
                            iconLocation = new Point(_filler, _filler);
                            break;
                        case ContentAlignment.TopRight:
                            iconLocation = new Point((ClientRectangle.Width - iconSize.Width) - _filler, _filler);
                            break;
                    }

                    g.DrawImage((Theme == MetroThemeStyle.Dark) ? ((isPressed) ? _image : Image) : (isPressed) ? Image : _image, new Rectangle(iconLocation, iconSize));
                }
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
                isHovered = false;
                Invalidate();

                base.OnMouseLeave(e);
            }

            #endregion
        }
        #endregion
    }
}
