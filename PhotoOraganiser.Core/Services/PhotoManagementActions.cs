using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace PhotoOraganiser.Core
{
    public class PhotoManagementActions : IFolderActions
    {
        private readonly ILogger<PhotoManagementActions> _logger;

        public PhotoManagementActions(ILogger<PhotoManagementActions> logger)
        {
            _logger = logger;
        }

        public string[] CopyFolderContents(string originLocation, string destinationLocation, bool removeAfterCopy)
        {
            if (Directory.Exists(originLocation))
            {
                string[] files = Directory.GetFiles(originLocation);
                _logger.LogInformation($"Found {files.Count()} new files, copying...");

                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    string fileName = Path.GetFileName(s);
                    string destFile = Path.Combine(destinationLocation, fileName);
                    File.Copy(s, destFile, true);

                    if (removeAfterCopy)
                    {
                        File.Delete(s);
                    }
                }

                return Directory.GetFiles(destinationLocation);
            }
            else
            {
                _logger.LogWarning("Folder does not exist, please check your settings and try again.");
                return new string[0];
            }
        }

        public bool CreateFolders(string[] files, string destinationLocation)
        {
            try
            {
                foreach (var file in files)
                {
                    var info = new FileInfo(file);
                    var created = info.LastWriteTime;

                    Directory.CreateDirectory(Path.Combine(destinationLocation, created.Year.ToString(), created.ToString("MMMM")));
                }

                _logger.LogInformation("New folder(s) generated");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured trying to create new folders: {ex.Message}");
                return false;
            }
        }

        public void OrganiseFilesIntoFolders(string destinationLocation)
        {
            foreach (string fileName in Directory.GetFiles(destinationLocation))
            {
                MoveFileToFolder(destinationLocation, fileName);
            }
        }

        private void MoveFileToFolder(string destinationLocation, string filePath)
        {
            var info = new FileInfo(filePath);
            var created = info.LastWriteTime;

            string destFile = Path.Combine(destinationLocation, created.Year.ToString(), created.ToString("MMMM"), info.Name);
            File.Move(filePath, destFile);


        }
    }
}
