using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace BiometricVerificationClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const string BlogRssUrl = "http://80.237.85.55:8088/api/clients/submit_user_profile";
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPageLoaded;
        }

        void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            var client = new WebClient();
            client.UploadStringCompleted += ClientDownloadStringCompleted;
            var bytes = GetBytes("testBytesString");
            string request = "password=testPassword&profile=" + bytes.ToString();
            client.UploadStringAsync(new Uri(BlogRssUrl), "POST", request);
        }

        void ClientDownloadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error == null && !e.Cancelled)
            {
                MessageBox.Show(e.Result.ToString());
            }
            else
            {
                Console.WriteLine(e.Error.ToString());
                MessageBox.Show(e.Error.ToString());
            }
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
 
    }
}