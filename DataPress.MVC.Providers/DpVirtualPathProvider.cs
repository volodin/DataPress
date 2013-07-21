using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Providers
{
    using System.Web.Hosting;

    class DpVirtualPathProvider : VirtualPathProvider 
    {
        protected IVirtualFileStorage _Storage;
        DpCacheDependancy cacheDependancy;

        public DpVirtualPathProvider(IVirtualFileStorage storage)
        {
            _Storage = storage;
        }

        public override bool FileExists(string virtualPath)
        {
            if (_Storage.IsFileExists(virtualPath))
                return true;

            return Previous.FileExists(virtualPath);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            if (_Storage.IsDirExists(virtualDir))
                return true;

            return Previous.DirectoryExists(virtualDir);
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (_Storage.IsFileExists(virtualPath))
            {
                return new DpVirtualFile(virtualPath, _Storage);
            }

            return Previous.GetFile(virtualPath);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            if (_Storage.IsDirExists(virtualDir))
                return new DpVirtualDirectory(virtualDir, _Storage);

            return Previous.GetDirectory(virtualDir);
        }

        public override string GetFileHash(string virtualPath, System.Collections.IEnumerable virtualPathDependencies)
        {
            string hash = _Storage.GetFileHash(virtualPath);
            if (!String.IsNullOrEmpty(hash))
                return hash;

            return Previous.GetFileHash(virtualPath, virtualPathDependencies);
        }

        public override System.Web.Caching.CacheDependency GetCacheDependency(string virtualPath, System.Collections.IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            if (_Storage.IsFileExists(virtualPath))
            {
                if (cacheDependancy.HasChanged)
                    cacheDependancy = new DpCacheDependancy(_Storage);

                return cacheDependancy;
            }

            return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);

        }
    }
}
