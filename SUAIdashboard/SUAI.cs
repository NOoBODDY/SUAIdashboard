using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using SUAIdashboard.Model;

namespace SUAIdashboard
{
    public class SUAI
    {
        public delegate void ErrorMessage(string message);
        public event ErrorMessage ErrorNotify;

        string PHPSESSID;
        string sharedsessioID;
        string login;
        string password;
        string userId;

        public SUAI(string name, string pass)
        {
            login = name;
            password = pass;
            //getPHPSessID();
            //LogIn();
            //GetUserID();
        }

        public void getPHPSessID()
        {
            string url = "https://pro.guap.ru/user/login";

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response != null)
            {
                PHPSESSID = response.Headers.Get("Set-Cookie");
                if (PHPSESSID != null)
                {
                    PHPSESSID = PHPSESSID.Substring(10, 26);
                }
                else
                {
                    ErrorNotify("PHPSESSID ERROR");
                }
                
                response.Close();
            }
            else
            {
                ErrorNotify("PHPSESSID RESPONSE ERROR");
            }
        }
        public void LogIn()
        {
            string url = "https://pro.guap.ru/user/login_check";
            string url2 = "https://pro.guap.ru/login_redirect";

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();
            Cookie cookie = new Cookie("PHPSESSID", PHPSESSID);
            cookie.Domain = "pro.guap.ru";
            cookie.Path = "/";
            request.AllowAutoRedirect = false;
            request.CookieContainer.Add(cookie);
            string payload = String.Format("_username={0}&_password={1}", login, password);
            request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            if (response.Cookies["PHPSESSID"] != null)
            {
                PHPSESSID = response.Cookies["PHPSESSID"].Value;
                response.Close();
                request = HttpWebRequest.Create(url2) as HttpWebRequest;
                request.Method = "GET";
                request.CookieContainer = new CookieContainer();
                cookie = new Cookie("PHPSESSID", PHPSESSID);
                cookie.Domain = "pro.guap.ru";
                cookie.Path = "/";
                request.AllowAutoRedirect = false;
                request.CookieContainer.Add(cookie);
                response = request.GetResponse() as HttpWebResponse;

                sharedsessioID = response.Cookies["sharedsessioID"].Value.ToString();
            }
            else
            {
                ErrorNotify("PASSWORD OR LOGIN WAS WRONG");
            }

            
            response.Close();
        }

        public void GetUserID()
        {
            string url = "https://pro.guap.ru/inside_s";
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("PHPSESSID", PHPSESSID, "/", "pro.guap.ru"));
            request.CookieContainer.Add(new Cookie("sharedsessioID", sharedsessioID, "/", ".guap.ru"));

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string htmlString;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    htmlString = reader.ReadToEnd();
                }
            }
            response.Close();
            int startIndex = htmlString.IndexOf("window.__initialServerData =") + 29;
            int endIndex = htmlString.IndexOf(";", startIndex);
            string jsonStr = htmlString.Substring(startIndex, endIndex - startIndex);

            dynamic json = JObject.Parse(jsonStr);

            userId = json.user[0].user_id;
        }

        public ObservableCollection<LabWork> GetTasks()
        {
            string url = "https://pro.guap.ru/get-student-tasksdictionaries/";
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("PHPSESSID", PHPSESSID, "/", "pro.guap.ru"));
            request.CookieContainer.Add(new Cookie("sharedsessioID", sharedsessioID, "/", ".guap.ru"));

            string payload = String.Format("iduser={0}", userId);
            request.GetRequestStream().Write(Encoding.UTF8.GetBytes(payload), 0, payload.Length);
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            string jsonString;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    jsonString = reader.ReadToEnd();
                }
            }
            response.Close();
            var json = JObject.Parse(jsonString).SelectToken("tasks");
            ObservableCollection<LabWork> labs = json.ToObject<ObservableCollection<LabWork>>();
            return labs;
        }

    }
}
