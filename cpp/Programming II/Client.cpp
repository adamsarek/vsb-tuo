#include "Client.h"

int Client::objectsCount = 0;

Client::Client(int c, string n)
{
    this->code = c;
    this->name = n;
    Client::objectsCount += 1;
}

Client::~Client(){
    Client::objectsCount -= 1;
}

int Client::GetObjectsCount(){
    return Client::objectsCount;
}

int Client::GetCode()
{
    return this->code;
}

string Client::GetName()
{
    return this->name;
}
