﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WebParser
{
    public class WebSiteParsedData
    {
        public string title { get; set; }
        public string content { get; set; }
        public string author { get; set; }
        public string date_published { get; set; }
        public string lead_image_url { get; set; }
        public string dek { get; set; }
        public string next_page_url { get; set; }
        public string url { get; set; }
        public string domain { get; set; }
        public string excerpt { get; set; }
        public int word_count { get; set; }
        public string direction { get; set; }
        public int total_pages { get; set; }
        public int rendered_pages { get; set; }
    }

}