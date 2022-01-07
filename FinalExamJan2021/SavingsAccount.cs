using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalExamJan2021
{
    public class SavingsAccount : Account
    {
        //constants
        private const decimal InterestRate = 0.06m;

        //constructors
        public SavingsAccount(string firstName, string lastName, decimal balance, DateTime interestDate)
            : base(firstName, lastName, balance, interestDate) { }

        //methods
        public override void CalculateInterest()
        {
            //Check if interest has been set in previous year
            DateTime allowedDate = DateTime.Now.AddYears(-1);
            if (allowedDate >= InterestDate)    //allowed date will be greater once interest has not been set in the last year
            {
                Balance = Balance + (Balance * InterestRate);   //apply interest
                InterestDate = DateTime.Now;    //set interest date to today for future checks
            }
        }
    }
}
