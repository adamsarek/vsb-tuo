#include <iostream>

using namespace std;

int factorial(const int n){
    if(n == 0){
        return 1;
    }
    return n * factorial(n - 1);
}

int main()
{
    int n,k;
    cout << "Zadejte n a k:" << endl;
    cin >> n >> k;

    if(cin.good() && n >= k && k >= 0){
        int vysledek = factorial(n) / (factorial(k) * factorial(n-k));
        cout << "C = " << vysledek << endl;
    }
    else{
        cout << "Nespravny vstup." << endl;
    }

    return 0;
}
