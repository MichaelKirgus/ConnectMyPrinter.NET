﻿using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

using MetroFramework.Components;
using MetroFramework.Drawing;
using MetroFramework.Interfaces;
using System.Drawing.Imaging;
using System.Collections;
using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MetroFramework.Controls
{
    public partial class MetroListView : ListView, IMetroControl
    {
        private ListViewColumnSorter lvwColumnSorter;
        private Font stdFont = new Font("Segoe UI", 11f, FontStyle.Regular, GraphicsUnit.Pixel);
        float _offset = 0.2F;

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
            set
            {
                metroStyle = value;
            }
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
            set
            {
                metroStyleManager = value;
            }
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

        #region Scrollbar
        [StructLayout(LayoutKind.Sequential)]
        struct SCROLLINFO
        {
            public uint cbSize;
            public uint fMask;
            public int nMin;
            public int nMax;
            public uint nPage;
            public int nPos;
            public int nTrackPos;
        }

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        private enum ScrollInfoMask
        {
            SIF_RANGE = 0x1,
            SIF_PAGE = 0x2,
            SIF_POS = 0x4,
            SIF_DISABLENOSCROLL = 0x8,
            SIF_TRACKPOS = 0x10,
            SIF_ALL = SIF_RANGE + SIF_PAGE + SIF_POS + SIF_TRACKPOS
        }

        //fnBar values
        private enum SBTYPES
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }
        //lpsi values
        private enum LPCSCROLLINFO
        {
            SIF_RANGE = 0x0001,
            SIF_PAGE = 0x0002,
            SIF_POS = 0x0004,
            SIF_DISABLENOSCROLL = 0x0008,
            SIF_TRACKPOS = 0x0010,
            SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS)
        }

        //ListView item information
        [StructLayoutAttribute(LayoutKind.Sequential)]
        private struct LVITEM
        {
            public uint mask;
            public int iItem;
            public int iSubItem;
            public uint state;
            public uint stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
        }

        public enum ScrollBarCommands
        {
            SB_LINEUP = 0,
            SB_LINELEFT = 0,
            SB_LINEDOWN = 1,
            SB_LINERIGHT = 1,
            SB_PAGEUP = 2,
            SB_PAGELEFT = 2,
            SB_PAGEDOWN = 3,
            SB_PAGERIGHT = 3,
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_TOP = 6,
            SB_LEFT = 6,
            SB_BOTTOM = 7,
            SB_RIGHT = 7,
            SB_ENDSCROLL = 8
        }

        private const UInt32 WM_VSCROLL = 0x0115;
        private const UInt32 WM_NCCALCSIZE = 0x83;

        private const UInt32 LVM_FIRST = 0x1000;
        private const UInt32 LVM_INSERTITEMA = (LVM_FIRST + 7);
        private const UInt32 LVM_INSERTITEMW = (LVM_FIRST + 77);
        private const UInt32 LVM_DELETEITEM = (LVM_FIRST + 8);
        private const UInt32 LVM_DELETEALLITEMS = (LVM_FIRST + 9);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref SCROLLINFO lpsi);

        public delegate void ScrollPositionChangedDelegate(MetroListView listview, int pos);

        public event ScrollPositionChangedDelegate ScrollPositionChanged;
        public event Action<MetroListView> ItemAdded;
        public event Action<MetroListView> ItemsRemoved;

        private int _disableChangeEvents = 0;

        private MetroScrollBar _vScrollbar = new MetroScrollBar();

        private void BeginDisableChangeEvents()
        {
            _disableChangeEvents++;
        }

        private void EndDisableChangeEvents()
        {
            if (_disableChangeEvents > 0)
                _disableChangeEvents--;
        }

        void _vScrollbar_ValueChanged(object sender, int newValue)
        {
            if (_disableChangeEvents > 0)
                return;

            SetScrollPosition(_vScrollbar.Value);
        }

        public void GetScrollPosition(out int min, out int max, out int pos, out int smallchange, out int largechange)
        {
            SCROLLINFO scrollinfo = new SCROLLINFO();
            scrollinfo.cbSize = (uint)Marshal.SizeOf(typeof(SCROLLINFO));
            scrollinfo.fMask = (int)ScrollInfoMask.SIF_ALL;
            if (GetScrollInfo(this.Handle, (int)SBTYPES.SB_VERT, ref scrollinfo))
            {
                min = scrollinfo.nMin;
                max = scrollinfo.nMax;
                pos = scrollinfo.nPos + 1;
                smallchange = 1;
                largechange = (int)scrollinfo.nPage;
            }
            else
            {
                min = 0;
                max = 0;
                pos = 0;
                smallchange = 0;
                largechange = 0;
            }
        }


        public void UpdateScrollbar()
        {
            if (_vScrollbar != null)
            {
                int max, min, pos, smallchange, largechange;
                GetScrollPosition(out min, out max, out pos, out smallchange, out largechange);

                BeginDisableChangeEvents();
                _vScrollbar.Value = pos == 1 ? 0 : pos;
                _vScrollbar.Maximum = max;// -largechange < largechange ? largechange : max - largechange;
                _vScrollbar.Minimum = min;
                _vScrollbar.SmallChange = smallchange;
                _vScrollbar.LargeChange = largechange;
                _vScrollbar.Visible = max > largechange;
                EndDisableChangeEvents();
            }
        }

        public void SetScrollPosition(int pos)
        {
            pos = Math.Min(Items.Count -1, pos);

            if (pos < 0 || pos >= Items.Count)
                return;

            SuspendLayout();
            EnsureVisible(pos);

            if (View == System.Windows.Forms.View.Tile || View == System.Windows.Forms.View.LargeIcon || View == System.Windows.Forms.View.SmallIcon) return;
            for (int i = 0; i < 10; i++)
            {
                if (TopItem != null && TopItem.Index != pos)
                    TopItem = Items[pos];
            }

            ResumeLayout();
        }


        protected void OnItemAdded()
        {
            if (_disableChangeEvents > 0) return;

            UpdateScrollbar();

            if (ItemAdded != null)
                ItemAdded(this);
        }

        protected void OnItemsRemoved()
        {
            if (_disableChangeEvents > 0) return;

            UpdateScrollbar();

            if (ItemsRemoved != null)
                ItemsRemoved(this);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (_vScrollbar != null)
                _vScrollbar.Value -= 3 * Math.Sign(e.Delta);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_VSCROLL)
            {
                int max, min, pos, smallchange, largechange;
                GetScrollPosition(out min, out max, out pos, out smallchange, out largechange);

                if (ScrollPositionChanged != null)
                    ScrollPositionChanged(this, pos);

                if (_vScrollbar != null)
                    _vScrollbar.Value = pos;
            }
            else if (m.Msg == WM_NCCALCSIZE) // WM_NCCALCSIZE
            {
                int style = (int)GetWindowLong(this.Handle, GWL_STYLE);
                if ((style & WS_VSCROLL) == WS_VSCROLL)
                    SetWindowLong(this.Handle, GWL_STYLE, style & ~WS_VSCROLL);
            }

            else if (m.Msg == LVM_INSERTITEMA || m.Msg == LVM_INSERTITEMW)
                OnItemAdded();
            else if (m.Msg == LVM_DELETEITEM || m.Msg == LVM_DELETEALLITEMS)
                OnItemsRemoved();

            base.WndProc(ref m);
        }


        const int GWL_STYLE = -16;
        const int WS_VSCROLL = 0x00200000;


        public static int GetWindowLong(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
                return (int)GetWindowLong32(hWnd, nIndex);
            else
                return (int)(long)GetWindowLongPtr64(hWnd, nIndex);
        }

        public static int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong)
        {
            if (IntPtr.Size == 4)
                return (int)SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            else
                return (int)(long)SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, int dwNewLong);
        #endregion

        public MetroListView()
        {
            this.Font = new Font("Segoe UI", 12.0f);
            this.HideSelection = true;

            this.OwnerDraw = true;
            this.DrawColumnHeader += MetroListView_DrawColumnHeader;
            this.DrawItem += MetroListView_DrawItem;
            this.DrawSubItem += MetroListView_DrawSubItem;
            this.Resize += MetroListView_Resize;
            this.ColumnClick += MetroListView_ColumnClick;
            this.SelectedIndexChanged += MetroListView_SelectedIndexChanged;
            this.FullRowSelect = true;
            this.Controls.Add(_vScrollbar);
            _vScrollbar.Visible = false;
            _vScrollbar.Width = 15;
            _vScrollbar.Dock = DockStyle.Right;
            _vScrollbar.ValueChanged += _vScrollbar_ValueChanged;

            //this.DoubleBuffering(true);
        }

        void MetroListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateScrollbar();
        }


        private bool allowSorting = false;
        [DefaultValue(false)]
        [Category(MetroDefaults.PropertyCategory.Behaviour)]
        public bool AllowSorting
        {
            get
            {
                return allowSorting;
            }
            set
            {
                allowSorting = value;
                if (!value)
                {
                    lvwColumnSorter = null;
                    this.ListViewItemSorter = null;
                }
                else
                {
                    lvwColumnSorter = new ListViewColumnSorter();
                    this.ListViewItemSorter = lvwColumnSorter;
                }
            }
        }

        void MetroListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lvwColumnSorter == null) return;
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.Sort();
        }

        void MetroListView_Resize(object sender, EventArgs e)
        {
            if (this.Columns.Count <= 0) return;
        }

        [Description("Set the font of the button caption")]
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

        void MetroListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Color itemForeColor = MetroPaint.ForeColor.Button.Disabled(Theme);
            if (this.View == View.Details)
            {
             
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(ControlPaint.Light(MetroPaint.GetStyleColor(Style), _offset)), e.Bounds);
                    itemForeColor = Color.White;
                }

                TextFormatFlags align = TextFormatFlags.Left;

                int _ded = 0, _left = 0;
                if (this.CheckBoxes && e.ColumnIndex == 0)
                {
                    _ded = 12; _left = 14;
                    int _top = (e.Bounds.Height / 2) - 6;
                    using (Pen p = new Pen(itemForeColor))
                    {
                        Rectangle boxRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + _top, 12, 12);
                        e.Graphics.DrawRectangle(p, boxRect);
                    }

                    if (e.Item.Checked)
                    {
                        Color fillColor = MetroPaint.GetStyleColor(Style);
                        if (e.Item.Selected) fillColor = Color.White;

                        using (SolidBrush b = new SolidBrush(fillColor))
                        {
                            _top = (e.Bounds.Height / 2) - 4;
                            Rectangle boxRect = new Rectangle(e.Bounds.X + 4, e.Bounds.Y + _top, 9, 9);
                            e.Graphics.FillRectangle(b, boxRect);
                        }
                    }
                }

                if (this.SmallImageList != null)
                {
                    int _top = 0;
                    Image _img = null;
                    if (e.Item.ImageIndex > -1) _img = this.SmallImageList.Images[e.Item.ImageIndex];
                    if (e.Item.ImageKey != "") _img = this.SmallImageList.Images[e.Item.ImageKey];
                    if (_img != null)
                    {
                        _left += _left > 0 ? 4 : 2;
                        _top = (e.Item.Bounds.Height - _img.Height) / 2;
                        e.Graphics.DrawImage(_img, new Rectangle(e.Item.Bounds.Left + _left, e.Item.Bounds.Top + _top, _img.Width, _img.Height));

                        _left += this.SmallImageList.ImageSize.Width;
                        _ded += this.SmallImageList.ImageSize.Width;
                    }
                }

                int _colWidth = e.Item.Bounds.Width;
                if (this.View == View.Details) _colWidth = this.Columns[e.ColumnIndex].Width;

                using (StringFormat sf = new StringFormat())
                {
                    TextFormatFlags flags = TextFormatFlags.Left;

                    switch (e.Header.TextAlign)
                    {
                        case HorizontalAlignment.Center:
                            sf.Alignment = StringAlignment.Center;
                            flags = TextFormatFlags.HorizontalCenter;
                            break;
                        case HorizontalAlignment.Right:
                            sf.Alignment = StringAlignment.Far;
                            flags = TextFormatFlags.Right;
                            break;
                        default:
                            sf.Alignment = StringAlignment.Near;
                            flags = TextFormatFlags.Left;
                            break;
                    }

                    double subItemValue;
                    if (e.ColumnIndex > 0 && Double.TryParse(e.SubItem.Text, NumberStyles.Currency, NumberFormatInfo.CurrentInfo, out subItemValue))
                    {
                        sf.Alignment = StringAlignment.Far;
                        flags = TextFormatFlags.Right;
                    }


                    //TextFormatFlags align = TextFormatFlags.Left;
                    Rectangle rect = new Rectangle(e.Bounds.X + _left, e.Bounds.Y, _colWidth - _ded, e.Item.Bounds.Height);
                    TextRenderer.DrawText(e.Graphics, e.SubItem.Text, stdFont, rect, itemForeColor, align | TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);
                }
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        void MetroListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            Color itemForeColor = MetroPaint.ForeColor.Button.Disabled(Theme);
            if (this.View == View.Details | this.View == View.List | this.View == View.SmallIcon)
            {
                Color fillColor = MetroPaint.GetStyleColor(Style);

                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(ControlPaint.Light(MetroPaint.GetStyleColor(Style), _offset)), e.Bounds);
                    itemForeColor = Color.White;
                    fillColor = Color.White;
                }

                TextFormatFlags align = TextFormatFlags.Left;

                int _ded = 0, _left = 0;
                if (this.CheckBoxes)
                {
                    _ded = 12; _left = 14;
                    int _top = (e.Bounds.Height / 2) - 6;
                    using (Pen p = new Pen(itemForeColor))
                    {
                        Rectangle boxRect = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + _top, 12, 12);
                        e.Graphics.DrawRectangle(p, boxRect);
                    }

                    if (e.Item.Checked)
                    {
                        using (SolidBrush b = new SolidBrush(fillColor))
                        {
                            _top = (e.Bounds.Height / 2) - 4;
                            Rectangle boxRect = new Rectangle(e.Bounds.X + 4, e.Bounds.Y + _top, 9, 9);
                            e.Graphics.FillRectangle(b, boxRect);
                        }
                    }
                }

                if (this.SmallImageList != null)
                {
                    int _top = 0;
                    Image _img = null;
                    if (e.Item.ImageIndex > -1) _img = this.SmallImageList.Images[e.Item.ImageIndex];
                    if (e.Item.ImageKey != "") _img = this.SmallImageList.Images[e.Item.ImageKey];
                    if (_img != null)
                    {
                        _left += _left > 0 ? 4 : 2;
                        _top = (e.Item.Bounds.Height - _img.Height) / 2;
                        e.Graphics.DrawImage(_img, new Rectangle(e.Item.Bounds.Left + _left, e.Item.Bounds.Top + _top, _img.Width, _img.Height));

                        _left += this.SmallImageList.ImageSize.Width;
                        _ded += this.SmallImageList.ImageSize.Width;
                    }
                }

                if (this.View == View.Details) return;
                int _colWidth = e.Item.Bounds.Width;
                if (this.View == View.Details) _colWidth = this.Columns[0].Width;

                Rectangle rect = new Rectangle(e.Bounds.X + _left, e.Bounds.Y, _colWidth - _ded, e.Item.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, e.Item.Text, stdFont, rect, itemForeColor, align | TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);
            }

            else if (this.View == View.Tile)
            {
                int _left = 0;

                if (this.LargeImageList != null)
                {
                    int _top = 0;
                    _left = this.LargeImageList.ImageSize.Width + 2;

                    Image _img = null;
                    if (e.Item.ImageIndex > -1) _img = this.LargeImageList.Images[e.Item.ImageIndex];
                    if (e.Item.ImageKey != "") _img = this.LargeImageList.Images[e.Item.ImageKey];
                    if (_img != null)
                    {
                        _top = (e.Item.Bounds.Height - _img.Height) / 2;
                        e.Graphics.DrawImage(_img, new Rectangle(e.Item.Bounds.Left + _left, e.Item.Bounds.Top + _top, _img.Width, _img.Height));
                    }
                }

                if (e.Item.Selected)
                {
                    Rectangle rect = new Rectangle(e.Item.Bounds.X + _left, e.Item.Bounds.Y, e.Item.Bounds.Width, e.Item.Bounds.Height);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(250, 194, 87)), rect);
                }

                int _fill = 0;
                foreach (ListViewItem.ListViewSubItem item in e.Item.SubItems)
                {
                    if (_fill > 0 && !e.Item.Selected) itemForeColor = Color.Silver;
                    int _y = (e.Item.Bounds.Y + _fill) + ((e.Item.Bounds.Height - ((e.Item.SubItems.Count) * 15)) / 2);

                    Rectangle rect = new Rectangle(e.Item.Bounds.X + _left, e.Item.Bounds.Y + _fill, e.Item.Bounds.Width, e.Item.Bounds.Height);

                    TextFormatFlags align = TextFormatFlags.Left;
                    TextRenderer.DrawText(e.Graphics, item.Text, new Font("Segoe UI", 9.0f), rect, itemForeColor, align | TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.WordEllipsis);
                    _fill += 15;
                }
            }
            else
            {
                if (this.CheckBoxes)
                {
                    int _top = (e.Bounds.Height / 2) - 6;
                    using (Pen p = new Pen(Color.Black))
                    {
                        Rectangle boxRect = new Rectangle(e.Bounds.X + 6, e.Bounds.Y + _top, 12, 12);
                        e.Graphics.DrawRectangle(p, boxRect);
                    }

                    if (e.Item.Checked)
                    {
                        Color fillColor = MetroPaint.GetStyleColor(Style);
                        if (e.Item.Selected) fillColor = Color.White;
                        using (SolidBrush b = new SolidBrush(fillColor))
                        {
                            _top = (e.Bounds.Height / 2) - 4;

                            Rectangle boxRect = new Rectangle(e.Bounds.X + 8, e.Bounds.Y + _top, 9, 9);
                            e.Graphics.FillRectangle(b, boxRect);
                        }
                    }

                    Rectangle rect = new Rectangle(e.Bounds.X + 23, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height);

                    e.Graphics.DrawString(e.Item.Text, stdFont, new SolidBrush(itemForeColor), rect);
                }

                this.Font = stdFont;
                e.DrawDefault = true;
            }
        }

        void MetroListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            Color _headColor = MetroPaint.ForeColor.Button.Press(Theme);
            e.Graphics.FillRectangle(new SolidBrush(MetroPaint.GetStyleColor(Style)), e.Bounds);

            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(e.Header.Text, stdFont, new SolidBrush(_headColor), e.Bounds, sf);
            }
        }
    }
}

