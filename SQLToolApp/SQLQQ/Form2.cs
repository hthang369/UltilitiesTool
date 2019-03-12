using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SQLQQ
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string ConvertToUnsign3(string str)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = ConvertToUnsign3(textBox1.Text);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DateTime @loanDate = new DateTime(2017,1,1);
            int @loanMonth = @loanDate.Month;
            int @loanYear = @loanDate.Year;
            DateTime @loanDueDate = new DateTime(2017, 3, 31);
            DateTime @loanInDate;
            int loanDay = 30;
            while (true)
            {

                @loanInDate = new DateTime(loanYear, loanMonth, loanDay);
                if (loanInDate.DayOfWeek == DayOfWeek.Saturday)
                    loanInDate.AddDays(2);
                else if (loanInDate.DayOfWeek == DayOfWeek.Sunday)
                    loanInDate.AddDays(1);
                loanMonth += 1;
                listBox1.Items.Add(loanInDate.ToShortDateString());
                if ((loanMonth > loanDueDate.Month) && (loanYear >= loanDueDate.Year))
                    break;
                if(loanMonth > 12)
                {
                    loanMonth = 1;
                    loanYear += 1;
                }
            }
        }
    }
}
