using System;
using System.IO;
using System.Linq;

public static class FileNameValidator
{
    // Открытые символы, недопустимые в имени файла на Windows
    private static readonly char[] InvalidChars = Path.GetInvalidFileNameChars();

    // Резервированные имена по Windows (без расширения)
    private static readonly string[] ReservedNames = new[]
    {
        "CON","PRN","AUX","NUL",
        "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8","COM9",
        "LPT1","LPT2","LPT3","LPT4","LPT5","LPT6","LPT7","LPT8","LPT9"
    };

    public static bool IsValidFileName(string fileName, bool allowTrailingSpace = false, bool showLog = false)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            if (showLog) Console.WriteLine("Имя файла пустое или состоит только из пробелов.");
            return false;
        }

        // Разделяем имя и расширение, чтобы проверять базовую часть на резервированные имена
        string baseName = Path.GetFileNameWithoutExtension(fileName);

        // Нельзя, чтобы имя было одним из резервированных имен (игнорируем регистр)
        if (ReservedNames.Contains(baseName, StringComparer.OrdinalIgnoreCase))
        {
            if (showLog) Console.WriteLine($"Имя '{baseName}' является резервированным именем в Windows.");
            return false;
        }

        // Проверяем наличие недопустимых символов
        foreach (char c in fileName)
        {
            if (InvalidChars.Contains(c))
            {
                if (showLog) Console.WriteLine($"Символ '{c}' недопустим в имени файла.");
                return false;
            }
        }

        // Дополнительно: нельзя заканчивать имя пробелом или точкой (для Windows)
        if (!allowTrailingSpace && (fileName.EndsWith(" ") || fileName.EndsWith(".")))
        {
            if (showLog) Console.WriteLine("Имя файла не может заканчиваться пробелом или точкой.");
            return false;
        }

        return true;
    }
}
