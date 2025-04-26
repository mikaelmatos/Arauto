using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Arauto.Redator;
using static System.Net.Mime.MediaTypeNames;

namespace Arauto
{
    public partial class Postador : Form
    {
        Postagem postagem = null;
        Redator redatorPai = null;
        int contaPostagem = -1;
        public Postador(Postagem postagem, int contaPostagem, Redator redatorPai)
        {
            this.postagem = postagem;
            InitializeComponent();
            this.contaPostagem = contaPostagem;
            this.redatorPai = redatorPai;
        }

        private void InitializeComponent()
        {
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(12, 12);
            webView21.Name = "webView21";
            webView21.Size = new Size(1308, 611);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // Postador
            // 
            ClientSize = new Size(1332, 635);
            Controls.Add(webView21);
            Name = "Postador";
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            Load += Postador_Load;
            ((ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
        }

        int nTentativa = 0;

        private async void Postador_Load(object sender, EventArgs e)
        {
            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData0" + contaPostagem);

            Text = "Usando conta: " + contaPostagem;

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView21.EnsureCoreWebView2Async(environment);

            webView21.CoreWebView2.Navigate("https://www.tiktok.com/tiktokstudio/upload?from=webapp");

            webView21.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {
                if (await Postar())
                {
                    nTentativa = 1;
                    Text = "Postado com sucesso! na tentativa Nº " + nTentativa;
                }
                else
                {
                    nTentativa = 2;

                    SendKeys.Send("{PGDN}");

                    string script = "document.querySelectorAll('button[data-e2e=\"post_video_button\"]')[0].click();";

                    await webView21.ExecuteScriptAsync(script);

                    File.AppendAllText(DateTime.Now.ToString("yyyyMMdd") + "-" + contaPostagem + "blacklist.log", postagem.Titulo.ToUpper() + ", ");
                    Text = "Postado!";

                    await Task.Delay(25000);

                    script = "window.location.href";
                    string result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                    if (result.Contains("https://www.tiktok.com/tiktokstudio/content"))
                    {
                        Text = "Postado com sucesso! na tentativa Nº " + nTentativa;
                    }
                    else
                    {
                        nTentativa = 3;

                        SendKeys.Send("{PGDN}");

                        script = "document.querySelectorAll('button[data-e2e=\"post_video_button\"]')[0].click();";

                        await webView21.ExecuteScriptAsync(script);

                        File.AppendAllText(DateTime.Now.ToString("yyyyMMdd") + "-" + contaPostagem + "blacklist.log", postagem.Titulo.ToUpper() + ", ");
                        Text = "Postado!";

                        await Task.Delay(25000);

                        script = "window.location.href";
                        result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                        if (result.Contains("https://www.tiktok.com/tiktokstudio/content"))
                        {
                            Text = "Postado com sucesso! na tentativa Nº " + nTentativa;
                        }
                        else
                        {
                            MessageBox.Show("Não postado mesmo com " + nTentativa + " tentativas");
                        }
                    }
                }
                await Task.Delay(5000);

                File.AppendAllText(DateTime.Now.ToString("yyyyMMdd") + "-" + contaPostagem + "blacklist.log", postagem.Titulo.ToUpper() + ", ");


                redatorPai.PostarKwai(postagem);

                this.Close();
            };
        }
        public async Task<bool> Postar()
        {
            try
            {
                string script = "document.getElementsByTagName(\"body\")[0].innerHTML.includes(\"Quando publicar\")";
                string result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                Random rand = new Random();
                int delay = rand.Next(2000, 3000);
                await Task.Delay(delay);


                script = "document.getElementsByClassName('upload-stage-btn')[0].click();";
                await webView21.ExecuteScriptAsync(script);

                string pastaAtual = AppDomain.CurrentDomain.BaseDirectory.Replace("/", "\\") + "video-bruto\\";
                await Task.Delay(2000);

                SendKeys.Send(pastaAtual + postagem.Titulo.ToUpper() + ".mp4");
                await Task.Delay(400);

                SendKeys.Send("{ENTER}");
                await Task.Delay(delay);

                script = "document.querySelectorAll('input[type=\"radio\"]')[0].click();";
                await webView21.ExecuteScriptAsync(script);

                await Task.Delay(delay * 4);


                script = "document.querySelectorAll('button[aria-label=\"Hashtag\"]')[0].click();";
                await webView21.ExecuteScriptAsync(script);
                await Task.Delay(delay);

                if (contaPostagem == 3)
                {
                    SendKeys.Send("jogodasaude #treino #dieta");
                }
                else
                {
                    SendKeys.Send("noticia #tiktoknews");
                }

                await Task.Delay(100);
                SendKeys.Send("{ENTER}");
                await Task.Delay(100);
                SendKeys.Send("{ENTER}");
                await Task.Delay(delay);
                Clipboard.SetText(postagem.Resumo);
                await Task.Delay(100);

                if (!Clipboard.GetText().Contains("Arauto"))
                {
                    SendKeys.Send("^v"); await Task.Delay(delay);
                }

                Text = "Postando!";

                script = "document.querySelectorAll('button[data-e2e=\"post_video_button\"]')[0].click();";

                await webView21.ExecuteScriptAsync(script);

                Text = "Postado!";

                await Task.Delay(25000);

                script = "window.location.href";
                result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                if (result.Contains("https://www.tiktok.com/tiktokstudio/content"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                string script = "window.location.href";
                string result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                if (result.Contains("https://www.tiktok.com/tiktokstudio/content"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    }
}
