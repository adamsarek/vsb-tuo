#ifndef __PROGTEST__
#include <iostream>
#include <cstdlib>
#include <fstream>
#include <vector>

using namespace std;
#endif /* __PROGTEST__ */

/* Vase pomocne funkce (jsou-li potreba) */

int evenOdd ( const char * srcFileName, const char * dstFileName ){
    string radek;
    ifstream vstup;
    ofstream vystup;

    vstup.open(srcFileName,ios::in);
    if(!vstup.is_open()){return 0;}

    vystup.open(dstFileName,ios::out);
    if(!vystup.is_open()){return 0;}

    while(vstup >> radek){
        for(unsigned int i = 0; i < radek.length(); i++){
            if(!isdigit(radek[i])){
                if(radek[i] != '-'){
                    vstup.close();
                    vystup.flush();
                    vystup.close();
                    return 0;
                }
            }
        }

        if(atoi(radek.c_str()) % 2 == 0){
            vystup << radek << endl;
        }
    }
    vstup.clear();
    vstup.seekg(0,ios::beg);

    while(vstup >> radek){
        if(atoi(radek.c_str()) % 2 != 0){
            vystup << radek << endl;
        }
    }
    vstup.close();
    vystup.flush();
    vystup.close();

    return 1;
}

#ifndef __PROGTEST__
int main ( int argc, char * argv [] ){
    /* Vase testy */
    cout << evenOdd("in.txt","out.txt");
    return 0;
}
#endif /* __PROGTEST__ */
