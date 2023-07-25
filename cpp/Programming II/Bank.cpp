#include "Bank.h"

Bank::Bank(int c, int a)
{
    this->clients = new Client*[c];
    for(int i = 0; i < c; i++){
        this->clients[i] = nullptr;
    }

    this->accounts = new Account*[a];
    for(int i = 0; i < a; i++){
        this->accounts[i] = nullptr;
    }
}

Bank::~Bank()
{
    for(int i = 0; i < this->clientsCount; i++){
        delete this->clients[i];
    }
    delete this->clients;
    this->clients = nullptr;

    for(int i = 0; i < this->accountsCount; i++){
        delete this->accounts[i];
    }
    delete this->accounts;
    this->accounts = nullptr;
}

Client* Bank::GetClient(int c){
    for(int i = 0; i < this->clientsCount; i++){
        if(this->clients[i]->GetCode() == c){
            return this->clients[i];
        }
    }
}

Account* Bank::GetAccount(int n){
    for(int i = 0; i < this->accountsCount; i++){
        if(this->accounts[i]->GetNumber() == n){
            return this->accounts[i];
        }
    }
}

Client* Bank::CreateClient(int c, string n){
    return this->clients[this->clientsCount++] = new Client(c, n);
}

Account* Bank::CreateAccount(int n, Client *o){
    return this->accounts[this->accountsCount++] = new Account(n, o);
}

Account* Bank::CreateAccount(int n, Client *o, double ir){
    return this->accounts[this->accountsCount++] = new Account(n, o, ir);
}

PartnerAccount* Bank::CreateAccount(int n, Client *o, Client *p){
    PartnerAccount* partnerAccount = new PartnerAccount(n, o, p);
    this->accounts[this->accountsCount++] = partnerAccount;
    return partnerAccount;
}

PartnerAccount* Bank::CreateAccount(int n, Client *o, Client *p, double ir){
    PartnerAccount* partnerAccount = new PartnerAccount(n, o, p, ir);
    this->accounts[this->accountsCount++] = partnerAccount;
    return partnerAccount;
}

void Bank::AddInterest(){
    for(int i = 0; i < this->accountsCount; i++){
        this->accounts[i]->AddInterest();
    }
}
