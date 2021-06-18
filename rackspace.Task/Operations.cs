using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rackspace.Task;


namespace rackspace.Task
{
    public class Operations
    {

        public BookManager BM { get; set; }
        public List<Book> Books { get; set; }

        /*
         * Class constructor 
         */
        public Operations(BookManager BM , List<Book> Books)
        {
            this.BM = BM;
            this.Books = Books;

        }

        /*
         * this function is used to get all books and save it in Books list
         */
        public  void ReadAllBooks()
        {
            Books = BM.GetAllBooks();
        }


        /*
         * this function is used to display the main menu Options
         */
        public  void DisplayFirstMenu()
        {
            Console.WriteLine("==== Book Manager ====");

            Console.WriteLine("     1) View all books");
            Console.WriteLine("     2) Add a book");
            Console.WriteLine("     3) Edit a book");
            Console.WriteLine("     4) Search for a book");
            Console.WriteLine("     5) Save and exit");

            Console.Write("Choose [1-5]:");


            CheckMainMenuUserInput();

        }


        /*
         * this function is used to display all books ( IDs & titles) 
         * and check if user need to display all the data for a specific user
         * if so it will call the function that display the data for the Book
         */
        public  void ViewAllBooks()
        {

            if (Books != null || Books.Count > 0)
            {
                foreach (Book b in Books)
                {
                    Console.WriteLine("[" + b.id + "] " + b.title);
                }



                Console.WriteLine("To view details enter the book ID, to return press <Enter>");
                Console.Write("\t\t Book ID:");

            }

            else
                Console.WriteLine("there are no books\n\n");


            string key = Console.ReadKey().Key.ToString();
            if (key == "Enter")
                DisplayFirstMenu();
            else
            {

                string UserInput = key.Substring(1);

                int BookId = Convert.ToInt32(UserInput);
                DisplayBookById(BookId);
            }
        }

        /*
         * this function is used to display the data for sepcific Book by its ID
         */
        public  void DisplayBookById(int id)
        {
            BM.DisplayBookById(id);


            // Ask If he Need To view Another Book Or not
            Console.WriteLine("To view details enter the book ID, to return press <Enter>");
            Console.Write("\t\t Book ID:");




            string key = Console.ReadKey().Key.ToString();
            if (key == "Enter")
                DisplayFirstMenu();
            else
            {
                string UserInput = key.Substring(1); ;
                int Id = Convert.ToInt32(UserInput);
                DisplayBookById(Id);
            }

        }


        /*
         * this function is used to Add Book To the file 
         * by taking the data and send it to File Book Object (PM) to save it in the file
         * 
         */
        public  void AddBook()
        {
            Book NewBook = new Book();

            Console.WriteLine("\t\t ==== Add a Book ====");
            Console.WriteLine("\t\t Please enter the following information:");

            NewBook.id = BM.GetLastBookId() + 1;

            Console.Write("\t\t Title:");
            NewBook.title = Console.ReadLine();
            Console.WriteLine();

            Console.Write("\t\t Author:");
            NewBook.author = Console.ReadLine();
            Console.WriteLine();

            Console.Write("\t\t Description:");
            NewBook.description = Console.ReadLine();
            Console.WriteLine();

            BM.AddBook(NewBook);
        }


        /*
         * this function is used to udate the data for a specific book by its ID 
         * and sends the new Data to Book Manager (PM) to save it in the file
         * 
         * displayBooks parameter is used to distinguish to show the main  menu of editing or not
         * that if the Edit function is called from the main menu ==> edit main menu shoul be appeared
         * or its called after edit a book to edit another one  ==> edit main menu will not appear
         * 
         */
        public  void EditBook(bool displayBooks)
        {
            if (displayBooks)
            {
                Console.WriteLine("==== Edit a Book ====");

                BM.DisplayAllBooks();
            }

            Console.WriteLine("Enter the book ID of the book you want to edit; to return press <Enter>.");
            Console.Write("\t\t Book ID:");


            string key = Console.ReadKey().Key.ToString();
            Console.WriteLine();

            if (key == "Enter")
                DisplayFirstMenu();
            else
            {
                string UserInput = key.Substring(1);

                int Id = Convert.ToInt32(UserInput);

                Book CurrentBook = BM.GetBookById(Id);
                Console.WriteLine("Input the following information. To leave a field unchanged, hit <Enter>");


                Console.Write("\t\t Title [" + CurrentBook.title + "]:");
                key = Console.ReadKey().Key.ToString();
                Console.WriteLine();


                if (key != "Enter")
                {
                    string newTitle = Console.ReadLine();
                    Console.WriteLine();
                    CurrentBook.title = key.Substring(1) + newTitle;
                }

                Console.Write("\t\t Author [" + CurrentBook.author + "]:");
                

                key = Console.ReadKey().Key.ToString();
                Console.WriteLine();

                if (key != "Enter")
                {
                    string newAuthor = Console.ReadLine();
                    Console.WriteLine();
                    CurrentBook.author = key.Substring(1) + newAuthor;
                }

                Console.Write("\t\t description [" + CurrentBook.description + "]");
                

                key = Console.ReadKey().Key.ToString();
                Console.WriteLine();

                if (key != "Enter")
                {
                    string newDescription = Console.ReadLine();
                    Console.WriteLine();
                    CurrentBook.description = key.Substring(1) + newDescription;
                }

                bool check = BM.EditBook(CurrentBook, CurrentBook.id);

                if (check)
                {
                    Console.WriteLine("\t\t\t\t Book saved.");
                    Console.WriteLine();
                    EditBook(false);

                }
            }

        }

        /*
         *  this function is used for searching for books by taking any data from user 
         *  and if any book have this data will be displayed to user
         */
        public  void SearchBook()
        {
            Console.WriteLine("==== Search ====");
            Console.WriteLine("Type in one or more keywords to search for");
            Console.Write("\t\t\t Search:");


            List<Book> ReturnedBooks = new List<Book>();
            string UserInput = Console.ReadLine();
            string[] SearchingQueries = UserInput.Split(" ");

            foreach (string word in SearchingQueries)
            {
                ReturnedBooks.Add(BM.SearchBooks(word));

            }

            if (ReturnedBooks.Count > 0)
            {

                Console.WriteLine("The following books matched your query. Enter the book ID to see more details, or <Enter> to return");

                foreach (Book b in ReturnedBooks)
                {
                    Console.WriteLine("\t\t\t [" + b.id + "]" + b.title);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\t\t\t there is no books found");
                Console.WriteLine();

            }

        }

        /*
         * this function is used to check the user input for main menu
         */
        public  void CheckMainMenuUserInput()
        {
            int UserInput = Convert.ToInt32(Console.ReadLine());

            if (UserInput == 1)
            {
                ViewAllBooks();
                DisplayFirstMenu();
            }

            else if (UserInput == 2)
            {
                AddBook();
                DisplayFirstMenu();
            }

            else if (UserInput == 3)
            {
                EditBook(true);
                DisplayFirstMenu();
            }

            else if (UserInput == 4)
            {
                SearchBook();
                DisplayFirstMenu();
            }
            else
            {
                // call Book Maanager to save the Books in file and exit;
                BM.SaveAndExit();

                Console.WriteLine("\n Library saved. \n");
                Console.WriteLine();
            }
        }

    }
}
