using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Arauto.Redator;
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
            Obterconta7();
        }

        public bool VerificarCarregamento()
        {
            return (webView21 == null && webView22 == null && webView23 == null && webView24 == null && webView25 == null && webView26 == null && webView27 == null);
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

                    if (result.Contains("Google"))
                    {
                        button22.Enabled = false;
                        label2.Text = "Faça login no Google";
                    }
                    else
                    {
                        button22.Enabled = true;
                        label2.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

                webView21.Dispose();
                webView21 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                
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

                    if (result.Contains("Google"))
                    {
                        button21.Enabled = false;
                        label3.Text = "Faça login no Google";
                    }
                    else
                    {
                        button21.Enabled = true;
                        label3.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

                webView22.Dispose();
                webView22 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                
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

                    if (result.Contains("Google"))
                    {
                        button20.Enabled = false;
                        label5.Text = "Faça login no Google";
                    }
                    else
                    {
                        button20.Enabled = true;
                        label5.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

                webView23.Dispose();
                webView23 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                
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

                    if (result.Contains("Google"))
                    {
                        button19.Enabled = false;
                        label7.Text = "Faça login no Google";
                    }
                    else
                    {
                        button19.Enabled = true;
                        label7.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

                webView24.Dispose();
                webView24 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                
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

                    if (result.Contains("Google"))
                    {
                        button18.Enabled = false;
                        label9.Text = "Faça login no Google";
                    }
                    else
                    {
                        button18.Enabled = true;
                        label9.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

                webView25.Dispose();
                webView25 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                
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

                    if (result.Contains("Google"))
                    {
                        button17.Enabled = false;
                        label11.Text = "Faça login no Google";
                    }
                    else
                    {
                        button17.Enabled = true;
                        label11.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

                webView26.Dispose();
                webView26 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                
            };
        }
        public async void Obterconta7()
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData07");

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView27.EnsureCoreWebView2Async(environment);

            webView27.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");

            webView27.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                int indice = 0;
                string script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                string result = await webView27.CoreWebView2.ExecuteScriptAsync(script);
                bool buscarPerfil = true;

                while (buscarPerfil)
                {
                    script = "document.getElementsByTagName(\"img\")[" + indice + "].src";
                    result = await webView27.CoreWebView2.ExecuteScriptAsync(script);
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
                            pictureBox7.Image = Image.FromStream(ms);
                        }
                    }

                    script = "document.getElementsByTagName(\"h1\")[0].innerText";
                    result = await webView27.CoreWebView2.ExecuteScriptAsync(script);

                    if (result.Contains("Google"))
                    {
                        button16.Enabled = false;
                        label13.Text = "Faça login no Google";
                    }
                    else
                    {
                        button16.Enabled = true;
                        label13.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');
                    }

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro ao carregar imagem: " + ex.Message);
                }

                webView27.Dispose();
                webView27 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                
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
        private void button7_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(7);
            browser.ShowDialog();
        }

        public void LoopPostagem()
        {
            if (checkBox1.Checked && numericUpDown1.Value > 1)
            {
                numericUpDown1.Value--;
                Redator redator = new Redator(this, checkBox2.Checked ? (int)numericUpDown2.Value : -1);
                redator.ShowDialog();

                timer1.Stop();
                timer2.Stop();
            }
            else
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs"))
            {
                int quantidadeArquivos = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs").Length;

                if (quantidadeArquivos > 0)
                {
                    Redator redator = new Redator(this, checkBox2.Checked ? (int)numericUpDown2.Value : -1);
                    redator.ShowDialog();
                }
            }

            Text = "Contas";

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

                if (VerificarCarregamento())
                {
                    MatarNavegadoresObsoletos.KillUnusedWebView2Processes();
                }
            }
            else
            {
                Text = "Contas";

                if (!VerificarCarregamento())
                {
                    Text += " - Internet lenta...";
                }

                timer1.Stop();
                timer2.Stop();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                numericUpDown1.Enabled = false;
            }
            else
            {
                numericUpDown1.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Size = new Size(Size.Width, MaximumSize.Height);
            button8.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            ConfigConta configConta = new ConfigConta(1, this);
            configConta.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            ConfigConta configConta = new ConfigConta(2, this);
            configConta.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            ConfigConta configConta = new ConfigConta(3, this);
            configConta.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            ConfigConta configConta = new ConfigConta(4, this);
            configConta.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            ConfigConta configConta = new ConfigConta(5, this);
            configConta.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            ConfigConta configConta = new ConfigConta(6, this);
            configConta.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            ConfigConta configConta = new ConfigConta(7, this);
            configConta.ShowDialog();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(1, "https://www.tiktok.com/tiktokstudio");
            browser.ShowDialog();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(2, "https://www.tiktok.com/tiktokstudio");
            browser.ShowDialog();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(3, "https://www.tiktok.com/tiktokstudio");
            browser.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(4, "https://www.tiktok.com/tiktokstudio");
            browser.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(5, "https://www.tiktok.com/tiktokstudio");
            browser.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(6, "https://www.tiktok.com/tiktokstudio");
            browser.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Stop();

            Browser browser = new Browser(7, "https://www.tiktok.com/tiktokstudio");
            browser.ShowDialog();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
            {
                numericUpDown2.Enabled = false;
            }
            else
            {
                numericUpDown2.Enabled = true;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Size = new Size(Size.Width, MinimumSize.Height);
            button8.Visible = true;
        }
    }
}
