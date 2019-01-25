using System;
using System.IO;

namespace PhotoOraganiser.Core
{
    public class PhotoManagementActions : IFolderActions
    {

        public string[] CopyFolderContents(string originLocation, string destinationLocation, bool removeAfterCopy)
        {
            if (Directory.Exists(originLocation))
            {
                string[] files = Directory.GetFiles(originLocation);

                // Copy the files and overwrite destination files if they already exist.
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
                Console.WriteLine("Source path does not exist!");
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
                    var created = info.CreationTime;

                    //Year
                    Directory.CreateDirectory(Path.Combine(destinationLocation, created.Year.ToString(), created.Month.ToString()));
                    
                }

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public void OrganiseFilesIntoFolders(string destinationLocation)
        {
            string[] fileEntries = Directory.GetFiles(destinationLocation);

            foreach (string fileName in fileEntries)
                MoveFileToFolder(destinationLocation, fileName);
        }

        private void MoveFileToFolder(string destinationLocation, string filePath)
        {
            var info = new FileInfo(filePath);
            var created = info.CreationTime;
            
            string destFile = Path.Combine(destinationLocation, created.Year.ToString(), created.Month.ToString(), info.Name);
            File.Move(filePath, destFile);


        }
    }
}
