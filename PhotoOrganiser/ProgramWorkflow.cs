using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhotoOraganiser.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotoOrganiser
{
    internal class ProgramWorkflow
    {
        private readonly IFolderActions _iFolderActions;
        private readonly ILogger<ProgramWorkflow> _logger;
        private readonly AppSettings _appSettings;

        public ProgramWorkflow(IFolderActions iFolderActions, ILogger<ProgramWorkflow> logger, IOptions<AppSettings> appSettings)
        {
            _iFolderActions = iFolderActions;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public void Run()
        {
            //Copy Files
            var files = _iFolderActions.CopyFolderContents(_appSettings.OriginLocation, _appSettings.DestinationLocation, true);
            if (files.Any())
            {
                Console.WriteLine("Lets organise these new files.");

                _iFolderActions.CreateFolders(files, _appSettings.DestinationLocation);
                _iFolderActions.OrganiseFilesIntoFolders(_appSettings.DestinationLocation);
            }
        }
    }
}
