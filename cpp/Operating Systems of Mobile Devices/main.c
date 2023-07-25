// Based on https://c9x.me/articles/gthreads/code0.html
#include <assert.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <signal.h>
#include <unistd.h>
#include <errno.h>
#include <time.h>

#include "gthr.h"

static int lastTicket = 0;

// Dummy function to simulate some thread work
void f(void) {
  static int x;
  int i, id;

  id = ++x;
  while (true) {

    printf("F Thread id = %d, val = %d BEGINNING\n", id, ++i);
    uninterruptibleNanoSleep(0, 50000000);
    printf("F Thread id = %d, val = %d END\n", id, ++i);
    uninterruptibleNanoSleep(0, 50000000);
  }
}

// Dummy function to simulate some thread work
void g(void) {
  static int x;
  int i, id;

  id = ++x;
  while (true) {

    printf("G Thread id = %d, val = %d BEGINNING\n", id, ++i);
    uninterruptibleNanoSleep(0, 50000000);
    printf("G Thread id = %d, val = %d END\n", id, ++i);
    uninterruptibleNanoSleep(0, 50000000);

  }
}

void signalHandle(int sig) {
  signal(sig, SIG_IGN);
  
  switch(sig) {
    case SIGINT: {
      printf("\nShowing stats...\n");
      printStats();
      break;
    }
    case SIGQUIT: {
      printf("\nShowing stats and quitting process...\n");
      printStats();
      exit(0);
      break;
    }
  }

  signal(sig, signalHandle);
}

void generateData(int * priority, int ** tickets, int * ticketsCount) {
  *priority += 1;
  *ticketsCount -= 1;
  *tickets = calloc(*ticketsCount, sizeof(int));
  for(int i = 0; i < *ticketsCount; i++) {
    (*tickets)[i] = lastTicket++;
  }
}

int main(int argc, char* argv[]) {
  signal(SIGINT, signalHandle);  /* [CTRL] + [C] */
  signal(SIGQUIT, signalHandle); /* [CTRL] + [\] */

  // Scheduler: RR (round robin), PRI (priority scheduling), LS (lottery scheduling)
  int scheduler = 0; // Default is RR
  if(argc > 1) {
    if(strcmp(argv[1], "RR") == 0) { scheduler = 0; }       // ./gttest RR
    else if(strcmp(argv[1], "PRI") == 0) { scheduler = 1; } // ./gttest PRI
    else if(strcmp(argv[1], "LS") == 0) { scheduler = 2; }  // ./gttest LS
  }

  int priority = -1;
  int * tickets;
  int ticketsCount = MaxGThreads;
  
  gtinit(scheduler, 10, NULL, 0);

  for(int i = 1; i < MaxGThreads; i++) {
    generateData(&priority, &tickets, &ticketsCount);
    gtgo((i % 2 == 0 ? f : g), priority, tickets, ticketsCount);
  }
  
  free(tickets);
  gtret(1);		// wait until all threads terminate
}