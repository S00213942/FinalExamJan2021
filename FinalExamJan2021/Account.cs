using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExamJan2021
{
    public abstract class Account
    {
        #region Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public DateTime InterestDate { get; set; }

        #endregion Properties

        #region Constructors
        public Account(string firstName, string lastName, decimal balance, DateTime interestDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
            InterestDate = interestDate;
        }

        public Account(string firstName, string lastName) : this(firstName, lastName, 0, DateTime.Now) { }

        public Account() : this("Unknown", "Unknown") { }

        #endregion Constructors

        #region Methods
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (Balance >= amount)
                Balance -= amount;
        }

        public abstract void CalculateInterest();

        public override string ToString()
        {
            return $"{LastName}, {FirstName}";
        }

        #endregion Methods
    }
}
