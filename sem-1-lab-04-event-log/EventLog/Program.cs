namespace EventLog;

public class Program
{
    public static void Main()
    {
        string[] lines = File.ReadAllLines("..//..//..//event_server.log");
        LogEntry[] entries = ParseLog(lines);
        // foreach (LogEntry entry in entries)
        // {
        //     Console.WriteLine($"{entry.Timestamp}{entry.Level}{entry.Category}{entry.Message}");
        // }
    }

    public static LogEntry[] ParseLog(string[] lines)
    {
        LogEntry[] entries = new LogEntry[lines.Length];
        
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            
            int index1 = line.IndexOf("[");
            DateTime timestamp = DateTime.Parse(line.Substring(0, index1 - 1));
            
            int index2 = line.IndexOf("]");
            string level = line.Substring(index1 + 1, index2 - index1 - 1);
            
            int index3 = line.IndexOf("[", index1 + 1);
            int index4 = line.IndexOf("]", index2 + 1);
            string category = line.Substring(index3 + 1, index4 - index3 - 1);
            
            string message = line.Substring(index4 + 2);
            
            LogEntry entry = new LogEntry();
            entry.Timestamp = timestamp;
            entry.Level = level;
            entry.Category = category;
            entry.Message = message;
            entries[i] = entry;
        }

        return entries;
    }

    public static LogEntry[] FilterByDate(LogEntry[] entries, DateTime date)
    {
        List<LogEntry> result = new List<LogEntry>();
        foreach (LogEntry entry in entries)
        {
            if (entry.Timestamp.Date == date.Date)
            {
                result.Add(entry);
            }
        }
        
        return result.ToArray();
    }

    public static LogEntry[] FilterByLevel(LogEntry[] entries, string level)
    {
        List<LogEntry> result = new List<LogEntry>();
        foreach (LogEntry entry in entries)
        {
            if (entry.Level == level)
            {
                result.Add(entry);
            }
        }
        return result.ToArray();
    }

    public static LogEntry[] FilterByCategory(LogEntry[] entries, string category)
    {
        List<LogEntry> result = new List<LogEntry>();
        foreach (LogEntry entry in entries)
        {
            if (entry.Category == category)
            {
                result.Add(entry);
            }
        }
        return result.ToArray();
    }
    
    public static LogEntry[] Search(LogEntry[] entries, string text)
    {
        List<LogEntry> result = new List<LogEntry>();
        foreach (LogEntry entry in entries)
        {
            if (entry.Message.Contains(text, StringComparison.InvariantCultureIgnoreCase))
            {
                result.Add(entry);
            }
        }
        return result.ToArray();
    }

    public static int CountByLevel(LogEntry[] entries, string level)
    {
        int count = 0;

        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].Level == level)
            {
                count++;
            }
        }
        
        return count;
    }

    public static string GetServerStatus(LogEntry[] entries)
    {
        bool hasError = false;
        
        foreach (LogEntry entry in entries)
        {
            if (entry.Level == "Fatal" && entry.Category == "Server")
            {
                return "КРИТИЧЕСКАЯ ОШИБКА: сервер остановлен";
            }
            else if (entry.Level == "Error")
            {
                hasError = true;
            }
        }

        if (hasError == true)
        {
            return "Есть ошибки: требуется проверка";
        }

        return "Сервер работает штатно";
    }
}
