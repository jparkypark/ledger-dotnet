using System;
using System.Collections.Generic;

namespace Ledger
{
    class Program
    {
        static List<Account> Accounts = new List<Account>();
        static Account currentAccount;
        public static void Main(string[] args)
        {
            MainMenu();
            
        }
        public static void MainMenu()
        {
            currentAccount = null;
            Console.Clear();
            Console.WriteLine("Welcome to the world’s greatest banking ledger!");
            Start:
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Create a new account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Quit");
            switch (Console.ReadLine())
            {
                case "1":
                    currentAccount = CreateAccountPrompt();
                    break;
                case "2":
                    if (Accounts.Count != 0)
                    {
                        currentAccount = LoginPrompt();
                        break;
                    }
                    Console.WriteLine("There are no accounts. Please create a new one.");
                    goto Start;
                case "3":
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    goto Start;
            }
            AccountMenu(currentAccount);
        }

        public static void AccountMenu(Account account)
        {
            Console.Clear();
            Console.WriteLine(string.Format("Welcome {0}", account.Name));
            Start:
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Check balance");
            Console.WriteLine("4. See transaction history");
            Console.WriteLine("5. Logout");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("How much would you like to deposit?");
                    string input = Console.ReadLine();
                    decimal amount;
                    if(decimal.TryParse(input, out amount))
                    {
                        account.Deposit(amount);
                    } else
                    {
                        Console.WriteLine("Unable to deposit {0}", input);
                    }
                    goto Start;
                case "2":
                    Console.WriteLine("How much would you like to withdraw?");
                    input = Console.ReadLine();
                    if (decimal.TryParse(input, out amount))
                    {
                        account.Withdraw(amount);
                    }
                    else
                    {
                        Console.WriteLine("Unable to withdraw {0}", input);
                    }
                    goto Start;
                case "3":
                    account.GetBalance();
                    goto Start;
                case "4":
                    account.GetHistory();
                    goto Start;
                case "5":
                    break;
                default:
                    goto Start;
            }

            MainMenu();
        }

        public static Account CreateAccountPrompt()
        {
            Console.Clear();
            string newName = string.Empty;
            string newPassword = string.Empty;
            bool newAccount = CreateAccount(newName, newPassword);
            while (!newAccount)
            {
                Console.WriteLine("Account name: ");
                newName = Console.ReadLine();
                if (newName == "q")
                {
                    break;
                }
                if (Accounts.Find(a => a.Name == newName) != null)
                {
                    Console.WriteLine("Account already exists.");
                    continue;
                }
                Console.WriteLine("Account password: ");
                newPassword = Console.ReadLine();
                if (newPassword == "q")
                {
                    break;
                }
                newAccount = CreateAccount(newName, newPassword);
            }
            return Login(newName, newPassword);
        }

        public static Account LoginPrompt()
        {
            Console.Clear();
            string name = string.Empty;
            string password = string.Empty;
            var loginAttempt = Login(name, password);
            while (loginAttempt == null)
            {
                Console.WriteLine("Enter \"q\" to quit");
                Console.WriteLine("Account name: ");
                name = Console.ReadLine();
                if(name == "q")
                {
                    break;
                }

                Console.WriteLine("Account password: ");
                password = Console.ReadLine();
                if(password == "q")
                {
                    break;
                }
                loginAttempt = Login(name, password);
            }

            return loginAttempt;
        }
        
        //Create a new account, add it to the list of accounts, and return true if the name is available
        //Return false if the name is not available
        public static bool CreateAccount(string name, string password)
        {
            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Name and Password cannot be empty");
                return false;
            }
            Accounts.Add(new Account(name, password));
            Console.WriteLine("New account created!");
            return true;
        }

        //Return account with matching name and password
        //Return null if name not found or password incorrect
        public static Account Login(string name, string password)
        {
            var loginAttempt = Accounts.Find(a => a.Name == name);
            if (loginAttempt == null)
            {
                return null;
            } else if (loginAttempt.Login(password))
            {
                return loginAttempt;
            } else return null;
        }
    }
}
