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

using Windows.UI.Notifications;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace トースト通知のサンプル
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //【参考】
            //https://msdn.microsoft.com/ja-jp/library/windows/apps/hh779727.aspx?f=255&MSPPError=-2147217396

            //①Package.appxmanifestの[アプリケーション]->[トースト対応]を「はい」にする

            //②名前空間Windows.UI.Notificationsの宣言（省略可能）

            //③トーストのXMLテンプレートを取得
            //以下に一覧がある
            //https://msdn.microsoft.com/ja-jp/library/windows/apps/windows.ui.notifications.toasttemplatetype.aspx?f=255&MSPPError=-2147217396
            var template = ToastTemplateType.ToastImageAndText01;
            var toastXml = ToastNotificationManager.GetTemplateContent(template);

            //以下のXMLスケルトンが取得される
            //<toast>
            //    <visual>
            //        <binding template="ToastImageAndText01">
            //            <image id="1" src=""/>
            //            <text id="1"></text>
            //        </binding>
            //    </visual>
            //</toast>

            //④通知のテキストコンテンツを指定する
            //DOMを使ってノードにアクセスする
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode("Hello World!"));

            //⑤通知の画像を指定する
            var toastImageElements = toastXml.GetElementsByTagName("image");
        }
    }
}
