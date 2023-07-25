enum {
    MaxGThreads = 11,		// Maximum number of threads, used as array size for gttbl
    StackSize = 0x400000,	// Size of stack of each thread
    MaxPriority = 0,
    MinPriority = 10
};

struct gt {
  // Saved context, switched by gtswtch.S (see for detail)
  struct gtctx {
    uint64_t rsp;
    uint64_t r15;
    uint64_t r14;
    uint64_t r13;
    uint64_t r12;
    uint64_t rbx;
    uint64_t rbp;
  }
  ctx;
  // Thread state
  enum {
    Unused,
    Running,
    Ready,
  }
  st;

  // Time stats
  long runtime;
  long runtimeCount;
  long runtimeMin;
  long runtimeMax;
  long runtimeAvg;

  // Priority
  int priorityCurrent;
  int priorityDefault;

  // Tickets
  int * tickets;
  int ticketsCount;
};

void gtinit(int scheduler, int priority, int * tickets, int ticketsCount);				// initialize gttbl
void gtret(int ret);			// terminate thread
void gtswtch(struct gtctx * old, struct gtctx * new);	// declaration from gtswtch.S
bool gtyield(void);				// yield and switch to another thread
void gtstop(void);				// terminate current thread
int gtgo(void( * f)(void), int priority, int * tickets, int ticketsCount);		// create new thread and set f as new "run" function
void resetsig(int sig);			// reset signal
void gthandle(int sig);			// periodically triggered by alarm
int uninterruptibleNanoSleep(time_t sec, long nanosec);	// uninterruptible sleep
void addTickets(int ** ticketsTo, int * ticketsFrom, int ticketsCount);
void printStats();