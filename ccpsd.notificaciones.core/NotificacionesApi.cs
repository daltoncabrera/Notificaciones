using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ccpsd.notificaciones.core
{
    /// <summary>
    /// Api para consumir webapi de notificaciones
    /// </summary>
    public class NotificacionesApi
    {

        #region Propiedades

        public string Server { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string App { get; set; }
        public string Key { get; set; }
        public string Token { get; private set; }

        #endregion

        #region contructores

        public NotificacionesApi(string server,
            string user,
            string pass,
            string app = "AppApiConsumer",
            string key = "Dzm5Pk2Sq96dn8GpbORoP7stDHXPTrhJaaJc3k5ocRg=")
        {
            if (!server.EndsWith("/"))
                server = server.TrimEnd('/');

            this.Server = server;//string.Format("{0}/api/", server);
            this.App = app;
            this.Key = key;
            this.User = user;
            this.Pass = pass;

            ConnectApi();
        }



        #endregion

        #region Public Api

        public int CrearNotificacion(NotificacionModel notiModel)
        {
            notiModel.Aplicacion = this.App;
            return SendNotificacion(notiModel);
        }

        private NotificacionModel GetNotificacion(int notificacionId)
        {
            throw new NotImplementedException();
        }

        private List<NotificacionesLogModel> GetNotificacionLogs(int notificacionId)
        {
            throw new NotImplementedException();
        }

        private NotificacionesLogModel GetUserNotificacionLog(int notificacionId, string username)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helpers

        private void ConnectApi()
        {
            Token = GetAPIToken(this.User, this.Pass, this.App, this.Key, this.Server);
        }

        private string GetAPIToken(string userName, string password, string clientId, string secret, string apiBaseUri)
        {
            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(apiBaseUri);
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //setup login data
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"), 
                    new KeyValuePair<string, string>("client_id", clientId), 
                    new KeyValuePair<string, string>("client_secret", secret),
                    new KeyValuePair<string, string>("username", userName), 
                    new KeyValuePair<string, string>("password", password), 
                });

                HttpResponseMessage responseMessage = null;

                //send request
                responseMessage = client.PostAsync("/token", formContent).Result;

                //voy a manejar el error
                //responseMessage.EnsureSuccessStatusCode();

                //get access token from response body
                var responseJson = responseMessage.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(responseJson);

                JToken tokenError = "";
                var hasError = jObject.TryGetValue("error", out tokenError);

                if (hasError)
                {
                    JToken tokenMsgError = "";
                    jObject.TryGetValue("error_description", out tokenMsgError);
                    throw new Exception(string.Format("Error - {0}: {1}", tokenError, tokenMsgError));
                }

                JToken tokenAccess = "";
                var hasToken = jObject.TryGetValue("access_token", out tokenAccess);

                if (!hasToken)
                    throw new Exception("No se puedo tener token de  accesso, favor verificar servidor");

                return tokenAccess.ToString();
            }
        }


        int SendNotificacion(NotificacionModel notiModel)
        {

            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(this.Server);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.Token);



                var content = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(notiModel),
                    Encoding.UTF8,
                    "application/json");

                var responseMessage = client.PostAsync("api/Notificaciones/Create", content).Result;

                //get access token from response body
                var responseJson = responseMessage.Content.ReadAsStringAsync().Result;
                var jObject = JObject.Parse(responseJson);

                JToken tokenError = "";
                var hasError = jObject.TryGetValue("error", out tokenError);

                if (hasError)
                {
                    JToken tokenMsgError = "";
                    jObject.TryGetValue("error_description", out tokenMsgError);
                    throw new Exception(string.Format("Error - {0}: {1}", tokenError, tokenMsgError));
                }

                var notiModelResult = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificacionModel>(responseJson);

                if (notiModelResult == null || !notiModelResult.NotificacionId.HasValue)
                    throw  new Exception("Error creando notificacion");


                return notiModelResult.NotificacionId.Value;// notiCreated.NotificacionId.Value;
            }
        }




        string GetRequest(string token, string apiBaseUri, string requestPath)
        {
            using (var client = new HttpClient())
            {
                //setup client
                client.BaseAddress = new Uri(apiBaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                //make request
                HttpResponseMessage response = client.GetAsync(requestPath).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                return responseString;
            }
        }


        #endregion

    }
}
