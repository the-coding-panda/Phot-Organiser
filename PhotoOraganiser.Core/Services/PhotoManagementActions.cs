using PhotoOraganiser.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PhotoOraganiser.Core.Services
{
    public class PhotoManagementActions : IFolderActions
    {
        
        public void CopyFolderContents(string originLocation, string destinationLocation, bool removeAfterCopy)
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
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }
        }

        public bool CreateFolder(string directory, DateTime dateTime)
        {
            try
            {
                Path.Combine(directory, dateTime.Month.ToString());
                Directory.CreateDirectory(directory);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool FolderExist(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
