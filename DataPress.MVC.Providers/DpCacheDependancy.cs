using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.MVC.Providers
{
    using System.Web.Caching;

    public class DpCacheDependancy : CacheDependency
    {
        IVirtualFileStorage _Storage;

        public DpCacheDependancy(IVirtualFileStorage storage)
        {
            _Storage = storage;
            _Storage.OnInvalidateCache += new Action(storage_OnInvalidateCache);
        }

        void storage_OnInvalidateCache()
        {
            this.NotifyDependencyChanged(this, EventArgs.Empty);
        }

        protected override void DependencyDispose()
        {
            _Storage.OnInvalidateCache -= storage_OnInvalidateCache;
            base.DependencyDispose();
        }
    }
}
