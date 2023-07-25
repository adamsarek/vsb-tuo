#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <time.h>
#include <sys/time.h>
#include <sys/param.h>
#include <pthread.h>
#include <iostream>

#define TYPE_OPTION 1
#if TYPE_OPTION == 1
    #define TYPE int
    #define TYPE_PRINT "%d"
#elif TYPE_OPTION == 2
    #define TYPE float
    #define TYPE_PRINT "%.1f"
#elif TYPE_OPTION == 3
    #define TYPE double
    #define TYPE_PRINT "%.1f"
#elif TYPE_OPTION == 4
    #define TYPE char
    #define TYPE_PRINT "%c"
#endif

class Array{
    private:
        TYPE* array;
        int from;
        int to;
        int length;
        bool order;  // ASC: true, DESC: false
    public:
        Array(TYPE* array, int from, int to, int length, bool order) : array(array), from(from), to(to), length(length), order(order) {};
        ~Array(){
            delete[] this->array;
        }
        void print(){
            for(int i = this->from; i < this->to; i++){
                printf(TYPE_PRINT"  ", this->array[i]);
            }
        }
        void generateRandom(){
            srand(time(NULL));
            for(int i = this->from; i < this->to; i++){
                this->array[i] = (TYPE)(rand() % this->length);
            }
        }
};

void* generateRandom(void* arg){
    Array* array = (Array*)arg;
    array->generateRandom();
    return NULL;
}

int main()
{
    int threadsCount = 4;

    int my_length = 40;
    TYPE* my_array = new TYPE[my_length];

    // Create threads
    pthread_t threads[threadsCount];

    // Create objects with pointers to parts of Array
    Array** objects;
    for(int i = 0; i < threadsCount; i++){
        objects[i] = new Array(my_array, (my_length / threadsCount) * i, (my_length / threadsCount) * (i+1), my_length, true);
    }

    // Generate random data
    /*for(int i = 0; i < threadsCount; i++){
        pthread_create(&threads[i], nullptr, generateRandom, objects[i]);
        pthread_join(threads[i], nullptr);
        objects[i]->print();
    }*/
    int i = 0;
    pthread_create(&threads[i], nullptr, generateRandom, objects[i]);
    pthread_join(threads[i], nullptr);
    objects[i]->print();
    printf("\n");
    i = 1;
    pthread_create(&threads[i], nullptr, generateRandom, objects[i]);
    pthread_join(threads[i], nullptr);
    objects[i]->print();
    printf("\n");
    i = 2;
    pthread_create(&threads[i], nullptr, generateRandom, objects[i]);
    pthread_join(threads[i], nullptr);
    objects[i]->print();
    printf("\n");
    i = 3;
    pthread_create(&threads[i], nullptr, generateRandom, objects[i]);
    pthread_join(threads[i], nullptr);
    objects[i]->print();
    printf("\n");

    /*Array object1(my_array,                 0, my_length / 4 * 1, my_length, true);
    Array object2(my_array, my_length / 4 * 1, my_length / 4 * 2, my_length, true);
    Array object3(my_array, my_length / 4 * 2, my_length / 4 * 3, my_length, true);
    Array object4(my_array, my_length / 4 * 3, my_length / 4 * 4, my_length, true);

    // Initialize threads
    pthread_t thread1, thread2, thread3, thread4;

    // Generate random numbers
    pthread_create(&thread1, nullptr, generateRandom, &object1);
    pthread_create(&thread2, nullptr, generateRandom, &object2);
    pthread_create(&thread3, nullptr, generateRandom, &object3);
    pthread_create(&thread4, nullptr, generateRandom, &object4);
    pthread_join(thread1, nullptr);
    pthread_join(thread2, nullptr);
    pthread_join(thread3, nullptr);
    pthread_join(thread4, nullptr);

    // Display arrays
    object1.print();
    printf("\n");
    object2.print();
    printf("\n");
    object3.print();
    printf("\n");
    object4.print();
    printf("\n");*/

    return 0;
}
