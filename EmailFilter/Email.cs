﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmailFilter
{
    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
    }
}
