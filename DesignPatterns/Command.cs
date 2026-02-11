using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class BankAccount
    {
        private int balance;
        private int overdraflimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited {amount}, balance is now {balance}");
        }

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraflimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount}, balance is now {balance}");
                return true;
            }

            Console.WriteLine($"Cannot Withdrew {amount}, balance is now {balance}");
            return false;   
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public interface ICommand
    {
        void Call();

        void Undo();
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount account;
        public enum Action
        {
            Deposit, Withdraw
        }
        private Action action;
        private int amount;
        private bool succeeded;
        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account;
            this.action = action;
            this.amount = amount;
        }
        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    succeeded = true;
                    break;
                case Action.Withdraw:
                    succeeded = account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void Undo()
        {
            if (!succeeded) return;
            switch (action)
            {
                case Action.Deposit:
                    account.Withdraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class  Demo
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var commands = new List<BankAccountCommand>
            {
                new(ba, BankAccountCommand.Action.Deposit, 100),
                new(ba, BankAccountCommand.Action.Withdraw, 50),
                new(ba, BankAccountCommand.Action.Withdraw, 12),
                new(ba, BankAccountCommand.Action.Withdraw, 1200),
                new(ba, BankAccountCommand.Action.Withdraw, 2),
            };

            foreach (var cmd in commands)
                cmd.Call();

            Console.WriteLine(ba);

            foreach (var cmd in Enumerable.Reverse(commands))
                cmd.Undo();

            Console.WriteLine(ba);

        }

    }
}
