namespace ReportGenerator;

public class Program
{
    public static void Main ()
    {
        string[] lines = File.ReadAllLines("..//..//..//event_server.log");
        string report = BuildReport(lines);
        Console.WriteLine(report);
    }

    public static string BuildReport(string[] lines)
    {
        string[] eventLines = EventLines(lines);
        
        string name = ProcessingName(eventLines);
        string date = ProcessingDate(eventLines);
        string winner = ProcessingWinner(eventLines);
        int pointWinner = ProcessingPointWinner(eventLines);
        string eventItem = ProcessingEventItem(eventLines);
        int miniPoint = ProcessingMiniPoint(eventLines);
        int countWarning = ProcessingWarning(eventLines);
        int countError = ProcessingError(eventLines);
        
        return "# Итоги события: " + name + "\n" +
               "Дата: " + date + "\n" +
               "Победитель: " + winner + "\n" +
               "Очки победителя: " + pointWinner + "\n" +
               "Ивентовый предмет: " + eventItem + "\n" +
               "Утешительная награда Железных волков: " + miniPoint + " очков \n" +
               "Предупреждений во время события: " + countWarning + "\n" +
               "Ошибок во время события: " + countError;
    }

    static string[] EventLines(string[] line)
    {
        int start = -1;
        int end = -1;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i].Contains("Событие началось:"))
            {
                start = i;
            }
            // if (line[i].Contains("Событие \n"Восстание Ледяного Пламени\" закрыто")) Если надо через конкретное событие
            if (line[i].Contains("Событие") && line[i].Contains("закрыто"))
                {
                end = i;
                break;
                }
        }
        string[] eventLines = new string[end - start + 1];
        for (int j = 0; j < eventLines.Length; j++)
        {
            eventLines[j] = line[start + j];
        }
        return eventLines;
    }

    static (string dateTime, string level, string category,  string message) ProcessingLine(string line)
    {
        string dateTime = line.Substring(0, 23);

        int index1 = line.IndexOf('[');
        int index2 = line.IndexOf(']');
        string level = line.Substring(index1 + 1, index2 - index1 - 1);
        
        int index3 = line.IndexOf('[', index1 + 1);
        int index4 = line.IndexOf(']', index2 + 1);
        string category = line.Substring(index3 + 1, index4 - index3 - 1);
        
        string message = line.Substring(index4 + 1, line.Length - index4 - 1);

        return (dateTime, level, category, message);
    }

    static string FindLine(string[] eventLines, string search)
    {
        foreach (string line in eventLines)
        {
            if (line.Contains(search))
            {
                return line;
            }
        }
        return null;
    }
    static string ProcessingName(string[] eventLines)
    {
        string line = FindLine(eventLines, "Событие началось:");
        string nameLine = ProcessingLine(line).message;
        int index = nameLine.IndexOf("Событие началось: ") + "Событие началось: ".Length;
        string name = nameLine.Substring(index, nameLine.Length - index);
        return name;
    }
    static string ProcessingDate(string[] eventLines)
    {
        string line = FindLine(eventLines, "Событие началось:");
        string datePart = ProcessingLine(line).dateTime;
        DateTime date = DateTime.Parse(datePart);
        return date.ToShortDateString();
    }
    
    static string ProcessingWinner(string[] eventLines)
    {
        string line = FindLine(eventLines, " объявлены победителями события");
        string winnerLine = ProcessingLine(line).message;
        int index = winnerLine.IndexOf(" объявлены победителями события");
        string winner = winnerLine.Substring(1, index - 1);
        return winner;
    }
    
    static int ProcessingPointWinner(string[] eventLines)
    {
        foreach (string line in eventLines)
        {
            if (line.Contains("получили") && line.Contains("очков события"))
            {
                string pointWinnerLine = ProcessingLine(line).message;
                int index1 = pointWinnerLine.IndexOf("получили ") + "получили ".Length;
                int index2 = pointWinnerLine.IndexOf(" очков события");
                string pointWinner = pointWinnerLine.Substring(index1, index2 - index1);
                return int.Parse(pointWinner);
            }
        }
        return -1;
    }
    
    static string ProcessingEventItem(string[] eventLines)
    {
        string line = FindLine(eventLines, "получили ивентовый предмет:");
        string item = ProcessingLine(line).message;
        int index1 = item.IndexOf("получили ивентовый предмет: ") + "получили ивентовый предмет: ".Length;
        int index2 = item.Length;
        string eventItem = item.Substring(index1, index2 - index1);
        return eventItem;
    }
    
    // Если надо было бы искать клан с утешительной наградой
    // static string ProcessingSecondClan(string[] eventLines)
    // {
    //     string line = FindLine(eventLines, " получили утешительную награду:");
    //     string clan = ProcessingLine(line).message;
    //     int index = clan.IndexOf(" получили утешительную награду:");
    //     string secondClan = clan.Substring(1, index - 1);
    //     return secondClan;
    // }
    
    static int ProcessingMiniPoint(string[] eventLines)
    {
        string line = FindLine(eventLines, "утешительную награду:");
        string mini = ProcessingLine(line).message;
        int index1 = mini.IndexOf("утешительную награду: ") + "утешительную награду: ".Length;
        int index2 = mini.Length - " очков события".Length;
        string miniPoint = mini.Substring(index1, index2 - index1);
        return int.Parse(miniPoint);
    }
    
    static int ProcessingWarning(string[] eventLines)
    {
        int countWarning = 0;
        foreach (string line in eventLines)
        {
            if (line.Contains("[Warning]"))
            {
                countWarning++;
            }
        }
        return countWarning;
    }

    static int ProcessingError(string[] eventLines)
    {
        int countError = 0;
        foreach (string line in eventLines)
        {
            if (line.Contains("[Error]"))
            {
                countError++;
            }
        }
        return countError;
    }
}