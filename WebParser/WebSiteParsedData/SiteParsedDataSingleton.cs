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

namespace WebParser
{
    public class SiteParsedDataSingleton
    {
        private const int urlHistoryMaxSize = 5;
        private static SiteParsedDataSingleton instance;
        private Stack<string> urlHistory = new Stack<string>();
        private string[] htmlTagsInContent;

        private SiteParsedDataSingleton()
        {
            urlHistory.Push("https://www.wired.com/2016/09/ode-rosetta-spacecraft-going-die-comet/");
            urlHistory.Push("https://habr.com/post/354796/");
            urlHistory.Push("https://www.google.com");

        }

        public WebSiteParsedData parsedData;
        public static SiteParsedDataSingleton Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SiteParsedDataSingleton();

                }
                return instance;
            }
        }

        public List<Tuple<string, int>> HtmlTagCount()
        {
            string text = parsedData.content;
            List<string> strings = new List<string>();
            List<Tuple<string, int>> tuples = new List<Tuple<string, int>>();
            htmlTagsInContent= text.Split('<');
            int i = 0;
            foreach(string item in htmlTagsInContent)
            {

                if (item != "" && item[0] == '/')
                {
                    string newItem = item;
                    newItem = item.Replace("/", "");
                    newItem = newItem.Remove(newItem.IndexOf('>'));
                    item.Replace(item, newItem);
                    strings.Add(newItem);
                    i++;
                }
            }
            if(tuples.Count == 0 && strings.Count >= 1)
            {
                tuples.Add(new Tuple<string, int>(strings[0], 1));
            }
            bool bToAdd = true;
            for (i = 1; i < strings.Count; i++)
            {
                bToAdd = true;
                for (int j = 0; j < tuples.Count; j++)
                {
                    if (strings[i] == tuples[j].Item1)
                    {
                        tuples[j] = new Tuple<string, int>(strings[i], tuples[j].Item2 + 1);
                        bToAdd = false;
                        break;
                    }
                    
                }
                if (bToAdd)
                {
                    tuples.Add(new Tuple<string, int>(strings[i], 1));
                }
            }
            var result = tuples.OrderBy(x => x.Item2).ToList<Tuple<string, int>>();
            result.Reverse();
            while(result.Count > 10)
            {
                result.RemoveAt(result.Count - 1);
            }
            return result;
        }

        public void AddUrl(string uri)
        {
            if(urlHistory.Count < urlHistoryMaxSize)
            {
                urlHistory.Push(uri);

            }
            else if(urlHistory.Count >= urlHistoryMaxSize)
            {
                Stack<string> tempStack = new Stack<string>();
                int size = urlHistory.Count;
                for (int i = 0; i < size; i++)
                {
                    if(i == (size-1))
                    {
                        urlHistory.Pop();
                        break;
                    }
                    tempStack.Push(urlHistory.Pop());
                }
                urlHistory.Clear();
                foreach(string item in tempStack)
                {
                    urlHistory.Push(item);
                }
                urlHistory.Push(uri);
            }

        }

        public List<string> GetUrlHistoryList()
        {
            List<string> returnList = new List<string>();
            foreach(string item in urlHistory)
            {
                returnList.Add(item);
            }
            return returnList;
        }
    }
}