using agent;
using agent.Hardware;

internal class Program
{
    private static void Main(string[] args)
    {
        IHardwareInfoProvider provider = new WindowsHardwareInfoProvider();
        var printer = new ConsolePrinter();

        try
        {
            Console.WriteLine("Сбор данных о железе...");
            var info = provider.Collect();
            printer.Print(info);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.ResetColor();
            Environment.ExitCode = 1;
        }
    }
}