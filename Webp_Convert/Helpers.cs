using ImageMagick;

public class Helpers
{
    public static void RenderImages(string destination, List<string> paths)
    {
        Console.WriteLine("Do you want to use the original name? y/n ");
        var response = Console.ReadKey();
        bool useOriginal = response.Key == ConsoleKey.Y;
        string name = "";
        if (!useOriginal)
        {
            Console.Write("\nGive a name ([name]_[incremented number]): ");
            name = Console.ReadLine() ?? string.Empty;
        }

        int count = 0;
        foreach (var path in paths)
        {
            using (MagickImage image = new(path))
            {
                image.Quality = 70;
                image.Settings.SetDefine(MagickFormat.WebP, "lossless", false);
                image.Write($"{destination}\\{(!useOriginal ? $"{name}_{count}" : path.Split(Path.DirectorySeparatorChar).Last().Split('.').First())}.webp");
            };

            count++;
        }
    }

    public static List<string> GetFiles(string dirPath)
    {
        return Directory.GetFiles(dirPath).Where(x => x.Contains(".jpg")).ToList();
    }

    public static string GetImagePath()
    {
        Console.Write("Do you wanna specify a directory path (default: current directory)? y/n ");
       
        var input = Console.ReadKey();
        var isSpecifiedPath = input.Key == ConsoleKey.Y;
        if (isSpecifiedPath)
        {
            Console.Write("\nPath: ");
            return Console.ReadLine() ?? string.Empty;
        }
        else
        {
            return Directory.GetCurrentDirectory();
        }
    }

    public static string GetOutputPath()
    {
        Console.Write("New directory for files? y/n ");
        var input = Console.ReadKey();
        var newDir = input.Key == ConsoleKey.Y;

        if (newDir)
        {
            Console.Write("\nName the new directory: ");
            var dirName = Console.ReadLine() ?? "default";
            var path = Directory.GetCurrentDirectory();
            var di = Directory.CreateDirectory(Path.Combine(path, dirName));
            return di.FullName;
        }
        else
        {
            return Directory.GetCurrentDirectory();
        }
    }
}

