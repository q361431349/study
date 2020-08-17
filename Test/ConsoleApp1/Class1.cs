using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Class1
    {
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                fullName = null;
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                fullName = null;
            }
        }

        private string fullName;
        public string FullName
        {
            get
            {
                if (fullName == null)
                    fullName = $"{FirstName} {LastName}";
                return fullName;
            }
        }

        [field: NonSerialized]
        public int Id { get; set; }
    }
}
