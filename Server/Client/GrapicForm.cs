using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Packets;

namespace Client
{
    public partial class GrapicForm : Form
    {
        Client client;

        Graphics g;
        int x = -1;
        int y = -1;
        bool moving = false;
        Pen pen, pen2;
        int[] penColour = { 0, 0, 0, 255 };

        public GrapicForm(Client tempClient)
        {
            InitializeComponent();
            g = BackgroundPanel.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen2 = new Pen(Color.Black, 5);
            pen2.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            client = tempClient;
        }

        private void BackgroundPanel_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
            BackgroundPanel.Cursor = Cursors.Cross;
        }

        private void BackgroundPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving && x != -1 && y != -1)
            {
                g.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;

                penColour[0] = RedTrackBar.Value;
                penColour[1] = BlueTrackBar.Value;
                penColour[2] = GreenTrackBar.Value;
                penColour[3] = AlphaTrackBar.Value;

                client.TCPSendMessage(new PenPacket(x, y, penColour));
            }
        }

        private void BackgroundPanel_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
            BackgroundPanel.Cursor = Cursors.Default;
        }

        public void UpdateBackgroundPanel(int drawX, int drawY, int[] penColour)
        {
            if (BackgroundPanel.InvokeRequired)
            {
                Invoke(new Action(() => 
                {
                    UpdateBackgroundPanel(drawX, drawY, penColour);
                }));
            }

            else
            {
                UpdateColourShowPanel();

                pen2.Color = System.Drawing.Color.FromArgb(penColour[3], penColour[0], penColour[1], penColour[2]);

                x = drawX;
                y = drawY;
                g.DrawLine(pen2, new Point(x, y), new Point(x, y + 1));
                x = drawX;
                y = drawY;
            }
        }

        private void RedTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateColourShowPanel();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            BackgroundPanel.Invalidate();
        }

        private void BlueTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateColourShowPanel();
        }

        private void GreenTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateColourShowPanel();
        }

        private void AlphaTrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateColourShowPanel();
        }

        private void UpdateColourShowPanel()
        {
            if (ColourShowPanel.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateColourShowPanel();
                }));
            }

            else
            {
                pen.Color = System.Drawing.Color.FromArgb(AlphaTrackBar.Value, RedTrackBar.Value, BlueTrackBar.Value, GreenTrackBar.Value);
                ColourShowPanel.BackColor = System.Drawing.Color.FromArgb(AlphaTrackBar.Value, RedTrackBar.Value, BlueTrackBar.Value, GreenTrackBar.Value);
            }
        }
    }
}
