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
            //Postagem postagem = new Postagem();
            //postagem.Titulo = "Falecimento do Papa Francisco marca o dia 21 de abril de 2025";
            //postagem.Resumo = "O Papa Francisco faleceu nesta segunda-feira, 21 de abril de 2025, aos 88 anos, na residência Domus Sanctae Marthae, no Vaticano. A causa da morte foi um acidente vascular cerebral que resultou em insuficiência cardíaca irreversível. O funeral será realizado na Basílica de Santa Maria Maior, em Roma, conforme desejo do pontífice. Este evento encerra um papado de 12 anos e inicia o processo para a escolha de seu sucessor, com o conclave podendo começar a partir de 6 de maio.";
            //postagem.Imagem = "";

            //Postador postador = new Postador(postagem,4);
            //postador.Show();

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

            if (File.Exists("configs/conf-" + contaPostagem + ".mjson"))
            {
                StreamReader stream = new StreamReader("configs/conf-" + contaPostagem + ".mjson");
                string conteudoConf = stream.ReadToEnd();
                stream.Close();

                configuracao = JsonConvert.DeserializeObject<Configuracao>(conteudoConf);
            }

            while (!configuracao.conta_ativa)
            {
                contaPostagem = new Random().Next(1, 8);

                if (File.Exists("configs/conf-" + contaPostagem + ".mjson"))
                {
                    StreamReader stream = new StreamReader("configs/conf-" + contaPostagem + ".mjson");
                    string conteudoConf = stream.ReadToEnd();
                    stream.Close();

                    configuracao = JsonConvert.DeserializeObject<Configuracao>(conteudoConf);
                }
            }

            string blackList = "";

            if (File.Exists(DateTime.Now.ToString("yyyyMMdd") + "-" + contaPostagem + "blacklist.log"))
            {
                StreamReader streamReader = new StreamReader(DateTime.Now.ToString("yyyyMMdd") + "-" + contaPostagem + "blacklist.log");
                string blackListTexto = streamReader.ReadToEnd().Trim().Trim(',').Trim().Trim(',').Trim().Trim(',').Trim().Trim(',');

                if (blackListTexto != "")
                {
                    blackList = "[NÃO FALE sobre os assuntos: " + blackListTexto.ToLower() + "]";
                }

                streamReader.Close();
            }

            DateTime horaIni = DateTime.Now;

            int conta = new Random().Next(1, 8);

            string userDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WebView2UserData0" + conta);//1,2 3,4,5 ou 6

            Text = "Usando Conta: " + conta;

            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolder);

            await webView21.EnsureCoreWebView2Async(environment);

            webView21.CoreWebView2.Navigate("https://www.chatgpt.com");

            webView21.CoreWebView2.NavigationCompleted += async (sender, e) =>
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

                    resumo = resumo.Replace(resumo.Split('.')[resumo.Split('.').Count() - 1], "");
                    resumo = resumo.Replace("Você gosta desta personalidade?", "");

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

                    titulo = RemoverCaracteresInvalidos(titulo);
                }
                catch
                {
                    Application.Restart();
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
                    Application.Restart();
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
                        File.AppendAllText("REGISTRONAOPOSTADOS.TXT", contaPostagem + ";" + titulo.ToUpper() + ";" + resumo + ";" + 0 + ";" + "problema-ao-gerar-imagem;" + DateTime.Now + "\r\n");
                        Application.Exit();
                    }
                }
                catch
                {
                    Application.Restart();
                }

                Postagem postagem = new Postagem();
                postagem.Titulo = titulo;
                postagem.Resumo = resumo;
                postagem.Imagem = imagem;
                postagem.DataHora = DateTime.Now;
                postagem.DataInicio = horaIni;

                string json = System.Text.Json.JsonSerializer.Serialize(postagem, new JsonSerializerOptions { WriteIndented = true });

                if (!Directory.Exists("json"))
                {
                    Directory.CreateDirectory("json");
                }

                File.AppendAllText("json/" + titulo.ToUpper() + ".json", CorrigirJson(json));

                if (!await GerarWav2(ConversorNumeros.SubstituirNumerosPorExtenso(titulo + "! " + resumo.Replace(".", "!")), "narracao/" + titulo.ToUpper() + ".wav"))
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
                    await CriarVideoComAudioEImagemAsync("narracao/" + titulo.ToUpper() + ".wav", imagem, "video-bruto", titulo.ToUpper());
                }
                catch
                {
                    File.AppendAllText("REGISTRONAOPOSTADOS.TXT", contaPostagem + ";" + postagem.Titulo.ToUpper() + ";" + postagem.Resumo + ";" + 0 + ";" + "problema-ao-gerar-video;" + DateTime.Now + "\r\n");

                    Application.Exit();
                }

                string caminhoVideo = Path.Combine("video-bruto", titulo.ToUpper() + ".mp4");

                if (File.Exists(caminhoVideo))
                {
                    Postador postador = new Postador(postagem, contaPostagem, this);
                    postador.Show();
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
            if (!Directory.Exists("narracao"))
            {
                Directory.CreateDirectory("narracao");
            }

            string[] chaves = new string[14];
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

            int indice = new Random().Next(10, 14);

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

            if (!Directory.Exists("narracao"))
            {
                Directory.CreateDirectory("narracao");
            }

            const int SSFMCreateForWrite = 3;
            stream.Open("narracao/" + caminhoWav, SSFMCreateForWrite, false);

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

            if (!Directory.Exists("narracao"))
            {
                Directory.CreateDirectory("narracao");
            }

            const int SSFMCreateForWrite = 3;
            stream.Open("narracao/" + caminhoWav, SSFMCreateForWrite, false);

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
