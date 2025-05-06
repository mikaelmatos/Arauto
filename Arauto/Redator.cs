using Microsoft.Web.WebView2.Core;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit;
using System.Diagnostics;
using static Arauto.Redator;
using Arauto.Util;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;

namespace Arauto
{
    public partial class Redator : Form
    {
        Contas ContasPai;
        int IdContaParam = -1;
        public Redator(Contas ContasPai, int IdContaParam = -1)
        {
            this.IdContaParam = IdContaParam;
            this.ContasPai = ContasPai;
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            List<int> IDsAtivos = new List<int>();

            for (int i = 1; i < 8; i++)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + i + ".mjson"))
                {
                    StreamReader stream = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + i + ".mjson");
                    string conteudoConf = stream.ReadToEnd();
                    stream.Close();

                    Configuracao conf = JsonConvert.DeserializeObject<Configuracao>(conteudoConf);

                    if (conf.conta_ativa)
                    {
                        IDsAtivos.Add(i);
                    }
                }
            }

            if (IDsAtivos.Count == 0)
            {
                MessageBox.Show("Ative pelo menos uma conta");
                this.Close();
            }

            int larguraTela = Screen.PrimaryScreen.Bounds.Width;
            int alturaTela = Screen.PrimaryScreen.Bounds.Height;
            int larguraJanela = this.Width;
            int alturaJanela = this.Height;
            int posX = (larguraTela - larguraJanela) / 2 - (int)(larguraJanela * 1.5);
            int posY = (alturaTela - alturaJanela) / 4;
            this.Location = new Point(posX, posY);

            WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.TopMost = false;
            this.TopMost = true;
            this.Activate();

            int contaPostagem = new Random().Next(1, 8);

            if (IdContaParam > -1)
            {
                contaPostagem = IdContaParam;
            }

            //contaPostagem = 3;// só pra testar
            //contaPostagem = 4;// só pra testar
            //contaPostagem = 7;// só pra testar
            //contaPostagem = 2;// só pra testar

            Configuracao configuracao = null;

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + contaPostagem + ".mjson"))
            {
                StreamReader stream = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + contaPostagem + ".mjson");
                string conteudoConf = stream.ReadToEnd();
                stream.Close();

                configuracao = JsonConvert.DeserializeObject<Configuracao>(conteudoConf);
            }

            while (!configuracao.conta_ativa)
            {
                contaPostagem = new Random().Next(1, 8);

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + contaPostagem + ".mjson"))
                {
                    StreamReader stream = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/configs/conf-" + contaPostagem + ".mjson");
                    string conteudoConf = stream.ReadToEnd();
                    stream.Close();

                    configuracao = JsonConvert.DeserializeObject<Configuracao>(conteudoConf);
                }
            }

            string blackList = "";

            if (File.Exists(Path.GetTempPath() + DateTime.Now.ToString("yyyyMMdd") + "-" + contaPostagem + "blacklist.log"))
            {
                StreamReader streamReader = new StreamReader(Path.GetTempPath() + DateTime.Now.ToString("yyyyMMdd") + "-" + contaPostagem + "blacklist.log");
                string blackListTexto = streamReader.ReadToEnd().Trim().Trim(',').Trim().Trim(',').Trim().Trim(',').Trim().Trim(',');

                if (blackListTexto != "")
                {
                    blackList = "[NÃO FALE sobre os assuntos: " + blackListTexto.ToLower() + "]";
                }

                streamReader.Close();
            }

            DateTime horaIni = DateTime.Now;

            int conta = IDsAtivos[new Random().Next(0, IDsAtivos.Count)];

            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData0" + conta);//1,2 3,4,5 ou 6

            Text = "Usando Conta: " + conta;

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView21.EnsureCoreWebView2Async(environment);

            webView21.CoreWebView2.Navigate("https://www.chatgpt.com");

            webView21.CoreWebView2.NavigationCompleted += async (sender, e) =>
            {

                string checaLogado = "document.getElementsByTagName('button')[3].innerText";

                string resultLogado = await webView21.CoreWebView2.ExecuteScriptAsync(checaLogado);

                if (resultLogado.Contains("Entrar") || resultLogado.Contains("Login"))
                {
                    MessageBox.Show("Faça login com o Google no chatGPT para obter os roteiros e imagens.");
                }
                else
                {


                    Random rand = new Random();
                    int delay = rand.Next(1000, 2000);
                    await Task.Delay(delay);

                    string phrase = "Qual a noticia mais importante do dia, resuma em um paragrafo com titulo e resumo, use texto sem caracteres estranhos e sem citar fonte [apenas texto, sem imagem ou thumb][números e datas por extenso] " + blackList;

                    //conta = 3: jogo da saude (pt-br)
                    //conta = 4: esportes
                    //conta = 5: hora news
                    //conta = 6: atitude positiva
                    //conta = 7: jogo da saude(en - us)

                    Text += " - Conta Postagem: " + contaPostagem;

                    //if (contaPostagem == 3)
                    //{
                    //    phrase = "Quero uma dica de saúde ou curiosidade surpreendente e chamativa, apenas um titulo e um paragrafo " + blackList;
                    //}

                    //if (contaPostagem == 4)
                    //{
                    //    phrase = "Qual a noticia mais importante do dia sobre futebol, resuma em um paragrafo com titulo e resumo, use texto sem caracteres estranhos e sem citar fonte [apenas texto, sem imagem ou thumb][números e datas por extenso] " + blackList;
                    //}

                    //if (contaPostagem == 6)
                    //{
                    //    phrase = "Qual a noticia mais importante do dia sobre famosos, resuma em um paragrafo com titulo e resumo, use texto sem caracteres estranhos e sem citar fonte [apenas texto, sem imagem ou thumb][números e datas por extenso] " + blackList;
                    //}

                    //if (contaPostagem == 7)
                    //{
                    //    phrase = "I want a health tip or fun fact, just a title and a paragraph. " + blackList;
                    //}

                    if (configuracao != null)
                    {
                        phrase = configuracao.prompt_gerar_noticia + " " + blackList;
                    }

                    Clipboard.SetText(phrase);
                    await Task.Delay(500);
                    SendKeys.Send("^v");

                    await Task.Delay(delay);

                    string script = "document.querySelectorAll('button[aria-label=\"Enviar prompt\"]')[0].click();";
                    string result = "";

                    await webView21.ExecuteScriptAsync(script);

                    await Task.Delay(delay);

                    bool liberar1 = true;

                    while (liberar1)
                    {
                        script = "document.getElementsByTagName(\"body\")[0].innerHTML.includes(\"Iniciar modo voz\")";

                        result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                        if (result.Trim().ToLower() == "false")
                        {
                            await Task.Delay(delay);
                        }
                        else
                        {
                            liberar1 = false;
                        }
                    }

                    await Task.Delay(delay);

                    script = "document.getElementsByTagName(\"body\")[0].innerHTML.replace(\"O ChatGPT disse:\",\"¬\").split('¬')[1]";
                    result = await webView21.CoreWebView2.ExecuteScriptAsync(script);
                    result = result.Replace(@"\u003C", "<");

                    result = Regex.Replace(result, "<[^>]+>", "");

                    string titulo = "";
                    string resumo = "";

                    try
                    {
                        titulo = result.Split('\n')[0].Replace("\\n", "\\").Split('\\')[0].Replace("Título: ", "").Replace("Title: ", "").Replace(": ", " ").Trim('"');
                        resumo = result.Split('\n')[0].Trim('"').Replace("\\n", "\\").Split('\\')[1].Replace("\\nParagraph:", "").Replace("Paragraph:", "").Trim('"');

                        try
                        {
                            resumo = resumo.Replace(resumo.Split('.')[resumo.Split('.').Count() - 1], "");
                            resumo = resumo.Replace("Você gosta desta personalidade?", "");
                        }
                        catch (Exception ex) { }

                        if (titulo == "")
                        {
                            titulo = result.Split('\n')[0].Trim('\\').Trim('"').Replace("\\n", "\\").Split('\\')[1].Replace("\\nParagraph:", "").Replace("Paragraph:", "").Trim('\\').Trim('"');
                        }

                        if (resumo == "")
                        {
                            resumo = result.Replace(titulo, "").Replace("\\nParagraph:", "").Replace("Paragraph:", "").Replace("Título: ", "").Replace("Title: ", "").Replace("\\n", "").Replace("\\\"", "").Trim('"').Trim('\n').Trim().Trim('"').Trim('\n').Trim().Trim('"').Trim('\n').Trim();
                        }

                        if (resumo.Contains("4owindow"))
                        {
                            resumo = resumo.Replace("4owindow", "¬").Split('¬')[0];
                        }

                        if (resumo.Contains("window"))
                        {
                            resumo = resumo.Replace("window", "¬").Split('¬')[0];
                        }

                        //if (contaPostagem == 3)
                        //{
                        //    resumo = resumo + " Compartilhe este vídeo, deixe seu like e entre no jogo da saúde";
                        //}

                        //if (contaPostagem == 4)
                        //{
                        //    resumo = resumo + " Compartilhe este vídeo, deixe seu like";
                        //}

                        //if (contaPostagem == 7)
                        //{
                        //    resumo = resumo + " Share this video, leave your like.";
                        //}

                        if (configuracao != null)
                        {
                            resumo = resumo + " " + configuracao.chamada;
                        }

                        if (titulo.Contains("+"))
                        {
                            titulo = titulo.Split("+")[0];
                        }

                        titulo = RemoverCaracteresInvalidos(titulo);
                    }
                    catch (Exception erro)
                    {
                        ContasPai.LoopPostagem();
                        //Application.Restart();
                    }

                    await Task.Delay(delay * 4);

                    phrase = "Gere uma imagem sobre a noticia apenas com o titulo e uma chamada, com um fundo atras desfocado que esteja relacionado ao tema";

                    //if (contaPostagem == 3)
                    //{
                    //    phrase = "Gere uma imagem sobre isso no estilo videogame antigo [não precisa usar o texto todo se for muito longo, foco na imagem e criatividade, faça estilo pixel art, com os pixels do mesmo tamanho]";
                    //}

                    //if (contaPostagem == 4)
                    //{
                    //    phrase = "Gere uma imagem sobre a noticia apenas com o titulo e uma chamada, que esteja relacionado ao tema, use os símbolos dos times ou outros recursos visuais criativos, use estética futebolística, no estilo pixel art com todos os pixels do mesmo tamanho";
                    //}

                    //if (contaPostagem == 7)
                    //{
                    //    phrase = "Gere uma imagem sobre isso no estilo videogame antigo [não precisa usar o texto todo se for muito longo, foco na imagem e criatividade, faça estilo pixel art, com os pixels do mesmo tamanho]";
                    //}

                    if (configuracao != null)
                    {
                        phrase = configuracao.prompt_gerar_imagem + " Se houver textos que seja no idioma " + configuracao.idioma;
                    }

                    script = "document.getElementsByTagName(\"body\")[0].innerHTML.includes(\"feedback\")";

                    result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                    if (result.Trim().ToLower() == "true")
                    {
                        //ContasPai.LoopPostagem();
                        //Application.Restart();

                        titulo = titulo.Replace("ChatGPTResposta 1", "¬").Split('¬')[1];
                    }

                    SendKeys.Send(phrase);
                    await Task.Delay(delay);

                    script = "document.querySelectorAll('button[aria-label=\"Enviar prompt\"]')[0].click();";
                    await webView21.ExecuteScriptAsync(script);

                    await Task.Delay(delay * 2);

                    bool liberar = true;

                    while (liberar)
                    {
                        script = @"document.getElementsByTagName(""body"")[0].innerHTML.includes('aria-label=""Gerando imagem…""')";
                        result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                        if (result.Trim().ToLower() == "true")
                        {
                            await Task.Delay(delay);
                        }
                        else
                        {
                            liberar = false;
                        }
                    }

                    liberar1 = true;

                    while (liberar1)
                    {
                        script = "document.getElementsByTagName(\"body\")[0].innerHTML.includes(\"Iniciar modo voz\")";

                        result = await webView21.CoreWebView2.ExecuteScriptAsync(script);

                        if (result.Trim().ToLower() == "false")
                        {
                            await Task.Delay(delay);
                        }
                        else
                        {
                            liberar1 = false;
                        }
                    }

                    await Task.Delay(delay * 10);

                    script = "document.getElementsByTagName(\"body\")[0].innerHTML.replace(\"O ChatGPT disse:\",\"¬\").split('¬')[1]";
                    string imagem = "";

                    try
                    {
                        result = await webView21.CoreWebView2.ExecuteScriptAsync(script);
                        result = result.Replace(@"\u003C", "<");
                        result = result.Replace("\\", "");
                        imagem = result.Replace("src=", "¬").Split('¬')[1].Split('"')[1].Replace("&amp;", "&");

                        if (imagem.Contains("favicon"))
                        {
                            imagem = result.Replace("src=", "¬").Split('¬')[2].Split('"')[1].Replace("&amp;", "&");
                        }

                        if (!imagem.Contains("https"))
                        {
                            string scriptErro = "document.getElementsByTagName(\"body\")[0].innerHTML.includes(\"limite de criação\")";
                            string resultErro = await webView21.CoreWebView2.ExecuteScriptAsync(scriptErro);


                            //File.AppendAllText("REGISTRONAOPOSTADOS.TXT", contaPostagem + ";" + titulo.ToUpper() + ";" + resumo + ";" + 0 + ";" + "problema-ao-gerar-imagem;" + DateTime.Now + "\r\n");
                            //Application.Exit();
                            ContasPai.LoopPostagem();
                        }
                    }
                    catch
                    {
                        string scriptErro = "document.getElementsByTagName(\"body\")[0].innerHTML.includes(\"limite de criação\")";
                        string resultErro = await webView21.CoreWebView2.ExecuteScriptAsync(scriptErro);

                        ContasPai.LoopPostagem();
                        //Application.Restart();
                    }

                    Postagem postagem = new Postagem();
                    postagem.Titulo = titulo;
                    postagem.Resumo = resumo;
                    postagem.Imagem = imagem;
                    postagem.DataHora = DateTime.Now;
                    postagem.DataInicio = horaIni;

                    string json = System.Text.Json.JsonSerializer.Serialize(postagem, new JsonSerializerOptions { WriteIndented = true });

                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/json"))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/json");
                    }

                    File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/json/" + titulo.ToUpper() + ".json", CorrigirJson(json));

                    if (!await GerarWav2(ConversorNumeros.SubstituirNumerosPorExtenso(titulo + "! " + resumo.Replace(".", "!")), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao/" + titulo.ToUpper() + ".wav"))
                    {
                        string idioma = (configuracao != null) ? configuracao.idioma : "Português";

                        if (idioma == "Português")
                        {
                            GerarWav(titulo + "! " + resumo.Replace(".", "!"), titulo.ToUpper() + ".wav");
                        }
                        else
                        {
                            GerarWavEn(titulo + "! " + resumo.Replace(".", "!"), titulo.ToUpper() + ".wav");
                        }
                    }

                    try
                    {
                        await CriarVideoComAudioEImagemAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao/" + titulo.ToUpper() + ".wav", imagem, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/video-bruto", titulo.ToUpper());
                    }
                    catch
                    {
                        //File.AppendAllText("REGISTRONAOPOSTADOS.TXT", contaPostagem + ";" + postagem.Titulo.ToUpper() + ";" + postagem.Resumo + ";" + 0 + ";" + "problema-ao-gerar-video;" + DateTime.Now + "\r\n");

                        //Application.Exit();
                        ContasPai.LoopPostagem();
                    }

                    string caminhoVideo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/video-bruto", titulo.ToUpper() + ".mp4");

                    if (File.Exists(caminhoVideo))
                    {
                        Postador postador = new Postador(postagem, contaPostagem, this);
                        postador.Show();
                    }
                }
            };
        }
        public static string RemoverCaracteresInvalidos(string nome)
        {
            char[] caracteresInvalidos = Path.GetInvalidFileNameChars();
            string regex = $"[{Regex.Escape(new string(caracteresInvalidos))}]";
            return Regex.Replace(nome, regex, "");
        }
        public void LoopPostagem(Postagem postagem)
        {
            MatarNavegadoresObsoletos.KillUnusedWebView2Processes();
            ContasPai.LoopPostagem();
            this.Close();
        }

        public static async Task<string> CriarVideoComAudioEImagemAsync(string caminhoAudio, string urlImagem, string caminhoSaida, string nomeSaida)
        {
            Directory.CreateDirectory(caminhoSaida);

            string imagemLocal = Path.Combine(Path.GetTempPath(), "imagem.jpg");
            using (HttpClient client = new HttpClient())
            {
                var imagemBytes = await client.GetByteArrayAsync(urlImagem);
                await File.WriteAllBytesAsync(imagemLocal, imagemBytes);
            }

            string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");

            string caminhoVideo = Path.Combine(caminhoSaida, nomeSaida + ".mp4");

            string argumentos = $"-loop 1 -i \"{imagemLocal}\" -i \"{caminhoAudio}\" -c:v libx264 -tune stillimage -c:a aac -b:a 192k -shortest -y \"{caminhoVideo}\"";

            using (Process process = new Process())
            {
                process.StartInfo.FileName = ffmpegPath;
                process.StartInfo.Arguments = argumentos;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Erro ao gerar vídeo: {output}");
                }
            }

            return caminhoVideo;
        }
        private const string ApiUrl = "https://api.elevenlabs.io/v1/text-to-speech/";

        public static async Task<bool> GerarWav2(string texto, string caminhoArquivoSaida)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao");
            }

            #region
            string[] chaves = new string[91];
            chaves[0] = "sk_ec8ae644a856030565a24f6b48c2a1177c0e8b86b12a28dd";//mikaelroquematos
            chaves[1] = "sk_83b1bdf1bd31ec72410974ad278a1e10191fad68009114a5";//robokwai3
            chaves[2] = "sk_727c1ff47bcf7f9c1cb6ebc66439de43ddc0e80f8d910f8d";//robokwai2
            chaves[3] = "sk_2830f11f30e4a2aa94bc3acdc35ab9812c6e4e1e9ab457be";//robokwai6
            chaves[4] = "sk_d0bd7e364739e2c25fa6ec7daf05dd05d039e47c207e6da5";//continueanadar
            chaves[5] = "sk_aa85ae779004c3c5dad24d4877126eb47f09f762a2a1f8cd";//mikaelroquematos
            chaves[6] = "sk_cf285ba8cbccb2e7d47440367c0e5227cae55ac4175c398f";//atitude
            chaves[7] = "sk_9ca578e537d8ba3bdb3a1d74a4357646b854685d76680c12"; //mikael.14
            chaves[8] = "sk_82502e28e0ccebef35cda1e3cb8bc58618f965bec79cb268"; //robokwai
            chaves[9] = "sk_f2ca5c1b48973ccff1d11e293b73652f94149da8d970bfe5"; //robokwai4

            chaves[10] = "sk_e3ba57dc9be43057f55c2013ee0f38200b17ce97f6b8014f"; //Descartavel
            chaves[11] = "sk_aadccaf574003b8f73d8db4320b0d403def08375bd34f8a6"; //Descartavel
            chaves[12] = "sk_7484f1f54e438fa7dc661c51e4ab3258c2ef91adb28529ec"; //Descartavel
            chaves[13] = "sk_7e2abeb66db9f1a99cd2f96484050efd89cd8d5b42ba6f0b"; //Descartavel

            chaves[14] = "sk_98d0a2cf97301d7483c4e1128348d751e0af5c9cca03bd93"; //Descartavel
            chaves[15] = "sk_50fc4629e72c4b4e7f266adb3e5c3df6e536bff8c9eb6d87"; //Descartavel
            chaves[16] = "sk_c76abbe32411bfa5db1c8cf9dd3bfd9687d2a417ea5dd50e"; //Descartavel
            chaves[17] = "sk_98d0a2cf97301d7483c4e1128348d751e0af5c9cca03bd93"; //Descartavel
            chaves[18] = "sk_e65d38f4f44b0cfae5a3050e8abce1445d0412e7853d5593"; //Descartavel
            chaves[19] = "sk_4ae2222aa30d980b8b2152300a782fa2fe793b0d8770296e"; //Descartavel
            chaves[20] = "sk_cfe2aff2abfde4376ab06525b3c7e587db0f18557d543df5"; //Descartavel
            chaves[21] = "sk_677688d1eace38a9abdca3965c54c0a342c5cc7f49949bf1"; //Descartavel
            chaves[22] = "sk_1c78b988a85b99719d7620a3d483f6115529503c4521ea61"; //Descartavel
            chaves[23] = "sk_ca3851459bf8407e734a42aeb3b00ed8299f4f25e9f51c01"; //Descartavel
            chaves[24] = "sk_11e9bbba8c2d46c4baafee2328c9ce7a3a66bd4bebb81c0b"; //Descartavel
            chaves[25] = "sk_8918991ed441ae35890ce0104cc746bc3d8c50a13d3e1591"; //Descartavel
            chaves[26] = "sk_85b269d4a9b06ff68ff3347e70a128b71fb6e84d6b211037"; //Descartavel
            chaves[27] = "sk_e878b474dc35d49673fc6d8e61425879f8b49dcc226fa2c4"; //Descartavel
            chaves[28] = "sk_7109ef1a18cd3c89887f08ccbf666e090886757ebef278be"; //Descartavel
            chaves[29] = "sk_ef624fb43e4d00f7aa0aed482f4277b160489a3a292b748d"; //Descartavel
            chaves[30] = "sk_02ed75d1033ddd86ab4f93543eebe2219bd8e4de17858ddf"; //Descartavel
            chaves[31] = "sk_45568fca0ed5eeaa5a6e437d4525c3c1a9e2db861ff753aa"; //Descartavel
            chaves[32] = "sk_981a7d1667103240658f7f0de5c066d335c85b4cadc641bb"; //Descartavel
            chaves[33] = "sk_80f39235b7085b3bd220410b1fb687bddaa97aaa999e75c3"; //Descartavel
            chaves[34] = "sk_ba8590825b1dfb4ab7682ba3ddcd7278f6dd2b8064f79ec9"; //Descartavel
            chaves[35] = "sk_416a4b6e61a0f829d9d535a479e86f947418fc1bd36e5ef4"; //Descartavel
            chaves[36] = "sk_0f9e25a83d11a83a110668a553496f88a73b71577867d37d"; //Descartavel
            chaves[37] = "sk_8f377f16948a90ec9714bd6903eeec447f0f6a35b41ebe0e"; //Descartavel
            chaves[38] = "sk_71a36ed6b3f17868f482029bd65bed7bd4b2f3c60576731d"; //Descartavel
            chaves[39] = "sk_f5ea038c2440a62a575eef1caf6aba9959fa925dc7aa9dcb"; //Descartavel
            chaves[40] = "sk_921aa4a381b09870cf26eb51cfac3b3085c43bccf019be0b"; //Descartavel
            chaves[41] = "sk_3a191cb338e1082d036733690bf2db2b9a9c68984f5073ca"; //Descartavel
            chaves[42] = "sk_8d771f83346af3bd1e19189017aca4c1d80622464e3b345d"; //Descartavel
            chaves[43] = "sk_5aceaac2860242baa6f4f6d44e18bc65da119c05970eb896"; //Descartavel
            chaves[44] = "sk_2b3f8343636f4d0acd4b80af31b42b024e24df87816f0791"; //Descartavel
            chaves[45] = "sk_126aed54fa44d84741ee9ba885884827a7b481e0c72b1f8a"; //Descartavel
            chaves[46] = "sk_d96b77d85aec322c23c7b5a4c5806669b2e2fa82a10eb0d5"; //Descartavel
            chaves[47] = "sk_0676c3bb0a3a1c492fc4a95638da2eeab7ce58d581895353"; //Descartavel
            chaves[48] = "sk_be6cabb3a4f56a234995def96a9c4b6742b64f408bbf2ee6"; //Descartavel
            chaves[49] = "sk_9de81b070ac477adcdb6bcd61609c7bef7e56bcf8c1a76a0"; //Descartavel
            chaves[50] = "sk_25ab061424957be8c86ea351b7a14d3754819f9c88e9e1a9"; //Descartavel
            chaves[51] = "sk_c9c8a2696b4aba118a547d6be72bfc57a834f060ce750671"; //Descartavel
            chaves[52] = "sk_1372aa865f25cf1bd39f60952d953b8089651c777c1ce1d2"; //Descartavel
            chaves[53] = "sk_f796dbcf54e83016ab78fe8669988ad8ba37532bbdddfbd4"; //Descartavel
            chaves[54] = "sk_2e41f607776c3f66eb9cdbea3abd3e591215f4fd812809a4"; //Descartavel
            chaves[55] = "sk_69d34760c7d79a9fb2afc47d2149c7d0cee327f277b86001"; //Descartavel
            chaves[56] = "sk_b7115c6f395af43583a7d8f22ed4e6e9416b48fd2c8fd838"; //Descartavel
            chaves[57] = "sk_10b37950512c20bae476d6dd52bd0ca2e153c073a3851565"; //Descartavel
            chaves[58] = "sk_8a1aee6ccf01b9a5a294c5b6998c092d31c62c0d5f5256f3"; //Descartavel
            chaves[59] = "sk_7573054e7236152c341eb5a8112b4fe9e948e6043a0cf50b"; //Descartavel
            chaves[60] = "sk_bf14c56b5b4d8d85a13edf58f6169e0967d9acb5d32aca84"; //Descartavel
            chaves[61] = "sk_4d61743004be32985098e5a197650b668f487059fc58360a"; //Descartavel
            chaves[62] = "sk_72b8171e56728ea65a4c4a56eb87e4b3dd8c876d8c96bc16"; //Descartavel
            chaves[63] = "sk_ac3fb968e4a3e15a4394c66b9c6d2ca32da71e28f0dfabf2"; //Descartavel
            chaves[64] = "sk_15901cebc7852c1573b8dfbfe928eb5666456a464eec175b"; //Descartavel
            chaves[65] = "sk_5895d020596c83a78d0e3405939438e904d9800c1dc9d66d"; //Descartavel
            chaves[66] = "sk_77e7570371b54a21037e8a30115813116baeb902dd5d0415"; //Descartavel
            chaves[67] = "sk_5e51445c7952fb0ace70a04fe9d0239a6707b9f3abc3d2de"; //Descartavel
            chaves[68] = "sk_925f5b0467bc2ef99bef70b9d30a4d3a0537d1af53ea28e6"; //Descartavel
            chaves[69] = "sk_5f0407984903605692726188104a57e39a48567e0ea1e226"; //Descartavel
            chaves[70] = "sk_d2998ca5ed89dce701a1843f6ca0fb7b3183b03bff70e3fa"; //Descartavel
            chaves[71] = "sk_e23fed9511289b8cd1dd9d74ee6e996168d592dc7fcefcb6"; //Descartavel
            chaves[72] = "sk_5255b95ea87f391e4d01165bb758af6373d150993161f687"; //Descartavel
            chaves[73] = "sk_f760939ae407e61979f4f0e53b5c7a0d206f24ed88cb112f"; //Descartavel
            chaves[74] = "sk_f8870d2d4ce3126d78121a3fff871e630054f2a4d1b37bbf"; //Descartavel
            chaves[75] = "sk_df55a3db15eae9e9067ad2019d2fc5f604309f000aedca44"; //Descartavel
            chaves[76] = "sk_d1f55ee0877b34704ecc6d4e35ac7531836c9a778e81261d"; //Descartavel
            chaves[77] = "sk_e024b03f13e845c5dcb4a39024bef2708df4a44b8659e373"; //Descartavel
            chaves[78] = "sk_aa0baa115b7a1771b5125a03d723203839fbdb7d2601a4eb"; //Descartavel
            chaves[79] = "sk_5e8a11ec9399cef700e0f4c40e5bbedd71fdbcc7ce5b2829"; //Descartavel
            chaves[80] = "sk_193b4467ad525ee807416a49777bc3c934bf8c85c5e9ceaa"; //Descartavel
            chaves[81] = "sk_7320836c8ae7e73e1b296f84ba64213978b92de7056de313"; //Descartavel
            chaves[82] = "sk_b96416e93ea1a19387c955078a81c801a8d06ee5de33ca68"; //Descartavel
            chaves[83] = "sk_8c7360943fb3741e852fb71a071bd887d7df954bd6480d72"; //Descartavel
            chaves[84] = "sk_b312b33548f197a32af576ea6998d360e76e9484df2574b8"; //Descartavel
            chaves[85] = "sk_f6672519226b99a2c766ef0c01cac8c016582465f042af3e"; //Descartavel
            chaves[86] = "sk_3b8d46f351a6ac93a323b569ed52c0104fb71dc792d85752"; //Descartavel
            chaves[87] = "sk_192dc010e35cee35c6cc251a675b2ea019914ceec26ed9c6"; //Descartavel
            chaves[88] = "sk_fb00c73a286ed97e50946d099b5fff115599aa0a45427385"; //Descartavel
            chaves[89] = "sk_449e2ecd45f023bdf376f95af4ad24a3399c22688a3843f2"; //Descartavel
            chaves[90] = "sk_f1d667dc732ff26080cfdbe6657e2d95daa81951db91bf1d"; //Descartavel
            #endregion

            int indice = new Random().Next(0, 91);

            string ApiKey = chaves[indice]; //Depois botar todas e incluir mais

            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("xi-api-key", ApiKey);

                var payload = new
                {
                    text = texto,
                    model_id = "eleven_multilingual_v1",
                    voice_settings = new
                    {
                        stability = 0.5,
                        similarity_boost = 0.75
                    }
                };

                string[] vozes = new string[4];
                vozes[0] = "ErXwobaYiN019PkySvjV"; //Antônio
                vozes[1] = "ErXwobaYiN019PkySvjV"; //Thiago
                vozes[2] = "EXAVITQu4vr4xnSDxMaL"; //Camila
                vozes[3] = "EXAVITQu4vr4xnSDxMaL"; //Amélia

                string VoiceId = vozes[new Random().Next(0, 4)];

                var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{ApiUrl}{VoiceId}", jsonContent);
                response.EnsureSuccessStatusCode();

                var audioBytes = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(caminhoArquivoSaida, audioBytes);

                Console.WriteLine($"Áudio salvo em: {caminhoArquivoSaida}");

                return true;
            }
            catch (Exception erro)
            {
                return false;
            }
        }
        public void GerarWav(string texto, string caminhoWav, int velocidade = 2, int volume = 100, string nomeVoz = null)
        {
            dynamic voz = Activator.CreateInstance(Type.GetTypeFromProgID("SAPI.SpVoice"));
            dynamic stream = Activator.CreateInstance(Type.GetTypeFromProgID("SAPI.SpFileStream"));

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao");
            }

            const int SSFMCreateForWrite = 3;
            stream.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao/" + caminhoWav, SSFMCreateForWrite, false);

            voz.AudioOutputStream = stream;
            voz.Rate = velocidade;
            voz.Volume = volume;

            if (!string.IsNullOrEmpty(nomeVoz))
            {
                foreach (var v in voz.GetVoices())
                {
                    if (v.GetDescription().Contains(nomeVoz, StringComparison.OrdinalIgnoreCase))
                    {
                        voz.Voice = v;
                        break;
                    }
                }
            }

            voz.Speak(texto);
            stream.Close();
        }
        public void GerarWavEn(string texto, string caminhoWav, int velocidade = 2, int volume = 100, string nomeVoz = null)
        {
            dynamic voz = Activator.CreateInstance(Type.GetTypeFromProgID("SAPI.SpVoice"));
            dynamic stream = Activator.CreateInstance(Type.GetTypeFromProgID("SAPI.SpFileStream"));

            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao");
            }

            const int SSFMCreateForWrite = 3;
            stream.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao/" + caminhoWav, SSFMCreateForWrite, false);

            voz.AudioOutputStream = stream;
            voz.Rate = velocidade;
            voz.Volume = volume;

            // Seleciona uma voz em inglês, se nomeVoz não for informado
            foreach (var v in voz.GetVoices())
            {
                string desc = v.GetDescription();
                if (!string.IsNullOrEmpty(nomeVoz))
                {
                    if (desc.Contains(nomeVoz, StringComparison.OrdinalIgnoreCase))
                    {
                        voz.Voice = v;
                        break;
                    }
                }
                else if (desc.Contains("English", StringComparison.OrdinalIgnoreCase))
                {
                    voz.Voice = v;
                    break;
                }
            }

            voz.Speak(texto);
            stream.Close();
        }

        public static string CorrigirJson(string json)
        {
            string jsonDecodificado = Regex.Unescape(json);

            jsonDecodificado = jsonDecodificado.Replace("\\u0026", "&");

            try
            {
                using var doc = JsonDocument.Parse(jsonDecodificado);
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine("Erro ao validar JSON: " + ex.Message);
            }

            return jsonDecodificado;
        }

        public class Postagem
        {
            public string Titulo { get; set; }
            public string Resumo { get; set; }
            public string Imagem { get; set; }
            public DateTime DataInicio { get; set; }
            public DateTime DataHora { get; set; }
        }
    }
}
