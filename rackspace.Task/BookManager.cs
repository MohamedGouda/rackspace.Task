using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using rackspace.Task;

namespace rackspace.Task
{
    public class BookManager
    {

         List<Book> Books { get; set; }
         FileManager FileOperations { get; set; }

        public BookManager(FileManager FM)
        {
            this.FileOperations = FM;
        }
        /*
         * get all book from File using file manager  
         */
        public void ReadAllBooks()
        {
           Books = FileOperations.ReadFromFile("Books.txt");
        }

        /*
         * this function is used to retirn all book 
         */
        public List<Book> GetAllBooks()
        {
            try
            {
                ReadAllBooks();

                return Books;
            }
            catch
            {
                return null;
            }
        }

        /*
         * searching for book by any data
         */
        public Book SearchBooks(string query)
        {
            return (Books.Find(b => b.title.Contains(query) || b.author.Contains(query) || b.description.Contains(query) || b.id == Convert.ToInt32(query)));
        }

        /*
         * get Book By ID
         */
        public Book GetBookById(int BookId)
        {
            return Books.Find(b => b.id == BookId);
        }

        /*
         * add book to the list this 
         */
        public bool AddBook(Book NewBook)
        {
            try
            {
                //FileOperations.AppendNewBook(NewBook);
                if (Books == null)
                    Books = new List<Book>() { new Book() { id = NewBook.id, author = NewBook.author, description = NewBook.description, title = NewBook.title } };
                else
                    Books.Add(NewBook);
                FileOperations.AppendNewBook(NewBook);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
         * edit a book with new data
         */
        public bool EditBook(Book EditedBook, int id)
        {
            try
            {
                Book OldBook = Books.Find(b => b.id == id);
                OldBook = EditedBook;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
         * write all the books to the file
         */
        public bool SaveAndExit()
        {
            try
            {
                return FileOperations.WriteAllBooks(Books);
            }
            catch
            {
                return false;
            }
        }

        /*
         * display data for one book
         */
        public void DisplayBookById(int BookId)
        {
            Book B = Books.Find(b => b.id == BookId);

            if (B != null)
            {
                // display Book Details
                Console.WriteLine();
                Console.WriteLine("\t\t\t Title:" + B.title);
                Console.WriteLine("\t\t\t Author:" + B.author);
                Console.WriteLine("\t\t\t Description:" + B.description);
                Console.WriteLine();
            }
            else
                Console.WriteLine("there is no book with this id");
        }

        /*
         * display all book data
         */
        public void DisplayAllBooks()
        {
            if (Books.Count > 0)
            {
                foreach (Book B in Books)
                {
                    Console.WriteLine("\t\t [" + B.id + "] " + B.title);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("\t\t there are no Books");
            }
        }

        /*
         * get the last ID used for book to know the next Id for next added book
         */
        public int GetLastBookId()
        {
            if (Books == null || Books.Count == 0)
                return 0;

            return Books[Books.Count - 1].id;
        }
    }
}
