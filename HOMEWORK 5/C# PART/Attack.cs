using System.Diagnostics;

namespace hom5
{
    internal class Attack
    {
        private Random rand;
        private AttackForm atkForm;
        private Stopwatch sw;

        // Pens to draw all the machines
        private Pen[] pens = {
                new Pen(Color.Olive, 3),
                new Pen(Color.Red, 3),
                new Pen(Color.Blue, 3),
                new Pen(Color.Green, 3),
                new Pen(Color.Orange, 3),
                new Pen(Color.Aqua, 3),
                new Pen(Color.Coral, 3),
                new Pen(Color.Purple, 3),
                new Pen(Color.Salmon, 3),
                new Pen(Color.SandyBrown, 3)
        };

        private Pen axis_pen;
        private Pen company_pen;
        private Pen rect1_pen;
        private Brush histo_pen;

        public Thread thread;
        private int nAttacks;
        public int picking;
        

        public int mSystems;
        private double lambdaAttack;
        public int aHistogram;

        private List<List<Point>> zero_points;
        private List<int> zero_points_Y = new List<int>();

        private Rectangle zero_histo;
        private Rectangle zero_histo_end;
        private Dictionary<int, int> countZero_points;
        private Dictionary<int, int> countZero_points_end;

        private int barHeight = 10;
        private int scaling = 5;

        public Attack(AttackForm atkForm)
        {
            this.atkForm = atkForm;
            this.rand = new Random();
            this.sw = new Stopwatch();

            this.histo_pen = new SolidBrush(Color.FromArgb(150, Color.Maroon));
            this.axis_pen = new Pen(Color.DarkGray, 2);
            this.company_pen = new Pen(Color.LightGray, 1);
            this.rect1_pen = new Pen(Color.LightBlue, 4);
            this.zero_points = new List<List<Point>>();

            this.countZero_points = new Dictionary<int, int>();
            this.countZero_points_end = new Dictionary<int, int>();
            this.zero_histo = new Rectangle(0, 0, 0, this.barHeight);
            this.zero_histo_end = new Rectangle(0, 0, 0, this.barHeight);

            this.picking = -1;
            this.nAttacks = 1;
            this.lambdaAttack = 0;
            this.mSystems = 1;
            this.aHistogram = 1;

            this.thread = new Thread(new ThreadStart(StartThreadedAttacks));

            // Starting thread
            this.thread.Start();
        }

        public void startAttacks(int n, double lambda, int m, int a)
        {
            this.zero_points.Clear();
            this.zero_points_Y.Clear();
            this.countZero_points.Clear();
            this.countZero_points_end.Clear();

            for (int i = 0; i < m; i++)
            {
                this.zero_points.Add(new List<Point>());
                this.zero_points[i].Add(new Point(0, 0));
                this.zero_points_Y.Add(0);
            }

            this.lambdaAttack = lambda;
            this.nAttacks = n;
            this.mSystems = m;
            this.picking = 0;
            this.aHistogram = a;
        }

        public void cancelAttack()
        {
            if (this.picking >= this.aHistogram) this.picking = -3;
            else this.picking = -4;

            // Reset UI to default values (Thread  safe)
            this.atkForm.BeginInvoke((Action)delegate ()
            {
                this.atkForm.cancBT.Enabled = false;
                this.atkForm.startBT.Enabled = true;
                this.atkForm.nValue.Enabled = true;
                this.atkForm.lambdaValue.Enabled = true;
            });

        }