//namespace System.Runtime.CompilerServices
//{
//    public class ExtensionAttribute : Attribute { }
//}

//public static class ControlExtensions
//{
//    public static void DoubleBuffering(this Control control, bool enable)
//    {
//        var method = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
//        method.Invoke(control, new object[] { ControlStyles.OptimizedDoubleBuffer, enable });
//    }
//}

public class ListViewColumnSorter : IComparer
{
    public enum SortModifiers
    {
        SortByImage,
        SortByCheckbox,
        SortByText
    }

    /// <summary>
    /// Specifies the column to be sorted
    /// </summary>
    public int ColumnToSort;

    /// <summary>
    /// Specifies the order in which to sort (i.e. 'Ascending').
    /// </summary>
    public SortOrder OrderOfSort;

    /// <summary>
    /// Case insensitive comparer object
    /// </summary>
    private CaseInsensitiveComparer ObjectCompare;

    private SortModifiers mySortModifier = SortModifiers.SortByText;
    public SortModifiers _SortModifier
    {
        set
        {
            mySortModifier = value;
        }
        get
        {
            return mySortModifier;
        }
    }

    /// <summary>
    /// Class constructor.  Initializes various elements
    /// </summary>
    public ListViewColumnSorter()
    {
        // Initialize the column to '0'
        ColumnToSort = 0;

        // Initialize the CaseInsensitiveComparer object
        ObjectCompare = new CaseInsensitiveComparer();
    }

