using OneCLibrary;
using OneCLibrary.Models;
using OneCLibrary.Models.Collections;
using OneCLibrary.Services;
using System;

namespace OneCLibraryTest
{
    class Program
    {
        static void Main(string[] args)
        {
            OneСServer server = new OneСServer(
                "tvk49.technovik.ru",
                "80",
                "ServerTest",
                "admin",
                "123"
            );
            Console.WriteLine("Server: " + server.ToString());

            Console.WriteLine("\tEntities: ");
            OneCService service = new OneCService(server);
            foreach (OneCEntity elem in service.GetServerEntities().OneCEntitiesList)
            {
                Console.WriteLine("\t\t" + elem.ToString());
                OneCEntityColumnsCollection entityColumnsCollection = service.GetServerEntitiesColumns(elem);
                Console.WriteLine("\t\t\tColumns:");
                foreach (OneCEntityColumn entityColumn in entityColumnsCollection.OneCEntityColumns)
                {
                    Console.WriteLine("\t\t\t\t" + entityColumn.ColumnName);
                }
            }

            Console.ReadKey();
        }
    }
}
