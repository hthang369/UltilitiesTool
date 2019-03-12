using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerFormStyle
{
    public class MenuStripZ : MenuStrip
    {
        public MenuStripZ()
        {
            //add NightMenuRenderer class to MenuStrip  
            this.Renderer = new NightMenuRenderer();
        }
    }
    //Renderer class  
    public class NightMenuRenderer : ToolStripRenderer
    {
        //when mouse is on menu item  
        //draw background using rectangle  
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);

            if (e.Item.Enabled)
            {
                if (e.Item.IsOnDropDown == false && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    var rect2 = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, 120, 120)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect2);
                    e.Item.ForeColor = Color.White;
                }

                else if (e.Item.IsOnDropDown && e.Item.Selected)
                {
                    var rect = new Rectangle(2, 0, e.Item.Width - 5, e.Item.Height - 1);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, 120, 120)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect);
                    e.Item.ForeColor = Color.White;
                }
                else
                {
                    e.Item.ForeColor = Color.White;
                }
                if ((e.Item as ToolStripMenuItem).DropDown.Visible && e.Item.IsOnDropDown == false)
                {
                    var rect = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    var rect2 = new Rectangle(2, 1, e.Item.Width - 5, e.Item.Height - 3);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(150, 150, 150)), rect);
                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect2);
                    e.Item.ForeColor = Color.Black;
                }
            }
        }

        //draw Separator  
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            var DarkLine = new SolidBrush(Color.FromArgb(140, 140, 140));
            var rect = new Rectangle(30, 3, e.Item.Width - 32, 1);
            e.Graphics.FillRectangle(DarkLine, rect);
        }

        //draw rectangle for menu item checked  
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);

            if (e.Item.Selected)
            {
                var rect = new Rectangle(4, 2, 18, 18);
                var rect2 = new Rectangle(5, 3, 16, 16);
                SolidBrush b = new SolidBrush(Color.Black);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(230, 230, 230));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
            else
            {
                var rect = new Rectangle(4, 2, 18, 18);
                var rect2 = new Rectangle(5, 3, 16, 16);
                SolidBrush b = new SolidBrush(Color.White);
                SolidBrush b2 = new SolidBrush(Color.FromArgb(255, 240, 250, 250));

                e.Graphics.FillRectangle(b, rect);
                e.Graphics.FillRectangle(b2, rect2);
                e.Graphics.DrawImage(e.Image, new Point(5, 3));
            }
        }

        //draw menu drop down margin  
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);

            var rect = new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), rect);

            var DarkLine = new SolidBrush(Color.FromArgb(70, 70, 70));
            var rect3 = new Rectangle(0, 0, 26, e.AffectedBounds.Height);
            e.Graphics.FillRectangle(DarkLine, rect3);

            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.FromArgb(140, 140, 140))), 28, 0, 28, e.AffectedBounds.Height);

            var rect2 = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), rect2);
        }
    }
}
