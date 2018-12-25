using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace WebParser
{
    class ExpandableView : BaseExpandableListAdapter
    {
        Activity context;
        WebSiteParsedData parsedData;
        List<string> parentList;
        List<string> childList;

        public ExpandableView(Activity newContext, WebSiteParsedData newParsedData) : base()
        {
            context = newContext;
            parsedData = newParsedData;

            parentList = new List<string>(){ "Title", "Content", "Author", "Date Published", "Image url", "Dek", "Next page url", 
                           "Url", "Domain", "Excerpt", "Word count", "Direction", "Total pages", "Rendered pages"};

            childList = new List<string>{ parsedData.title, parsedData.content, parsedData.author, parsedData.date_published,
                                          parsedData.lead_image_url, parsedData.dek, parsedData.next_page_url, parsedData.url,
                                          parsedData.domain, parsedData.excerpt, parsedData.word_count.ToString(), parsedData.direction,
                                          parsedData.total_pages.ToString(), parsedData.rendered_pages.ToString()};
        }
        public override int GroupCount => parentList.Count;

        public override bool HasStableIds => true;

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return 1;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            View child = convertView;
            if (child == null)
            {
                child = context.LayoutInflater.Inflate(Resource.Layout.parsed_data_row, null);
            }
            child.FindViewById<TextView>(Resource.Id.parsedDataText).Text = childList[groupPosition];
            return child;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            throw new NotImplementedException();
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            View parentView = convertView;
            if (parentView == null)
            {
                parentView = context.LayoutInflater.Inflate(Resource.Layout.parent_parsed_data_layout, null);
            }
            TextView text = parentView.FindViewById<TextView>(Resource.Id.titleParsedData);
            text.Text = parentList[groupPosition];
            return parentView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;    
        }
    }
}