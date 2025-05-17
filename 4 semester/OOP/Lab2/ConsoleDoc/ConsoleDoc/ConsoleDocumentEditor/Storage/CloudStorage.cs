using Google.Apis.Drive.v3;
using Google.Apis.Upload;
using System.Text;
namespace ConsoleDoc.ConsoleDocumentEditor.Storage
{
    public class CloudStorage : IStorage
    {
        [Obsolete("Obsolete")]
        private readonly DriveService _driveService = GoogleDriveService.GetDriveService();
        private readonly string _folderId = "root";
        
        public CloudStorage()
        {
            Console.WriteLine("Using root folder in Google Drive for file storage.");
        }

        [Obsolete("Obsolete")]
        public void SaveDocument(string fileName, string content, string format)
        {
            try
            {
                var fullFileName = $"{fileName}.{format}";
                Console.WriteLine($"Attempting to save file '{fullFileName}' with content: '{content}'");

                var listRequest = _driveService.Files.List();
                listRequest.Q = $"name='{fullFileName}' and '{_folderId}' in parents and trashed=false";
                listRequest.Fields = "files(id, name)";
                var listResponse = listRequest.Execute();

                var updateMetadata = new Google.Apis.Drive.v3.Data.File
                {
                    Name = fullFileName,
                    MimeType = "text/plain"
                };

                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
                stream.Position = 0;

                if (listResponse.Files != null && listResponse.Files.Count > 0)
                {
                    string fileId = listResponse.Files[0].Id;
                    var updateRequest = _driveService.Files.Update(updateMetadata, fileId, stream, "text/plain");
                    updateRequest.Fields = "id";
                    var updateResult = updateRequest.Upload();
                    if (updateResult.Status != UploadStatus.Completed)
                    {
                        Console.WriteLine("Error updating file: " + updateResult.Exception?.Message);
                    }
                    else
                    {
                        Console.WriteLine($"File '{fullFileName}' updated successfully in root folder.");
                    }
                }
                else
                {
                    updateMetadata.Parents = new List<string> { _folderId };
                    var createRequest = _driveService.Files.Create(updateMetadata, stream, "text/plain");
                    createRequest.Fields = "id";
                    var createResult = createRequest.Upload();
                    if (createResult.Status != UploadStatus.Completed)
                    {
                        Console.WriteLine("Error creating file: " + createResult.Exception?.Message);
                    }
                    else
                    {
                        Console.WriteLine($"File '{fullFileName}' uploaded successfully to root folder.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file '{fileName}': {ex.Message}");
            }
        }

        [Obsolete("Obsolete")]
        public string? LoadDocument(string fullFileName)
        {
            try
            {
                var fileRequest = _driveService.Files.List();
                fileRequest.Q = $"name='{fullFileName}' and '{_folderId}' in parents and trashed=false";
                fileRequest.Fields = "files(id, name)";
                var fileResponse = fileRequest.Execute();

                if (fileResponse.Files.Count > 0)
                {
                    var fileId = fileResponse.Files[0].Id;
                    var stream = new MemoryStream();
                    _driveService.Files.Get(fileId).Download(stream);
                    stream.Position = 0;
                    using var reader = new StreamReader(stream);
                    var result = reader.ReadToEnd();
                    Console.WriteLine($"Loaded content: '{result}'");
                    return result;
                }

                Console.WriteLine($"File '{fullFileName}' not found in root folder.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file '{fullFileName}': {ex.Message}");
                return null;
            }
        }

        [Obsolete("Obsolete")]
        public List<string> ListDocuments()
        {
            try
            {
                var fileRequest = _driveService.Files.List();
                fileRequest.Q = $"'{_folderId}' in parents and trashed=false";
                fileRequest.Fields = "files(id, name)";
                var fileResponse = fileRequest.Execute();

                var fileNames = new List<string>();
                foreach (var file in fileResponse.Files)
                {
                    fileNames.Add(file.Name);
                }

                return fileNames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing files in root folder: {ex.Message}");
                return new List<string>();
            }
        }

        [Obsolete("Obsolete")]
        public void DeleteDocument(string fileName)
        {
            try
            {
                var query = $"name='{fileName}' and '{_folderId}' in parents and trashed=false";

                var fileRequest = _driveService.Files.List();
                fileRequest.Q = query;
                fileRequest.Fields = "files(id, name)";
                var fileResponse = fileRequest.Execute();

                if (fileResponse.Files != null && fileResponse.Files.Count > 0)
                {
                    var fileId = fileResponse.Files[0].Id;
                    var deleteRequest = _driveService.Files.Delete(fileId);
                    deleteRequest.Execute();
                    Console.WriteLine($"File '{fileName}' deleted successfully from root folder.");
                }
                else
                {
                    Console.WriteLine($"File '{fileName}' not found in root folder.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file '{fileName}': {ex.Message}");
            }
        }
    }
}