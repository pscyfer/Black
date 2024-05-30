using Microsoft.AspNetCore.Http;

namespace Common.AspNetCore.LocalFileProvider
{
    public interface IFileManager
    {
        public string RootDirectory { get;}
        Task<bool> FileExistsAsync(string path);
        Task<string> CreateFileAsync(string path);
        Task<string> DeleteAsync(string path);
        Task<string> GenerateNewFileName(string path);
        Task<string> ReplaceFileName(string path, string oldPath);
        Task<string> CreateFileAtPathAsync(string path);
        Task<string> CreateFileAtPathAsync(string path, IFormFile sourceFile);
        Task<string> CreateFileAtPathAsync(string path, string oldFileNamePath, IFormFile sourceFile);
    }
}
