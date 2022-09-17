using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Library;
using System.Linq;

namespace Library.UnitTests
{
    [TestClass]
    public class LibraryAccountTests
    {
        #region MS_TESTS
        
        [TestMethod,Timeout(1)]
   
        public void LoanBook_ChildLoanAvailableChildrenBook_returnsTrue()
        {
            
            //arrange
            Reader child = new Reader("child", "baby", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(child);
            Book childrenBook = new Book(Book.BookType.ChildrenBook,Book.BookStatus.InTheLibrary);

            bool actual; 
            //act
            
            actual =libraryAccount.LoanBook(childrenBook);

            //assert
           Assert.IsTrue(actual,"BUG: a child didn't make to loan a children book");
           Assert.IsTrue(libraryAccount.LoanBooks.Contains(childrenBook),"BUG: book was not updated in the loan list");
           Assert.IsTrue(childrenBook.Status == Book.BookStatus.OutOfTheLibrary);
           Assert.IsFalse(libraryAccount.ReservedBooks.Contains(childrenBook),"BUG: book was loaned but still mentioned in the reserve list");
        }
        
        [TestMethod]

        public void ReserveBook_ChildReserveAvailableChildrenBook_returnsTrue()
        {
            //arrange
            Reader child = new Reader("child", "baby", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(child);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);
           // libraryAccount.OwnerDebt = 10;
            bool actual;
            //act
            
            actual = libraryAccount.ReserveBook(childrenBook);

            //assert
            Assert.IsTrue(actual, "BUG: a child didn't Reserve a this book");
            Assert.IsTrue(libraryAccount.ReservedBooks.Contains(childrenBook), "BUG: book was not updated in the reserve list");
            Assert.IsTrue(childrenBook.Status == Book.BookStatus.Reserved,"BUG: book status was not changed");
            
        }
       
        [TestMethod]

        public void LoanBook_ChildLoanAvailableAdultBook_returnsFalse()
        {
            //arrange
            Reader child = new Reader("child", "baby", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(child);
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibrary);

            bool actual;
            //act
           
            actual = libraryAccount.LoanBook(adultBook);

            //assert
            Assert.IsFalse(actual, "BUG: book suitable for this reader");
           
        }

        [TestMethod]

        public void LoanBook_AdultLoanAvailableChildBook_returnsFalse()
        {
            //arrange
            Reader adult = new Reader("adult", "big", Reader.ReaderType.Adult);
            LibraryAccount libraryAccount = new LibraryAccount(adult);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);

            bool actual;
            //act
          
            actual = libraryAccount.LoanBook(childrenBook);

            //assert
            Assert.IsFalse(actual, "BUG: a Adult didn't make to loan a child book");
        }
       
        [TestMethod]

        public void CancelBook_AdultLoanAvailableAdultBook_returnsTrue()
        {
            //arrange
            Reader adult = new Reader("adult", "big", Reader.ReaderType.Adult);
            LibraryAccount libraryAccount = new LibraryAccount(adult);
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibrary);
            libraryAccount.ReserveBook(adultBook);
           bool actual;
            //act
           
            actual = libraryAccount.CancelReserveBook(adultBook);
            
            //assert

            Assert.IsTrue(actual, "BUG: a Adult didn't make to Cancel a adult book");
            Assert.IsFalse(libraryAccount.ReservedBooks.Contains(adultBook),"BUG: book was not in the reserved list");
         
        }
       
        [TestMethod]

        public void LoanBook_AdultLoanAvailableAdultBookButHasDept_returnsFalse()
        {
            //arrange
            Reader adult = new Reader("adult", "big", Reader.ReaderType.Adult);
            LibraryAccount libraryAccount = new LibraryAccount(adult);
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibrary);
            libraryAccount.OwnerDebt = 10;
            bool actual;
            //act
            
            actual = libraryAccount.LoanBook(adultBook);

