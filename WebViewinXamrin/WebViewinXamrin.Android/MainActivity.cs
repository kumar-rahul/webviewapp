using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;

namespace WebViewinXamrin.Droid
{
	[Activity (Label = "WebView App", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        //Creating Instance of Button,TextView, WebView
        Button btnBack;
        Button btnGo;
        Button btnForward; TextView txtURL;
        WebView webView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            //Get txtURL TextView, btnGo Button,webView WebView from Main Resource Layout.
            txtURL = FindViewById<TextView>(Resource.Id.txtURL);
            btnGo = FindViewById<Button>(Resource.Id.btnGO);
            btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnForward = FindViewById<Button>(Resource.Id.btnForward);
            webView = FindViewById<WebView>(Resource.Id.webView);

            //SetWebViewClient with an instance of WebViewClientClass
            webView.SetWebViewClient(new WebViewClientClass());
            webView.LoadUrl(txtURL.Text);

            //Enabled Javascript in Websettings
            WebSettings websettings = webView.Settings;
            websettings.JavaScriptEnabled = true;

            btnGo.Click += BtnGo_Click;
            btnBack.Click += BtnBack_Click;
            btnForward.Click += BtnForward_Click;
        }

        public bool isValidUrl(string url)
        {
            return Android.Util.Patterns.WebUrl.Matcher(url).Matches();
        }

        private void BtnForward_Click(object sender, System.EventArgs e)
        {
            //If WebView has forward History item then forward to the next visited page.
            if (webView.CanGoForward())
            {
                webView.GoForward();
            }
        }
        private void BtnBack_Click(object sender, System.EventArgs e)
        {
            //If WebView has back History item then navigate to the last visited page.
            if (webView.CanGoBack())
            {
                webView.GoBack();
            }
        }
        private void BtnGo_Click(object sender, System.EventArgs e){
            string inputurl = txtURL.Text.ToString();
            var urlvalidate = isValidUrl(inputurl);
            if (inputurl == ""){
                Toast.MakeText(this, "Enter the Web Url.!", ToastLength.Short).Show();
            }
            else{
                if (urlvalidate == true){
                    webView.LoadUrl(txtURL.Text);
                }
                else{
                    Toast.MakeText(this, "Url is Not Valid", ToastLength.Short).Show();
                }
            }
        }
    }
    internal class WebViewClientClass : WebViewClient{
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}


