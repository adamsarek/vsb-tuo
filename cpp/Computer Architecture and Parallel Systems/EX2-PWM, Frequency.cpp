// ************************************************************************
//
//               Demo program for labs
//
// Subject:      Computer Architectures and Parallel systems
// Author:       Petr Olivka, petr.olivka@vsb.cz, 09/2019
// Organization: Department of Computer Science, FEECS,
//               VSB-Technical University of Ostrava, CZ
//
// File:         Demo program
//
// ************************************************************************
 
#include \"mbed.h\"
 
void demo_leds();
void demo_lcd();
void demo_i2c();
 
// DO NOT REMOVE OR RENAME FOLLOWING GLOBAL VARIABLES!!
 
// Serial line for printf output
Serial g_pc(USBTX, USBRX);
 
// LEDs on K64F-KIT - instances of class DigitalOut
DigitalOut g_led1(PTA1);
DigitalOut g_led2(PTA2);
 
// Buttons on K64F-KIT - instances of class DigitalIn
DigitalIn g_but9(PTC9);
DigitalIn g_but10(PTC10);
DigitalIn g_but11(PTC11);
DigitalIn g_but12(PTC12);
 
int cas = 0;
int frekvence = 500;
int jas = 10;
 
void zvysitCas()
{
    cas++;
}
 
int main()
{
    // Serial line initialization 1000
    g_pc.baud(115200);
 
    g_pc.printf( \"Program started...\\r\\n\" );
 
    Ticker l_t1;
    l_t1.attach_us( callback( zvysitCas ), 1000);
 
    int t = 20;
 
    while(1)
    {
        if(cas % t > t * (jas / 100)){
            g_led1 = 0;
        }
        else{
            g_led1 = 1;
        }
 
        if(cas % 1000 > frekvence){
            g_led2 = !g_led2;
        }
    }
 
    g_pc.printf( \"Program stopped!\\r\\n\" );
}