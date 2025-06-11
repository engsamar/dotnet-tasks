namespace Task04;
using System;
using System.Collections.Generic;
using System.Security.Principal;

class Program
{
    static void Main()
    {
        // Accounts
        var accounts = new List<Account>();
        accounts.Add(new Account());
        accounts.Add(new Account("Larry"));
        accounts.Add(new Account("Moe", 2000));
        accounts.Add(new Account("Curly", 5000));

        AccountUtil.Deposit(accounts, 1000);
        AccountUtil.Withdraw(accounts, 2000);

        // Savings
        var savAccounts = new List<SavingsAccount>();
        savAccounts.Add(new SavingsAccount());
        savAccounts.Add(new SavingsAccount("Superman"));
        savAccounts.Add(new SavingsAccount("Batman", 2000));
        savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

        AccountUtil.DepositSavings(savAccounts, 1000);
        AccountUtil.WithdrawSavings(savAccounts, 2000);

        // Checking
        var checAccounts = new List<CheckingAccount>();
        checAccounts.Add(new CheckingAccount());
        checAccounts.Add(new CheckingAccount("Larry2"));
        checAccounts.Add(new CheckingAccount("Moe2", 2000));
        checAccounts.Add(new CheckingAccount("Curly2", 5000));

        AccountUtil.DepositChecking(checAccounts, 1000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);

        // Trust
        var trustAccounts = new List<TrustAccount>();
        trustAccounts.Add(new TrustAccount());
        trustAccounts.Add(new TrustAccount("Superman2"));
        trustAccounts.Add(new TrustAccount("Batman2", 2000));
        trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

        AccountUtil.DepositTrust(trustAccounts, 1000);
        AccountUtil.DepositTrust(trustAccounts, 6000);
        AccountUtil.WithdrawTrust(trustAccounts, 2000);
        AccountUtil.WithdrawTrust(trustAccounts, 3000);
        AccountUtil.WithdrawTrust(trustAccounts, 500);

        Console.WriteLine();
    }

}

public static class AccountUtil
{
    // Utility helper functions for Account class
    public static void Deposit(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void Withdraw(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    // Helper functions for SavingsAccount
    public static void DepositSavings(List<SavingsAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Savings Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawSavings(List<SavingsAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Savings Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    // Helper functions for CheckingAccount
    public static void DepositChecking(List<CheckingAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Checking Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawChecking(List<CheckingAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Checking Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }

    // Helper functions for TrustAccount
    public static void DepositTrust(List<TrustAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Trust Accounts =================================");
        foreach (var acc in accounts)
        {
            if (acc.Deposit(amount))
                Console.WriteLine($"Deposited {amount} to {acc}");
            else
                Console.WriteLine($"Failed Deposit of {amount} to {acc}");
        }
    }

    public static void WithdrawTrust(List<TrustAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Trust Accounts ==============================");
        foreach (var acc in accounts)
        {
            if (acc.Withdraw(amount))
                Console.WriteLine($"Withdrew {amount} from {acc}");
            else
                Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
        }
    }
}

public class SavingsAccount : Account
{
    public double InterestRate { get; set; }

    public SavingsAccount(string name = "Unnamed Savings", double balance = 0.0, double interestRate = 0.0)
        : base(name, balance)
    {
        InterestRate = interestRate;
    }

    public override string ToString() =>
        $"[SavingsAccount: {Name}, Balance: {Balance:F2}, Interest: {InterestRate}%]";
}

public class TrustAccount : SavingsAccount
{
    private int withdrawalCount = 0;
    private const int maxWithdrawals = 3;
    private const double maxWithdrawalPercent = 0.2;

    public TrustAccount(string name = "Unnamed Trust", double balance = 0.0, double interestRate = 0.0)
        : base(name, balance, interestRate)
    {
    }

    public override bool Deposit(double amount)
    {
        if (amount >= 5000.0)
            amount += 50.0;

        return base.Deposit(amount);
    }

    public override bool Withdraw(double amount)
    {
        if (withdrawalCount >= maxWithdrawals)
            return false;

        if (amount > Balance * maxWithdrawalPercent)
            return false;

        withdrawalCount++;
        return base.Withdraw(amount);
    }

    public override string ToString() =>
        $"[TrustAccount: {Name}, Balance: {Balance:F2}, Interest: {InterestRate}%]";
}

public class CheckingAccount : Account
{
    private const double Fee = 1.5;

    public CheckingAccount(string name = "Unnamed Checking", double balance = 0.0)
        : base(name, balance)
    {
    }

    public override bool Withdraw(double amount)
    {
        return base.Withdraw(amount + Fee);
    }

    public override string ToString() =>
        $"[CheckingAccount: {Name}, Balance: {Balance:F2}]";
}
public class Account
{
    public string Name { get; set; }
    public double Balance { get; set; }

    public Account(string name = "Unnamed Account", double balance = 0.0)
    {
        Name = name;
        Balance = balance;
    }

    public virtual bool Deposit(double amount)
    {
        if (amount <= 0) return false;
        Balance += amount;
        return true;
    }

    public virtual bool Withdraw(double amount)
    {
        if (amount <= 0 || amount > Balance) return false;
        Balance -= amount;
        return true;
    }

    public override string ToString() =>
        $"[Account: {Name}, Balance: {Balance:F2}]";
}
