using System;

namespace PhotoOraganiser.Core
{
    public interface IFolderActions
    {
        bool FolderExist(DateTime dateTime);
        bool CreateFolder(string directory, DateTime dateTime);
        string[] CopyFolderContents(string originLocation, string destinationLocation, bool removeAfterCopy);
    }
}
