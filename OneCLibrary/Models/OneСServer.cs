using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace OneCLibrary.Models
{
    public class OneСServer
    {

        public OneСServer(string host, string port, string dbName, string login, string password)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port ?? throw new ArgumentNullException(nameof(port));
            DbName = dbName ?? throw new ArgumentNullException(nameof(dbName));
            Login = login ?? throw new ArgumentNullException(nameof(login));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public OneСServer(int serverID, string host, string port, string dbName, string login, string password)
        {
            ServerID = serverID;
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port ?? throw new ArgumentNullException(nameof(port));
            DbName = dbName ?? throw new ArgumentNullException(nameof(dbName));
            Login = login ?? throw new ArgumentNullException(nameof(login));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public int ServerID { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string DbName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string ToHostPort()
        {
            return this.Host + ":" + this.Port;
        }

        public override string ToString()
        {
            return this.ServerID.ToString() + " - " + this.Host + ":" + this.Port + " - " + this.DbName;
        }

        
    }
}