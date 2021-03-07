using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    class FileViewModel : ViewModel
    {
       private readonly FileInfo _FileInfo;

       public string Name => _FileInfo.Name;

       public string Path => _FileInfo.FullName;
       public DateTime CreationTime => _FileInfo.CreationTime;


        public FileViewModel(string Path) =>_FileInfo = new FileInfo(Path);




    }
}
