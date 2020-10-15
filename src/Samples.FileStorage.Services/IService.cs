#region Using Statements
using Samples.FileStorage.Models;
using System.Collections.Generic;
using System.Threading.Tasks; 
#endregion

namespace Samples.FileStorage.Services
{
    public interface IService
    {
        string ErrorMessage { get; set; }
        bool HasError { get; }
        string ConnectionString { get; set; }

        Task DeleteFile(string source, string share, string folder, string fileName);

        Task<FileDownloadResponse> DownloadFile(string source, string share, string folder, string fileName);

        Task<FileUploadResponse> UploadFile(string source, string share, string folder, FileUploadRequest request);

        Task<FolderListResponse> ListFolders(string source, string share);

        Task<FileListResponse> ListFiles(string source, string share, string folder);
    }
}
