#include <iostream>
#include "Client.h"
#include "Account.h"
#include "Bank.h"

using namespace std;

void newClient(Client* client){
    cout << "New client [Code: " << client->GetCode() << ", Name: " << client->GetName() << "]" << endl;
}

void newAccount(Account* account){
    cout << "New account [Number: " << account->GetNumber() << ", Balance: " << account->GetBalance() << ", Interest rate: " << account->GetInterestRate() << ", Owner: {" << account->GetOwner()->GetCode() << ", " << account->GetOwner()->GetName() << "}]" << endl;
}

void newAccount(PartnerAccount* account){
    cout << "New account [Number: " << account->GetNumber() << ", Balance: " << account->GetBalance() << ", Interest rate: " << account->GetInterestRate() << ", Owner: {" << account->GetOwner()->GetCode() << ", " << account->GetOwner()->GetName() << "}, Partner: " << (account->GetPartner() != nullptr ? account->GetPartner()->GetName() : "{NONE}") << "]" << endl;
}

void newAccount(CreditAccount* account){
    cout << "New account [Number: " << account->GetNumber() << ", Balance: " << account->GetBalance() << ", Credit: " << account->GetCredit() << ", Interest rate: " << account->GetInterestRate() << ", Owner: {" << account->GetOwner()->GetCode() << ", " << account->GetOwner()->GetName() << "}]" << endl;
}

void accountInfo(Account* account){
    cout << "Account [Number: " << account->GetNumber() << ", Balance: " << account->GetBalance() << ", Interest rate: " << account->GetInterestRate() << ", Owner: {" << account->GetOwner()->GetCode() << ", " << account->GetOwner()->GetName() << "}]" << endl;
}

void accountInfo(PartnerAccount* account){
    cout << "Account [Number: " << account->GetNumber() << ", Balance: " << account->GetBalance() << ", Interest rate: " << account->GetInterestRate() << ", Owner: {" << account->GetOwner()->GetCode() << ", " << account->GetOwner()->GetName() << "}, Partner: " << (account->GetPartner() != nullptr ? account->GetPartner()->GetName() : "{NONE}") << "]" << endl;
}

void accountInfo(CreditAccount* account){
    cout << "Account [Number: " << account->GetNumber() << ", Balance: " << account->GetBalance() << ", Credit: " << account->GetCredit() << ", Interest rate: " << account->GetInterestRate() << ", Owner: {" << account->GetOwner()->GetCode() << ", " << account->GetOwner()->GetName() << "}]" << endl;
}

void deposit(Account* account, double amount){
    cout << "Deposit [Account number: " << account->GetNumber() << ", Amount: " << amount << "]" << endl;
}

void withdraw(Account* account, double amount, bool canWithdraw = true){
    cout << "Withdraw [Account number: " << account->GetNumber() << ", Amount: " << amount << "]" << (!canWithdraw ? " - Withdraw is not possible!" : "") << endl;
}

void addInterest(){
    cout << "Add interest to all accounts!" << endl;
}

void clientInfo(Client* client){
    cout << "Client [Code: " << client->GetCode() << ", Name: " << client->GetName() << "]" << endl;
}

int main()
{
    /*Client *o = new Client(0, "Smith");

    CreditAccount *ca = new CreditAccount(1, o, 1000);
    cout << ca->CanWithdraw(1000) << endl;

    Account *a = ca;
    cout << a->CanWithdraw(1000) << endl;

    cout << ca->Withdraw(1000) << endl;

    a = nullptr;
    delete ca;*/

    /*Client *o = new Client(0, "Smith");
    CreditAccount *ca = new CreditAccount(1, o, 1000);

    AbstractAccount *aa = ca;

    delete aa;
    delete o;*/

    // Spawn clients
    Client* clientA = new Client(0, "Alvin");
    Client* clientB = new Client(1, "Ben");

    // Output new clients
    newClient(clientA);
    newClient(clientB);

    // Create accounts
    CreditAccount* accountA = new CreditAccount(0, clientA, 1000);
    CreditAccount* accountB = new CreditAccount(1, clientB, 0.007, 1000);

    // Output new accounts
    newAccount(accountA);
    newAccount(accountB);
    cout << endl;

    accountA->Deposit(750);
    deposit(accountA,750);
    accountA->Withdraw(250);
    withdraw(accountA,250);
    accountInfo(accountA);
    cout << endl;

    accountB->Deposit(500);
    deposit(accountB,500);
    accountB->Withdraw(1000);
    withdraw(accountB,1000);
    accountB->Withdraw(1000);
    withdraw(accountB,1000,false);
    accountInfo(accountB);
    cout << endl;

    // Final info
    accountInfo(accountA);
    accountInfo(accountB);
    cout << endl;

    getchar();
    return 0;
}
