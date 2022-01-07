using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalExamJan2021
{
    /// <summary>
    /// https://github.com/S00213942/S002131942-January-Exam
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Account> accounts = new List<Account>();  //list to contain all accounts
        List<Account> filteredAccounts = new List<Account>();

        public MainWindow()
        {
            InitializeComponent();
        }

        //Method runs on start up
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentAccount ca1 = new CurrentAccount("John", "Doe", 1000, DateTime.Now.AddYears(-2));  //date is set more than 1 year ago to test interest
            CurrentAccount ca2 = new CurrentAccount("Jane", "Doe", 2000, DateTime.Now.AddYears(-2));  //date is set more than 1 year ago to test interest

            SavingsAccount sa1 = new SavingsAccount("John", "Smith", 3000, DateTime.Now.AddYears(-2));  //date is set more than 1 year ago to test interest
            SavingsAccount sa2 = new SavingsAccount("Jane", "Smith", 4000, DateTime.Now.AddYears(-2));  //date is set more than 1 year ago to test interest

            accounts.Add(ca1);
            accounts.Add(ca2);
            accounts.Add(sa1);
            accounts.Add(sa2);

            lbxAccounts.ItemsSource = accounts;

        }

        //Handles selecting an account from the list of accounts
        private void lbxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Determine what object has been selected
            Account selected = lbxAccounts.SelectedItem as Account;

            //Check it is not null
            if (selected != null)
            {
                //Update display
                UpdateDisplay(selected);
            }
        }

        private void UpdateDisplay(Account selected)
        {
            tblkFirstName.Text = selected.FirstName;
            tblkLastName.Text = selected.LastName;
            tblkBalance.Text = selected.Balance.ToString("c");  //currency formatting
            tblkAccountType.Text = selected.GetType().Name;     //gets the name of the class
            tblkInterestDate.Text = selected.InterestDate.ToString("d");    //short date time formatting
        }

        //Clear textbox
        private void tbxAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxAmount.Clear();
        }

        //Handles the filter using checkboxes
        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            //Reset the listbox
            lbxAccounts.ItemsSource = null;
            
            //Clear previous filter
            filteredAccounts.Clear();

            //Determine what checkboxes are checked
            bool savings = false, current = false;

            if (cbCurrent.IsChecked.Value == true)
                current = true;

            if (cbSavings.IsChecked.Value == true)
                savings = true;

            //Update display
            if (current && savings)
            {
                lbxAccounts.ItemsSource = accounts;
            }
            
            else if (current)
            {
                foreach (Account account in accounts)   //loop through all accounts
                {
                    if (account is CurrentAccount)  //if the account is a current account add to filtered list
                        filteredAccounts.Add(account);
                }

                lbxAccounts.ItemsSource = filteredAccounts;
            }

            else if (savings)
            {
                foreach (Account account in accounts)   //loop though all accounts
                {
                    if (account is SavingsAccount)  //if account is a savings account add to filtered list
                        filteredAccounts.Add(account);
                }

                lbxAccounts.ItemsSource = filteredAccounts;
            }
        }

        //Handles deposit functionality
        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            //Check amount to deposit
            decimal amount = 0;
            if ( Decimal.TryParse(tbxAmount.Text, out amount))
            {
                //Determine selected account
                Account selected = lbxAccounts.SelectedItem as Account;
                if (selected != null)
                {
                    selected.Deposit(amount);
                    UpdateDisplay(selected);
                }
            }
        }

        //Handles withdraw functionality
        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            //Check amount to withdraw
            decimal amount = 0;
            if (Decimal.TryParse(tbxAmount.Text, out amount))
            {
                //Determine selected account
                Account selected = lbxAccounts.SelectedItem as Account;
                if (selected != null)
                {
                    selected.Withdraw(amount);
                    UpdateDisplay(selected);
                }
            }
        }

        //Handles interest functionality
        private void btnInterest_Click(object sender, RoutedEventArgs e)
        {
            Account selected = lbxAccounts.SelectedItem as Account;
            if (selected != null)
            {
                selected.CalculateInterest();
                UpdateDisplay(selected);
            }
        }
    }
}
