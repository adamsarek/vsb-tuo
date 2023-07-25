#include <iostream>
#include <iomanip>
#include <cmath>
#define _USE_MATH_DEFINES

using namespace std;

int main()
{
    char x;
    double a,b;
    cout << fixed << setprecision(4) << "Zadejte rovinny obrazec, jehoz obsah a obvod chcete spocitat: a - ctverec, b - obdelnik, c - kruh" << endl;
    cin >> x;
    switch(x){
        case 'a':
            cout << "Zadejte stranu ctverce:" << endl;
            cin >> a;
            if(cin.good() && a > 0){
                cout << "Obsah ctverce je: " << (a*a) << endl;
                cout << "Obvod ctverce je: " << (4*a) << endl;
            }
            else{
                cout << "Nespravny vstup." << endl;
            }
            break;
        case 'b':
            cout << "Zadejte strany obdelniku:" << endl;
            cin >> a >> b;
            if(cin.good() && a > 0 && b > 0){
                cout << "Obsah obdelniku je: " << (a*b) << endl;
                cout << "Obvod obdelniku je: " << (2*(a+b)) << endl;
            }
            else{
                cout << "Nespravny vstup." << endl;
            }
            break;
        case 'c':
            cout << "Zadejte polomer kruznice:" << endl;
            cin >> a;
            if(cin.good() && a > 0){
                cout << "Obsah kruznice je: " << (M_PI*a*a) << endl;
                cout << "Obvod kruznice je: " << (M_PI*2*a) << endl;
            }
            else{
                cout << "Nespravny vstup." << endl;
            }
            break;
        default:
            cout << "Nespravny vstup." << endl;
            break;
    }
    return 0;
}
