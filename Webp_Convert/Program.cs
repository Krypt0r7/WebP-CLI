
var image = args.AsQueryable().FirstOrDefault();

var dirPath = image ?? Helpers.GetImagePath();
Console.WriteLine("\nYou chose: " + dirPath);
var files = image == null 
    ? Helpers.GetFiles(dirPath) 
    : new List<string> { image };

var destination = Helpers.GetOutputPath();

Helpers.RenderImages(destination, files);

Console.WriteLine("\nAll done!");


