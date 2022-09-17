using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Reader
    {
        #region Enums
        public enum ReaderType 
        { 
            Child,
            Adult
        }
        #endregion
        #region Private Variables
        private string firstName;
        private string lastName;
        private ReaderType type;
        #endregion
        #region Properties
        public string FirstName
        {
            get => firstName;
        }
        public string LastName
        {
            get => lastName;    
        }
        public ReaderType Type
        {
            get => type;
        }
        #endregion
        #region constructors
        public Reader(string firstName,string lastName,ReaderType type)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.type = type;
        }
        #endregion

    }
}
