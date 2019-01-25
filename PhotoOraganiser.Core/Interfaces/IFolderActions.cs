using System;

namespace PhotoOraganiser.Core
{
    public interface IFolderActions
    {
        string[] CopyFolderContents(string originLocation, string destinationLocation, bool removeAfterCopy);
        bool CreateFolders(string[] files, string destinationLocation);
        void OrganiseFilesIntoFolders(string destinationLocation);
    }
}
