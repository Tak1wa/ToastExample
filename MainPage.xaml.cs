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
using Windows.Data.Xml.Dom;
using System.Xml;

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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //ここではトースト通知からのアクティブ化の場合を想定
            if(e.Parameter != null && !string.IsNullOrWhiteSpace(e.Parameter.ToString()))
            {
                txtHoge.Text = e.Parameter.ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Package.appxmanifestの[アプリケーション]->[トースト対応]を「はい」にする

            //名前空間Windows.UI.Notificationsの宣言（省略可能）

            //トーストのXMLテンプレートを取得
            //【参考】テンプレート一覧
            //      https://msdn.microsoft.com/ja-jp/library/windows/apps/windows.ui.notifications.toasttemplatetype.aspx?f=255&MSPPError=-2147217396
            ToastTemplateType template = ToastTemplateType.ToastImageAndText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(template);


            //通知のテキストコンテンツを指定する
            //DOMを使ってノードにアクセスする
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(txtHoge.Text));

            //通知の画像を指定する
            XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            ((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///assets/nezumi.png");

            //トーストの期間を指定する
            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "long");

            //トーストのオーディオを指定する
            //参考：トーストのオーディオオプションカタログ
            //https://msdn.microsoft.com/ja-jp/library/windows/apps/hh761492.aspx
            XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", "ms-winsoundevent:Notification.Mail");
            toastNode.AppendChild(audio);

            //トースト通知からのアクティブ化動作を定義
            //Navigateされてきた時のe.Parameterに格納されてくる
            ((XmlElement)toastNode).SetAttribute("launch", "hogeClicked");

            //指定したXMLコンテンツに基づいてトースト通知を作成する
            var toast = new ToastNotification(toastXml);

            //トースト通知を送信する
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
