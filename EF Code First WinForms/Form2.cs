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
    public partial class Form2 : Form
    {
        public Form2()
        {
            //InitializeComponent();
        }
        private bool czyDodac;
        private int _id;
        public Form2(bool add)
        {
            InitializeComponent();
            czyDodac = add;         
            if (add)
            {
                button1.Text = "Dodaj";
                this.Text = "Dodaj uczestnika";
            }
            else
            {
                button1.Text = "Edytuj";
                this.Text = "Edytuj uczestnika";
            }
        }
        public Form2(bool add,int id)
        {
            InitializeComponent();
            czyDodac = add;
            this._id = id;
            button1.Text = "Edytuj";
            this.Text = "Edytuj uczestnika";
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (!czyDodac)
            {
                WyswietlJednego(_id);
            }
        }
        private void WyswietlJednego(int id)
        {
            Uczestnik doWyswietlenia = Baza.context.Uczestnicy.First(e => e.ID == id);
            textBox1.Text = doWyswietlenia.Imie;
            textBox2.Text = doWyswietlenia.Nazwisko;
            checkBox1.Checked = doWyswietlenia.takObiad;
            checkBox2.Checked = doWyswietlenia.takNocleg;
            checkBox3.Checked = doWyswietlenia.takOplata;
            dateTimePicker1.Value = doWyswietlenia.dataPrzyjazdu;
            dateTimePicker2.Value = doWyswietlenia.dataOdjazdu;
        }
        private void Edytuj(Uczestnik uczestnik)
        {
            var local = Baza.context.Set<Uczestnik>()
                    .Local
                    .FirstOrDefault(e => e.ID == uczestnik.ID);
            if (local != null)
            {
                Baza.context.Entry(local).State = EntityState.Detached;
            }
            var entry = Baza.context.Entry(uczestnik);
            entry.State = EntityState.Modified;
            Baza.context.SaveChanges();
        }
        private void Dodaj(Uczestnik osoba)
        {
            Baza.context.Uczestnicy.Add(osoba);
            Baza.context.SaveChanges();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Uczestnik doDodania = new Uczestnik();
            doDodania.ID = this._id;
            doDodania.Imie = textBox1.Text;
            doDodania.Nazwisko = textBox2.Text;
            doDodania.takObiad = checkBox1.Checked;
            doDodania.takNocleg = checkBox2.Checked;
            doDodania.takOplata = checkBox3.Checked;
            doDodania.dataPrzyjazdu = dateTimePicker1.Value;
            doDodania.dataOdjazdu = dateTimePicker2.Value;
            if (!czyDodac)
            {
                Edytuj(doDodania);
            }
            else
            {
                Dodaj(doDodania);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
