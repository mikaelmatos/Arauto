using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Arauto
{
    public partial class Contas : Form
    {
        public Contas()
        {
            InitializeComponent();
        }

        private async void Contas_Load(object sender, EventArgs e)
        {
            Obterconta1();
            Obterconta2();
            Obterconta3();
            Obterconta4();
            Obterconta5();
            Obterconta6();
        }

        public async void Obterconta1()
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData01");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView21.EnsureCoreWebView2Async(environment);

            webView21.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");

            webView21.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                int indice = 0;
                string script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                string result = await webView21.CoreWebView2.ExecuteScriptAsync(script);
                bool buscarPerfil = true;

                while (buscarPerfil)
                {
                    script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                    result = await webView21.CoreWebView2.ExecuteScriptAsync(script);
                    if (result.Contains("usercontent"))
                    {
                        buscarPerfil = false;
                        break;
                    }
                    indice++;
                }

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var imageBytes = await client.GetByteArrayAsync(result.Trim('"'));
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }

                    script = "document.getElementsByTagName(\"h1\")[0].innerText";
                    result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                    label2.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

            };
        }

        public async void Obterconta2()
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData02");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView22.EnsureCoreWebView2Async(environment);

            webView22.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");

            webView22.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                int indice = 0;
                string script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                string result = await webView22.CoreWebView2.ExecuteScriptAsync(script);
                bool buscarPerfil = true;

                while (buscarPerfil)
                {
                    script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                    result = await webView22.CoreWebView2.ExecuteScriptAsync(script);
                    if (result.Contains("usercontent"))
                    {
                        buscarPerfil = false;
                        break;
                    }
                    indice++;
                }

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var imageBytes = await client.GetByteArrayAsync(result.Trim('"'));
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            pictureBox2.Image = Image.FromStream(ms);
                        }
                    }

                    script = "document.getElementsByTagName(\"h1\")[0].innerText";
                    result = await webView22.CoreWebView2.ExecuteScriptAsync(script);

                    label3.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

            };
        }

        public async void Obterconta3()
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData03");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView23.EnsureCoreWebView2Async(environment);

            webView23.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");

            webView23.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                int indice = 0;
                string script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                string result = await webView23.CoreWebView2.ExecuteScriptAsync(script);
                bool buscarPerfil = true;

                while (buscarPerfil)
                {
                    script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                    result = await webView23.CoreWebView2.ExecuteScriptAsync(script);
                    if (result.Contains("usercontent"))
                    {
                        buscarPerfil = false;
                        break;
                    }
                    indice++;
                }

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var imageBytes = await client.GetByteArrayAsync(result.Trim('"'));
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            pictureBox3.Image = Image.FromStream(ms);
                        }
                    }

                    script = "document.getElementsByTagName(\"h1\")[0].innerText";
                    result = await webView23.CoreWebView2.ExecuteScriptAsync(script);

                    label5.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

            };
        }

        public async void Obterconta4()
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData04");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView24.EnsureCoreWebView2Async(environment);

            webView24.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");

            webView24.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                int indice = 0;
                string script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                string result = await webView24.CoreWebView2.ExecuteScriptAsync(script);
                bool buscarPerfil = true;

                while (buscarPerfil)
                {
                    script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                    result = await webView24.CoreWebView2.ExecuteScriptAsync(script);
                    if (result.Contains("usercontent"))
                    {
                        buscarPerfil = false;
                        break;
                    }
                    indice++;
                }

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var imageBytes = await client.GetByteArrayAsync(result.Trim('"'));
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            pictureBox4.Image = Image.FromStream(ms);
                        }
                    }

                    script = "document.getElementsByTagName(\"h1\")[0].innerText";
                    result = await webView24.CoreWebView2.ExecuteScriptAsync(script);

                    label7.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

            };
        }

        public async void Obterconta5()
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData05");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView25.EnsureCoreWebView2Async(environment);

            webView25.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");

            webView25.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                int indice = 0;
                string script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                string result = await webView25.CoreWebView2.ExecuteScriptAsync(script);
                bool buscarPerfil = true;

                while (buscarPerfil)
                {
                    script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                    result = await webView25.CoreWebView2.ExecuteScriptAsync(script);
                    if (result.Contains("usercontent"))
                    {
                        buscarPerfil = false;
                        break;
                    }
                    indice++;
                }

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var imageBytes = await client.GetByteArrayAsync(result.Trim('"'));
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            pictureBox5.Image = Image.FromStream(ms);
                        }
                    }

                    script = "document.getElementsByTagName(\"h1\")[0].innerText";
                    result = await webView25.CoreWebView2.ExecuteScriptAsync(script);

                    label9.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

            };
        }

        public async void Obterconta6()
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData06");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView26.EnsureCoreWebView2Async(environment);

            webView26.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");

            webView26.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                int indice = 0;
                string script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                string result = await webView26.CoreWebView2.ExecuteScriptAsync(script);
                bool buscarPerfil = true;

                while (buscarPerfil)
                {
                    script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                    result = await webView26.CoreWebView2.ExecuteScriptAsync(script);
                    if (result.Contains("usercontent"))
                    {
                        buscarPerfil = false;
                        break;
                    }
                    indice++;
                }

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var imageBytes = await client.GetByteArrayAsync(result.Trim('"'));
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            pictureBox6.Image = Image.FromStream(ms);
                        }
                    }

                    script = "document.getElementsByTagName(\"h1\")[0].innerText";
                    result = await webView26.CoreWebView2.ExecuteScriptAsync(script);

                    label11.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(1);
            browser.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(2);
            browser.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(3);
            browser.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(4);
            browser.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(5);
            browser.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(6);
            browser.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Redator redator = new Redator();
            redator.ShowDialog();

            timer1.Stop();
            timer2.Stop();
        }
        int regresssiva = 20;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (regresssiva >= 0)
            {
                Text = "Contas (" + regresssiva + ")";
                regresssiva--;
            }
            else
            {
                Text = "Contas";

                timer1.Stop();
                timer2.Stop();
            }
        }
    }
}
