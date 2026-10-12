using agent.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agent
{
    public sealed class ConsolePrinter
    {
        public void Print(HardwareInfo info)
        {
            Section("CPU");
            Line("Модель", info.Cpu.Name);
            Line("Ядра", $"{info.Cpu.Cores} физических / {info.Cpu.Threads} логических");
            Line("Частота", $"{info.Cpu.MaxClockGhz:F2} GHz");

            Section("RAM");
            Line("Всего", FormatBytes(info.TotalRamBytes));
            Line("Планок", info.RamModules.Count.ToString());
            for (int i = 0; i < info.RamModules.Count; i++)
            {
                var m = info.RamModules[i];
                Console.WriteLine(
                    $"    [{i + 1}] {m.Type} {FormatBytes(m.SizeBytes)} @ {m.SpeedMhz} MHz ({m.Manufacturer})");
            }

            Section("Диски");
            if (info.Disks.Count == 0) Console.WriteLine("    нет данных");
            foreach (var d in info.Disks)
                Console.WriteLine($"    • {d.Model} — {FormatBytes(d.SizeBytes)} [{d.InterfaceType}, {d.MediaType}]");

            Section("GPU");
            if (info.Gpus.Count == 0) Console.WriteLine("    нет данных");
            foreach (var g in info.Gpus)
            {
                var vram = g.VramBytes is { } v ? FormatBytes(v) : "n/a";
                Console.WriteLine($"    • {g.Name} — VRAM: {vram}, драйвер: {g.DriverVersion}");
            }

            Section("Материнская плата");
            Line("Производитель", info.Motherboard.Manufacturer);
            Line("Модель", info.Motherboard.Product);
            Line("Серийный №", info.Motherboard.SerialNumber);

            Section("ОС");
            Line("Система", info.Os.Caption);
            Line("Версия", info.Os.Version);
            Line("Архитектура", info.Os.Architecture);
            Line("Имя ПК", info.Os.MachineName);
            Line("Uptime", info.Os.Uptime.ToString(@"dd\.hh\:mm\:ss"));
        }

        // ---------- helpers ----------

        private static void Section(string title)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"── {title} " + new string('─', Math.Max(0, 50 - title.Length)));
            Console.ResetColor();
        }

        private static void Line(string key, string value)
        {
            Console.Write("  ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{key,-14}");
            Console.ResetColor();
            Console.WriteLine(value);
        }

        private static string FormatBytes(long bytes)
        {
            string[] units = { "B", "KB", "MB", "GB", "TB" };
            double size = bytes;
            int u = 0;
            while (size >= 1024 && u < units.Length - 1) { size /= 1024; u++; }
            return $"{size:F2} {units[u]}";
        }
    }
}
