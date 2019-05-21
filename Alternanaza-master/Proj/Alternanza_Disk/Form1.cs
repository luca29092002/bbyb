using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;




namespace Alternanza_Disk
{
    public partial class Form1 : Form
    {
        Data ricerca = new Data();
    

        public Form1()
        {

            
            InitializeComponent();
            #region li
            linkLabel1.Hide();
            panel3.Hide();
            linkLabel2.Hide();
            linkLabel3.Hide();
            linkLabel4.Hide();
            linkLabel5.Hide();
            label2.Hide();
            bianco.Hide();
            rosa.Hide();
            giallo.Hide();
            textBox1.Hide();
            button2.Hide();
            verde.Hide();
            listBox1.Hide();
            richTextBox1.Hide();
            MODOFF.Hide();

            #endregion

        }


        private void searchButton_Click(object sender, EventArgs e)
        {
            
            #region li
            linkLabel1.Hide();
            linkLabel2.Hide();
            linkLabel3.Hide();
            linkLabel4.Hide();
            linkLabel5.Hide();
            
            #endregion
            if (searchText.Text == " " || searchText.Text == "")
                Caesar.speakerError();
            else
            {
                ricerca.elementoRicerca = searchText.Text;
                searchText.Text = "Sto cercando ...";
                Ricerca();
                Caesar.speaker(ricerca.elementoRicerca);
                #region li
                linkLabel1.Text = ricerca.urls[0];
                linkLabel2.Text = ricerca.urls[1];
                linkLabel3.Text = ricerca.urls[2];
                linkLabel4.Text = ricerca.urls[3];
                linkLabel5.Text = ricerca.urls[4];

                linkLabel1.Show();
                linkLabel2.Show();
                linkLabel3.Show();
                linkLabel4.Show();
                linkLabel5.Show();
                #endregion
            }
        }
        public void Ricerca()
        {
            if (ricerca.elementoRicerca.Contains("<<zz<<"))                                                                 //Sarà implementato un dizionario per il linguaggio scurrile
            {
                MessageBox.Show("Ricerca non disponibile, linguaggio non adeguato");
            }
            if (Data.controlloConnessione() == false)                                                                           //Nel caso in cui la funzione controlloConnessione restituisca un valore false ovvero assenza di connessione il message box apparirà
            {
                MessageBox.Show("Connessione assente, utilizzare funzionalità offline ");
                searchText.Text = "";
                
            }
            else
            {
                WebRequest request = WebRequest.Create("https://www.google.com/search?q=" + ricerca.elementoRicerca);      //La richiesta viene inoltrata a https://www.google.com/search?q=, essendo il modello principale per la ricerca, attaccandogli cioò che l'utente vuole cercare
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                //MessageBox.Show(((HttpWebResponse)response).StatusDescription);
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                StreamWriter a = new StreamWriter("output.txt", false);
                a.Write(responseFromServer);                                                                                   //Dopo il processo di interrogazione a google la risposta viene scritta sul file di testo output.txt
                a.Close();
                string[] urls = ricercaUrl();                                                                                 //viene richiamata la funzione ricercaUrl()
                searchText.Text = "";
                ricerca.urls.Clear();
                #region copiaUrl
                
                for (int i = 1; i < 6; i++)
                    ricerca.urls.Add(urls[i]);
                
                #endregion
            }
        }
        public string[] ricercaUrl()
        {
            int i = 1;
            var a = '"';
            string[] links = new string[15];                                                                                    //vettore di stringhe, contenente i link
            int counter = 6;
            int x = default(int);
            StreamReader r = new StreamReader("output.txt");                                                                    //links.txt è il file di testo contenente i link
            while (x < counter)
            {
                try
                {
                    bool c = default(bool);
                    string controllo = r.ReadLine();
                    if (controllo.Contains("<cite>"))                                                                           //Il controllo funziona in base alla presenza di url?q= all'interno di una riga di codice usiamo questa logica due volte, e infine nella stringa splittata creiamo una Substring che parte dalla dicitura http presente in ogni link utile già filtrato
                    {
                        string[] y = controllo.Split(a, a);
                        foreach (string s in y)
                            if (s.Contains("<cite>") && i < 16)
                            {
                                int indexE;
                                string str = "";
                                int indexF = s.LastIndexOf("http");
                                indexE = s.LastIndexOf("/") - 6;                                                               //Eccezione generale
                                try
                                {
                                    str = s.Substring(indexF, indexE);
                                }
                                catch { }
                                str = str.Replace("<b>", "");
                                str = str.Replace("</b>", "");
                                str = str.Replace("</cite>", "");
                                str = str.Replace("</", "");
                                str = str.Replace("�", "à");
                                if (str.Contains("/.../"))
                                {
                                    try
                                    {
                                        HttpWebRequest request = WebRequest.Create(str) as HttpWebRequest;
                                        request.Method = "HEAD";
                                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                                        response.Close();
                                    }
                                    catch { c = true; }
                                }
                                if (c == false && str != "")
                                {
                                    links[i] = str;
                                    i++;
                                    x++;
                                }
                            }
                    }

                }
                catch { x++; i++; }                                                                                                      //Metodo utilizzato per ricerca l'url nel file di testo "output.txt"
            }
            r.Close();
            return links;
        }
        #region moveapp
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();



