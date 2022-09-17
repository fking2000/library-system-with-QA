 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class LibraryAccount
    {
        #region Private Variables
        private const int MAX_NUM_OF_LOAN_BOOKS = 3;
        private const int MAX_NUM_OF_RESERVE_BOOKS = 3;
        private Reader owner;
        private List<Book> loanbooks;
        private List<Book> reservedBooks;
        private double ownerDebt;
        #endregion
        #region Properties
        public int MaxNumOfLoanBooks
        {
           get=>MAX_NUM_OF_LOAN_BOOKS; 
            
        }
        public int MaxNumOfReserveBooks
        {
           get=>MAX_NUM_OF_RESERVE_BOOKS; 
            
        }
        public Reader Owner
        {
            get { return owner; }
            set { owner = value; }

        }
            public List<Book> LoanBooks
        {
            get { return loanbooks; }
            set { loanbooks = value; }
        }
        public  List<Book> ReservedBooks
        {
            get { return reservedBooks; }
            set { reservedBooks = value; }
        }
        public double OwnerDebt
        { 
            get { return ownerDebt; } 
            set { ownerDebt = value; }
        }
        #endregion
        #region Constructors
        public LibraryAccount(Reader owner)
        {
            this.owner = owner;
            loanbooks = new List<Book>();
            reservedBooks = new List<Book>();
            this.ownerDebt = 0;
        }
        #endregion

        #region Methods
        public bool LoanBook(Book bookToLoan)
        {
            
           if (bookToLoan.Status != Book.BookStatus.InTheLibrary) 
                return false;

           if ((bookToLoan.Type == Book.BookType.AdultBook && Owner.Type != Reader.ReaderType.Adult)
                 ||( bookToLoan.Type == Book.BookType.ChildrenBook && Owner.Type != Reader.ReaderType.Child))
           return false;

             if (loanbooks.Count == MaxNumOfLoanBooks)
           return false;

             if (OwnerDebt > 0)        
             throw new InvalidOperationException("you have debt to the library");
                       
          loanbooks.Add(bookToLoan);
          bookToLoan.Status = Book.BookStatus.OutOfTheLibrary;
          return true;
                       

        }

        public bool ReturnBook(Book bookToReturn)
        {
            if (!loanbooks.Contains(bookToReturn))
                throw new InvalidOperationException("the book is not from this library");

            if (bookToReturn.Status == Book.BookStatus.OutOfTheLibrary)
            bookToReturn.Status = Book.BookStatus.InTheLibrary;
            
            if (bookToReturn.Status == Book.BookStatus.OutOfTheLibraryAndReserved)
            bookToReturn.Status = Book.BookStatus.Reserved;
           
           loanbooks.Remove(bookToReturn);
           return true; 
        }

        public bool ReserveBook(Book bookToReserve)
        {
            if (bookToReserve.Status == Book.BookStatus.OutOfTheLibraryAndReserved ||
                bookToReserve.Status == Book.BookStatus.Reserved) 
               throw new InvalidOperationException("Book is already reserved by someone");  
            
            if ((bookToReserve.Type == Book.BookType.AdultBook && Owner.Type != Reader.ReaderType.Adult)
             || (bookToReserve.Type == Book.BookType.ChildrenBook && Owner.Type != Reader.ReaderType.Child))
                throw new InvalidOperationException("Book is not suitable for Reader");

            if (reservedBooks.Count == MaxNumOfReserveBooks)
                return false;

            if (OwnerDebt > 0)
                throw new InvalidOperationException("you have debt to the library");

            if (bookToReserve.Status == Book.BookStatus.InTheLibrary)
                bookToReserve.Status = Book.BookStatus.Reserved;
            
            if (bookToReserve.Status == Book.BookStatus.OutOfTheLibrary)
            bookToReserve.Status = Book.BookStatus.OutOfTheLibraryAndReserved;

            
            reservedBooks.Add(bookToReserve);
            return true;
        }

        public bool CancelReserveBook(Book bookToCancel)
        {
            if(!reservedBooks.Contains(bookToCancel))
           throw new InvalidOperationException("the book is not in your reserved list");
            
            if (bookToCancel.Status == Book.BookStatus.OutOfTheLibraryAndReserved)
                bookToCancel.Status = Book.BookStatus.OutOfTheLibrary;

            if (bookToCancel.Status == Book.BookStatus.Reserved)
                bookToCancel.Status = Book.BookStatus.InTheLibrary;

            reservedBooks.Remove(bookToCancel);
            return true;
        }

            #endregion
        }
}
