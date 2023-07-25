#include <iostream>
#include <string>

using namespace std;

int main()
{
    cout << "Zadejte dve binarni cisla:" << endl;

    // Declare variables
    string a,b;
    char znak;

    // Initialize first binary number
    while((znak=cin.get()) != ' '){
        if(znak - '0' == 0 || znak - '0' == 1){
            if(a.length() > 0 || (a.length() == 0 && znak == '1')){
                a += znak;
            }
            else if(a.length() == 0 && cin.peek() == ' '){
                a = "0";
            }
        }
        else{
            a = "";
            break;
        }
    }

    // Initialize second number if everything till now is OK
    if(a.length() > 0){
        while((znak=cin.get()) != '\n'){
            if(znak - '0' == 0 || znak - '0' == 1){
                if(b.length() > 0 || (b.length() == 0 && znak == '1')){
                    b += znak;
                }
                else if(b.length() == 0 && cin.peek() == '\n'){
                    b = "0";
                }
            }
            else{
                b = "";
                break;
            }
        }
    }

    // If input had disabled characters
    if(a.length() == 0 || b.length() == 0){
        cout << "Nespravny vstup." << endl;
    }
    // If everything till now is OK
    else{
        // Get length of the longest string
        int al = a.length();
        int bl = b.length();
        if(bl > al){
            string c = a;
            a = b;
            al = bl;
            b = c;
            bl = b.length();
        }

        // Perform summation
        string d;
        int z = 0;
        for(int i = 0; i < al; i++){
            if(bl-1-i >= 0){
                int c = (a[al-1-i] - '0') + (b[bl-1-i] - '0') + z;
                if(c == 3){
                    c = 1;
                    z = 1;
                }
                else if(c == 2){
                    c = 0;
                    z = 1;
                }
                else{
                    z = 0;
                }
                d = (c == 0 ? '0' : '1') + d;
            }
            else{
                int c = (a[al-1-i] - '0') + z;
                if(c == 2){
                    c = 0;
                    z = 1;
                }
                else{
                    z = 0;
                }
                d = (c == 0 ? '0' : '1') + d;
            }
        }
        if(z == 1){d = '1' + d;}
        cout << "Soucet: " << d << endl;
    }
    return 0;
}
