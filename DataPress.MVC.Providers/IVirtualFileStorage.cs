using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataPress.MVC.Providers
{
    public interface IVirtualFileStorage
    {
        Stream OpenStream(string virualPath);

        string GetFileHash(string virualPath);

        bool IsFileExists(string virualPath);
        bool IsDirExists(string virualPath);

        IEnumerable<string> GetDirecrories(string virtualPath);
        IEnumerable<string> GeFiles(string virtualPath);

        void InvalidateCache();

        event Action OnInvalidateCache;
    }
}
