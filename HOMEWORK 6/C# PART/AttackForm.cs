namespace lvlRandomWalk
{
    public partial class AttackForm : Form
    {
        public Bitmap bitmap;
        public Graphics grph;

        private Attack attck;

        public Rectangle rect1;
        public float r1zoom = 2.0f;
        public Point r1move = new(0, 0);

        private int edgeprecision = 12;

        public bool running;

        public AttackForm()
        {
            InitializeComponent();

            this.bitmap = new Bitmap(this.picBox.Width, this.picBox.Height);
            this.grph = Graphics.FromImage(this.bitmap);
            this.picBox.Image = this.bitmap;

            this.picBox.MouseWheel += new MouseEventHandler(MouseWheelFunc);

            this.cancBT.Enabled = false;
            this.rect1 = new Rectangle(10, 10, this.picBox.Width - 20, this.picBox.Height - 20);

            this.running = true;
            this.attck = new Attack(this);
        }

        private void startBT_Click(object sender, EventArgs e)
        {
            this.attck.startAttacks((int)this.nValue.Value, (double) this.pValue.Value, (int)this.mSystems.Value, (int) this.sValue.Value);
        }

        private void cancBT_Click(object sender, EventArgs e)
        {
            this.attck.cancelAttack();
        }

        private void closingFunc(object sender, FormClosingEventArgs e)
        {
            this.running = false;
            this.Dispose();
        }

        private void resizePicBox(object sender, EventArgs e)
        {
            this.picBox.Width = this.Width;
            this.picBox.Height = this.Height - this.picBox.Top;
            this.bitmap = new Bitmap(this.bitmap, new Size(this.picBox.Width, this.picBox.Height));
            this.grph = Graphics.FromImage(this.bitmap);
            this.picBox.Image = this.bitmap;
        }

        private bool holding = false;
        private bool sliding = false;
        private bool resizing = false;

        private int edge;
        private Point previous;

        private void mouseDown(object sender, MouseEventArgs e)
        {
            if (!findRectangle(e.Location)) return;

            if (e.Button == MouseButtons.Left)
            {
                if (this.resizing) return;
                this.edge = inEdge(this.rect1, e.Location);

                if (this.edge == 8) this.holding = true;
                else this.resizing = true; // In case I'm pressing down on an edge, I resize the rectangle instead of moving it
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.sliding = true;
                this.r1move.X += e.Location.X - this.previous.X;
                this.r1move.Y += e.Location.Y - this.previous.Y;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                this.r1move.X = 0;
                this.r1move.Y = 0;
                this.r1zoom = 1.0f;
            }

            this.previous = e.Location;
        }

        private void MouseWheelFunc(object sender, MouseEventArgs e)
        {
            if (!findRectangle(e.Location) || this.r1zoom == float.MaxValue) return;

            if (e.Delta == 120) this.r1zoom += 0.1f;
            else if (this.r1zoom >= 1.0) this.r1zoom -= 0.1f;
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (inEdge(this.rect1, e.Location) != 8) Cursor.Current = Cursors.Arrow;

            int moveX = (int)(e.X - previous.X);
            int moveY = (int)(e.Y - previous.Y);

            if (this.sliding)
            {
                this.r1move.X += moveX;
                this.r1move.Y += moveY;
            }
            else if (this.holding)
            {
                this.rect1.X += moveX;
                this.rect1.Y += moveY;
            }
            else if (this.resizing)
            {
                switch (this.edge)
                {
                    case 0:
                        this.rect1.X += moveX; this.rect1.Width -= moveX; this.rect1.Y += moveY; this.rect1.Height -= moveY;
                        break;
                    case 1:
                        this.rect1.Width += moveX; this.rect1.Y += moveY; this.rect1.Height -= moveY;
                        break;
                    case 2:
                        this.rect1.Width += moveX; this.rect1.Height += moveY;
                        break;
                    case 3:
                        this.rect1.X += moveX; this.rect1.Width -= moveX; this.rect1.Height += moveY;
                        break;
                    case 4:
                        this.rect1.Y += moveY; this.rect1.Height -= moveY;
                        break;
                    case 5:
                        this.rect1.Width += moveX;
                        break;
                    case 6:
                        this.rect1.Height += moveY;
                        break;
                    case 7:
                        this.rect1.X += moveX; this.rect1.Width -= moveX;
                        break;
                }
            }

            this.previous = e.Location;
        }

        private void mouseUp(object sender, MouseEventArgs e) { this.holding = false; this.sliding = false; this.resizing = false; }

        private bool findRectangle(Point p)
        {
            if (this.rect1.Contains(p)) return true;
            else return false;
        }

        private byte inEdge(Rectangle rect, Point p)
        {
            // In a corner
            if (Math.Pow(rect.Left - p.X, 2) + Math.Pow(rect.Top - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 0;
            else if (Math.Pow(rect.Right - p.X, 2) + Math.Pow(rect.Top - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 1;
            else if (Math.Pow(rect.Right - p.X, 2) + Math.Pow(rect.Bottom - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 2;
            else if (Math.Pow(rect.Left - p.X, 2) + Math.Pow(rect.Bottom - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 3;

            // In an edge
            if (Math.Pow(rect.Top - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 4;
            else if (Math.Pow(rect.Right - p.X, 2) <= Math.Pow(this.edgeprecision, 2)) return 5;
            else if (Math.Pow(rect.Bottom - p.Y, 2) <= Math.Pow(this.edgeprecision, 2)) return 6;
            else if (Math.Pow(rect.Left - p.X, 2) <= Math.Pow(this.edgeprecision, 2)) return 7;
            return 8;
        }
    }
}