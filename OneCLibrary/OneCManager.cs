using OneCLibrary.Models;
using OneCLibrary.Models.Collections;
using OneCLibrary.Services;
using System;
using System.Collections.Generic;

namespace OneCLibrary
{
    public static class OneCManager
    {
        private static OneCService Service;

        public static void SetService(OneCService service)
        {
            Service = service;
        }
        public static void SetServer(OneСServer server)
        {
            Service.Server = server;   
        }
        public static List<OneCEntity> GetEntities()
        {
            return Service.GetServerEntities().OneCEntitiesList;
        }

        public static List<OneCEntityColumn> GetEntityColumns(OneCEntity entity)
        {
            return Service.GetServerEntitiesColumns(entity).OneCEntityColumns;
        }

        public static Dictionary<string, string> GetEntityRows(OneCEntity entity, List<OneCEntityColumn> columns)
        {
            return Service.GetEntityRows(entity, columns);
        }
    }
}
