// **************************************************************************
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
// **************************************************************************
 
#include "mbed.h"
 
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

// Invert value of both LED lights 
void blink(){
    g_led1 = !g_led1;
    g_led2 = !g_led2;
}
 
int main()
{
    // Serial line initialization
    g_pc.baud(115200);
 
    // uncomment selected demo
    //demo_leds();
    //demo_lcd();
    //demo_i2c();
 
    // ******************************************************************

    g_pc.printf( "Program started...\r\n" );
    
    // turn on 2nd LED light
    g_led2 = 1;
 
    Ticker l_t1;
 
    // blink every 0.5s
    l_t1.attach_us( callback( blink ), 500000);

    // wait until button 9 is pressed 
    while( g_but9 == 1 );
 
    g_pc.printf( "Program stopped!\r\n" );
}