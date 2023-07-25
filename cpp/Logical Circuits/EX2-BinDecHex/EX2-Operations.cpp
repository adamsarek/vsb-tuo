#include <cmath>
#include <cstring>
#include <string>
#include "Operations.h"

Operations::Operations(string num, int sys)
{
    this->number = this->Zero(num);
    this->system = sys;
}

string Operations::Bin(){
    if(this->system == 2){
        return this->number;
    }

    // Search floating point
    int i = 0;
    for(; i < this->number.length(); i++){
        if(this->number[i] == '.'){
            break;
        }
    }

    // Convert number before floating point
    string result = "";
    int decimal = stoi(this->Dec());
    while(decimal != 0){
        result = to_string(decimal % 2) + result;
        decimal = decimal / 2;
    }

    // Convert number after floating point
    if(i != this->number.length()){
        result += '.';
        double decimal = (stod(this->Dec()) - (int)stod(this->Dec()));
        while(decimal != 0){
            result = result + to_string((int)(decimal * 2));
            decimal = (decimal * 2) - (int)(decimal * 2);
        }
    }

    return this->Zero(result);
}

string Operations::Dec(){
    if(this->system == 10){
        return this->number;
    }

    // Search floating point
    int i = 0;
    for(; i < this->number.length(); i++){
        if(this->number[i] == '.'){
            break;
        }
    }

    // Convert number including the part after floating point
    string result = "0";
    int stop = (i != this->number.length() ? - ((int)this->number.length() - i - 1) : 0);
    for(int j = i-1, k = 0; j >= stop; j--, k++){
        if(this->number[k] == '.'){
            k++;
        }
        result = to_string(stod(result) + (this->CharToInt(this->number[k]) * pow(this->system,j)));
    }

    return (stod(result) - stoi(result) == 0 ? to_string(stoi(result)) : result);
}

string Operations::Hex(){
    if(this->system == 16){
        return this->number;
    }

    // Search floating point
    int i = 0;
    for(; i < this->Bin().length(); i++){
        if(this->Bin()[i] == '.'){
            break;
        }
    }

    // Convert number before floating point
    string result = "";
    string binary = "";
    for(int j = i-1, k = 0; j >= -1; j--, k++){
        if(k == 4 || j == -1){
            int hex = 0;
            for(int l = 0; l < k; l++){
                hex += this->CharToInt(binary[l]) * pow(2,k-l-1);
            }
            result = this->IntToChar(hex) + result;
            k = 0;
            binary = "";
        }
        binary = this->Bin()[j] + binary;
    }

    // Convert number after floating point
    if(i != this->Bin().length()){
        result += '.';
        string decimal = "";
        for(int j = i+1; j <= this->Bin().length(); j++){
            if(decimal.length() == 4 || j == this->Bin().length()){
                int hex = 0;
                for(int k = 0; k < decimal.length(); k++){
                    hex += this->CharToInt(decimal[k]) * pow(2,3-k);
                }
                result += this->IntToChar(hex);
                hex = 0;
                decimal = "";
            }
            decimal += this->Bin()[j];
        }
    }

    return result;
}

string Operations::Zero(string num){
    for(int i = 0; i < num.length(); i++){
        if(num[i] != '0'){
            return num.substr(i);
        }
    }
}

int Operations::CharToInt(char num){
    if(num - 48 >= 0 && num - 57 <= 0){
        return (num - 48);
    }
    if(num - 65 >= 0 && num - 70 <= 0){
        return (num - 55);
    }
    if(num - 97 >= 0 && num - 102 <= 0){
        return (num - 87);
    }
}

char Operations::IntToChar(int num){
    switch(num){
        case 0:
            return '0';
        case 1:
            return '1';
        case 2:
            return '2';
        case 3:
            return '3';
        case 4:
            return '4';
        case 5:
            return '5';
        case 6:
            return '6';
        case 7:
            return '7';
        case 8:
            return '8';
        case 9:
            return '9';
        case 10:
            return 'A';
        case 11:
            return 'B';
        case 12:
            return 'C';
        case 13:
            return 'D';
        case 14:
            return 'E';
        case 15:
            return 'F';
    }
}
