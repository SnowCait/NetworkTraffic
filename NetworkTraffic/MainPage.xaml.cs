using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace NetworkTraffic
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // https://social.msdn.microsoft.com/Forums/ja-JP/27d3fdba-d433-4b6b-b370-f58cf41381ea?forum=winstoreapp

            var states = new Windows.Networking.Connectivity.NetworkUsageStates();

            var since = DateTime.Now - TimeSpan.FromMinutes(30);
            var until = DateTime.Now;

            var profile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
            var list = await profile.GetNetworkUsageAsync(since, until, Windows.Networking.Connectivity.DataUsageGranularity.PerMinute, states);
            for (var i = 0; i < list.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(i + string.Format(". Sent: {0:#,0}bytes, Received: {1:#,0}bytes." + Environment.NewLine, list[i].BytesSent, list[i].BytesReceived));
            }
        }
    }
}
