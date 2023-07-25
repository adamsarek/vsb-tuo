#ifndef CLIENT_H
#define CLIENT_H

#include <iostream>

using namespace std;

class Client
{
    private:
        static int objectsCount;
        int code;
        string name;
    public:
        static int GetObjectsCount();

        Client(int c, string n);
        ~Client();

        int GetCode();
        string GetName();
};

#endif // CLIENT_H
