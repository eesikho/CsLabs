namespace ServerStatistics;

class Program
{
    public static void Main( )
    {
        Console.WriteLine("Название сервера: ");
        string ServerName = Console.ReadLine();

        Console.WriteLine("Количество игроков: ");
        string buffer1 = Console.ReadLine();
        short CountPlayers = Convert.ToInt16(buffer1);

        Console.WriteLine("Загрузка CPU (%): ");
        string buffer2 = Console.ReadLine();
        float cpuLoad = Convert.ToSingle(buffer2);
        
        Console.WriteLine("Загрузка RAM (%): ");
        string buffer3 = Console.ReadLine();
        float ramLoad = Convert.ToSingle(buffer3);
        
        Console.WriteLine("Включен ли сервер? (True/False): ");
        string buffer4 = Console.ReadLine();
        bool result = Convert.ToBoolean(buffer4);
        
        Console.WriteLine("+-----------------------+");
        Console.WriteLine("|   СОСТОЯНИЕ СЕРВЕРА   |");
        Console.WriteLine("+-----------------------+");
        Console.WriteLine("");
        Console.WriteLine($"Сервер: {ServerName}");
        Console.WriteLine($"Игроки: {CountPlayers}");
        Console.WriteLine($"CPU: {cpuLoad}%");
        Console.WriteLine($"RAM: {ramLoad}%");
        Console.WriteLine($"Запущен: {result}");
    }
}