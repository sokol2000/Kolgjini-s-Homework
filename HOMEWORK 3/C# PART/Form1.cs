using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW4_First_Part
{
    public partial class Form1 : Form
    {
        private TextBox[] inputTextBoxes;
        private TableLayoutPanel tableLayoutPanel;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            CreateTable();
        }
        private void CreateTable()
        {

            int columns = 4; // Valore predefinito
            int number = 10; // Valore predefinito

            // Leggi i valori dai TextBox
            int.TryParse(txtColumns.Text, out columns);
            int.TryParse(txtNumber.Text, out number);

            // Crea array di TextBox dinamici
            inputTextBoxes = new TextBox[columns];

            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;

            Label titleLabel = new Label();
            titleLabel.Text = "Joint distribution of any number of 2,3, ...k, continuous quantitative variables";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            tableLayoutPanel.Controls.Add(titleLabel);

            for (int i = 0; i < columns; i++)
            {
                Label label = new Label();
                label.Text = "Choose the intervals number for variable " + i;
                label.Width = 420;
                label.Height = 40;
                tableLayoutPanel.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Text = (i+1).ToString();
                inputTextBoxes[i] = textBox;
                tableLayoutPanel.Controls.Add(textBox);
            }

            Button calculateButton = new Button();
            calculateButton.Text = "Calcola";
            calculateButton.Click += CalculateButton_Click;
            calculateButton.Width = 100;
            calculateButton.Height = 40;
            tableLayoutPanel.Controls.Add(calculateButton);

            Controls.Add(tableLayoutPanel);
        }

        ///////////////////////////////////////////////////////////

        private List<double> GenerateRandomNumbers(int n, double min, double max)
        {
            Random random = new Random();
            List<double> randomNumbers = new List<double>();
            for (int i = 0; i < n; i++)
            {
                double randomNumber = random.NextDouble() * (max - min) + min;
                randomNumbers.Add(Math.Round(randomNumber, 1));
            }
            return randomNumbers;
        }

        private void GetAllCombinations(List<List<string>> arrays, int index, List<string> currentCombination, List<List<string>> result)
        {
            if (index == arrays.Count)
            {
                result.Add(new List<string>(currentCombination));
                return;
            }

            for (int i = 0; i < arrays[index].Count; i++)
            {
                currentCombination.Add(arrays[index][i]);
                GetAllCombinations(arrays, index + 1, currentCombination, result);
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }



        private void CalculateButton_Click(object sender, EventArgs e)
        {
            int columns = 4; // Valore predefinito
            int number = 10; // Valore predefinito

            int.TryParse(txtColumns.Text, out columns);
            int.TryParse(txtNumber.Text, out number);

            // Valori per gli intervalli
            List<int> intervals = new List<int>();
            for (int i = 0; i < columns; i++)
            {
                if (!int.TryParse(inputTextBoxes[i].Text, out int interval))
                {
                    MessageBox.Show($"Devi scegliere il numero di intervalli per la variabile {i}.");
                    return;
                }
                intervals.Add(interval);
            }

            List<List<string>> intervalli = new List<List<string>>();
            for (int i = 0; i < columns; i++)
            {
                List<string> tmp = new List<string>();
                for (double j = 0.00; j < 1.00; j += 1.000 / intervals[i])
                {
                    double inizio = j;
                    double fine = inizio + 1.000 / intervals[i];
                    string intervallo = inizio.ToString("0.000") + "-" + fine.ToString("0.000");
                    tmp.Add(intervallo);
                }
                intervalli.Add(tmp);
            }


            List<List<double>> dataset = new List<List<double>>();
            for (int i = 0; i < columns; i++)
            {
                List<double> randomNumbers = GenerateRandomNumbers(number, 0, 1);
                dataset.Add(randomNumbers);
            }

            List<List<string>> datasetIntervalli = new List<List<string>>();
            for (int i = 0; i < columns; i++)
            {
                List<string> tmp = new List<string>();
                for (int j = 0; j < dataset[i].Count; j++)
                {
                    for (int k = 0; k < intervalli[i].Count; k++)
                    {
                        string[] intParts = intervalli[i][k].Split('-');
                        double inf = double.Parse(intParts[0]);
                        double sup = double.Parse(intParts[1]);


                        if (dataset[i][j] >= inf && dataset[i][j] < sup)
                        {
                            tmp.Add(intervalli[i][k]);
                            break;
                        }

                        if (sup == 1.00 && dataset[i][j] == 1.000)
                        {
                            tmp.Add(intervalli[i][k]);
                            break;
                        }
                    }
                }
                datasetIntervalli.Add(tmp);
            }

            Dictionary<string, int> joinDistribution = new Dictionary<string, int>();
            List<List<string>> result = new List<List<string>>();
            GetAllCombinations(intervalli, 0, new List<string>(), result);

            foreach (List<string> combination in result)
            {
                string chiave = string.Join(" || ", combination) + " || ";
                joinDistribution[chiave] = 0;
            }
            for (int i = 0; i < datasetIntervalli[0].Count; i++)
            {
                string chiave = string.Empty;
                for (int j = 0; j < columns; j++)
                {
                    chiave += datasetIntervalli[j][i] + " || ";
                }
                if (joinDistribution.ContainsKey(chiave))
                {
                    joinDistribution[chiave] += 1;
                }
            }

            DataTable table = new DataTable();
            table.Columns.Add("Key", typeof(string));
            table.Columns.Add("Value", typeof(int));

            // Inserimento dei dati nella tabella
            foreach (var kvp in joinDistribution)
            {
                table.Rows.Add(kvp.Key, kvp.Value);
            }

            // Collega il DataGridView alla tabella
            dataGridView1.DataSource = table;

            // Imposta la larghezza delle colonne in modo che si adatti al contenuto
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.DataSource = table;
        }
    }
}
