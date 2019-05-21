using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alternanza_Disk
{
    class Data
    {
        public string elementoRicerca;                                                      //elementoRicerca è la string che utilizziamo per sapere cosa vuole cercare l'utente                                                                                                     // primoUrl, secondoUrl ... sono le string dove immagazziniamo i link
        public List<string> urls = new List<string>();
        public static Form2 form2 = new Form2();
        public static Form1 form1 = new Form1();
        public static bool controlloConnessione()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void thproc()
        {
            for (; ; )
            {
                if(controlloConnessione()==false)
                {
                    form1.pictureBox3.Hide();
                }

            }


        }
    }
}
