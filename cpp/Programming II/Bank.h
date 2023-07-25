#ifndef BANK_H
#define BANK_H

#include "Client.h"
#include "Account.h"

using namespace std;

class Bank
{
    private:
        Client** clients = nullptr;
        int clientsCount = 0;

        Account** accounts = nullptr;
        int accountsCount = 0;
    public:
        Bank(int c, int a);
        ~Bank();

        Client* GetClient(int c);
        Account* GetAccount(int n);

        Client* CreateClient(int c, string n);
        Account* CreateAccount(int n, Client *o);
        Account* CreateAccount(int n, Client *o, double ir);
        PartnerAccount* CreateAccount(int n, Client *o, Client *p);
        PartnerAccount* CreateAccount(int n, Client *o, Client *p, double ir);

        void AddInterest();
};

#endif // BANK_H
