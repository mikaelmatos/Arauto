using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

public static class MatarNavegadoresObsoletos
{
    public static void KillUnusedWebView2Processes()
    {
        int currentPid = Process.GetCurrentProcess().Id;

        foreach (var process in Process.GetProcessesByName("msedgewebview2"))
        {
            int parentPid = GetParentProcessId(process.Id);

            if (parentPid != currentPid)
                continue;

            try
            {
                var cpuTime = process.TotalProcessorTime.TotalMilliseconds;
                var uptime = DateTime.Now - process.StartTime;
                bool isIdle = cpuTime < 100; // quase nenhum uso de CPU
                bool isOld = uptime.TotalSeconds > 30;
                bool hasNoWindow = process.MainWindowHandle == IntPtr.Zero;

                if (isIdle && isOld && hasNoWindow)
                {
                    process.Kill();
                    process.WaitForExit();
                    Console.WriteLine($"Processo {process.Id} encerrado (inativo, antigo e sem janela).");
                }
                else
                {
                    Console.WriteLine($"Ignorado PID {process.Id} - Idle: {isIdle}, Old: {isOld}, NoWindow: {hasNoWindow}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar PID {process.Id}: {ex.Message}");
            }
        }
    }

    private static int GetParentProcessId(int pid)
    {
        try
        {
            PROCESS_BASIC_INFORMATION pbi = new PROCESS_BASIC_INFORMATION();
            int returnLength;

            IntPtr hProcess = OpenProcess(0x1000, false, pid);
            if (hProcess == IntPtr.Zero) return -1;

            NtQueryInformationProcess(hProcess, 0, ref pbi, Marshal.SizeOf(pbi), out returnLength);
            CloseHandle(hProcess);

            return (int)pbi.InheritedFromUniqueProcessId;
        }
        catch
        {
            return -1;
        }
    }

    #region P/Invoke
    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_BASIC_INFORMATION
    {
        public IntPtr Reserved1;
        public IntPtr PebBaseAddress;
        public IntPtr Reserved2_0;
        public IntPtr Reserved2_1;
        public IntPtr UniqueProcessId;
        public IntPtr InheritedFromUniqueProcessId;
    }

    [DllImport("ntdll.dll")]
    private static extern int NtQueryInformationProcess(IntPtr processHandle, int processInformationClass, ref PROCESS_BASIC_INFORMATION processInformation, int processInformationLength, out int returnLength);

    [DllImport("kernel32.dll")]
    private static extern IntPtr OpenProcess(int access, bool inheritHandle, int processId);

    [DllImport("kernel32.dll")]
    private static extern bool CloseHandle(IntPtr handle);
    #endregion
}
