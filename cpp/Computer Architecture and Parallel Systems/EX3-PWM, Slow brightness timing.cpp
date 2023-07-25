//**************************************************************************
//
//               Demo program for labs
//
// Subject:      Computer Architectures and Parallel systems
// Author:       Petr Olivka, petr.olivka@vsb.cz, 08/2016
// Organization: Department of Computer Science, FEECS,
//               VSB-Technical University of Ostrava, CZ
//
// File:         LED Brightness
// Student ID:   SAR0083
//
//**************************************************************************
 
#include \"mbed.h\"
 
// LEDs on K64F-KIT - instances of class DigitalOut
DigitalOut g_led1( PTA1 );
DigitalOut g_led2( PTA2 );
 
// Button on K64F-KIT - instance of class DigitalIn
DigitalIn g_but9( PTC9 );
DigitalIn g_but10( PTC10 );
DigitalIn g_but11( PTC11 );
DigitalIn g_but12( PTC12 );
 
int cas = 0;
int t = 20;
 
DigitalOut ledPole [8] = {
    DigitalOut (PTC0),
    DigitalOut (PTC1),
    DigitalOut (PTC2),
    DigitalOut (PTC3),
    DigitalOut (PTC4),
    DigitalOut (PTC5),
    DigitalOut (PTC7),
    DigitalOut (PTC8)
};
 
double jasPole[8] = {0,0,0,0,0,0,0,0};
 
void pridatCas()
{
    cas++;
}
 
int main()
{
    Ticker l_t;
    l_t.attach_us( callback( pridatCas ), 1000);
 
    int delkaCyklu = 1000;
    int vybranaDioda = 0;
    int krokCyklu = 0;
    int pocetKrokuCyklu = 12;
 
    while(1){
        // zacatek kazde diody
        if(krokCyklu == 0){
            jasPole[vybranaDioda] = 0;
        }
        if(cas % (delkaCyklu * 8) > (vybranaDioda * delkaCyklu) + ((delkaCyklu / pocetKrokuCyklu) * krokCyklu)){
            jasPole[vybranaDioda] += 10;
 
            if(krokCyklu == pocetKrokuCyklu-1){
                krokCyklu = 0;
                vybranaDioda = (vybranaDioda == 7 ? 0 : vybranaDioda+1);
            }
            else{
                krokCyklu++;
            }
        }
        if(!g_but9){
            vybranaDioda = 0;
            krokCyklu = 0;
            cas = 0;
            for(int i = 0; i < 8; i++){
                jasPole[i] = 0;
            }
        }
        // blikani diody
        for(int i = 0; i < 8; i++){
            if(cas % t >= t * (jasPole[i] / 100)){
                ledPole[i] = 0;
            }
            else{
                ledPole[i] = 1;
            }
        }
    }
}