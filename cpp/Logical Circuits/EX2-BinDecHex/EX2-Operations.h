#ifndef OPERATIONS_H
#define OPERATIONS_H
#include <iostream>

using namespace std;

class Operations
{
    private:
        string number;
        int system;
    public:
        Operations(string num, int sys);
        string Bin();
        string Dec();
        string Hex();

        string Zero(string num);
        int CharToInt(char num);
        char IntToChar(int num);
};

#endif // OPERATIONS_H
