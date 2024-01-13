using System.Reflection;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sin_cos_tan_simulation
{
    public partial class Form1 : Form
    {
        int height = 10, width = 10;
        int currWidth;
        int currHeight;
        int angle = 0;
        SolidBrush _brSin = new SolidBrush(Color.LightBlue);
        SolidBrush _brCos = new SolidBrush(Color.LawnGreen);
        SolidBrush _brTan = new SolidBrush(Color.Orange);

        float sin, cos;

        public Form1()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, panel1, new object[] { true });

            
            textBox1.ForeColor = Color.White;
            textBox2.ForeColor = _brSin.Color;
            textBox3.ForeColor = _brCos.Color;      
        }

        /*private void RegisterEventHandler()
        {
            panel1.SizeChanged += new EventHandler(this.panel1_SizeChanged);

        }

        private void panel1_SizeChanged(object sender, System.EventArgs e)
        {
            panel1.Invalidate();
            
        }*/

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Draw_Simulation(e.Graphics);
            //RegisterEventHandler();
            angle++;
            //label2.Text = $"Angle: {angle}\nSin: {sin}\nCos: {cos}";
            textBox1.Text = $"Angle: {angle}";
            textBox2.Text = $"Sin: {sin}";
            textBox3.Text = $"Cos: {cos}";
            if (angle == 360)
                angle = 0;

            Wait(20);
            panel1.Invalidate();
        }

        void Draw_Simulation(Graphics gr)
        {
            currWidth = panel1.Width;
            currHeight = panel1.Height;

            
            Draw_Grid(gr);

            Draw_Sin(gr);
            Draw_Cos(gr);
            Draw_Circle(gr);
           
            Point location = new Point(currWidth, currHeight);
            location.X = currWidth - 100;
            location.Y = currHeight - 30;

            /*this.label2.Location = location;
            label2.Refresh();
            location.Y -= 30;
            this.label3.Location = location;
            label3.Refresh();
            location.Y -= 30;
            this.label4.Location = location;
            label4.Refresh();*/
            
        }

        float To_Radiant(int degree)
        {
            double radiant = degree * (Math.PI/180);
            return (float)radiant;
        }

        private void Draw_Grid(Graphics gr)
        {
            Point pt1 = new Point();
            pt1.X = currWidth / 2 - 180;
            pt1.Y = currHeight / 2 - 180;
            Pen _pen = new Pen(Color.Black, 2);
            gr.DrawLine(_pen, pt1.X, pt1.Y + 180, pt1.X + 360, pt1.Y + 180);
            gr.DrawLine(_pen, pt1.X + 180, pt1.Y, pt1.X + 180, pt1.Y + 360);
        }

        void Draw_Sin(Graphics gr)
        {
            sin = (float)Math.Round(Math.Sin(To_Radiant(angle)), 3);
            gr.FillRectangle(_brSin, currWidth / 2 -180 * sin - width / 2, currHeight / 2 - height / 2, width, height);
        }

        void Draw_Cos(Graphics gr)
        {
            cos = (float)Math.Round(Math.Cos(To_Radiant(angle)), 3);
            gr.FillRectangle(_brCos, currWidth / 2 - width / 2, currHeight / 2 - 180 * cos - height / 2, width, height);
        }

        void Draw_Circle(Graphics gr)
        {
            float sin = (float)Math.Round(Math.Sin(To_Radiant(angle)), 3);
            float cos = (float)Math.Round(Math.Cos(To_Radiant(angle)), 3);
            gr.FillRectangle(_brTan, currWidth / 2 - 180 * sin - width / 2, currHeight / 2 - 180  * cos - height / 2, width, height);
        }

        public void Wait(int time)
        {
            Thread thread = new Thread(delegate ()
            {
                System.Threading.Thread.Sleep(time);
            });
            thread.Start();
            while (thread.IsAlive) { }
                Application.DoEvents();
        }
    }
}
