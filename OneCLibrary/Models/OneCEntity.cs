using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace OneCLibrary.Models
{
    public class OneCEntity
    {
        public OneCEntity(string name, string type, string url)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
        public OneCEntity(long id, string name, string type, string url)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }
        public OneCEntity(long id, string name, string type, string url, long serverId)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            ServerId = serverId;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public long ServerId { get; set; }

        public override string ToString()
        {
            return this.Url;
        }

    }
}