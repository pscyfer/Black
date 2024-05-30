using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Common.AspNetCore.LocalFileProvider;

public class FileManager : IFileManager
{


    public string RootDirectory => @"C:\Users\Rabbit\Desktop\Projects\Freedom\src\ServiceHosts\Static";

    public async Task<bool> FileExistsAsync(string path)
    {
        return await Task.Run(() => File.Exists(path));
    }

    public async Task<string> CreateFileAsync(string path)
    {
        await Task.Run(() => File.Create(path));
        return path;
    }

    public async Task<string> DeleteAsync(string path)
    {
        await Task.Run(() => File.Delete(path));
        return path;
    }

    public async Task<string> GenerateNewFileName(string path)
    {
        string directory = Path.GetDirectoryName(path);
        if (directory != null) Directory.CreateDirectory(directory);
        string extension = Path.GetExtension(path);
        string fileName = Path.GetFileNameWithoutExtension(path);

        int counter = 1;
        string newPath = path;

        while (await FileExistsAsync(newPath))
        {
            string newFileName = $"{fileName}_{counter}{extension}";
            newPath = Path.Combine(directory, newFileName);
            counter++;
        }

        return newPath;
    }

    public async Task<string> ReplaceFileName(string path, string oldPath)
    {
        string directory = Path.GetDirectoryName(oldPath);

        string newFileName = Path.GetFileName(path);
        string newPath = Path.Combine(directory, newFileName);

        await Task.Run(() => File.Move(oldPath, newPath));
        return newPath;
    }
    public async Task<string> CreateFileAtPathAsync(string path)
    {
        // Create the directory if it doesn't exist
        var directory = Path.GetDirectoryName(path);
        if (directory != null) Directory.CreateDirectory(directory);

        // Create the file asynchronously
        await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write,
            FileShare.None, bufferSize: 4096, useAsync: true);
        await fileStream.FlushAsync();

        return path;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="path">Path for save File</param>
    /// <param name="sourceFile">IFormFile</param>
    /// <returns></returns>
    public async Task<string> CreateFileAtPathAsync(string path, IFormFile sourceFile)
    {

        // if not path form C://
        string PathFromProject = path;
        // Create the directory if it doesn't exist
        path = RootDirectory + path;
        string? directory = Path.GetDirectoryName(path);
        if (directory != null) Directory.CreateDirectory(directory);

        path += sourceFile.FileName.Replace(" ", "-");
        path = await GenerateNewFileName(path);
        // Copy the content from the source file to the new file asynchronously
        await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
        await sourceFile.CopyToAsync(fileStream);
        await fileStream.FlushAsync();

        string fileNameRetruned = PathFromProject.Replace("/wwwroot/", "/") + Path.GetFileName(path);
        return fileNameRetruned;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path">Path for save File</param>
    /// <param name="oldFileNamePath">Send old FileName for remove them</param>
    /// <param name="sourceFile">IFormFile</param>
    /// <returns></returns>
    public async Task<string> CreateFileAtPathAsync(string path, string oldFileNamePath, IFormFile sourceFile)
    {
        // if not path form C://
        string PathFromProject = path;
        // Create the directory if it doesn't exist
        path = RootDirectory + path;
        string? directory = Path.GetDirectoryName(path);
        if (directory != null) Directory.CreateDirectory(directory);
        string getJustoldFileName = Path.GetFileName(oldFileNamePath);
        await DeleteAsync(path + getJustoldFileName);

        path += sourceFile.FileName.Replace(" ", "-");
        path = await GenerateNewFileName(path);
        // Copy the content from the source file to the new file asynchronously
        await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
        await sourceFile.CopyToAsync(fileStream);
        await fileStream.FlushAsync();

        string fileNameRetruned = PathFromProject.Replace("/wwwroot/", "/") + Path.GetFileName(path);
        return fileNameRetruned;
    }
}