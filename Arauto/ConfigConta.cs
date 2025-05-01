using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            string blackList = "";

            if (File.Exists(DateTime.Now.ToString("yyyyMMdd") + "-" + idConta + "blacklist.log"))
            {
                StreamReader streamReader = new StreamReader(DateTime.Now.ToString("yyyyMMdd") + "-" + idConta + "blacklist.log");
                string blackListTexto = streamReader.ReadToEnd().Trim().Trim(',').Trim().Trim(',').Trim().Trim(',').Trim().Trim(',');

                if (blackListTexto != "")
                {
                    textBox3.Text = "* " + blackListTexto.Replace(", ", "\r\n* ");
                }

                streamReader.Close();
            }


            string conteudoConf = "";

            if (!Directory.Exists("configs"))
            {
                Directory.CreateDirectory("configs");
            }

            if (File.Exists("configs/conf-" + idConta + ".mjson"))
            {
                StreamReader stream = new StreamReader("configs/conf-" + idConta + ".mjson");
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

                label4.Text = result.Replace("Bem-vindo,", "").Replace("Bem-vinda,", "").Trim().Trim('"').Trim().Trim('"');

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
                    textBox4.Text = result.Replace("@", "#").Replace("\"", "");
                }

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

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            if (!configuracao.nome_conta_google.Contains("Carregando") && !configuracao.nome_conta_tiktok.Contains("Carregando"))
            {
                if (configuracao.prompt_gerar_noticia.Length > 50 && configuracao.prompt_gerar_imagem.Length > 50)
                {
                    if (File.Exists("configs/conf-" + idConta + ".mjson"))
                    {
                        File.Move("configs/conf-" + idConta + ".mjson", "configs/conf-" + idConta + "_old.mjson");
                    }

                    string json = System.Text.Json.JsonSerializer.Serialize(configuracao, options);
                    File.AppendAllText("configs/conf-" + idConta + ".mjson", json);

                    if (File.Exists("configs/conf-" + idConta + "_old.mjson"))
                    {
                        File.Delete("configs/conf-" + idConta + "_old.mjson");
                    }
                }
                else
                {
                    MessageBox.Show("Os prompts precisam ter no mínimo 50 caracteres");
                }

                MessageBox.Show("Salvo! " + "configs/conf-" + idConta + ".mjson");
            }
            else
            {
                MessageBox.Show("Aguarde o carregamento");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Redator redator = new Redator(contas, idConta);
            redator.ShowDialog();
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
    }

}
