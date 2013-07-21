using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Providers
{
    using System.Web.Hosting;

    public class DpVirtualFile : VirtualFile
    {
        protected IVirtualFileStorage _Storage;

        public DpVirtualFile(string virtualPath, IVirtualFileStorage storage)
            : base(virtualPath)
        {
            _Storage = storage;
        }

        public override System.IO.Stream Open()
        {
            return _Storage.OpenStream(this.VirtualPath);
        }

    }
}
