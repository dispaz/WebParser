using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace WebParser
{
    public class ParsedDataFragment : Fragment
    {
        ExpandableListView expandableParsedData;
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            expandableParsedData = this.View.FindViewById<ExpandableListView>(Resource.Id.myExpandableListview);
            var parsedData = JsonConvert.DeserializeObject<WebSiteParsedData>(this.Activity.Intent.GetStringExtra("parsedData"));
            SiteParsedDataSingleton.Instance.parsedData = parsedData;
            expandableParsedData.SetAdapter(new ExpandableView(this.Activity, SiteParsedDataSingleton.Instance.parsedData));

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            
            return inflater.Inflate(Resource.Layout.parseddata_layout, container, false);
        }
    }
}