        private void StartThreadedAttacks()
        {
            int posX = 0; int posY = 0, index = 0, max = 0;
            int x = 0, y = 0, newZero_Y = 0;

            double randNumber;
            Point newZero;

            while (this.atkForm.running)
            {
                sw.Start();

                if (this.picking > -1 && this.picking < this.nAttacks)
                {
                    this.atkForm.BeginInvoke((Action)delegate ()
                    {
                        this.atkForm.nValue.Enabled = false;
                        this.atkForm.lambdaValue.Enabled = false;
                        this.atkForm.startBT.Enabled = false;
                        this.atkForm.cancBT.Enabled = true;
                    });

                    if (this.picking == this.aHistogram)
                    {
                        max = 0;
                        for (int i = 0; i < this.mSystems; i++) if (this.zero_points_Y[i] > max) max = this.zero_points_Y[i];
                        for (int i = 0; i <= max; i++) countZero_points[i] = 0;
                        for (int i = 0; i < this.mSystems; i++) countZero_points[this.zero_points_Y[i]] += 1;
                    }

                    for (int i = 0; i < this.mSystems; i++)
                    {
                        randNumber = rand.NextDouble();
                        newZero = this.zero_points[i].Last();
                        newZero_Y = this.zero_points_Y[i];

                        newZero.X++;
                        newZero.Y += (randNumber <= this.lambdaAttack/(this.nAttacks + 1) ? -1 : 0);
                        newZero_Y += (randNumber <= this.lambdaAttack / (this.nAttacks + 1) ? 1 : 0);

                        this.zero_points[i].Add(newZero);
                        this.zero_points_Y[i] = newZero_Y;
                    }

                    this.picking += 1;
                }

                if(this.picking == this.nAttacks)
                {
                    max = 0;
                    for (int i = 0; i < this.mSystems; i++) if (this.zero_points_Y[i] > max) max = this.zero_points_Y[i];
                    for (int i = 0; i <= max; i++) countZero_points_end[i] = 0;
                    for (int i = 0; i < this.mSystems; i++) countZero_points_end[this.zero_points_Y[i]] += 1;

                    this.picking = -2;
                }

                if (this.picking == -2)
                {
                    // Reset UI to default values (Thread  safe)
                    this.atkForm.BeginInvoke((Action)delegate ()
                    {
                        this.atkForm.cancBT.Enabled = false;
                        this.atkForm.startBT.Enabled = true;
                        this.atkForm.nValue.Enabled = true;
                        this.atkForm.lambdaValue.Enabled = true;
                    });
                }

                // Drawing all the things
                this.atkForm.grph.Clear(Color.Transparent);
                try
                {
                    this.atkForm.Invoke(new MethodInvoker(delegate ()
                    {
                        // Draw rectangle
                        this.atkForm.grph.DrawRectangle(this.rect1_pen, this.atkForm.rect1);

                        // Draw axis in rectangle
                        // - Horizontal
                        posY = this.atkForm.rect1.Y + (int)this.atkForm.rect1.Height / 2 + this.atkForm.r1move.Y;
                        if (!(posY < this.atkForm.rect1.Top || posY > this.atkForm.rect1.Bottom)) this.atkForm.grph.DrawLine(this.axis_pen, this.atkForm.rect1.Left, posY, this.atkForm.rect1.Right, posY);

                        // smaller lines
                        index = 0;
                        while (posY - index * this.atkForm.r1zoom > this.atkForm.rect1.Top)
                        {
                            if (posY - index * this.atkForm.r1zoom < this.atkForm.rect1.Bottom) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect1.Left, posY - index * this.atkForm.r1zoom, this.atkForm.rect1.Right, posY - index * this.atkForm.r1zoom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posY + index * this.atkForm.r1zoom < this.atkForm.rect1.Bottom)
                        {
                            if (posY + index * this.atkForm.r1zoom > this.atkForm.rect1.Top) this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect1.Left, posY + index * this.atkForm.r1zoom, this.atkForm.rect1.Right, posY + index * this.atkForm.r1zoom);
                            index += this.scaling;
                        }

                        // - Vertical
                        posX = this.atkForm.rect1.X + this.atkForm.r1move.X + (int)this.atkForm.rect1.Width / 2;
                        if (!(posX < this.atkForm.rect1.Left || posX > this.atkForm.rect1.Right)) this.atkForm.grph.DrawLine(this.axis_pen, this.atkForm.rect1.Left + this.atkForm.r1move.X + (int)this.atkForm.rect1.Width / 2, this.atkForm.rect1.Top, this.atkForm.rect1.Left + this.atkForm.r1move.X + (int)this.atkForm.rect1.Width / 2, this.atkForm.rect1.Bottom);

                        // smaller lines
                        index = 0;
                        while (posX - index * this.atkForm.r1zoom > this.atkForm.rect1.Left)
                        {
                            if (posX - index * this.atkForm.r1zoom < this.atkForm.rect1.Right) this.atkForm.grph.DrawLine(this.company_pen, posX - index * this.atkForm.r1zoom, this.atkForm.rect1.Top, posX - index * this.atkForm.r1zoom, this.atkForm.rect1.Bottom);
                            index += this.scaling;
                        }
                        index = 0;
                        while (posX + index * this.atkForm.r1zoom < this.atkForm.rect1.Right)
                        {
                            if (posX + index * this.atkForm.r1zoom > this.atkForm.rect1.Left) this.atkForm.grph.DrawLine(this.company_pen, posX + index * this.atkForm.r1zoom, this.atkForm.rect1.Top, posX + index * this.atkForm.r1zoom, this.atkForm.rect1.Bottom);
                            index += this.scaling;
                        }

                        // Drawing graphs in the rectangle
                        for (int i = 0; i < this.zero_points.Count() && this.atkForm.running; i++)
                        {
                            for (int j = 0; j < this.zero_points[i].Count() - 1 && this.atkForm.running; j++)
                            {
                                Point a = this.zero_points[i].ElementAt(j);
                                Point b = this.zero_points[i].ElementAt(j + 1);

                                // Applying zoom effect
                                a.X = (int)(a.X * this.atkForm.r1zoom * this.scaling);
                                a.Y = (int)(a.Y * this.atkForm.r1zoom * this.scaling);
                                b.X = (int)(b.X * this.atkForm.r1zoom * this.scaling);
                                b.Y = (int)(b.Y * this.atkForm.r1zoom * this.scaling);

                                // Moving points into rectangle
                                a.X += this.atkForm.rect1.Left; a.Y += this.atkForm.rect1.Top;
                                b.X += this.atkForm.rect1.Left; b.Y += this.atkForm.rect1.Top;

                                // Starting from the middle of the rectangle
                                a.X += (int)this.atkForm.rect1.Width / 2;
                                a.Y += (int)this.atkForm.rect1.Height / 2;
                                b.X += (int)this.atkForm.rect1.Width / 2;
                                b.Y += (int)this.atkForm.rect1.Height / 2;

                                // Moving based on the mov items
                                a.X += this.atkForm.r1move.X;
                                a.Y += this.atkForm.r1move.Y;
                                b.X += this.atkForm.r1move.X;
                                b.Y += this.atkForm.r1move.Y;

                                // In case it's out of the rectangle, I can skip the draw
                                if (b.X > this.atkForm.rect1.Right || b.X < this.atkForm.rect1.Left || b.Y > this.atkForm.rect1.Bottom || b.Y < this.atkForm.rect1.Top) continue;
                                if (a.X > this.atkForm.rect1.Right || a.X < this.atkForm.rect1.Left || a.Y > this.atkForm.rect1.Bottom || a.Y < this.atkForm.rect1.Top) continue;

                                this.atkForm.grph.DrawLine(this.pens[i % this.pens.Count()], a, b);
                            }
                        }

                        if (this.picking == -2 || this.picking == -3 || this.picking > this.aHistogram)
                        {
                            foreach (var item in countZero_points)
                            {
                                if (item.Value == 0) continue;

                                x = this.aHistogram;
                                y = -(int)(item.Key);

                                x = (int)(x * this.atkForm.r1zoom * this.scaling);
                                y = (int)(y * this.atkForm.r1zoom * this.scaling);

                                // Moving points into rectangle
                                x += this.atkForm.rect1.Left; y += this.atkForm.rect1.Top;

                                // Starting from the middle of the rectangle
                                x += (int)this.atkForm.rect1.Width / 2;
                                y += (int)this.atkForm.rect1.Height / 2;

                                // Moving based on the mov items
                                x += this.atkForm.r1move.X;
                                y += this.atkForm.r1move.Y;


                                this.zero_histo.X = x;
                                this.zero_histo.Y = y - (int)(this.scaling * this.atkForm.r1zoom / 2);
                                this.zero_histo.Width = Math.Max((int)(item.Value * this.atkForm.r1zoom * this.scaling), (1 + item.Value) * this.scaling);
                                this.zero_histo.Height = (int)(this.scaling * this.atkForm.r1zoom);

                                if (this.zero_histo.Right > this.atkForm.rect1.Right || this.zero_histo.Left < this.atkForm.rect1.Left || this.zero_histo.Bottom > this.atkForm.rect1.Bottom || this.zero_histo.Top < this.atkForm.rect1.Top) continue;

                                this.atkForm.grph.FillRectangle(this.histo_pen, this.zero_histo);
                            }
                        }

                        if (this.picking == -2)
                        {
                            foreach (var item in countZero_points_end)
                            {
                                if (item.Value == 0) continue;

                                x = this.nAttacks;
                                y = -(int)(item.Key);

                                x = (int)(x * this.atkForm.r1zoom * this.scaling);
                                y = (int)(y * this.atkForm.r1zoom * this.scaling);

                                // Moving points into rectangle
                                x += this.atkForm.rect1.Left; y += this.atkForm.rect1.Top;

                                // Starting from the middle of the rectangle
                                x += (int)this.atkForm.rect1.Width / 2;
                                y += (int)this.atkForm.rect1.Height / 2;

                                // Moving based on the mov items
                                x += this.atkForm.r1move.X;
                                y += this.atkForm.r1move.Y;

                                this.zero_histo_end.X = x;
                                this.zero_histo_end.Y = y - (int)(this.scaling * this.atkForm.r1zoom / 2);
                                this.zero_histo_end.Width = (int) Math.Ceiling(item.Value * this.atkForm.r1zoom * this.scaling);
                                this.zero_histo_end.Height = (int)(this.scaling * this.atkForm.r1zoom);

                                if ((x + zero_histo_end.Width) > this.atkForm.rect1.Right || x < this.atkForm.rect1.Left || y > this.atkForm.rect1.Bottom || y < this.atkForm.rect1.Top) continue;


                                this.atkForm.grph.FillRectangle(this.histo_pen, this.zero_histo_end);
                            }
                        }

                        this.atkForm.picBox.Refresh();
                    }));
                }
                catch (ObjectDisposedException ex) { break; }

                sw.Stop();
                // Max ~30fps (delta time calculation)
                double sleeptime = 33.34 - sw.ElapsedMilliseconds;
                if (sleeptime > 0) Thread.Sleep((int)sleeptime);
                sw.Reset();
            }

            for (int i = 0; i < this.pens.Count(); i++) this.pens[i].Dispose();
            this.axis_pen.Dispose();
            this.company_pen.Dispose();
            this.rect1_pen.Dispose();
            this.histo_pen.Dispose();
            return;
        }
    }
}
