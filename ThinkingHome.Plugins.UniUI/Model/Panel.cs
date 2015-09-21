﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkingHome.Plugins.UniUI.Model
{
    public class Panel
    {
        public virtual Guid Id { get; set; }

        public virtual Dashboard Dashboard { get; set; }

        public virtual string Title { get; set; }

        public virtual int SortOrder { get; set; }
    }
}
