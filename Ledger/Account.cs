using System;
using System.Collections.Generic;

namespace Ledger
{
    public class Account
    {
        public string Name { get; }
        private string Password;
        private decimal Balance;
        LinkedList<decimal> History;

        public Account(string name, string password)
        {
            Name = name;
            Password = password;
            Balance = 0;
            History = new LinkedList<decimal>();
        }

        public bool Login(string password)
        {
            if (Password == password)
            {
                return true;
            }

            return false;
        }

        public void Deposit(decimal deposit)
        {
            Balance = Balance + deposit;
            History.AddFirst(deposit);
            Console.WriteLine(String.Format("Deposited {0:C} to your account.", deposit));
            GetBalance();
        }

        public void Withdraw(decimal withdraw)
        {
            Balance = Balance - withdraw;
            History.AddFirst(withdraw * -1);
            Console.WriteLine(String.Format("Withdrew {0:C} from your account.", withdraw));
            GetBalance();
        }

        public void GetBalance()
        {
            Console.WriteLine(String.Format("Your balance is {0:C}", Balance));
        }

        public void GetHistory()
        {
            var transaction = History.First;
            Console.WriteLine("Most recent transaction is listed first.");
            while(transaction != null)
            {
                Console.WriteLine(transaction.Value);
                transaction = transaction.Next;
            }
        }
    }
}
