#include <iostream>
#include <cmath>

using namespace std;

int main()
{
    char znak;
    string y = "";

    cout << "Zadej cislo ve dvojkove soustave:" << endl;

    while((znak=cin.get()) != '\n'){
        if(znak == '0' || znak == '1'){
            y += znak;
        }
        else{
            y = "";
            break;
        }
    }
    if(y == ""){
        cout << "Nespravny vstup." << endl;
    }
    else{
        int z = 0;
        for(unsigned int i = 0; i < y.length(); i++){
            z += (y[i] - '0') * pow(2,y.length()-1-i);
        }
        cout << "Desitkove cislo je: " << z << endl;
    }

    return 0;
}
