using System.Diagnostics;

namespace lvlRandomWalk
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
        private Pen specialPen;
        private Pen rect1_pen;
        private Brush histo_pen;

        public Thread thread;
        private int nAttacks;
        private int sValue;
        public int mSystems;
        private double pAttack;
        public int picking;
        

        private List<List<Point>> zero_points;
        private List<int>[] firstOccurences = { new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>(), new List<int>() };

        private Rectangle zero_histo;
        private Dictionary<int, int> countPoints;

        private int scaling = 5;

        public Attack(AttackForm atkForm)
        {
            this.atkForm = atkForm;
            this.rand = new Random();
            this.sw = new Stopwatch();

            this.histo_pen = new SolidBrush(Color.FromArgb(150, Color.Maroon));
            this.axis_pen = new Pen(Color.DarkGray, 2);
            this.company_pen = new Pen(Color.LightGray, 1);
            this.specialPen = new Pen(Color.Red, 1);
            this.rect1_pen = new Pen(Color.LightBlue, 4);

            this.zero_points = new List<List<Point>>();

            this.countPoints = new Dictionary<int, int>();
            this.zero_histo = new Rectangle(0, 0, 0, 0);

            this.picking = -1;
            this.nAttacks = 1;
            this.pAttack = 0;
            this.mSystems = 1;
            this.sValue = 20;

            this.thread = new Thread(new ThreadStart(StartThreadedAttacks));

            // Starting thread
            this.thread.Start();
        }

        public void startAttacks(int n, double p, int m, int s)
        {
            this.zero_points.Clear();
            this.countPoints.Clear();

            for (int i = 0; i < this.firstOccurences.Length; i++) this.firstOccurences[i].Clear();
            for (int i = 0; i < m; i++)
            {
                this.zero_points.Add(new List<Point>());
                this.zero_points[i].Add(new Point(0, 0));
                
                this.firstOccurences[0].Add(-1);
                this.firstOccurences[1].Add(-1);
                this.firstOccurences[2].Add(-1);
                this.firstOccurences[3].Add(-1);
                this.firstOccurences[4].Add(-1);
                this.firstOccurences[5].Add(-1);
                this.firstOccurences[6].Add(-1);
                this.firstOccurences[7].Add(-1);
                this.firstOccurences[8].Add(-1);
                this.firstOccurences[9].Add(-1);
            }

            // Initializing the counters at 0
            this.countPoints[2] = 0;
            this.countPoints[3] = 0;
            this.countPoints[4] = 0;
            this.countPoints[5] = 0;
            this.countPoints[6] = 0;
            this.countPoints[7] = 0;
            this.countPoints[8] = 0;
            this.countPoints[9] = 0;
            this.countPoints[10] = 0;

            this.pAttack = p;
            this.nAttacks = n;
            this.mSystems = m;
            this.sValue = s;
            this.picking = 0;
        }

        public void cancelAttack()
        {

            this.picking = -3;

            // Reset UI to default values (Thread  safe)
            this.atkForm.BeginInvoke((Action)delegate ()
            {
                this.atkForm.cancBT.Enabled = false;
                this.atkForm.startBT.Enabled = true;
                this.atkForm.nValue.Enabled = true;
                this.atkForm.pValue.Enabled = true;
            });

        }

        private void StartThreadedAttacks()
        {
            int posX = 0; int posY = 0, index = 0, max = 0;
            int x = 0, y = 0;

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
                        this.atkForm.pValue.Enabled = false;
                        this.atkForm.startBT.Enabled = false;
                        this.atkForm.cancBT.Enabled = true;
                    });

                    for (int i = 0; i < this.mSystems; i++)
                    {
                        randNumber = rand.NextDouble();
                        newZero = this.zero_points[i].Last();

                        newZero.X++;
                        newZero.Y += (randNumber <= this.pAttack ? -1 : 1);

                        if (newZero.Y == this.sValue && this.firstOccurences[0][i] == -1) this.firstOccurences[0][i] = newZero.X;
                        else if(newZero.Y == -20 && this.firstOccurences[1][i] == -1) this.firstOccurences[1][i] = newZero.X;
                        else if(newZero.Y == -30 && this.firstOccurences[2][i] == -1) this.firstOccurences[2][i] = newZero.X;
                        else if (newZero.Y == -40 && this.firstOccurences[3][i] == -1) this.firstOccurences[3][i] = newZero.X;
                        else if (newZero.Y == -50 && this.firstOccurences[4][i] == -1) this.firstOccurences[4][i] = newZero.X;
                        else if (newZero.Y == -60 && this.firstOccurences[5][i] == -1) this.firstOccurences[5][i] = newZero.X;
                        else if (newZero.Y == -70 && this.firstOccurences[6][i] == -1) this.firstOccurences[6][i] = newZero.X;
                        else if (newZero.Y == -80 && this.firstOccurences[7][i] == -1) this.firstOccurences[7][i] = newZero.X;
                        else if (newZero.Y == -90 && this.firstOccurences[8][i] == -1) this.firstOccurences[8][i] = newZero.X;
                        else if (newZero.Y == -100 && this.firstOccurences[9][i] == -1) this.firstOccurences[9][i]  = newZero.X;

                        this.zero_points[i].Add(newZero);
                    }

                    this.picking += 1;
                }

                if(this.picking == this.nAttacks)
                {

                    for(int i = 1; i < this.mSystems; i++)
                    {
                        if (this.firstOccurences[0][i] == -1)
                        {
                            if (this.firstOccurences[1][i] != -1) this.countPoints[2]++;
                            if (this.firstOccurences[2][i] != -1) this.countPoints[3]++;
                            if (this.firstOccurences[3][i] != -1) this.countPoints[4]++;
                            if (this.firstOccurences[4][i] != -1) this.countPoints[5]++;
                            if (this.firstOccurences[5][i] != -1) this.countPoints[6]++;
                            if (this.firstOccurences[6][i] != -1) this.countPoints[7]++;
                            if (this.firstOccurences[7][i] != -1) this.countPoints[8]++;
                            if (this.firstOccurences[8][i] != -1) this.countPoints[9]++;
                            if (this.firstOccurences[9][i] != -1) this.countPoints[10]++;
                        } else {
                            if (this.firstOccurences[0][i] > this.firstOccurences[1][i] && this.firstOccurences[1][i] != -1) this.countPoints[2]++; //20
                            if (this.firstOccurences[0][i] > this.firstOccurences[2][i] && this.firstOccurences[2][i] != -1) this.countPoints[3]++; //30
                            if (this.firstOccurences[0][i] > this.firstOccurences[3][i] && this.firstOccurences[3][i] != -1) this.countPoints[4]++; //40
                            if (this.firstOccurences[0][i] > this.firstOccurences[4][i] && this.firstOccurences[4][i] != -1) this.countPoints[5]++; //50
                            if (this.firstOccurences[0][i] > this.firstOccurences[5][i] && this.firstOccurences[5][i] != -1) this.countPoints[6]++; //60
                            if (this.firstOccurences[0][i] > this.firstOccurences[6][i] && this.firstOccurences[6][i] != -1) this.countPoints[7]++; //70
                            if (this.firstOccurences[0][i] > this.firstOccurences[7][i] && this.firstOccurences[7][i] != -1) this.countPoints[8]++; //80
                            if (this.firstOccurences[0][i] > this.firstOccurences[8][i] && this.firstOccurences[8][i] != -1) this.countPoints[9]++; //90
                            if (this.firstOccurences[0][i] > this.firstOccurences[9][i] && this.firstOccurences[9][i] != -1) this.countPoints[10]++; //100
                        }
                    }

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
                        this.atkForm.pValue.Enabled = true;
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
                        while (posY - index * this.scaling * this.atkForm.r1zoom > this.atkForm.rect1.Top)
                        {
                            if (posY - index * this.scaling * this.atkForm.r1zoom < this.atkForm.rect1.Bottom)
                            {
                                if(index == 20 || index == 30 || index == 40 || index == 50 || index == 60 || index == 70 || index == 80 || index == 90 || index == 100) 
                                    this.atkForm.grph.DrawLine(this.specialPen, this.atkForm.rect1.Left, posY - index * this.scaling * this.atkForm.r1zoom, this.atkForm.rect1.Right, posY - index * this.scaling * this.atkForm.r1zoom);
                                else this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect1.Left, posY - index * this.scaling * this.atkForm.r1zoom, this.atkForm.rect1.Right, posY - index * this.scaling * this.atkForm.r1zoom);
                            }
                            index++;
                        }

                        index = 0;
                        while (posY + index * this.scaling * this.atkForm.r1zoom < this.atkForm.rect1.Bottom)
                        {
                            if (posY + index * this.scaling * this.atkForm.r1zoom > this.atkForm.rect1.Top)
                            {
                                if (index == this.sValue)
                                    this.atkForm.grph.DrawLine(this.specialPen, this.atkForm.rect1.Left, posY + index * this.scaling * this.atkForm.r1zoom, this.atkForm.rect1.Right, posY + index * this.scaling * this.atkForm.r1zoom);
                                else this.atkForm.grph.DrawLine(this.company_pen, this.atkForm.rect1.Left, posY + index * this.scaling * this.atkForm.r1zoom, this.atkForm.rect1.Right, posY + index * this.scaling * this.atkForm.r1zoom);
                            }
                            index++;
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

                        if (this.picking == -2)
                        {
                            foreach (var item in countPoints)
                            {
                                if (item.Value == 0) continue;

                                x = this.nAttacks;
                                y = -(int)(item.Key) * 10; //I've saved the keys without the last 0

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
                                this.zero_histo.Width = (int) Math.Ceiling(item.Value * this.atkForm.r1zoom);
                                this.zero_histo.Height = (int)(this.scaling * this.atkForm.r1zoom);

                                if ((x + zero_histo.Width) > this.atkForm.rect1.Right || x < this.atkForm.rect1.Left || y > this.atkForm.rect1.Bottom || y < this.atkForm.rect1.Top) continue;


                                this.atkForm.grph.FillRectangle(this.histo_pen, this.zero_histo);
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
            this.specialPen.Dispose();
            return;
        }
    }
}
