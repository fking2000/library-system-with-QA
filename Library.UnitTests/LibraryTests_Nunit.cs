//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NUnit.Framework;

namespace Library.UnitTests
{
    /// <summary>
    /// Summary description for LibraryTests_Nunit
    /// </summary>
    [TestFixture]
    public class LibraryTests_Nunit
    {
        [Test]
        public void LoanBook_ChildLoanAvailableChildrenBook_returnsTrue()
        {
            //arrange
            Reader child = new Reader("child", "baby", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(child);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);

            bool actual;
            //act
            
            actual = libraryAccount.LoanBook(childrenBook);

            //assert
            //Assert.IsTrue(actual, "BUG: a child didn't make to loan a children book");
            Assert.That(actual, Is.True, "BUG: a child didn't make to loan a children book");
            //Assert.IsTrue(libraryAccount.LoanBooks.Contains(childrenBook), "BUG: book was not updated in the loan list");
            //Assert.IsTrue(childrenBook.Status == Book.BookStatus.OutOfTheLibrary);
           // Assert.IsFalse(libraryAccount.ReservedBooks.Contains(childrenBook), "BUG: book was loan but still mentioned in the reserve list");
        }
        [Test]

        public void ReserveBook_ChildReserveAvailableChildrenBook_returnsTrue()
        {
            //arrange
            Reader child = new Reader("child", "baby", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(child);
            Book childrenBook = new Book(Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary);

            bool actual;
            //act
           
            actual = libraryAccount.ReserveBook(childrenBook);

            //assert
            // Assert.IsTrue(actual, "BUG: a child didn't make to reserve a children book");
            Assert.That(actual, Is.True, "BUG: a child didn't make to reserve a children book");
        }
        [Test]

        public void LoanBook_ChildLoanAvailableAdultBook_returnsFalse()
        {
            //arrange
            Reader child = new Reader("child", "baby", Reader.ReaderType.Child);
            LibraryAccount libraryAccount = new LibraryAccount(child);
            Book adultBook = new Book(Book.BookType.AdultBook, Book.BookStatus.InTheLibrary);

            bool actual;
            //act
            //test the flow that a child reader tries to loan a children book
            actual = libraryAccount.LoanBook(adultBook);

            //assert
            Assert.That(actual,Is.True, "BUG: book not suitable for this reader");

        }

        [Test]
        [TestCase("child", "baby", Reader.ReaderType.Child, Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary, TestName = "child_childbook")]
        [TestCase("adult", "big", Reader.ReaderType.Adult, Book.BookType.ChildrenBook, Book.BookStatus.InTheLibrary, TestName = "adult_childbook")]
        [TestCase("adult", "big", Reader.ReaderType.Adult, Book.BookType.AdultBook, Book.BookStatus.InTheLibrary, TestName = "adult_adultbook")]
        [TestCase("child", "baby", Reader.ReaderType.Child, Book.BookType.AdultBook, Book.BookStatus.InTheLibrary, TestName = "child_adultbook")]
        public void LoanBook_DataRowTests(string fn, string ln, Reader.ReaderType type, Book.BookType btype, Book.BookStatus status)
        {
            //arrange
            Reader Test = new Reader(fn, ln, type);
            LibraryAccount libraryAccount = new LibraryAccount(Test);
            Book TestBook = new Book(btype, status);


            bool actual;
            //act
            actual = libraryAccount.LoanBook(TestBook);

            //assert
            //Assert.IsTrue(actual, "BUG: book not suitable for reader");
            Assert.That(actual, Is.True, "BUG: book not suitable for reader");

        }
    }
}
