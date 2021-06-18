using System;
using System.Collections.Generic;

namespace rackspace.Task
{
    class Program
    {
        public static BookManager BM { get; set;}
        public static FileManager FM { get; set; }
        public static List<Book> Books { get; set; }  
        public static Operations tasks { get; set; }

        public static void Main(string[] args)
        {
            FM = new FileManager();
            BM = new BookManager(FM);


            tasks = new Operations(BM , BM.GetAllBooks());

            // display the main menu
            tasks.DisplayFirstMenu();
            
        }

        
    }
}
