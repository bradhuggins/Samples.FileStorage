#region Using Statements
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Samples.FileStorage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Samples.FileStorage.Services
{
    public class Service : IService
    {
        public string ErrorMessage { get; set; }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(this.ErrorMessage); }
        }

        public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private readonly ILogger<Service> _logger;
        private readonly IConfiguration _configuration;

        public Service(ILogger<Service> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private bool IsValid()
        {
            bool toReurn = false;

            return toReurn;
        }

        public async Task DeleteFile(string source, string share, string folder, string fileName)
        {
            Shared.FileStorage.IService service = this.GetServiceReference(source);

            if (!this.HasError && service != null)
            {
                await service.DeleteFile(share, folder, fileName);
                this.ErrorMessage = service.ErrorMessage;
            }
        }

        public async Task<FileDownloadResponse> DownloadFile(string source, string share, string folder, string fileName)
        {
            FileDownloadResponse toReturn = new FileDownloadResponse();

            Shared.FileStorage.IService service = this.GetServiceReference(source);

            if (!this.HasError && service != null)
            {
                byte[] fileData = await service.DownloadFile(share, folder, fileName);
                this.ErrorMessage = service.ErrorMessage;
                if (!service.HasError && fileData != null)
                {
                    toReturn.Base64FileData = Shared.Utilities.FileHelper.ByteArrayToBase64String(fileData);
                }
                else
                {
                    toReturn.ErrorMessage = "File not found.";
                }
            }
            return toReturn;

        }

        public async Task<FileUploadResponse> UploadFile(string source, string share, string folder, FileUploadRequest request)
        {
            FileUploadResponse toReturn = new FileUploadResponse();
            if (string.IsNullOrEmpty(request.Base64FileData))
            {
                toReturn.ErrorMessage = "Data must not be null.";
                return toReturn;
            }
            toReturn.Source = source;
            toReturn.Share = share;
            toReturn.Folder = folder;

            toReturn.InputFilename = request.InputFilename;
            string filename = this.GetUniqueFilename(request.InputFilename);
            toReturn.Filename = filename;

            toReturn.file_url = toReturn.file_url
                                .Replace("{source}", source + @"/")
                                .Replace("{share}", share)
                                 .Replace("{folder}", folder)
                                 .Replace("{filename}", filename);

            Shared.FileStorage.IService service = this.GetServiceReference(source);

            if (!this.HasError && service != null)
            {
                await service.UploadFile(share, folder, filename, Shared.Utilities.FileHelper.Base64StringToByteArray(request.Base64FileData));
                this.ErrorMessage = service.ErrorMessage;
            }
            return toReturn;
        }

        public async Task<FolderListResponse> ListFolders(string source, string share)
        {
            FolderListResponse toReturn = new FolderListResponse();
            toReturn.folder_url = toReturn.folder_url
                                .Replace(@"{source}", source + @"/")
                                .Replace(@"{share}", share);

            Shared.FileStorage.IService service = this.GetServiceReference(source);

            if (!this.HasError && service != null)
            {
                toReturn.folders = await service.ListFolders(share);
                this.ErrorMessage = service.ErrorMessage;
            }
            return toReturn;
        }

        public async Task<FileListResponse> ListFiles(string source, string share, string folder)
        {
            FileListResponse toReturn = new FileListResponse();
            toReturn.file_url = toReturn.file_url
                                .Replace("{source}", source + @"/")
                                .Replace("{share}", share)
                                 .Replace("{folder}", folder);

            Shared.FileStorage.IService service = this.GetServiceReference(source);

            if (!this.HasError && service != null)
            {
                toReturn.files = await service.ListFiles(share, folder);
                this.ErrorMessage = service.ErrorMessage;
            }
            return toReturn;
        }

        private Shared.FileStorage.IService GetServiceReference(string source)
        {
            Shared.FileStorage.IService service = null;
            switch (source)
            {
                case "native":
                    service = new Shared.FileStorage.NativeClient.Service();
                    service.ConnectionString = _configuration.GetValue<string>("FileStorageConfig:native");
                    break;
                case "azure":
                    service = new Shared.FileStorage.AzureFilesClient.Service();
                    service.ConnectionString = _configuration.GetValue<string>("FileStorageConfig:azure");
                    break;
                case "sql":
                    service = new Shared.FileStorage.SqlClient.Service();
                    service.ConnectionString = _configuration.GetValue<string>("FileStorageConfig:sql");
                    break;
                default:
                    this.ErrorMessage = "Invalid source specified.";
                    break;
            }
            return service;
        }

        private string GetUniqueFilename(string fileName)
        {
            string toReturn = string.Empty;
            toReturn = Shared.Utilities.StringHelper.GenerateCleanGuid() + System.IO.Path.GetExtension(fileName);
            return toReturn;
        }
    }
}
