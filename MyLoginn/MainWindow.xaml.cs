using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Wpf;  // Dodajte ovu direktivu za korišćenje WebView2 kontrole u WPF
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyLoginn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            InitializeWebView();
             
        }

        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

            string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.html");
            webView.Source = new Uri(htmlFilePath);
        }



       



        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            var message = e.TryGetWebMessageAsString();
            var loginData = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginData>(message);

            if (loginData.Type == "login")
            {
                string email = loginData.Email;
                string password = loginData.Password;

                // Validate login credentials
                // (implement your own validation logic here)
                if (email == "ssasa@gmail.com" && password == "password")
                {
                    MessageBox.Show("Login successful!");
                }
                else
                {
                    MessageBox.Show("Invalid email or password.");
                }
            }
        }
    }


    public class LoginData
    {
        public string Type { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

