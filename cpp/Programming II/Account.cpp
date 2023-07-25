#include "Account.h"

int Account::objectsCount = 0;
double Account::defaultInterestRate = 0.005;

PartnerAccount::PartnerAccount(int n, Client *o, Client *p) : Account(n, o){
    this->partner = p;
}

PartnerAccount::PartnerAccount(int n, Client *o, Client *p, double ir) : Account(n, o, ir){
    this->partner = p;
}

Client* PartnerAccount::GetPartner(){
    return this->partner;
}

CreditAccount::CreditAccount(int n, Client *o, double c) : Account(n, o){
    this->credit = c;
}

CreditAccount::CreditAccount(int n, Client *o, double ir, double c) : Account(n, o, ir){
    this->credit = c;
}

CreditAccount::~CreditAccount(){
    cout << "CreditAccount destructor" << endl;
}

bool CreditAccount::CanWithdraw(double a){
    return (this->GetBalance() + this->credit >= a);
}

bool CreditAccount::Withdraw(double a){
    if(a > 0 && this->CanWithdraw(a)){
        this->balance -= a;
        return true;
    }
    return false;
}

double CreditAccount::GetCredit(){
    return this->credit;
}

Account::Account(int n, Client *o)
{
    this->number = n;
    this->owner = o;
    this->interestRate = Account::defaultInterestRate;
    Account::objectsCount += 1;
}
Account::Account(int n, Client *o, double ir)
{
    this->number = n;
    this->owner = o;
    this->interestRate = ir;
    Account::objectsCount += 1;
}

Account::~Account(){
    cout << "Account destructor" << endl;
    Account::objectsCount -= 1;
}

double Account::GetDefaultInterestRate(){
    return Account::defaultInterestRate;
}

void Account::SetDefaultInterestRate(double newInterestRate){
    Account::defaultInterestRate = newInterestRate;
}

int Account::GetObjectsCount(){
    return Account::objectsCount;
}

int Account::GetNumber(){
    return this->number;
}

double Account::GetBalance(){
    return this->balance;
}

double Account::GetInterestRate(){
    return this->interestRate;
}

Client* Account::GetOwner(){
    return this->owner;
}

bool Account::CanWithdraw(double a){
    return (this->balance >= a);
}

void Account::Deposit(double a){
    if(a > 0){
        this->balance += a;
    }
}

bool Account::Withdraw(double a){
    if(a > 0 && this->CanWithdraw(a)){
        this->balance -= a;
        return true;
    }
    return false;
}

void Account::AddInterest(){
    this->balance *= (1 + this->interestRate);
}

AbstractAccount::AbstractAccount(){
    cout << "AbstractAccount constructor" << endl;
}

AbstractAccount::~AbstractAccount(){
    cout << "AbstractAccount destructor" << endl;
}
