#include <iostream>

using namespace std;

int main()
{
    string heslo;
    char znak;
    bool podminky[4];
    for(unsigned int i = 0; i < sizeof(podminky); i++){
        podminky[i] = false;
    }
    int i = 0;
    while((znak = cin.get()) != '\n'){
        i++;
        if(i >= 5){
            podminky[0] = true;
        }
        if((znak - 'a' >= 0 && znak - 'z' <= 0) || (znak - 'A' >= 0 && znak - 'Z' <= 0)){
            podminky[1] = true;
        }
        else if(znak - '0' >= 0 && znak - '9' <= 0){
            podminky[2] = true;
        }
        else{
            podminky[3] = true;
        }
    }
    if(podminky[0] == true && podminky[1] == true && podminky[2] == true && podminky[3] == true){
        cout << "Heslo splnuje pozadavky." << endl;
    }
    else{
        cout << "Heslo nesplnuje pozadavky." << endl;
    }
    return 0;
}
