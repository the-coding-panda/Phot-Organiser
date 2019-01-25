using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoOraganiser.Core.Interfaces
{
    public interface IFolderActions
    {
        bool FolderExist(DateTime dateTime);
        bool CreateFolder(string directory, DateTime dateTime);
        void CopyFolderContents(string originLocation, string destinationLocation, bool removeAfterCopy);
    }
}
