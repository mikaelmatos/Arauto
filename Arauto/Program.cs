using System.Diagnostics;

namespace Arauto
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Nome do processo atual (sem extensão .exe)
            string processName = Process.GetCurrentProcess().ProcessName;
            int currentProcessId = Process.GetCurrentProcess().Id;

            // Pega todos os processos com o mesmo nome
            var processes = Process.GetProcessesByName(processName);

            foreach (var process in processes)
            {
                // Mata o processo se não for o atual
                if (process.Id != currentProcessId)
                {
                    try
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        // Trate exceções se necessário
                        MessageBox.Show($"Erro ao encerrar processo: {ex.Message}");
                    }
                }
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Contas());
        }
    }
}