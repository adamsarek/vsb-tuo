#include <iostream>
#include "Operations.h"

using namespace std;

int main()
{
    string number;
    int system;
    cout << "Pro desetinne cisla prosim pouzivejte tecku a soustavu zadavejte cislem!" << endl << endl;
    cout << "   Cislo: ";
    cin >> number;
    cout << "Soustava: ";
    cin >> system;
    cout << endl;

    Operations* op = new Operations(number, system);

    cout << "     Bin: " << op->Bin() << endl;
    cout << "     Dec: " << op->Dec() << endl;
    cout << "     Hex: " << op->Hex() << endl;

    delete op;
    op = nullptr;

    return 0;
}
