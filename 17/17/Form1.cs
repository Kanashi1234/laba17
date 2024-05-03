using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _17
{
    public partial class Form1 : Form
    {
        private HttpClient httpClient;
        public Form1()
        {
            InitializeComponent();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 6000;
            timer1.Tick += timer1_Tick;
            httpClient = new HttpClient();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string quote = await GetRandomAnimeQuote();
                textBox1.Text = quote;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching quote: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private async Task<string> GetRandomAnimeQuote()
        {
            string apiUrl = "https://animechan.xyz/api/random";
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(responseBody);
            return $"{data.quote} - {data.character} ({data.anime})";
        }
    }
}
