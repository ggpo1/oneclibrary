using OneCLibrary;
using OneCLibrary.Models;
using OneCLibrary.Models.Collections;
using OneCLibrary.Services;
using System;
using System.Collections.Generic;

namespace OneCLibraryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("___________Service_testing___________");
            OneСServer server = new OneСServer(
                "tvk49.technovik.ru",
                "80",
                "ServerTest",
                "admin",
                "123"
            );
            Console.WriteLine("  Server: " + server.ToString());

            Console.WriteLine("    Entities: ");
            OneCService service = new OneCService(server);
            foreach (OneCEntity elem in service.GetServerEntities().OneCEntitiesList)
            {
                Console.WriteLine("      " + elem.ToString());
                OneCEntityColumnsCollection entityColumnsCollection = service.GetServerEntitiesColumns(elem);
                Console.WriteLine("        Columns:");
                foreach (OneCEntityColumn entityColumn in entityColumnsCollection.OneCEntityColumns)
                {
                    Console.WriteLine("          " + entityColumn.ColumnName);
                }
            }


            Console.WriteLine("___________Manager_testing___________");
            Console.WriteLine("  Server: " + server.ToString());
            Console.WriteLine("    Entities: ");
            OneCManager.SetService(new OneCService(server));
            List<OneCEntity> entities = OneCManager.GetEntities();
            foreach (OneCEntity entity in entities)
                Console.WriteLine("      " + entity.Name);

            Console.WriteLine("    Columns: ");
            foreach (OneCEntity entity in entities)
            {
                List<OneCEntityColumn> entityColumns = OneCManager.GetEntityColumns(entity);
                Console.WriteLine("      " + entity.Name + ":");
                foreach (OneCEntityColumn column in entityColumns)
                    Console.WriteLine("        " + column.ColumnName);
            }
            

            Console.ReadKey();
        }
    }
}
