using System.Collections.Concurrent;

namespace DownloadSimulator
{
    public partial class Form1 : Form
    {

        BlockingCollection<Fitxategia> fitxategiak = new BlockingCollection<Fitxategia>();

        public Form1()
        {
            InitializeComponent();
            fitxategiak.Add(new Fitxategia("Fitxategia1.txt", 100));
            fitxategiak.Add(new Fitxategia("Fitxategia2.txt", 200));
            fitxategiak.Add(new Fitxategia("Fitxategia3.txt", 300));
            fitxategiak.Add(new Fitxategia("Fitxategia4.txt", 400));
            fitxategiak.Add(new Fitxategia("Fitxategia5.txt", 500));
            fitxategiak.CompleteAdding();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Sortu lehenengo ataza.
            Task.Run(() => deskargatu(1));
            // Sortu bigarren ataza.
            Task.Run(() => deskargatu(2));
        }

        private void deskargatu(int atazaZenbakia)
        {
            while (!fitxategiak.IsCompleted) {
                
            }
        }
    }
}