            //assert
            Assert.IsTrue(actual, "BUG: a Adult didn't make to loan a adult book");
          
        }

        [TestMethod,Timeout(10)]

        public void LoanBook_AdultLoanAvailableAdultBookButHasDept_HasTimeOut()
        {
            //arrange
            Reader adult = new Reader("adult", "big", Reader.ReaderType.Adult);
            LibraryAccount libraryAccount = new LibraryAccount(adult);
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibrary);
            libraryAccount.OwnerDebt = 10;
            bool actual;
            //act

            actual = libraryAccount.LoanBook(adultBook);

            //assert
            Assert.IsTrue(actual, "BUG: a Adult didn't make to loan a adult book");

        }

        [TestMethod]

        public void CancelReserveBook_ChildCancelsReserved_returnsTrue()
        {
            //arrange
            Reader child = new Reader("child", "baby", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(child);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);
            libraryAccount.ReserveBook(childrenBook);
            bool actual;
            //act
           
            actual = libraryAccount.CancelReserveBook(childrenBook);

            //assert
            Assert.IsTrue(actual, "BUG: child didn't reserve this children book");
           
        }
#endregion
        #region DataRowTests

        [TestMethod]
        [DataRow("child","baby",Reader.ReaderType.Child, Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary,DisplayName = "child_childbook")]
        [DataRow("adult","big",Reader.ReaderType.Adult, Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary,DisplayName = "adult_childbook")]
        [DataRow("adult", "big", Reader.ReaderType.Adult, Book.BookType.AdultBook, Book.BookStatus.InTheLibrary, DisplayName = "adult_adultbook")]
        [DataRow("child", "baby", Reader.ReaderType.Child, Book.BookType.AdultBook, Book.BookStatus.InTheLibrary, DisplayName = "child_adultbook")] 
        public void LoanBook_DataRowTests(string fn,string ln,Reader.ReaderType type,Book.BookType btype,Book.BookStatus status)
        {
            //arrange
            Reader Test = new Reader(fn, ln,type);
            LibraryAccount libraryAccount = new LibraryAccount(Test);
            Book TestBook = new Book(btype,status);

            bool actual;
            //act
         
            actual = libraryAccount.LoanBook(TestBook);

            //assert
            Assert.IsTrue(actual, "BUG: book not suitable for reader");
          
        }
        [TestMethod]
        [DataRow("child", "baby", Reader.ReaderType.Child, Book.BookType.AdultBook, Book.BookStatus.InTheLibrary, DisplayName = "child_Adultbook_notsuitable")]
        [DataRow("adult", "big", Reader.ReaderType.Adult, Book.BookType.AdultBook, Book.BookStatus.Reserved, DisplayName = "adult_Can't_Reserve")]
        [DataRow("adult", "big", Reader.ReaderType.Adult, Book.BookType.AdultBook, Book.BookStatus.InTheLibrary,DisplayName = "adult_adultbook")]
        [DataRow("child", "baby", Reader.ReaderType.Child, Book.BookType.ChildrenBook, Book.BookStatus.OutOfTheLibraryAndReserved, DisplayName = "child_OutOfTheLibraryAndReserved")]
        public void ReserveBook_DataRowTests(string fn, string ln, Reader.ReaderType type, Book.BookType btype, Book.BookStatus status)
        {
            
            //arrange
            Reader Test = new Reader(fn, ln, type);
            LibraryAccount libraryAccount = new LibraryAccount(Test);
            Book TestBook = new Book(btype, status);

            bool actual;
            //act

            actual = libraryAccount.ReserveBook(TestBook);

            //assert
            Assert.IsTrue(actual, "BUG: Did not Reserve Book");

        }
        #endregion
        #region xmltests
        private TestContext context;
        public TestContext TestContext
        {
            get { return context; }
            set { context = value; }

        }
      
        
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\testData\LibraryTests.xml",
            "Test",
            DataAccessMethod.Sequential)]
        public void LoanBooks_XmlTests()
        
        { //arrange
            string fn = TestContext.DataRow["fn"].ToString();
            string ln = TestContext.DataRow["ln"].ToString();

            string tcheck = TestContext.DataRow["type"].ToString();
            Reader.ReaderType type= Reader.ReaderType.Child;
            if (tcheck.Equals("Adult"))
                type = Reader.ReaderType.Adult;
            if(tcheck.Equals("Child"))
                type = Reader.ReaderType.Child;

            string bcheck = TestContext.DataRow["btype"].ToString();
            Book.BookType btype = Book.BookType.ChildrenBook;
            if (bcheck.Equals("AdultBook"))
                btype = Book.BookType.AdultBook;
            if(bcheck.Equals("ChildrenBook"))
                btype = Book.BookType.ChildrenBook;

            string bscheck =TestContext.DataRow["status"].ToString();
            Book.BookStatus status = Book.BookStatus.InTheLibrary;
            if (bscheck.Equals("InTheLibrary"))
                status = Book.BookStatus.InTheLibrary;
            if(bscheck.Equals("OutOfTheLibrary"))
                status = Book.BookStatus.OutOfTheLibrary;
            if (bscheck.Equals("Reserved"))
                status = Book.BookStatus.Reserved;
            if(bscheck.Equals("OutOfTheLibraryAndReserved"))
                status = Book.BookStatus.OutOfTheLibraryAndReserved;

            //string bscheck = TestContext.DataRow["status"].ToString();


            Reader Test = new Reader(fn, ln, type);
            LibraryAccount libraryAccount = new LibraryAccount(Test);
            Book TestBook = new Book(btype, status);

            bool actual;
            //act
            
            actual = libraryAccount.LoanBook(TestBook);

            //assert
            Assert.IsTrue(actual, "BUG: book not suitable for reader");

        }
        [TestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
            @"|DataDirectory|\testData\LibraryTests.xml",
            "Test1",
            DataAccessMethod.Sequential)]
        public void ReserveBooks_XmlTests()

        { //arrange
            string fn = TestContext.DataRow["fn"].ToString();
            string ln = TestContext.DataRow["ln"].ToString();
            int debt = Convert.ToInt32(TestContext.DataRow["debt"]);
            string tcheck = TestContext.DataRow["type"].ToString();
            Reader.ReaderType type = Reader.ReaderType.Child;
            if (tcheck.Equals("Adult"))
                type = Reader.ReaderType.Adult;
            if (tcheck.Equals("Child"))
                type = Reader.ReaderType.Child;

            string bcheck = TestContext.DataRow["btype"].ToString();
            Book.BookType btype = Book.BookType.ChildrenBook;
            if (bcheck.Equals("AdultBook"))
                btype = Book.BookType.AdultBook;
            if (bcheck.Equals("ChildrenBook"))
                btype = Book.BookType.ChildrenBook;

            string bscheck = TestContext.DataRow["status"].ToString();
            Book.BookStatus status = Book.BookStatus.InTheLibrary;
            if (bscheck.Equals("InTheLibrary"))
                status = Book.BookStatus.InTheLibrary;
            if (bscheck.Equals("OutOfTheLibrary"))
                status = Book.BookStatus.OutOfTheLibrary;
            if (bscheck.Equals("Reserved"))
                status = Book.BookStatus.Reserved;
            if (bscheck.Equals("OutOfTheLibraryAndReserved"))
                status = Book.BookStatus.OutOfTheLibraryAndReserved;
            


           
            Reader Test = new Reader(fn, ln, type);
            LibraryAccount libraryAccount = new LibraryAccount(Test);
            Book TestBook = new Book(btype, status);
            
            libraryAccount.OwnerDebt = debt;
            bool actual;
            //act
            
            actual = libraryAccount.ReserveBook(TestBook);
            
            //assert
            Assert.IsTrue(actual, "BUG: Did not reserve the book");

        }
        #endregion

    }
}