    /// <summary>
    /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
    /// </summary>
    /// <param name="x">First object to be compared</param>
    /// <param name="y">Second object to be compared</param>
    /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
    public int Compare(object x, object y)
    {
        int compareResult = 0;
        ListViewItem listviewX, listviewY;

        // Cast the objects to be compared to ListViewItem objects
        listviewX = (ListViewItem)x;
        listviewY = (ListViewItem)y;

        DateTime dateX;
        DateTime dateY;

        if (DateTime.TryParse(listviewX.SubItems[ColumnToSort].Text, out dateX) &&
            DateTime.TryParse(listviewY.SubItems[ColumnToSort].Text, out dateY))
        {
            compareResult = ObjectCompare.Compare(dateX, dateY);
        }
        else
        {
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
        }

        // Calculate correct return value based on object comparison
        if (OrderOfSort == SortOrder.Ascending)
        {
            // Ascending sort is selected, return normal result of compare operation
            return compareResult;
        }
        else if (OrderOfSort == SortOrder.Descending)
        {
            // Descending sort is selected, return negative result of compare operation
            return (-compareResult);
        }
        else
        {
            // Return '0' to indicate they are equal
            return 0;
        }
    }

    /// <summary>
    /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
    /// </summary>
    public int SortColumn
    {
        set
        {
            ColumnToSort = value;
        }
        get
        {
            return ColumnToSort;
        }
    }

    /// <summary>
    /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
    /// </summary>
    public SortOrder Order
    {
        set
        {
            OrderOfSort = value;
        }
        get
        {
            return OrderOfSort;
        }
    }
}
