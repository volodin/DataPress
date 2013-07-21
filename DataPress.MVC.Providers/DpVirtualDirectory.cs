using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Providers
{
    using System.Web.Hosting;

    public class DpVirtualDirectory : VirtualDirectory
    {
        IVirtualFileStorage _Storage;

        public DpVirtualDirectory(string virtualDir, IVirtualFileStorage storage)
            : base(virtualDir)
        {
            _Storage = storage;
        }


        
        public override IEnumerable Children
        {
            get { 
                foreach (var dir in this.Directories)
                    yield return dir;
                foreach (var file in this.Files)
                    yield return file;
            }
        }


        protected List<DpVirtualDirectory> _Directories;
        public override IEnumerable Directories
        {
            get {
                return _Directories ?? (
                    _Directories = _Storage.GetDirecrories(VirtualPath).
                        Select(d => new DpVirtualDirectory(d, _Storage)).
                        ToList()
                    ); 
            }
        }

        protected List<DpVirtualFile> _Files;
        public override IEnumerable Files
        {
            get
            {
                return _Files ?? (
                    _Files = _Storage.GeFiles(VirtualPath).
                        Select(f => new DpVirtualFile(f, _Storage)).
                        ToList()
                    );
            }
        }
    }
}
