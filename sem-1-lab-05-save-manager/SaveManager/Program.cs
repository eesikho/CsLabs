using Newtonsoft.Json;
namespace SaveManager;

public class Program
{
    static void Main()
    {
        Directory.CreateDirectory("..//..//..//saves");
        Directory.CreateDirectory("..//..//..//backups");

        string savesDirectory = "..//..//..//saves";
        string indexPath = "..//..//..//index.json";
        string backupsDirectory = "..//..//..//backups";

        List<SaveEntry> entries = LoadIndex(indexPath);
        if (entries == null)
            return;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Показать список сохранений");
            Console.WriteLine("2. Посмотреть данные сохранения");
            Console.WriteLine("3. Изменить данные персонажа и записать их обратно");
            Console.WriteLine("4. Создать резервную копию");
            Console.WriteLine("5. Восстановить резервную копию");
            Console.WriteLine("6. Удалить сохранение");
            Console.WriteLine("7. Выйти");
            Console.WriteLine();
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                {

                    Console.Clear();
                    Console.WriteLine("Список сохранений:");

                    foreach (SaveEntry entry in entries)
                    {
                        Console.WriteLine(
                            $"Сохранение: {entry.Name}, файл: {entry.File}, последнее сохранение: {entry.UpdatedAt}");
                    }

                    Console.ReadKey();

                    break;
                }

                case "2":
                {
                    Console.Clear();
                    Console.Write("Введите имя сохранения: ");
                    string name = Console.ReadLine();
                    Console.Clear();
                    SaveEntry entry = FindByName(entries, name);

                    if (entry != null)
                    {
                        SaveData save = LoadSave(savesDirectory, entry);
                        if (save == null)
                        {
                            Console.ReadKey();
                            break;
                        }

                        PrintSave(entry, save);
                    }
                    else
                    {
                        Console.Write("Запись не найдена");
                    }

                    Console.ReadKey();
                    break;
                }

                case "3":
                {
                    Console.Clear();
                    Console.Write("Введите имя сохранения: ");
                    string name = Console.ReadLine();
                    Console.Clear();
                    SaveEntry entry = FindByName(entries, name);
                    if (entry != null)
                    {
                        SaveData save = LoadSave(savesDirectory, entry);

                        if (save == null)
                        {
                            Console.WriteLine("Не удалось загрузить сохранение");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("Здесь вы можете изменять данные сохранения");
                        Console.WriteLine();
                        Console.WriteLine($"Текущая карта: {save.Map}");
                        Console.Write($"Новая карта (Enter - без изменений): ");
                        string map = Console.ReadLine();
                        if (map != "")
                        {
                            save.Map = map;
                        }
                        
                        Console.WriteLine($"Текущая координата X: {save.Position.x}");
                        int x;
                        while (true)
                        {
                            Console.Write("Новое значение (целое число, Enter - без изменений): ");
                            string input = Console.ReadLine();

                            if (input == "")
                            {
                                break;
                            }

                            if (int.TryParse(input, out x))
                            {
                                save.Position.x = x;
                                break;
                            }

                            Console.Write("Ошибка! Введите целое число");
                        }
                        
                        Console.WriteLine($"Текущая координата Y: {save.Position.y}");
                        int y;
                        while (true)
                        {
                            Console.Write("Новое значение (целое число, Enter - без изменений): ");
                            string input = Console.ReadLine();
                            if (input == "")
                            {
                                break;
                            }

                            if (int.TryParse(input, out y))
                            {
                                save.Position.y = y;
                                break;
                            }

                            Console.Write("Ошибка! Введите целое число");
                        }
                        
                        Console.WriteLine($"Текущее здоровье: {save.Health}");
                        int health;
                        while (true)
                        {
                            Console.Write("Новое значение здоровья (целое число, Enter - без изменений): ");
                            string input = Console.ReadLine();

                            if (input == "")
                            {
                                break;
                            }

                            if (int.TryParse(input, out health))
                            {
                                save.Health = health;
                                break;
                            }

                            Console.Write("Ошибка! Введите целое число");
                        }
                        
                        Console.Write($"Текущий инвентарь: ");
                        for (int i = 0; i < save.Inventory.Count; i++)
                        {
                            Console.Write(save.Inventory[i]);

                            if (i < save.Inventory.Count - 1)
                            {
                                Console.Write(", ");
                            }
                        }
                        Console.WriteLine();
                        Console.Write("Новый инвентарь (через запятую, Enter - без изменений): ");
                        string inputInventory = Console.ReadLine();
                        if (inputInventory != "")
                        {
                            save.Inventory = inputInventory.Split(',').ToList();
                        }

                        SaveGame(savesDirectory, entry, save);
                        entry.UpdatedAt = DateTime.Now;
                        SaveIndex(indexPath, entries);
                    }
                    else
                    {
                        Console.Write("Запись не найдена");
                    }    
                    Console.ReadKey();
                    break;
                }

            case "4":
                {
                    Console.Clear();
                    Console.Write("Введите имя сохранения, для которого хотите создать резервную копию: ");
                    string name = Console.ReadLine();
                    Console.Clear();
                    SaveEntry entry = FindByName(entries, name);

                    if (entry != null)
                    {
                        CreateBackup(savesDirectory, backupsDirectory, entry);
                    }
                    else
                    {
                        Console.Write("Запись не найдена");
                    }

                    Console.ReadKey();
                    break;
                }
                case "5":
                {
                    Console.Clear();
                    Console.Write("Введите имя сохранения, резервную копию которого хотите восстановить: ");
                    string name = Console.ReadLine();
                    Console.Clear();
                    SaveEntry entry = FindByName(entries, name);
                    
                    if (entry != null)
                    {
                        RestoreBackup(savesDirectory, backupsDirectory, entry);
                    }
                    else
                    {
                        Console.Write("Запись не найдена");
                    }

                    Console.ReadKey();
                    break;
                }

                case "6":
                {
                    Console.Clear();
                    Console.Write("Введите имя сохранения, которое хотите удалить: ");
                    string name = Console.ReadLine();
                    Console.Clear();
                    SaveEntry entry = FindByName(entries, name);
                    if (entry != null)
                    {
                        DeleteSave(savesDirectory, indexPath, entries, entry);
                    }
                    else
                    {
                        Console.Write("Запись не найдена");
                    }
                    Console.ReadKey();
                    break;
                }
                case "7":
                    Console.Clear();
                    return;

                default:
                    break;
            }
        }
    }

    public static List<SaveEntry> LoadIndex(string indexPath)
    {
        try
        {
            List<SaveEntry> saveEntry = new List<SaveEntry>();
            string json = File.ReadAllText(indexPath);
            saveEntry = JsonConvert.DeserializeObject<List<SaveEntry>>(json);
            return saveEntry;
        }
        catch
        {
            Console.WriteLine("Файл отсутствует или JSON некорректен");
            return  null;
        }
    }

    public static SaveData LoadSave(string savesDirectory, SaveEntry entry)
    {
        try
        {
            string path = Path.Combine(savesDirectory, entry.File);
            string json = File.ReadAllText(path);
            SaveData save = JsonConvert.DeserializeObject<SaveData>(json);
            
            return save;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл отсутствует");
            
            return null;
        }
        catch (JsonException)
        {
            Console.WriteLine("Файл содержит некорректный JSON");
            
            return null;
        }
    }

    public static void PrintSave(SaveEntry entry, SaveData save)
    {
        Console.WriteLine($"Имя сохранения: {entry.Name}");
        Console.WriteLine($"Имя персонажа: {save.Character}");
        Console.WriteLine($"Карта: {save.Map}");
        Console.WriteLine($"Координаты: X = {save.Position.x}, Y = {save.Position.y}");
        Console.WriteLine($"Здоровье: {save.Health}");
        Console.Write($"Предметы инвентаря: ");
        for (int i = 0; i < save.Inventory.Count; i++)
        {
            Console.Write(save.Inventory[i]);

            if (i < save.Inventory.Count - 1)
            {
                Console.Write(", ");
            }
        }
    }

    public static SaveEntry FindByName(List<SaveEntry> entries, string name)
    {
        foreach (SaveEntry entry in entries)
        {
            if (entry.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
            {
                return entry;
            }
        }
        
        return null;
    }

    public static void SaveGame(string savesDirectory, SaveEntry entry, SaveData save)
    {
        try
        {
            string path = Path.Combine(savesDirectory, entry.File);
            string json = JsonConvert.SerializeObject(save, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        catch
        {
            Console.WriteLine("Не удалось сохранить игру!");
        }
    }

    public static void SaveIndex(string indexPath, List<SaveEntry> entries)
    {
        try
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
            File.WriteAllText(indexPath, json);
        }
        catch
        {
            Console.WriteLine("Не удалось сохранить индекс сохранений!");
        }
    }

    public static void CreateBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
    {
        string sourcePath = Path.Combine(savesDirectory, entry.File);
        string destinationPath = Path.Combine(backupsDirectory, entry.File);
        if (File.Exists(sourcePath))
        {
            File.Copy(sourcePath, destinationPath, overwrite: true);
            Console.Write("Резервная копия успешно создана!");
        }
        else
        {
            Console.WriteLine("Не удалось создать резервную копию!");
        }
    }

    public static void RestoreBackup(string savesDirectory, string backupsDirectory, SaveEntry entry)
    {
        string backupPath = Path.Combine(backupsDirectory, entry.File);
        string savePath = Path.Combine(savesDirectory, entry.File);
        if (File.Exists(backupPath))
        {
            File.Copy(backupPath, savePath, true);
            Console.Write("Резервная копия успешно восстановлена!");
        }
        else
        {
            Console.WriteLine($"Не удалось восстановить сохранение {entry.Name}");
        }
    }

    public static void DeleteSave(string savesDirectory, string indexPath, List<SaveEntry> entries, SaveEntry entry)
    {
        string path = Path.Combine(savesDirectory, entry.File);
        if (File.Exists(path))
        {
            File.Delete(path);
            Console.WriteLine("Сохранение успешно удалено!");
        }
        else
        {
            Console.WriteLine("Сохранение отсутствует");
        }
        
        entries.Remove(entry);
        SaveIndex(indexPath, entries);
    }
}
