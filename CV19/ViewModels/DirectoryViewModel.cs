using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Channels;
using CV19.ViewModels.Base;

namespace CV19.ViewModels 
{
    class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;

        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    return _DirectoryInfo.EnumerateDirectories()
                        .Select(dir_info => new DirectoryViewModel(dir_info.FullName));

                }
                catch (UnauthorizedAccessException e)
                {
                   Console.WriteLine(e);
                }

                return Enumerable.Empty<DirectoryViewModel>();
            }

        } 
        
        


        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                        .EnumerateFiles()
                        .Select(file => new FileViewModel(file.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                return Enumerable.Empty<FileViewModel>();
            }
        }
        
 



        public string Name => _DirectoryInfo.Name;

        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public IEnumerable<object> DirectoryItems => SubDirectories.Cast<object>().Concat(Files);

        public DirectoryViewModel(string Path) => _DirectoryInfo = new DirectoryInfo(Path);
    }
}
