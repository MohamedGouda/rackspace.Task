using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace rackspace.Task
{
    public class FileManager
    {
        /*
         * this function is used to write books in file 
         * each book in a row 
         * book data are separated by @
         */
        public bool WriteAllBooks(List<Book> books)
        {
            try
            {
                StreamWriter SW = new StreamWriter(File.Open("./Books.txt", System.IO.FileMode.Create));

                foreach (Book b in books)
                {
                    string currentBook = b.id.ToString() + '@' + b.title + '@' + b.author + '@' + b.description;
                    SW.WriteLine(currentBook);
                }
                SW.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }


        /*
        * this function is used to read books in file 
        * and return a list of books
        */
        public List<Book> ReadFromFile(string FileName)
        {
            try
            {
                List<Book> Books = new List<Book>();
                StreamReader SR = new StreamReader(FileName);
                string line = SR.ReadLine();
                while (line != null)
                {
                    string[] ReadedBook = line.Split('@');
                    Book CurrentBook = new Book();

                    CurrentBook.id = Convert.ToInt32(ReadedBook[0]);
                    CurrentBook.title = ReadedBook[1];
                    CurrentBook.author = ReadedBook[2];
                    CurrentBook.description = ReadedBook[3];

                    Books.Add(CurrentBook);
                    line = SR.ReadLine();

                }
                SR.Close();

                return Books;
            }
            catch
            {
                return null;
            }

        }

        /*
         * this function is used to append the file with a new book
         */
        public bool AppendNewBook(Book NewBook)
        {

            StreamWriter SW = new StreamWriter(File.Open("./Books.txt", System.IO.FileMode.Append));

            string currentBook = NewBook.id.ToString() + '@' + NewBook.title + '@' + NewBook.author + '@' + NewBook.description;
            SW.WriteLine(currentBook);

            SW.Close();
            return true;
        }
    }
}
