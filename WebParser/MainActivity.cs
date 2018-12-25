using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Json;
using System.IO;
using Android.Content;
using Newtonsoft.Json;
using System.Collections.Generic;




namespace WebParser
{
    [Activity(Label = "WebParser", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        Button btn;
        EditText urlToParse;
        ListView urlHistoryListview;
        ArrayAdapter<string> adapter;
        Uri uri;

        string siteParsedData;
        const string parserLink = "https://mercury.postlight.com/parser?url=";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            ActionBar.Hide();
            
            btn = FindViewById<Button>(Resource.Id.webRequestBtn);
            urlToParse = FindViewById<EditText>(Resource.Id.linkToParse);
            urlHistoryListview = FindViewById<ListView>(Resource.Id.listView);


            SetAdapter();
            urlHistoryListview.ItemClick += OnItemClicked;
            btn.Click += Btn_Click;

            
        }

        private void OnItemClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            List<string> urlList = SiteParsedDataSingleton.Instance.GetUrlHistoryList();
            urlToParse.Text = urlList[e.Position];
        }

        private void SetAdapter()
        {
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, SiteParsedDataSingleton.Instance.GetUrlHistoryList());
            urlHistoryListview.Adapter = adapter;
        }

        private void Btn_Click(object sender, System.EventArgs e)
        {
            uri = new Uri(parserLink + urlToParse.Text);
            SiteParsedDataSingleton.Instance.AddUrl(urlToParse.Text);
            SetAdapter();
            try
            {
                siteParsedData = SendGetRequest();


            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            OpenParsedDataActivity();
        }

        private void OpenParsedDataActivity()
        {
            var intent = new Intent(this, typeof(ParserResultActivity));
            intent.PutExtra("parsedData", siteParsedData);
            
            StartActivity(intent);
        }

        private string SendGetRequest()
        {
            
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("x-api-key", "DFq9EY6jHszz6mlNEp58IW773bALmTvErzyqNV4r");

                return client.GetStringAsync(uri).Result;
            }   

            

        }


    }
}

