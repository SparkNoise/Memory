using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    public partial class Form1 : Form
    {
        Random random = new Random(); //uzywam funkcji losujacej zeby dla poszczegolnego kwadratu wylosowac losowa ikone

        List<string> icons = new List<string>() 
    { 
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    }; // lista ikon w czcionce webdings, kazda ikona jest dwa razy na liscie

        private void AssignIconsToSquares()
        {
            // yablelayoutpanel ma 16 pol, mamy 16 ikon, przydzielenie ikon do pol
            foreach (Control control in tableLayoutPanel1.Controls) //foreach , rodzaj petli ktora zostanie wykonana dla kazdej etykiety
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor; // ustawienie koloru ikon taki sam jak tła
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        
        Label firstClicked = null;
        Label secondClicked = null;// zmienne odwolania, sledza obiekty label 
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares(); // wywolanie funkcji wypelniajacej pola
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e) //obsluga zdarzen labeli na klikniecie
        {
            if (timer1.Enabled == true) // timer tylko dwa dwoch blednych ikon
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                
                if (clickedLabel.ForeColor == Color.Black) //sprawdzenie czy kolor etykiety jest czarny,czyli czy zostalo klikniete
                    return;

                if (firstClicked == null) // pierwsze klikniecie i zaznaczenie obiektu na czarno
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                secondClicked = clickedLabel; // drugie klikniecie i zaznaczenie na czarno
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();//sprawdzenie czy wygral
                if (firstClicked.Text == secondClicked.Text) // zatrzymanie na ekranie dwoch trafionych ikon
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                timer1.Start(); // klikniecie dwoch roznych ikon odpala timera, czeka 3/4 sekundy i ukrywa ikony
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e) // dodawanie czasomierza
        {
            // zatrzymanie czasomierza
            timer1.Stop();

            // ukrycie ikon
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // resetowanie obu zmiennych do obslugi zdarzen zeby program wiedzial ze nastepne bedzie pierwszeklikneicie
            firstClicked = null;
            secondClicked = null; // wten sposob program sie resetuje
        }
        private void CheckForWinner()
        {
            // sprawdza wszystkie labele po kolei, jezeli petla foreach przejdzie bez returnu to jest wygrana
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }


            MessageBox.Show("Dopasowales wszystkie ikony!", "Gratulacje!");
            Close();
        } 
    }
}
