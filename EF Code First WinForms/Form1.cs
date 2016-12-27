using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_Code_First_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Baza.initContext();
            InitializeComponent();
        }
        private void wyswietlBaze(){
            dataGridView1.DataSource = Baza.context.Uczestnicy.ToList();
            dataGridView1.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            wyswietlBaze();        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form show = new Form2(true);
            DialogResult res = show.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Uczestnik dodany");
                wyswietlBaze();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Uczestnik edd=(Uczestnik)dataGridView1.SelectedRows[0].DataBoundItem;
            Form show = new Form2(false,edd.ID);           
            DialogResult res= show.ShowDialog();
            if (res == DialogResult.OK)
            {
                MessageBox.Show("Uczestnik edytowany");
                wyswietlBaze();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Uczestnik edd = (Uczestnik)dataGridView1.SelectedRows[0].DataBoundItem;
            Usun(edd.ID);
            System.Threading.Thread.Sleep(100);
            wyswietlBaze();
        }
        public static void Usun(int id)
        {
            var local = Baza.context.Set<Uczestnik>()
                    .Local
                    .FirstOrDefault(e => e.ID == id);
            if (local != null)
            {
                Baza.context.Entry(local).State = EntityState.Detached;
            }
            Uczestnik doUsuniecia = new Uczestnik() { ID = id };          
            Baza.context.Uczestnicy.Attach(doUsuniecia);
            Baza.context.Uczestnicy.Remove(doUsuniecia);
            Baza.context.SaveChanges();
        }
    }
}
