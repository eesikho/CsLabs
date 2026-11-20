namespace RealtimeMonitor;

using System;

public class Program
{
    private static void Main()
    {
        var monitor = new ServerMonitor();

        monitor.DataReceived += OnDataReceived;

        monitor.Start();

        Console.ReadLine();

        monitor.Stop();
    }

    static double[] cpuHistory = new double[20];
    static double[] ramHistory = new double[20];
    static double[] temperatureHistory = new double[20];

    private static void OnDataReceived(double cpu, double ram, double temperature)
    {
        for (int i = 0; i < cpuHistory.Length - 1; i++)
        {
            cpuHistory[i] = cpuHistory[i + 1];
            ramHistory[i] = ramHistory[i + 1];
            temperatureHistory[i] = temperatureHistory[i + 1];
        }

        cpuHistory[cpuHistory.Length - 1] = cpu;
        ramHistory[ramHistory.Length - 1] = ram;
        temperatureHistory[temperatureHistory.Length - 1] = temperature;

        Console.Clear();

        Console.WriteLine("Мониторинг запущен.");
        Console.WriteLine("Для завершения программы нажмите Enter.");
        Console.WriteLine();

        DrawGraph(cpuHistory, "Загрузка процессора (%)");
        DrawGraph(ramHistory, "Использование оперативной памяти (%)");
        DrawGraph(temperatureHistory, "Температура процессора (°C)");
    }

    public static void DrawGraph(double[] history, string text)
    {
        Console.WriteLine($"{text}: {history[history.Length - 1]}");
        for (int level = 100; level >= 10; level -= 10)
        {
            Console.Write($"{level,3} |");
            for (int i = 0; i < history.Length; i++)
            {
                Console.Write(Math.Round(history[i] / 10) >= level / 10 ? "  █ " : "    ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("    +--------------------------------------------------------------------------------");
        Console.WriteLine("       1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16  17  18  19  20");
        Console.WriteLine("Старое -> новое");
        Console.WriteLine();
    }
}