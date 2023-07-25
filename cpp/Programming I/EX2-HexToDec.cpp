#include <iostream>
#include <cmath>

using namespace std;

int main()
{
    string hex;
    char znak;
    int dec = 0;
    bool fail = false;

    cout << "Zadejte hexadecimalni cislo:" << endl;
    int i = 0;
    while((znak=cin.get()) != '\n'){
        znak = toupper(znak);
        if((znak - '0' >= 0 && znak - '0' <= 9) || (znak - 'A' >= 0 && znak - 'A' <= 5)){
            hex += znak;
        }
        else{
            fail = true;
            break;
        }
        i++;
    }
    if(fail == true){
        cout << "Nespravny vstup." << endl;
    }
    else{
        for(unsigned int j = 0; j < hex.length(); j++){
            znak = hex[j];
            if(znak - '0' >= 0 && znak - '0' <= 9){
                dec += (znak - '0') * pow(16,hex.length()-1-j);
            }
            else if(znak - 'A' >= 0 && znak - 'A' <= 5){
                dec += ((znak - 'A') + 10) * pow(16,hex.length()-1-j);
            }
        }
        cout << "Desitkove: " << dec << endl;
    }

    return 0;
}
