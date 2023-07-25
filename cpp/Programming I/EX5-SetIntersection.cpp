#include <iostream>

using namespace std;

bool find(int* pole, int pocet, int v){
    for(int i = 0; i < pocet; i++){
        if(pole[i] == v){
            return true;
        }
    }
    return false;
}

string prunik(int* poleA,int* poleB, int pocetA, int pocetB){
    string vysledek = "";
    bool prvni = true;
    for(int i = 0; i < (pocetA < pocetB ? pocetA : pocetB); i++){
        if(pocetA >= pocetB){
            if(find(poleA,pocetA,poleB[i])){
                vysledek += (prvni == false ? ", " : "") + to_string(poleB[i]);
                if(prvni == true){prvni = false;}
            }
        }
        else if(pocetB >= pocetA){
            if(find(poleB,pocetB,poleA[i])){
                vysledek += (prvni == false ? ", " : "") + to_string(poleA[i]);
                if(prvni == true){prvni = false;}
            }
        }
    }
    return vysledek;
}

int main()
{
    int pocetA, pocetB;
    cout << "Zadejte pocet prvku mnoziny A:" << endl;
    cin >> pocetA;
    if(cin.good() && pocetA > 0){
        int* poleA = new int[pocetA];
        cout << "Zadejte prvky mnoziny A:" << endl;
        for(int i = 0; i < pocetA; i++){
            int prvek;
            cin >> prvek;
            if(!find(poleA,i+1,prvek)){
                poleA[i] = prvek;
            }
            else{
                cout << "Nespravny vstup." << endl;
                return 0;
            }
        }

        cout << "Zadejte pocet prvku mnoziny B:" << endl;
        cin >> pocetB;
        if(cin.good() && pocetB > 0){
            int* poleB = new int[pocetB];
            cout << "Zadejte prvky mnoziny B:" << endl;
            for(int i = 0; i < pocetB; i++){
                int prvek;
                cin >> prvek;
                if(!find(poleB,i+1,prvek)){
                    poleB[i] = prvek;
                }
                else{
                    cout << "Nespravny vstup." << endl;
                    return 0;
                }
            }
            cout << "Prunik mnozin:" << endl;
            cout << "{" << prunik(poleA,poleB,pocetA,pocetB) << "}" << endl;
            delete[] poleA;
            poleA = nullptr;
            delete[] poleB;
            poleB = nullptr;
        }
        else{
            cout << "Nespravny vstup." << endl;
        }
    }
    else{
        cout << "Nespravny vstup." << endl;
    }
    return 0;
}
