using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataPress.Cms.Storage
{
    public class ViewPart
    {
        public int id { get; set; }


        public string Header { get; set; }
        public string Footer { get; set; }

        public int? ParentViewId { get; set; }
        public View ParentView { get; set; }

        public int? ParentViewPartId { get; set; }
        public ViewPart ParentViewPart { get; set; }

        private ICollection<ViewPart> _Parts;
        public ICollection<ViewPart> Parts {
            get { return _Parts ?? (_Parts = new HashSet<ViewPart>()); }
            set { _Parts = value; } 
        }

    }
}