        private void move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void a(object sender, EventArgs e)
        {
            
        }

        private void shutdown_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #endregion

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel3.Text);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel4.Text);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel5.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel2.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            label2.Hide();
            panel3.Hide();
            bianco.Hide();
            rosa.Hide();
            giallo.Hide();
            verde.Hide();
            label1.Hide();
            searchText.Hide();
            searchButton.Hide();
            linkLabel1.Hide();
            textBox1.Hide();
            button2.Hide();
            linkLabel2.Hide();
            linkLabel3.Hide();
            linkLabel4.Hide();
            linkLabel5.Hide();
            listBox1.Show();
            richTextBox1.Show();
            MODOFF.Show();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Show();
            bianco.Show();
            panel3.Show();
            rosa.Show();
            giallo.Show();
            verde.Show();
            label1.Hide();
            searchText.Hide();
            searchButton.Hide();
            linkLabel1.Hide();
            linkLabel2.Hide();
            linkLabel3.Hide();
            linkLabel4.Hide();
            textBox1.Hide();
            button2.Hide();
            linkLabel5.Hide();
            listBox1.Hide();
            richTextBox1.Hide();
            



        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        
        
       
        private void Form1_Load(object sender, EventArgs e)
        {
            
         


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Show();
            panel3.Hide();
            searchText.Show();
            searchButton.Show();
            label2.Hide();
            textBox1.Hide();
            button2.Hide();
            bianco.Hide();
            rosa.Hide();
            giallo.Hide();
            verde.Hide();
            if (linkLabel1.Text != "linkLabel1")
            {
                linkLabel1.Show();
                linkLabel2.Show();
                linkLabel3.Show();
                linkLabel4.Show();
                linkLabel5.Show();
            }
            listBox1.Hide();
            richTextBox1.Hide();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Hide();
            panel3.Hide();
            searchText.Hide();
            searchButton.Hide();
            label2.Hide();
            bianco.Hide();
            rosa.Hide();
            giallo.Hide();
            verde.Hide();
            listBox1.Hide();
            textBox1.Hide();
            richTextBox1.Hide();
            MODOFF.Hide();
            textBox1.Show();
            button2.Show();



        }

        private void bianco_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            button1.BackColor = Color.White;
            label2.BackColor = Color.White;
            button3.BackColor = Color.White;
            button4.BackColor = Color.White;
            label1.BackColor = Color.White;
            panel1.BackColor = Color.White;
            minimize.BackColor = Color.White;
            shutdown.BackColor = Color.White;
            richTextBox1.BackColor = Color.White;
            listBox1.BackColor = Color.White;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {

        }

        private void rosa_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Pink;
            button1.BackColor = Color.Pink;
            label2.BackColor = Color.Pink;
            button3.BackColor = Color.Pink;
            button4.BackColor = Color.Pink;
            label1.BackColor = Color.Pink;
            panel1.BackColor = Color.Pink;
            richTextBox1.BackColor = Color.Pink;
            listBox1.BackColor = Color.Pink;
        }

        private void giallo_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Yellow;
            button1.BackColor = Color.Yellow;
            label2.BackColor = Color.Yellow;
            button3.BackColor = Color.Yellow;
            button4.BackColor = Color.Yellow;
            label1.BackColor = Color.Yellow;
            panel1.BackColor = Color.Yellow;
            minimize.BackColor = Color.Yellow;
            shutdown.BackColor = Color.Yellow;
            richTextBox1.BackColor = Color.Yellow;
            listBox1.BackColor = Color.Yellow;
        }

        private void verde_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.GreenYellow;
            button1.BackColor = Color.GreenYellow;
            label2.BackColor = Color.GreenYellow;
            button3.BackColor = Color.GreenYellow;
            button4.BackColor = Color.GreenYellow;
            label1.BackColor = Color.GreenYellow;
            panel1.BackColor = Color.GreenYellow;
            minimize.BackColor = Color.GreenYellow;
            shutdown.BackColor = Color.GreenYellow;
            richTextBox1.BackColor = Color.GreenYellow;
            listBox1.BackColor = Color.GreenYellow;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (listBox1.SelectedItem.ToString() == "Attacco DDOS")
                {
                    richTextBox1.Text = "ciao";
                }
                if (listBox1.SelectedItem.ToString() == "Blackout")
                {
                    richTextBox1.Text = "lol";
                }
                if (listBox1.SelectedItem.ToString() == "Assenza di connessione(generico)")
                {
                    richTextBox1.Text = "down";
                }
                if (listBox1.SelectedItem.ToString() == "Tipi di cavi")
                {
                    richTextBox1.Text = "scemo chi legge";
                }
                if (listBox1.SelectedItem.ToString() == "Mancanza di elettricità")
                {
                    richTextBox1.Text = "viva la figa";
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private int i = 1;
        private void button2_Click(object sender, EventArgs e)
        {
            TextBox tx = new TextBox();
            tx.Text = textBox1.Text;
            Point p = new Point(220 + i, 90 * i);
            tx.Location = p;
            this.Controls.Add(tx);
            i++;
            Data a = new Data();
            Data.form1.Ricerca();
            Data.form1.ricercaUrl();
            LinkLabel lk = new LinkLabel();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
  
}
