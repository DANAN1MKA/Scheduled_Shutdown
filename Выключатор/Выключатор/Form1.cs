using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Выключатор
{
    public partial class Form1 : Form
    {
        string st = string.Empty;

        public Form1()
        {
            InitializeComponent();
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //textBox1.BackColor = Color.Transparent;
            comboBox1.SelectedIndex = 0;

            //st = "/s";
            
        }

        private void mtbMinutes_KeyDown(object sender, KeyEventArgs e)
        {
            lMessage.Visible = false;
            //если все текстбоксы пусты и нажат enter сообщаем об ошибке
            if (mtbHours.Text == string.Empty &&
                mtbMinutes.Text == string.Empty &&
                e.KeyCode == Keys.Enter)
            {
                lMessage.Text = "необходимо ввести время";
                lMessage.Visible = true;
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int time = mtbHours.Text != string.Empty ? Convert.ToInt32(mtbHours.Text) * 3600 : 0;
                       time += mtbMinutes.Text != string.Empty ? Convert.ToInt32(mtbMinutes.Text) * 60 : 0;
                        
                    if (time == 0)
                    {
                        string message = "Вы уверены что хотите ";
                        switch(comboBox1.SelectedIndex)
                        {
                            case 0:
                                message += "выключить компьютер";
                                break;
                            case 1:
                                message += "перезагрузить компьютер";
                                break;
                            case 2:
                                message += "погрузить компьютер в гибернацию";
                                break;
                        }
                        message += " сейчас?";

                        if(MessageBox.Show(message, 
                                           "Выключатор спрашивает", 
                                           MessageBoxButtons.YesNo) == DialogResult.No)
                        { return; }
                    }
                    else
                    { st += " /f /t " + Convert.ToString(time); }

                    //создаем и запускаем процесс со вссеми необходимыми атрибутами
                    Process cmd = new Process();
                    cmd.StartInfo = new ProcessStartInfo(@"shutdown", st);
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.Start();
                    Application.Exit();
                }
            }
        }

        private void mtbMinutes_TextChanged(object sender, EventArgs e)
        {
            if (mtbMinutes.Text != string.Empty && Convert.ToInt32(mtbMinutes.Text) >= 60)
            {
                int hour = Convert.ToInt32(mtbMinutes.Text) / 60;
                int minute = Convert.ToInt32(mtbMinutes.Text) - hour * 60;
                //если бокс пуст помещаем в него значение если нет изменяем (использована тернарная операция)
                mtbHours.Text = mtbHours.Text == string.Empty ? Convert.ToString(hour) : Convert.ToString(Convert.ToInt32(mtbHours.Text) + hour);
                mtbMinutes.Text = minute == 0 ? string.Empty : Convert.ToString(minute);
            }
        }

        private void mtbHours_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mtbMinutes.Focus();
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
            //comboBox1.FlatStyle = FlatStyle.System;
            //comboBox1.BackColor = Color.White;
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {
            //comboBox1.FlatStyle = FlatStyle.Flat;
            //comboBox1.BackColor = Color.White;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    st = "/s";
                    break;
                case 1:
                    st = "/r";
                    break;
                case 2:
                    st = "/h";
                    break;

            }

        }
    }
}
