// Исправления и улучшения:
// 1. Пустые строки пропускаются по одной, а не две _lineIndex += 2 -> _lineIndex ++
// 2. Индекс увеличивается перед рекурсией, устранен бесконечный вызов
// 3. Убрано лишнее увеличение индекса после рекурсии
// 4. Убран пробел перед именем line.Substring(5) -> line.Substring(6)
// 5. Исправлены глубина отсутов и отображение ├─, └─, │
// 6. Добавлен метод PrintFromLines для тестов
// 7. Добавлены 4 юнит теста

using System;
using System.IO;

public class Program
{
    private static int _lineIndex;
    private static int _depth;
    private static bool[] _hasNext = null!;
    private static string[] _lines = null!;

    private static void Main()
    {
        PrintFromLines(File.ReadAllLines("tree.txt"));
        Console.ReadLine();
    }

    public static void PrintFromLines(string[] lines)
    {
        // Строки файла
        _lines = lines;
        // Наличие следующих соседей
        _hasNext = new bool[_lines.Length];
        // Текущая строка файла
        _lineIndex = 1;
        // Глубина вложенности
        _depth = 0;

        Console.WriteLine(_lines[0]);
        PrintTree();
    }
    private static void PrintTree()
    {
        while (_lineIndex < _lines.Length)
        {
            var line = _lines[_lineIndex].Trim();

            if (line == "")
            {
                _lineIndex ++;
                continue;
            }

            if (line == "END")
            {
                _depth--;

                _lineIndex++;

                return;
            }

            if (line.StartsWith("BEGIN "))
            {
                var name = line.Substring(6);

                _hasNext[_depth] = HasNext();
                Console.Write(GetIndent());
                Console.WriteLine((_hasNext[_depth] ? "├─" : "└─") + name);

                _depth++;

                _lineIndex++;
                
                PrintTree();
            }
        }
    }

    private static string GetIndent()
    {
        var result = "";

        for (var i = 0; i < _depth; i++)
        {
            result += _hasNext[i] ? "│ ": "  ";
        }

        return result;
    }

    private static bool HasNext()
    {
        int index = _lineIndex + 1;
        int count = 1;
        for (; index < _lines.Length && count > 0; index++)
        {
            var line = _lines[index].Trim();
            
            if (line.StartsWith("BEGIN "))
            {
                count++;
            }
            else if (line == "END")
            {
                count--;
            }
        }

        for (; index < _lines.Length && _lines[index].Trim() == ""; index++)
        {
        }
        return index < _lines.Length && _lines[index].Trim().StartsWith("BEGIN ");
    }
}