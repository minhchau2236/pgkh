using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace CssEditor
{
    public partial class CSSEditor : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public CssFont FontFamily
        {
            get
            {
                if (ViewState["FontFamily"] == null)
                    ViewState["FontFamily"] = new CssFont();
                // Session["FontFamily"] = new CssFont { FontFamily = string.Empty, FontSize = string.Empty, FontStyle = string.Empty, FontWeight = string.Empty, FontVariant = string.Empty, LineThrough = string.Empty, Blink = string.Empty, Underline = string.Empty, Overline = string.Empty, None = string.Empty };
                return (CssFont)ViewState["FontFamily"];
            }
            set
            {
                ViewState["FontFamily"] = value;
            }
        }

        public CssBlock Block
        {
            get
            {
                if (ViewState["Block"] == null)
                    ViewState["Block"] = new CssBlock();
                return (CssBlock)ViewState["Block"];
            }
            set
            {
                ViewState["Block"] = value;
            }
        }

        public CssBackground BackgroundCss
        {
            get
            {
                if (ViewState["BackgroundCss"] == null)
                {
                    ViewState["BackgroundCss"] = new CssBackground();
                }
                return (CssBackground)ViewState["BackgroundCss"];
            }
            set
            {
                ViewState["BackgroundCss"] = value;
            }
        }

        public CssBorder BorderCss
        {
            get
            {
                if (ViewState["BorderCss"] == null)
                {
                    ViewState["BorderCss"] = new CssBorder();
                }
                return (CssBorder)ViewState["BorderCss"];
            }
            set
            {
                ViewState["BorderCss"] = value;
            }
        }

        public CssBox BoxCss
        {
            get
            {
                if (ViewState["BoxCss"] == null)
                {
                    ViewState["BoxCss"] = new CssBox();
                }
                return (CssBox)ViewState["BoxCss"];
            }
            set
            {
                ViewState["BoxCss"] = value;
            }
        }

        public CssPosition PositionCss
        {
            get
            {
                if (ViewState["PositionCss"] == null)
                {
                    ViewState["PositionCss"] = new CssPosition();
                }
                return (CssPosition)ViewState["PositionCss"];
            }
            set
            {
                ViewState["PositionCss"] = value;
            }
        }

        public CssLayout Layout
        {
            get
            {
                if (ViewState["Layout"] == null)
                    ViewState["Layout"] = new CssLayout();
                return ViewState["Layout"] as CssLayout;
            }
            set
            {
                ViewState["Layout"] = value;
            }
        }

        public CssList List
        {
            get
            {
                if (ViewState["List"] == null)
                    ViewState["List"] = new CssList();
                return ViewState["List"] as CssList;
            }
            set
            {
                Session["List"] = value;
            }
        }
        public CssTable Table
        {
            get
            {
                if (ViewState["Table"] == null)
                    ViewState["Table"] = new CssTable();
                return ViewState["Table"] as CssTable;
            }
            set
            {
                ViewState["Table"] = value;
            }
        }
        private void SetCssString()
        {
            string css = string.Empty;
            string cssFont = FontFamily.BuildCssString();
            css += (cssFont == string.Empty ? string.Empty : cssFont + ";");

            string cssBlock = Block.BuildCssString();
            css += (cssBlock == string.Empty ? string.Empty : cssBlock + ";");

            string cssBackground = BackgroundCss.BuildCssString();
            css += (cssBackground == string.Empty ? string.Empty : cssBackground + ";");

            string cssBorder = BorderCss.BuildCssString();
            css += (cssBorder == string.Empty ? string.Empty : cssBorder + ";");

            string cssBox = BoxCss.BuildCssString();
            css += (cssBox == string.Empty ? string.Empty : cssBox + ";");

            string cssPosition = PositionCss.BuildCssString();
            css += (cssPosition == string.Empty ? string.Empty : cssPosition + ";");

            string cssLayout = Layout.BuildCssString();
            css += (cssLayout == string.Empty ? string.Empty : cssLayout + ";");

            string cssList = List.BuildCssString();
            css += (cssList == string.Empty ? string.Empty : cssList + ";");

            string cssTable = Table.BuildCssString();
            css += (cssTable == string.Empty ? string.Empty : cssTable + ";");

            if (css.EndsWith(";"))
                css = css.Remove(css.Length - 1);

            txtCss.Text = css;

            Panel2.Style.Value = Panel2.Style.Value + ";" + txtCss.Text;
            //     Panel2.ApplyStyle;
        }

        protected void ddlFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontFamily.FontFamily = ddlFont.SelectedValue;
            SetCssString();
        }

        protected void ddlFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFontSize.SelectedValue != "(value)")
            {
                txtFontsize.Visible = false;
                txtFontsize.Text = string.Empty;
                FontFamily.FontSize = ddlFontSize.SelectedValue;
                SetCssString();
            }
            else
            {
                txtFontsize.Visible = true;
            }
        }

        protected void txtFontsize_TextChanged(object sender, EventArgs e)
        {
            FontFamily.FontSize = txtFontsize.Text;
            SetCssString();
        }

        protected void ddlFontWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontFamily.FontWeight = ddlFontWeight.SelectedValue;
            SetCssString();
        }

        protected void txtFontColor_TextChanged(object sender, EventArgs e)
        {
            FontFamily.FontColor = txtFontColor.Text;
            SetCssString();
        }

        protected void ddlFontStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontFamily.FontStyle = ddlFontStyle.SelectedValue;
            SetCssString();
        }

        protected void ddlFontVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontFamily.FontVariant = ddlFontVariant.SelectedValue;
            SetCssString();
        }

        protected void chkUnderline_CheckedChanged(object sender, EventArgs e)
        {
            FontFamily.SetUnderline(chkUnderline.Checked);
            FontFamily.SetNone(false);
            chkNone.Checked = false;
            SetCssString();
        }
        protected void ddlTextTransform_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontFamily.TextTransform = ddlTextTransform.SelectedValue;
            SetCssString();
        }

        protected void chkOverline_CheckedChanged(object sender, EventArgs e)
        {
            FontFamily.SetOverline(chkOverline.Checked);
            FontFamily.SetNone(false);
            chkNone.Checked = false;
            SetCssString();
        }

        protected void chkLineThrough_CheckedChanged(object sender, EventArgs e)
        {
            FontFamily.SetLineThrough(chkLineThrough.Checked);
            FontFamily.SetNone(false);
            chkNone.Checked = false;
            SetCssString();
        }

        protected void chkBlink_CheckedChanged(object sender, EventArgs e)
        {
            FontFamily.SetBlink(chkBlink.Checked);
            FontFamily.SetNone(false);
            chkNone.Checked = false;
            SetCssString();
        }

        protected void chkNone_CheckedChanged(object sender, EventArgs e)
        {
            FontFamily.SetNone(chkNone.Checked);

            chkUnderline.Checked = false;
            chkOverline.Checked = false;
            chkLineThrough.Checked = false;
            chkBlink.Checked = false;

            FontFamily.SetOverline(false);
            FontFamily.SetUnderline(false);
            FontFamily.SetLineThrough(false);
            FontFamily.SetBlink(false);
            SetCssString();
        }

        public void CreateObjectFromCss()
        {
            Dictionary<string, string> stylesTable = Utility.ParserCss(hdfCssValue.Value);
            FontFamily.SetAttributes(stylesTable);
            BuildFontConfiguration();

            Block.SetAttributes(stylesTable);
            BuildBlockConfiguration();

            BackgroundCss.SetAttributes(stylesTable);
            BuildBackgroundConfiguration();

            BorderCss.SetAttributes(stylesTable);
            BuildBorderConfiguration();

            BoxCss.SetAttributes(stylesTable);
            BuildBoxConfiguration();

            PositionCss.SetAttributes(stylesTable);
            BuildPositionConfiguration();

            Layout.SetAttributes(stylesTable);
            BuildLayoutCssConfiguration();

            List.SetAttributes(stylesTable);
            BuildListConfiguration();

            Table.SetAttributes(stylesTable);
            BuildTableConfiguration();
        }

        public void LoadFontConfiguration()
        {

        }

        /// <summary>
        /// Block
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlLineHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLineHeight.SelectedValue != "(value)")
            {
                txtLineHeight.Visible = false;
                txtLineHeight.Text = string.Empty;
                Block.LineHeight = ddlLineHeight.SelectedValue;
                SetCssString();
            }
            else
            {
                txtLineHeight.Visible = true;
            }
        }

        protected void ddlVerticalAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            Block.VerticalAlign = ddlVerticalAlign.SelectedValue;
            SetCssString();
        }

        protected void ddlTextAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            Block.TextAlign = ddlTextAlign.SelectedValue;
            SetCssString();
        }

        protected void ddlTextIndent_SelectedIndexChanged(object sender, EventArgs e)
        {
            Block.TextIndent = ddlTextIndent.SelectedValue;
            SetCssString();
        }

        protected void ddlWhiteSpace_SelectedIndexChanged(object sender, EventArgs e)
        {
            Block.WhileSpace = ddlWhiteSpace.SelectedValue;
            SetCssString();
        }

        protected void ddlWordSpacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            Block.WordSpacing = ddlWordSpacing.SelectedValue;
            SetCssString();
        }

        protected void ddlLetterSpacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            Block.LetterSpacing = ddlLetterSpacing.SelectedValue;
            SetCssString();
        }

        protected void txtLineHeight_TextChanged(object sender, EventArgs e)
        {
            Block.LineHeight = txtLineHeight.Text;
            SetCssString();
        }

        protected void lstCssStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = lstCssStyle.SelectedIndex;
            SetCssString();
        }

        /// <summary>
        /// Background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtBackgroundColor_TextChanged(object sender, EventArgs e)
        {
            BackgroundCss.BackgroundColor = txtBackgroundColor.Text;
            SetCssString();
        }

        protected void txtBackgroundImage_TextChanged(object sender, EventArgs e)
        {
            BackgroundCss.BackgroundImage = txtBackgroundImage.Text;
            SetCssString();
        }

        protected void ddlBacKgroundRepeat_SelectedIndexChanged(object sender, EventArgs e)
        {
            BackgroundCss.BackgroundRepeat = ddlBackgroundRepeat.SelectedValue;
            SetCssString();
        }

        protected void ddlBackgroundAttachment_SelectedIndexChanged(object sender, EventArgs e)
        {
            BackgroundCss.BackgroundAttachment = ddlBackgroundAttachment.SelectedValue;
            SetCssString();
        }

        protected void ddlBackgroundPositionX_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlBackgroundPositionX.SelectedValue != "(value)")
            {
                txtBackgroundPositionX.Visible = false;
                txtBackgroundPositionX.Text = string.Empty;
                BackgroundCss.BackgroundPositionX = ddlBackgroundPositionX.SelectedValue;

                SetCssString();
            }
            else
            {
                txtBackgroundPositionX.Visible = true;
            }
        }

        protected void ddlBackgroundPositionY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBackgroundPositionY.SelectedValue != "(value)")
            {
                txtBackgroundPositionY.Visible = false;
                txtBackgroundPositionY.Text = string.Empty;
                BackgroundCss.BackgroundPositionY = ddlBackgroundPositionY.SelectedValue;

                SetCssString();
            }
            else
            {
                txtBackgroundPositionY.Visible = true;
            }
        }

        protected void txtBackgroundPositionX_TextChanged(object sender, EventArgs e)
        {
            BackgroundCss.BackgroundPositionX = txtBackgroundPositionX.Text;
            SetCssString();
        }

        protected void txtBackgroundPositionY_TextChanged(object sender, EventArgs e)
        {
            BackgroundCss.BackgroundPositionY = txtBackgroundPositionY.Text;
            SetCssString();
        }

        /// <summary>
        /// Border
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBorderStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            BorderCss.BorderStyleCss = ddlBorderStyle.SelectedValue;
            SetCssString();
        }

        protected void ddlBorderWidth_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlBorderWidth.SelectedValue != "(value)")
            {
                txtBorderWidth.Visible = false;
                txtBorderWidth.Text = string.Empty;
                BorderCss.BorderWidth = ddlBorderWidth.SelectedValue;

                SetCssString();
            }
            else
            {
                txtBorderWidth.Visible = true;
            }
        }

        protected void txtBorderColor_TextChanged(object sender, EventArgs e)
        {
            BorderCss.BorderColor = txtBorderColor.Text;
            SetCssString();
        }

        protected void txtBorderWidth_TextChanged(object sender, EventArgs e)
        {
            BorderCss.BorderWidth = txtBorderWidth.Text;
            SetCssString();
        }

        /// <summary>
        /// Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPadding_TextChanged(object sender, EventArgs e)
        {
            BoxCss.Padding = txtPadding.Text;
            SetCssString();
        }

        protected void txtMargin_TextChanged(object sender, EventArgs e)
        {
            BoxCss.Margin = txtMargin.Text;
            SetCssString();
        }

        /// <summary>
        /// Position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            PositionCss.Postion = ddlPosition.SelectedValue;
            SetCssString();
        }

        protected void txtPositionZIndex_TextChanged(object sender, EventArgs e)
        {
            PositionCss.ZIndex = txtPositionZIndex.Text;
            SetCssString();
        }

        protected void txtPositionWidth_TextChanged(object sender, EventArgs e)
        {
            PositionCss.Width = txtPositionWidth.Text;
            SetCssString();
        }

        protected void txtPositionHeight_TextChanged(object sender, EventArgs e)
        {
            PositionCss.Height = txtPositionHeight.Text;
            SetCssString();
        }

        protected void txtPositionTop_TextChanged(object sender, EventArgs e)
        {
            PositionCss.Top = txtPositionTop.Text;
            SetCssString();
        }

        protected void txtPositionRight_TextChanged(object sender, EventArgs e)
        {
            PositionCss.Right = txtPositionRight.Text;
            SetCssString();
        }

        protected void txtPositionBottom_TextChanged(object sender, EventArgs e)
        {
            PositionCss.Bottom = txtPositionBottom.Text;
            SetCssString();
        }

        protected void txtPositionLeft_TextChanged(object sender, EventArgs e)
        {
            PositionCss.Left = txtPositionLeft.Text;
            SetCssString();
        }


        ///////////////////Layout
        protected void ddlvisibility_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layout.Visibility = ddlvisibility.SelectedValue;
            SetCssString();
        }

        protected void ddldisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layout.Display = ddldisplay.SelectedValue;
            SetCssString();
        }

        protected void ddlfloat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layout.Float = ddlfloat.SelectedValue;
            SetCssString();
        }

        protected void ddlclear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layout.Clear = ddlclear.SelectedValue;
            SetCssString();
        }

        protected void ddlcursor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layout.Cursor = ddlcursor.SelectedValue;
            SetCssString();
        }

        protected void ddloverflow_SelectedIndexChanged(object sender, EventArgs e)
        {
            Layout.Overflow = ddloverflow.SelectedValue;
            SetCssString();
        }

        protected void ddltop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddltop.SelectedValue != "(value)")
            {
                txtLayoutTop.Visible = false;
                txtLayoutTop.Text = string.Empty;
                Layout.Top = ddltop.SelectedValue;

                SetCssString();
            }
            else
            {
                txtLayoutTop.Visible = true;
            }
        }

        protected void ddlright_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlright.SelectedValue != "(value)")
            {
                txtLayoutRight.Visible = false;
                txtLayoutRight.Text = string.Empty;
                Layout.Right = ddlright.SelectedValue;

                SetCssString();
            }
            else
            {
                txtLayoutRight.Visible = true;
            }
        }

        protected void ddlbottom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbottom.SelectedValue != "(value)")
            {
                txtLayoutBottom.Visible = false;
                txtLayoutBottom.Text = string.Empty;
                Layout.Bottom = ddlbottom.SelectedValue;

                SetCssString();
            }
            else
            {
                txtLayoutBottom.Visible = true;
            }
        }

        protected void ddlleft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlleft.SelectedValue != "(value)")
            {
                txtLayoutLeft.Visible = false;
                txtLayoutLeft.Text = string.Empty;
                Layout.Left = ddlleft.SelectedValue;

                SetCssString();
            }
            else
            {
                txtLayoutLeft.Visible = true;
            }
        }

        protected void ddltablelayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table.TableLayout = ddltablelayout.SelectedValue;
            SetCssString();
        }

        protected void ddlbordercolapse_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table.BorderColapse = ddlbordercolapse.SelectedValue;
            SetCssString();
        }

        protected void ddlborderspacing_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlborderspacing.SelectedValue != "(value)")
            {
                txtTableBorderSpacing.Visible = false;
                txtTableBorderSpacing.Text = string.Empty;
                Table.BorderSpacing = ddlborderspacing.SelectedValue;

                SetCssString();
            }
            else
            {
                txtTableBorderSpacing.Visible = true;
            }
        }

        protected void ddlemptycells_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table.EmptyCells = ddlemptycells.SelectedValue;
            SetCssString();
        }

        protected void ddlcaptionside_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table.CaptionSide = ddlcaptionside.SelectedValue;
            SetCssString();
        }

        protected void ddlliststyletype_SelectedIndexChanged(object sender, EventArgs e)
        {
            List.ListStyleType = ddlliststyletype.SelectedValue;
            SetCssString();
        }

        protected void ddlliststyleposition_SelectedIndexChanged(object sender, EventArgs e)
        {
            List.ListStylePosition = ddlliststyleposition.SelectedValue;
            SetCssString();
        }

        protected void txtliststyleimage_TextChanged(object sender, EventArgs e)
        {
            List.ListStyleImage = txtliststyleimage.Text;
            SetCssString();
        }
        protected void txtLayoutTop_TextChanged(object sender, EventArgs e)
        {
            Layout.Top = txtLayoutTop.Text;
            SetCssString();
        }

        protected void txtLayoutRight_TextChanged(object sender, EventArgs e)
        {
            Layout.Right = txtLayoutRight.Text;
            SetCssString();
        }

        protected void txtLayoutBottom_TextChanged(object sender, EventArgs e)
        {
            Layout.Bottom = txtLayoutBottom.Text;
            SetCssString();
        }

        protected void txtLayoutLeft_TextChanged(object sender, EventArgs e)
        {
            Layout.Left = txtLayoutLeft.Text;
            SetCssString();
        }
        /// <summary>
        /// ////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtTableBorderSpacing_TextChanged(object sender, EventArgs e)
        {

        }

        ////////////////////////////////

        public void BuildFontConfiguration()
        {
            //bo chon cac ddl
            ddlFont.SelectedIndex = -1;
            ddlFontSize.SelectedIndex = -1;
            ddlFontStyle.SelectedIndex = -1;
            ddlFontWeight.SelectedIndex = -1;
            ddlFontVariant.SelectedIndex = -1;

            ddlFont.Items.FindByValue(FontFamily.FontFamily).Selected = true;
            ddlFontStyle.Items.FindByValue(FontFamily.FontStyle).Selected = true;
            ddlFontWeight.Items.FindByValue(FontFamily.FontWeight).Selected = true;
            ddlFontVariant.Items.FindByValue(FontFamily.FontVariant).Selected = true;

            string fontsize = FontFamily.FontSize.ToLower();
            if (Utility.ContainUnit(fontsize))
            {
                ddlFontSize.Items.FindByValue("(value)").Selected = true;
                txtFontsize.Text = fontsize;
                txtFontsize.Visible = true;
            }
            else
            {
                ddlFontSize.Items.FindByValue(FontFamily.FontSize).Selected = true;
            }
            txtFontColor.Text = FontFamily.FontColor;

            chkUnderline.Checked = (FontFamily.Underline != string.Empty);
            chkOverline.Checked = (FontFamily.Overline != string.Empty);
            chkLineThrough.Checked = (FontFamily.LineThrough != string.Empty);
            chkBlink.Checked = (FontFamily.Blink != string.Empty);
            chkNone.Checked = (FontFamily.None != string.Empty);
        }


        public void BuildBlockConfiguration()
        {
            //bo chon cac ddl
            ddlLineHeight.SelectedIndex = -1;
            ddlVerticalAlign.SelectedIndex = -1;
            ddlTextAlign.SelectedIndex = -1;
            ddlTextIndent.SelectedIndex = -1;
            ddlWordSpacing.SelectedIndex = -1;
            ddlLetterSpacing.SelectedIndex = -1;


            ddlVerticalAlign.Items.FindByValue(Block.VerticalAlign).Selected = true;
            ddlTextIndent.Items.FindByValue(Block.TextIndent).Selected = true;
            ddlTextAlign.Items.FindByValue(Block.TextAlign).Selected = true;
            ddlWordSpacing.Items.FindByValue(Block.WordSpacing).Selected = true;
            ddlLetterSpacing.Items.FindByValue(Block.LetterSpacing).Selected = true;

            string lineheight = Block.LineHeight.ToLower();
            if (Utility.ContainUnit(lineheight))
            {
                ddlLineHeight.Items.FindByValue("(value)").Selected = true;
                txtLineHeight.Text = lineheight;
                txtLineHeight.Visible = true;
            }
            else
            {
                ddlLineHeight.Items.FindByValue(Block.LineHeight).Selected = true;
            }

        }

        public void BuildBackgroundConfiguration()
        {

            //set lai text cua cac textbox

            txtBackgroundColor.Text = BackgroundCss.BackgroundColor;
            txtBackgroundImage.Text = BackgroundCss.BackgroundImage;

            // bo chon cac dropdownlist
            ddlBackgroundPositionY.SelectedIndex = -1;
            ddlBackgroundPositionX.SelectedIndex = -1;
            ddlBackgroundRepeat.SelectedIndex = -1;
            ddlBackgroundAttachment.SelectedIndex = -1;

            ddlBackgroundRepeat.Items.FindByValue(BackgroundCss.BackgroundRepeat).Selected = true;
            ddlBackgroundAttachment.Items.FindByValue(BackgroundCss.BackgroundAttachment).Selected = true;

            //BackgroundPositionXDDL.Items.FindByValue(BackgroundPositionX).Selected = true;
            //BackgroundPositionYDDL.Items.FindByValue(BackgroundPositionY).Selected = true;

            string backgroundPositionX = BackgroundCss.BackgroundPositionX.ToLower();
            if (Utility.ContainUnit(backgroundPositionX))
            {
                ddlBackgroundPositionX.Items.FindByValue("(value)").Selected = true;
                txtBackgroundPositionX.Text = backgroundPositionX;
                txtBackgroundPositionX.Visible = true;
            }
            else
            {
                ddlBackgroundPositionX.Items.FindByValue(BackgroundCss.BackgroundPositionX).Selected = true;
            }

            string backgroundPositionY = BackgroundCss.BackgroundPositionY.ToLower();
            if (Utility.ContainUnit(backgroundPositionY))
            {
                ddlBackgroundPositionY.Items.FindByValue("(value)").Selected = true;
                txtBackgroundPositionY.Text = backgroundPositionY;
                txtBackgroundPositionY.Visible = true;
            }
            else
            {
                ddlBackgroundPositionY.Items.FindByValue(BackgroundCss.BackgroundPositionY).Selected = true;
            }
        }

        public void BuildBorderConfiguration()
        {

            //set lai text cua cac textbox

            txtBorderColor.Text = BorderCss.BorderColor;

            // bo chon cac dropdownlist
            ddlBorderWidth.SelectedIndex = -1;
            ddlBorderStyle.SelectedIndex = -1;


            ddlBorderStyle.Items.FindByValue(BorderCss.BorderStyleCss).Selected = true;

            string borderwidth = BorderCss.BorderWidth.ToLower();
            if (Utility.ContainUnit(borderwidth))
            {
                ddlBorderWidth.Items.FindByValue("(value)").Selected = true;
                txtBorderWidth.Text = borderwidth;
                txtBorderWidth.Visible = true;
            }
            else
            {
                ddlBorderWidth.Items.FindByValue(BorderCss.BorderWidth).Selected = true;
            }
        }


        public void BuildBoxConfiguration()
        {
            txtMargin.Text = BoxCss.Margin;
            txtPadding.Text = BoxCss.Padding;
        }

        public void BuildPositionConfiguration()
        {
            ddlPosition.SelectedIndex = -1;

            //set lai text cua cac textbox
            txtPositionZIndex.Text = PositionCss.ZIndex;
            txtPositionWidth.Text = PositionCss.Width;
            txtPositionHeight.Text = PositionCss.Height;
            txtPositionTop.Text = PositionCss.Top;
            txtPositionLeft.Text = PositionCss.Left;
            txtPositionRight.Text = PositionCss.Right;
            txtPositionBottom.Text = PositionCss.Bottom;

            // bo chon cac dropdownlist

            ddlPosition.Items.FindByValue(PositionCss.Postion).Selected = true;
        }

        public void BuildLayoutCssConfiguration()
        {
            //bo chon cac ddl
            ddlvisibility.SelectedIndex = -1;
            ddldisplay.SelectedIndex = -1;
            ddlfloat.SelectedIndex = -1;
            ddlclear.SelectedIndex = -1;
            ddlcursor.SelectedIndex = -1;
            ddloverflow.SelectedIndex = -1;
            ddltop.SelectedIndex = -1;
            ddlright.SelectedIndex = -1;
            ddlbottom.SelectedIndex = -1;
            ddlleft.SelectedIndex = -1;

            ddlvisibility.Items.FindByValue(Layout.Visibility).Selected = true;
            ddldisplay.Items.FindByValue(Layout.Display).Selected = true;
            ddlfloat.Items.FindByValue(Layout.Float).Selected = true;
            ddlclear.Items.FindByValue(Layout.Clear).Selected = true;
            ddlcursor.Items.FindByValue(Layout.Cursor).Selected = true;
            ddloverflow.Items.FindByValue(Layout.Overflow).Selected = true;

            string layouttop = Layout.Top.ToLower();
            if (Utility.ContainUnit(layouttop))
            {
                ddltop.Items.FindByValue("(value)").Selected = true;
                txtLayoutTop.Text = layouttop;
                txtLayoutTop.Visible = true;
            }
            else
            {
                ddltop.Items.FindByValue(Layout.Top).Selected = true;
            }

            string layoutright = Layout.Right.ToLower();
            if (Utility.ContainUnit(layoutright))
            {
                ddlright.Items.FindByValue("(value)").Selected = true;
                txtPositionRight.Text = layoutright;
                txtPositionRight.Visible = true;
            }
            else
            {
                ddlright.Items.FindByValue(Layout.Right).Selected = true;
            }

            string layoutbottom = Layout.Bottom.ToLower();
            if (Utility.ContainUnit(layoutbottom))
            {
                ddlbottom.Items.FindByValue("(value)").Selected = true;
                txtPositionBottom.Text = layoutbottom;
                txtPositionBottom.Visible = true;
            }
            else
            {
                ddlbottom.Items.FindByValue(Layout.Bottom).Selected = true;
            }

            string layoutleft = Layout.Left.ToLower();
            if (Utility.ContainUnit(layoutbottom))
            {
                ddlleft.Items.FindByValue("(value)").Selected = true;
                txtPositionLeft.Text = layoutbottom;
                txtPositionLeft.Visible = true;
            }
            else
            {
                ddlleft.Items.FindByValue(Layout.Left).Selected = true;
            }
        }

        public void BuildListConfiguration()
        {
            //bo chon cac ddl
            ddlliststyletype.SelectedIndex = -1;
            txtliststyleimage.Text = string.Empty;
            ddlliststyleposition.SelectedIndex = -1;

            ddlliststyletype.Items.FindByValue(List.ListStyleType).Selected = true;
            txtliststyleimage.Text = List.ListStyleImage;
            ddlliststyleposition.Items.FindByValue(List.ListStylePosition).Selected = true;
        }

        public void BuildTableConfiguration()
        {
            //bo chon cac ddl
            ddltablelayout.SelectedIndex = -1;
            ddlbordercolapse.SelectedIndex = -1;
            ddlborderspacing.SelectedIndex = -1;
            ddlemptycells.SelectedIndex = -1;
            ddlcaptionside.SelectedIndex = -1;

            ddltablelayout.Items.FindByValue(Table.TableLayout).Selected = true;
            ddlbordercolapse.Items.FindByValue(Table.BorderColapse).Selected = true;
            ddlemptycells.Items.FindByValue(Table.EmptyCells).Selected = true;
            ddlcaptionside.Items.FindByValue(Table.CaptionSide).Selected = true;

            string borderspacing = Table.BorderSpacing.ToLower();
            if (Utility.ContainUnit(borderspacing))
            {
                ddlborderspacing.Items.FindByValue("(value)").Selected = true;
                txtTableBorderSpacing.Text = borderspacing;
                txtTableBorderSpacing.Visible = true;
            }
            else
            {
                ddlborderspacing.Items.FindByValue(Table.BorderSpacing).Selected = true;
            }
        }

        protected void hdfCssValue_ValueChanged(object sender, EventArgs e)
        {
            CreateObjectFromCss();
            SetCssString();
        }
    }
}