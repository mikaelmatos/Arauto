using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Arauto
{
    public partial class ConfigConta : Form
    {
        int idConta;
        Contas contas;
        public ConfigConta(int idConta, Contas contas)
        {
            this.contas = contas;
            this.idConta = idConta;
            InitializeComponent();
        }

        private async void ConfigConta_Load(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Quiz" || comboBox3.Text == "Misto")
            {
                label13.Enabled = true;
                textBox6.Enabled = true;
                label14.Enabled = true;
                textBox7.Enabled = true;
            }
            else
            {
                label13.Enabled = false;
                textBox6.Enabled = false;
                label14.Enabled = false;
                textBox7.Enabled = false;
            }

            string blackList = "";

            if (File.Exists(Path.GetTempPath() + DateTime.Now.ToString("yyyyMMdd") + "-" + idConta + "blacklist.log"))
            {
                StreamReader streamReader = new StreamReader(Path.GetTempPath() + DateTime.Now.ToString("yyyyMMdd") + "-" + idConta + "blacklist.log");
                string blackListTexto = streamReader.ReadToEnd().Trim().Trim(',').Trim().Trim(',').Trim().Trim(',').Trim().Trim(',');

                if (blackListTexto != "")
                {
                    textBox3.Text = "* " + blackListTexto.Replace(", ", "\r\n* ");
                }

                streamReader.Close();
            }

            string conteudoConf = "";

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs");
            }

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + ".mjson"))
            {
                StreamReader stream = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + ".mjson");
                conteudoConf = stream.ReadToEnd();
                stream.Close();

                Configuracao configuracao = JsonConvert.DeserializeObject<Configuracao>(conteudoConf);

                comboBox2.Text = configuracao.nicho;
                textBox2.Text = configuracao.prompt_gerar_noticia;
                textBox1.Text = configuracao.prompt_gerar_imagem;
                comboBox1.Text = configuracao.idioma;
                label4.Text = configuracao.nome_conta_google;
                label5.Text = configuracao.nome_conta_tiktok;
                textBox4.Text = configuracao.tags;
                textBox5.Text = configuracao.chamada;
                checkBox1.Checked = configuracao.conta_ativa;
                checkBox2.Checked = configuracao.audio_viral;
                comboBox3.Text = configuracao.estilo;
                textBox7.Text = configuracao.prompt_quiz_imagem;

                if (!String.IsNullOrEmpty(configuracao.prompt_quiz))
                {
                    textBox6.Text = configuracao.prompt_quiz;
                }
            }
            else
            {

            }

            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData0" + idConta);

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView21.EnsureCoreWebView2Async(environment);

            webView21.CoreWebView2.Navigate("https://myaccount.google.com/?utm_source=OGB&utm_medium=app");

            webView21.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {

                string script = "document.getElementsByTagName(\"h1\")[0].innerText";
                string result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                if (result.Contains("Google"))
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    label4.Text = "null";
                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    label4.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');
                }


                webView21.Dispose();
                webView21 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();


            };

            await webView22.EnsureCoreWebView2Async(environment);

            webView22.CoreWebView2.Navigate("https://www.tiktok.com");

            webView22.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                //bool liberar1 = true;

                //while (liberar1)
                //{
                //    string script1 = "document.getElementsByTagName(\"body\")[0].innerHTML.includes(\"Métricas principais\")";

                //    string result1 = await webView21.CoreWebView2.ExecuteScriptAsync(script1);

                //    if (result1.Trim().ToLower() == "false")
                //    {
                //        await Task.Delay(1000);
                //    }
                //    else
                //    {
                //        liberar1 = false;
                //    }
                //}

                await Task.Delay(6000);

                string script = "document.querySelectorAll(\"button[aria-label='Perfil']\")[0].parentNode.href.replace('https://www.tiktok.com/','')";
                string result = await webView22.CoreWebView2.ExecuteScriptAsync(script);

                label5.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');

                if (textBox4.Text == "")
                {
                    if (result.Trim() != "null")
                    {
                        textBox4.Text = result.Replace("@", "#").Replace("\"", "");
                    }
                }


                webView22.Dispose();
                webView22 = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Configuracao configuracao = new Configuracao();
            configuracao.id_conta = idConta;
            configuracao.nicho = comboBox2.Text;
            configuracao.prompt_gerar_noticia = textBox2.Text;
            configuracao.prompt_gerar_imagem = textBox1.Text;
            configuracao.idioma = comboBox1.Text;
            configuracao.nome_conta_google = label4.Text;
            configuracao.nome_conta_tiktok = label5.Text;
            configuracao.tags = textBox4.Text;
            configuracao.chamada = textBox5.Text;
            configuracao.conta_ativa = checkBox1.Checked;
            configuracao.audio_viral = checkBox2.Checked;
            configuracao.prompt_quiz = textBox6.Text;
            configuracao.estilo = comboBox3.Text;
            configuracao.prompt_quiz_imagem = textBox7.Text;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            if (!configuracao.nome_conta_google.Contains("Carregando") && !configuracao.nome_conta_tiktok.Contains("Carregando") && !configuracao.nome_conta_tiktok.Contains("null") && !configuracao.nome_conta_google.Contains("null"))
            {
                if (configuracao.prompt_gerar_noticia.Length > 50 && configuracao.prompt_gerar_imagem.Length > 50)
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + ".mjson"))
                    {
                        File.Move(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + ".mjson", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + "_old.mjson");
                    }

                    string json = System.Text.Json.JsonSerializer.Serialize(configuracao, options);
                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + ".mjson", json);

                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + "_old.mjson"))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + "_old.mjson");
                    }
                }
                else
                {
                    MessageBox.Show("Os prompts precisam ter no mínimo 50 caracteres");
                }

                MessageBox.Show("Salvo! " + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + idConta + ".mjson");
            }
            else
            {
                if (!configuracao.nome_conta_tiktok.Contains("null") && !configuracao.nome_conta_google.Contains("null"))
                {
                    MessageBox.Show("Faça login no Google para configurar");
                }
                else
                {
                    MessageBox.Show("Aguarde o carregamento");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Redator redator = new Redator(contas, idConta);
            redator.ShowDialog();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Quiz" || comboBox3.Text == "Misto")
            {
                label13.Enabled = true;
                textBox6.Enabled = true;
                label14.Enabled = true;
                textBox7.Enabled = true;
            }
            else
            {
                label13.Enabled = false;
                textBox6.Enabled = false;
                label14.Enabled = false;
                textBox7.Enabled = false;
            }
        }
    }

    public class Configuracao
    {
        public int id_conta { get; set; }
        public string nome_conta_google { get; set; }
        public string nome_conta_tiktok { get; set; }
        public string nicho { get; set; }
        public string idioma { get; set; }
        public string prompt_gerar_noticia { get; set; }
        public string prompt_gerar_imagem { get; set; }
        public string tags { get; set; }
        public string chamada { get; set; }
        public bool conta_ativa { get; set; }
        public bool audio_viral { get; set; }
        public string estilo { get; set; }
        public string prompt_quiz { get; set; }
        public string prompt_quiz_imagem { get; set; }
    }

}
