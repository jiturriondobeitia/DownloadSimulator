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
            // Aukeratu eguneratuko dugun Progress Bar jasotako argumentuaren arabera.
            ProgressBar auxProgressBar = (atazaZenbakia == 1) ? this.progressBar1 : this.progressBar2;
            // Aukeratu eguneratuko dugun Laberal jasotako argumentuaren arabera.
            Label auxLabel = (atazaZenbakia == 1) ? this.label1 : this.label2;

            // Fitxategi biltegian elementuak dauden bitartean.
            while (!fitxategiak.IsCompleted)
            {
                // Hartu elementua.
                Fitxategia f = fitxategiak.Take();

                this.Invoke((Action)(()=> {
                    // Hasieratu ProgressBar.
                    auxProgressBar.Minimum = 0;
                    auxProgressBar.Maximum = 100;
                    auxProgressBar.Value = 0;
                    // Eguneratu jaisten ari garen fitxategiaren izena.
                    auxLabel.Text = f.Izena + " " + f.Tamaina + "MB / 0MB";
                }));
                
                // Simulatu 200ms-duro 10MB jaisten ditugula.
                int blockSize = 10;
                for (int downloaded = 0; downloaded <= f.Tamaina; downloaded += blockSize)
                {
                    // Kalkulatu fitxategiaren zenbat portzentai jaitsi dugun.
                    int progressPercentage = (int)((double)downloaded / f.Tamaina * 100);
                    
                    this.Invoke((Action)(() => {
                        // Eguneratu jaisten ari garen fitxategiaren izena.
                        auxLabel.Text = f.Izena + " " + f.Tamaina + "MB / " + downloaded + "MB";
                        // Eguneratu ProgressBar balioa.
                        auxProgressBar.Value = progressPercentage;
                    }));
                    
                    // Itxaron 200ms sarea simulatzeko.
                    Thread.Sleep(200);
                }

                this.Invoke((Action)(() => {
                    auxProgressBar.Value = 100;
                    // Adierazi TextArean fitxategia jaitsi dela.
                    this.textBox1.AppendText(f.Izena + " (" + f.Tamaina + ") OK!\n");
                }));
            }
        }
    }
}
