using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        #region Enums
        public enum BookType {
            ChildrenBook,
            AdultBook

        }
        public enum BookStatus {
        InTheLibrary,
        Reserved,
        OutOfTheLibrary,
        OutOfTheLibraryAndReserved
        }


        #endregion
        #region Private Variables
        private string id;
        private BookType type;
        private BookStatus status;
        #endregion

        #region Properties
        public string Id
        {
            get { return id; }
        }
        public BookType Type
        {
            get => type;
        }
        public BookStatus Status
        {
            get {return status; }
            set { status = value; }
        }
        #endregion

        #region Constructors
        public Book()
        {
            Guid guid= Guid.NewGuid();
            id = guid.ToString();

            type = BookType.AdultBook;
            status = BookStatus.InTheLibrary;
        }
        public Book(BookType type , BookStatus status)
        {
            Guid guid = Guid.NewGuid();
            id= guid.ToString();
            this.type = type;
            this.status = status;
        }
        #endregion
    }
}
