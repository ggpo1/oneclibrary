using OneCLibrary.Models;
using OneCLibrary.Models.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace OneCLibrary.Services
{
    public class OneCService
    {
        private OneСServer server;

        public OneСServer Server { get => server; set => server = value; }

        public OneCService(OneСServer server)
        {
            this.Server = server ?? throw new ArgumentNullException(nameof(server)); 
        }



        #region методы для получения дданных из 1С
        /// <summary>
        /// Method helps to get list of 1C database entities
        /// </summary>
        /// <returns>List of OneCEntity</returns>
        public OneCEntityCollection GetServerEntities()
        {
            OneCEntityCollection entCollection = new OneCEntityCollection();

            var url = Server.Port == Convert.ToString(80) ? GetNormalizedBaseUrl(false) : GetNormalizedBaseUrl(true);
            WebRequest oneCWebRequest = WebRequest.Create(url);
            oneCWebRequest.Credentials = new NetworkCredential(Server.Login, Server.Password);
            WebResponse oneCResponse = oneCWebRequest.GetResponse();
            using (Stream dataStream = oneCResponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseText = reader.ReadToEnd();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(responseText);
                XmlElement xRt = doc.DocumentElement;

                foreach (XmlNode xNd in xRt)
                {
                    foreach (XmlNode xNdChild in xNd.ChildNodes)
                    {
                        if (xNdChild.Name == "collection")
                        {
                            string[] words = xNdChild.Attributes[0].Value.Split(new char[] { '_' });
                            var collectionName = words[1];
                            var collectionType = words[0];
                            var collectionUrl = xNdChild["atom:title"].InnerText;
                            entCollection.OneCEntitiesList.Add(new OneCEntity(
                                collectionName,
                                collectionType,
                                collectionUrl
                            ));
                        }
                    }
                }
            }

            return entCollection;
        }
        /// <summary>
        /// Method helps to get a collection of server entity columns by entity
        /// </summary>
        /// <param name="oneCEntity"></param>
        /// <returns></returns>
        public OneCEntityColumnsCollection GetServerEntitiesColumns(OneCEntity oneCEntity)
        {

            var url = Server.Port == Convert.ToString(80) ? GetNormalizedBaseUrl(false) : GetNormalizedBaseUrl(true);
            url = url + "/" + oneCEntity.Url + "?$format=application/json";
            WebRequest oneCWebRequest = WebRequest.Create(url);
            oneCWebRequest.Credentials = new NetworkCredential(Server.Login, Server.Password);
            WebResponse oneCResponse = oneCWebRequest.GetResponse();


            Stream receiveStream = oneCResponse.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);


            var text = readStream.ReadToEnd();
            oneCResponse.Close();
            readStream.Close();
            return FromJson(text);
            // return columnsList;
        }
        //TODO method for getting rows by entity and columns
        public Dictionary<string, string> GetEntityRows(OneCEntity entity, List<OneCEntityColumn> columns)
        {
            Dictionary<string, string> entityRows = new Dictionary<string, string>() { };
            foreach (OneCEntityColumn column in columns)
            {
                string value = "";

                entityRows.Add(column.ColumnName, value);
            }
            return entityRows;
        }

        #endregion



        #region методы для отправки данных в 1С

        #endregion



        #region вспомогательные методы для работы с 1С
        private OneCEntityColumnsCollection FromJson(string str)
        {
            OneCEntityColumnsCollection columnsList = new OneCEntityColumnsCollection();
            OneCEntityColumnsCollection collection = new OneCEntityColumnsCollection();
            int valueSpace = str.IndexOf("value");
            str = str.Substring(valueSpace + 12);
            str = str.Substring(0, str.Length - 5);

            string[] words = str.Split(new char[] { ':' });

            List<OneCEntityColumn> lister = new List<OneCEntityColumn>();

            foreach (var elem in words)
            {
                lister.Add(new OneCEntityColumn(elem));
            }
            List<OneCEntityColumn> newLister = new List<OneCEntityColumn>();
            lister.ForEach(delegate (OneCEntityColumn line) {
                string[] w = line.ColumnName.Split(new char[] { ',' });
                if (w.Length < 2)
                {
                    var podstr = w[0].Replace(Environment.NewLine, ""); ;
                    podstr = podstr.Replace("\"", "");
                    if (podstr.Contains('{'))
                        podstr = podstr.Replace("{", "");
                    bool inList = false;
                    foreach (var el in newLister)
                    {
                        if (el.ColumnName.Contains(podstr))
                            inList = true;
                    }
                    bool isConverted = false;
                    try
                    {
                        int converted = Convert.ToInt32(podstr);
                        isConverted = true;
                    }
                    catch
                    {
                        isConverted = false;
                    }
                    if (!inList)
                        if (!isConverted)
                            if (!podstr.Contains("-"))
                                newLister.Add(new OneCEntityColumn(podstr));

                }
                else
                {
                    string podstr = "";
                    foreach (var el in w)
                    {
                        podstr = el.Replace(Environment.NewLine, ""); ;
                        podstr = podstr.Replace("\"", "");
                    }
                    if (podstr.Contains('{'))
                        podstr = podstr.Replace("{", "");
                    bool inList = false;
                    foreach (var el in newLister)
                    {
                        if (el.ColumnName.Contains(podstr))
                            inList = true;
                    }
                    bool isConverted = false;
                    try
                    {
                        int converted = Convert.ToInt32(podstr);
                        isConverted = true;
                    }
                    catch
                    {
                        isConverted = false;
                    }
                    if (!inList)
                        if (!isConverted)
                            if (!podstr.Contains("-"))
                                newLister.Add(new OneCEntityColumn(podstr));
                }
            });
            newLister.RemoveAt(newLister.Count - 1);
            collection.OneCEntityColumns = newLister;
            return collection;
        }
        /// <summary>
        /// Helps to get BaseUrl
        /// </summary>
        /// <param name="hasPort"></param>
        /// <returns></returns>
        public string GetNormalizedBaseUrl(bool hasPort)
        {
            if (hasPort)
                return "http://" + Server.Host + ":" + Server.Port + "/" + Server.DbName+ "/odata/standard.odata";
            else
                return "http://" + Server.Host + "/" + Server.DbName + "/odata/standard.odata";
        }
        #endregion
    }
}