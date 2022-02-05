using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;


namespace APIConnect
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    class Quote
    {
        public int id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string description { get; set; }
    }
    class ResponceQuotes
    {
        public bool success { get; set; }
        public List<Quote> data { get; set; }
    }
    class Authorization
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    class Error
    {
        public string error { get; set; }
        public string success { get; set; }
    }
    class User
    {
        public string id { get; set; }
        public string email { get; set; }
        public string nickName { get; set; }
        public string avatar { get; set; }
        public string token { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GETQuotes("http://mskko2021.mad.hakta.pro/api/quotes");
        }
        async void GETQuotes(string site)
        {
            string str = "";
            await Task.Run(() => // запускаем отдельный поток
            {
                using(WebClient webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8; //задаем кодировку, в большенстве случаев - UTF-8
                    webClient.Headers.Add("Content-Type", "application/json"); //при необхоимости указываем 
                    //webClient.QueryString.Add("ключ", "значение"); - если нужны параметрты для запроса
                    str = webClient.DownloadString(site); // получаем ответ
                }
            });
            ResponceQuotes quotes = JsonConvert.DeserializeObject<ResponceQuotes>(str); //преобразуем строку JSON к классу
            getListBox.ItemsSource = quotes.data;
        }
        async void POSTUserLogin(string site)
        {
            string str = "";
            User user = null;
            string json = JsonConvert.SerializeObject(new Authorization { email = emailTextBox.Text, password = passwordBox.Password });
            await Task.Run(() => // запускаем отдельный поток
            {
                using(WebClient webClient = new WebClient())
                {
                    webClient.Encoding = Encoding.UTF8;
                    //NameValueCollection pars = new NameValueCollection(); - используем, если нам надо отправить несколько параметров
                    //pars.Add("", str); - используем, если нам надо отправить несколько параметров
                    try
                    {
                        str = webClient.UploadString(site, json); //используем, если нам надо отправить параметр в виде json 
                        //str = Encoding.Default.GetString(webClient.UploadValues(site, pars)); - используем, если нам надо отправить несколько параметров
                        user = JsonConvert.DeserializeObject<User>(str);
                    }
                    catch (WebException ex) //если получаем ошибку от сайта
                    {
                        string response = "";
                        using (var reader = new StreamReader(ex.Response.GetResponseStream(), Encoding.UTF8))
                            response = reader.ReadToEnd(); //преобразуем ответ в строку
                        Error error = JsonConvert.DeserializeObject<Error>(response); //преобразуем Json в объекта класса Error
                        MessageBox.Show(error.error);
                    }
                   
                }
            }
            );
            if(user !=null)
                userStackPanel.DataContext = user;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            POSTUserLogin("http://mskko2021.mad.hakta.pro/api/user/login");
        }
    }
}
