﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sia.Domain.Playbooks
{
    public class ActionTemplate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsUrl { get; set; }
        public string Template { get; set; }
        public ICollection<ActionTemplateSource> Sources { get; set; }
            = new List<ActionTemplateSource>();
    }
}
