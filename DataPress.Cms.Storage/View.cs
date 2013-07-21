using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.Cms.Storage
{
    public class View
    {
        public int id { get; set; }

        public string Content { get; set; }

        public string Header { get; set; }
        public string Footer { get; set; }

        public int? LayoutId { get; set; }
        public Layout Layout { get; set; }

        private ICollection<ViewPart> _Parts;
        public ICollection<ViewPart> Parts
        {
            get { return _Parts ?? (_Parts = new HashSet<ViewPart>()); }
            set { _Parts = value; }
        }
    }
}
