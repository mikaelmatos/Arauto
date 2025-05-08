using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Arauto
{
    public partial class TesteQuiz : Form
    {
        public TesteQuiz()
        {
            InitializeComponent();
        }

        public class Quiz
        {
            public string Pergunta { get; set; }
            public string AlternativaA { get; set; }
            public string AlternativaB { get; set; }
            public string AlternativaC { get; set; }
            public string AlternativaD { get; set; }
            public string AlternativaCorreta { get; set; }
        }

        private async void TesteQuiz_Load(object sender, EventArgs e)
        {
            string titulo = "Qual o nome do planeta mais próximo do Sol";
            string resumo = "Qual é o nome do planeta mais próximo do Sol? Opção A: Terra; Opção B: Marte; Opção C: Mercúrio ou Opção D: Vênus? Atenção, repetindo... Qual é o nome do planeta mais próximo do Sol? Opção A: Terra; Opção B: Marte; Opção C: Mercúrio ou Opção D: Vênus? Deixa nos comentários antes do resultado, não vale trapacear...";
            string resposta = "A resposta é: Opção C: Mercurio! siga-nos para mais testes de connhecimentos, deixe seu like e compartilhe este video.";

            GerarWav(resumo, titulo.ToUpper() + ".wav");
            GerarWav(resposta, "RESPOSTA_" + titulo.ToUpper() + ".wav");

            await CriarVideoComDoisAudiosEDuasImagensAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao/" + titulo.ToUpper() + ".wav", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/narracao/RESPOSTA_" + titulo.ToUpper() + ".wav", "https://sdmntprnorthcentralus.oaiusercontent.com/files/00000000-da94-622f-8791-c8702b383d69/raw?se=2025-05-08T15%3A14%3A57Z&sp=r&sv=2024-08-04&sr=b&scid=096372de-e179-5619-9751-38d0db7083d6&skoid=a3336399-497e-45e5-8f28-4b88ecca3d1f&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2025-05-07T22%3A43%3A26Z&ske=2025-05-08T22%3A43%3A26Z&sks=b&skv=2024-08-04&sig=135DH1Xq7EEjEm7mLOEgDyf8SPN2KTkPZnyw5hFuYEA%3D", "https://sdmntprnorthcentralus.oaiusercontent.com/files/00000000-b1e0-622f-be23-7371084e473e/raw?se=2025-05-08T15%3A14%3A57Z&sp=r&sv=2024-08-04&sr=b&scid=00000000-0000-0000-0000-000000000000&skoid=add8ee7d-5fc7-451e-b06e-a82b2276cf62&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2025-05-08T11%3A03%3A43Z&ske=2025-05-09T11%3A03%3A43Z&sks=b&skv=2024-08-04&sig=xSWXVhrM4rVBXfF1GlINZUYkfzLmKHQb8536AwTEqkk%3D", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/video-bruto", titulo.ToUpper());

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
        public static int ObterDuracaoAudioEmSegundos(string caminhoDoArquivo)
        {
            using (var reader = new AudioFileReader(caminhoDoArquivo))
            {
                return (int)reader.TotalTime.TotalSeconds;
            }
        }

        public static async Task<string> CriarVideoComDoisAudiosEDuasImagensAsync(
         string caminhoAudio1,
         string caminhoAudio2,
         string urlImagem1,
         string urlImagem2,
         string caminhoSaida,
         string nomeSaida)
        {
            Directory.CreateDirectory(caminhoSaida);
            string ffmpegPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg.exe");

            // Baixa imagens temporariamente
            async Task<string> BaixarImagem(string url, string nome)
            {
                using var client = new HttpClient();
                var bytes = await client.GetByteArrayAsync(url);
                string caminho = Path.Combine(Path.GetTempPath(), nome + ".jpg");
                await File.WriteAllBytesAsync(caminho, bytes);
                return caminho;
            }

            string img1 = await BaixarImagem(urlImagem1, "imagem1");
            string img2 = await BaixarImagem(urlImagem2, "imagem2");
            string loader = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "loader.mp4");
            string saida = Path.Combine(caminhoSaida, nomeSaida + ".mp4");

            int segundosMostrar = ObterDuracaoAudioEmSegundos(caminhoAudio1);

            // Montagem do comando FFmpeg
            string args = $"-y " +
                $"-loop 1 -t " + segundosMostrar + $" -i \"{img1}\" " +                  // imagem 1 por 8s
                $"-loop 1 -t 20 -i \"{img2}\" " +                 // imagem 2 por 20s
                $"-i \"{loader}\" " +                            // loader.mp4
                $"-i \"{caminhoAudio1}\" -i \"{caminhoAudio2}\" " + // dois áudios
                $"-filter_complex " +
                $"\"[0:v]format=yuv420p,setsar=1[v0]; " +
                  "[1:v]format=yuv420p,setsar=1[v1]; " +
                  "[2:v]scale=160:90[loader]; " +
                  "[v0][v1]concat=n=2:v=1:a=0[base]; " +
                  "[base][loader]overlay=W-w-10:10:enable='between(t,8,28)'[v]; " +
                  "[3:a]adelay=0|0[a1]; " +
                  "[4:a]adelay=28000|28000[a2]; " +
                  "[a1][a2]amix=inputs=2[a]\" " +
                $"-map \"[v]\" -map \"[a]\" -shortest \"{saida}\"";

            using var proc = new Process();
            proc.StartInfo.FileName = ffmpegPath;
            proc.StartInfo.Arguments = args;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();

            string erro = await proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();

            if (proc.ExitCode != 0)
                throw new Exception("Erro ao gerar vídeo: " + erro);

            return saida;
        }
    }
}
