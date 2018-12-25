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
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;

namespace WebParser
{
    public class ChartFragment : Fragment
    {
        List<Entry> entries = new List<Entry>();
        SiteParsedDataSingleton instance = SiteParsedDataSingleton.Instance;
        ChartView chartView;
        Spinner chartSpinner;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            

        }
        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            chartView = this.View.FindViewById<ChartView>(Resource.Id.barChartView);
            chartSpinner = this.View.FindViewById<Spinner>(Resource.Id.chartSpinner);

            SetChartEntries();
            ChooseChartStyle();

            chartSpinner.ItemSelected += ChartSpinner_ItemSelected;
        }

        private void ChooseChartStyle()
        {
            switch (chartSpinner.SelectedItemId)
            {
                case 0:
                    chartView.Chart = new LineChart() {
                        Entries = entries,
                        
                    };
                    break;
                case 1:
                    chartView.Chart = new DonutChart() {
                        Entries = entries,
                        HoleRadius = 0.5f
                        
                    };
                    break;
                case 2:
                    chartView.Chart = new RadialGaugeChart() {
                        Entries = entries
                    };
                    break;
                case 3:
                    chartView.Chart = new BarChart()
                    {
                        Entries = entries
                    };
                    break;
            }
            chartView.Chart.BackgroundColor = SKColor.Parse("003333");
            chartView.Chart.LabelTextSize = 25.0f;

        }

        private void ChartSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            //Toast.MakeText(this.Context, chartSpinner.SelectedItem.ToString(), ToastLength.Short).Show();
            ChooseChartStyle();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
           
            return inflater.Inflate(Resource.Layout.charts_layout, container, false);
        }

        private void SetChartEntries()
        {
            entries.Clear();
            foreach (var item in instance.HtmlTagCount())
            {
                var entry = new Entry(item.Item2)
                {

                    Color = SKColor.Parse(HexConverter()),
                    Label = item.Item1,
                    ValueLabel = item.Item2.ToString()
                };
                entries.Add(entry);
            }
        }

        private static string HexConverter()
        {
            Android.Graphics.Color c = new Android.Graphics.Color((int)(Java.Lang.Math.Random() * 0x1000000));
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

    }
}