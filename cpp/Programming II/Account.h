#ifndef ACCOUNT_H
#define ACCOUNT_H

#include "Client.h"

using namespace std;

class AbstractAccount{
    public:
        AbstractAccount();
        virtual ~AbstractAccount();

        virtual bool CanWithdraw(double a) = 0;
};

class Account : public AbstractAccount
{
    private:
        static int objectsCount;
        int number;
        static double defaultInterestRate;
        double interestRate = 0;

        Client* owner;
    protected:
        double balance;
    public:
        static double GetDefaultInterestRate();
        static void SetDefaultInterestRate(double ir);
        static int GetObjectsCount();

        Account(int n, Client *o);
        Account(int n, Client *o, double ir);
        virtual ~Account();

        int GetNumber();
        double GetBalance();
        double GetInterestRate();
        Client* GetOwner();
        virtual bool CanWithdraw(double a);

        void Deposit(double a);
        bool Withdraw(double a);
        void AddInterest();
};

class PartnerAccount : public Account{
    private:
        Client* partner;
    public:
        PartnerAccount(int n, Client *o, Client *p);
        PartnerAccount(int n, Client *o, Client *p, double ir);

        Client* GetPartner();
};

class CreditAccount : public Account{
    private:
        double credit;
    public:
        CreditAccount(int n, Client *o, double c);
        CreditAccount(int n, Client *o, double ir, double c);
        virtual ~CreditAccount();

        virtual bool CanWithdraw(double a);
        bool Withdraw(double a);
        double GetCredit();
};

#endif // ACCOUNT_H
