#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <time.h>
#include <sys/time.h>
#include <sys/param.h>
#include <pthread.h>
#include <iostream>

#define TYPE_OPTION 0
#if TYPE_OPTION == 0
    #define TYPE long int
    #define TYPE_PRINT "%d"
#elif TYPE_OPTION == 1
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
    public:
        TYPE* array;
        int from;
        int to;
        int length;
        bool order;  // ASC: true, DESC: false

        Array(TYPE* array, int from, int to, int length, bool order) : array(array), from(from), to(to), length(length), order(order){};
        ~Array(){
            delete[] array;
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
        void fill(){
            for(int i = this->from; i < this->to; i++){
                this->array[i] = i+1;
            }
        }
        void swapRandom(){
            srand(time(NULL));
            for(int i = this->from; i < this->to; i++){
                TYPE randomKey = (TYPE)(rand() % this->length);
                this->array[i] = randomKey;
            }
        }
        void swap(TYPE* x, TYPE* y){
            TYPE temp = *x;
            *x = *y;
            *y = temp;
        }
        void selectionSort(){
            if(this->order){
                for(int i = this->from; i < (this->to-1); i++){
                    int min_idx = i;
                    for(int j = i+1; j < this->to; j++){
                        if(this->array[j] < this->array[min_idx]){
                            min_idx = j;
                        }
                    }
                    this->swap(&this->array[min_idx], &this->array[i]);
                }
            }
            else{
                for(int i = this->from; i < (this->to-1); i++){
                    int min_idx = i;
                    for(int j = i+1; j < this->to; j++){
                        if(this->array[j] > this->array[min_idx]){
                            min_idx = j;
                        }
                    }
                    this->swap(&this->array[min_idx], &this->array[i]);
                }
            }
        }
        void insertionSort(){
            if(this->order){
                for(int i = this->from+1; i < this->to; i++){
                    int j = i;
                    while(j > this->from && this->array[j] < this->array[j-1]){
                        this->swap(&this->array[j], &this->array[j-1]);
                        j -= 1;
                    }
                }
            }
            else{
                for(int i = this->from+1; i < this->to; i++){
                    int j = i;
                    while(j > this->from && this->array[j] > this->array[j-1]){
                        this->swap(&this->array[j], &this->array[j-1]);
                        j -= 1;
                    }
                }
            }
        }
        void bubbleSort(){
            if(this->order){
                for(int i = this->from; i < (this->to-1); i++){
                    for(int j = this->from; j < (this->to-1); j++){
                        if(this->array[j] > this->array[j+1]){
                            this->swap(&this->array[j], &this->array[j+1]);
                        }
                    }
                }
            }
            else{
                for(int i = this->from; i < (this->to-1); i++){
                    for(int j = this->from; j < (this->to-1); j++){
                        if(this->array[j] < this->array[j+1]){
                            this->swap(&this->array[j], &this->array[j+1]);
                        }
                    }
                }
            }
        }
        void merge(){
            TYPE* sortedArray = new TYPE[this->length];
            int x = 0, y = this->from, z = 0;
            if(this->order){
                while(x < this->from && y < this->to){
                    sortedArray[z++] = (this->array[x] < this->array[y] ? this->array[x++] : this->array[y++]);
                }
            }
            else{
                while(x < this->from && y < this->to){
                    sortedArray[z++] = (this->array[x] > this->array[y] ? this->array[x++] : this->array[y++]);
                }
            }

            if(x < from){
                for(int i = x; i < from; i++){
                    sortedArray[z++] = this->array[i];
                }
            }
            else{
                for(int i = y; i < to; i++){
                    sortedArray[z++] = this->array[i];
                }
            }

            for(int i = 0; i < to; i++){
                this->array[i] = sortedArray[i];
            }

            delete[] sortedArray;
        }
};

void* generateRandom(void* arg){
    Array* array = (Array*)arg;
    array->generateRandom();
    return NULL;
}

void* selectionSort(void* arg){
    Array* array = (Array*)arg;
    array->selectionSort();
    return NULL;
}

void* insertionSort(void* arg){
    Array* array = (Array*)arg;
    array->insertionSort();
    return NULL;
}

void* bubbleSort(void* arg){
    Array* array = (Array*)arg;
    array->bubbleSort();
    return NULL;
}

void* merge(void* arg){
    Array* array = (Array*)arg;
    array->merge();
    return NULL;
}

void* fill(void* arg){
    Array* array = (Array*)arg;
    array->fill();
    return NULL;
}

void swapRandom(Array* arg){
    srand(time(NULL));
    for(int i = 0; i < arg->length; i++){
        TYPE randomKey = (TYPE)(rand() % arg->length);
        TYPE oldValue = arg->array[i];
        arg->array[i] = arg->array[randomKey];
        arg->array[randomKey] = oldValue;
    }
}

int main()
{
    const int threadsCount = 8;

    int arrayLength = 80;
    TYPE* array = new TYPE[arrayLength];

    // Create threads & objects
    pthread_t threads[threadsCount];
    Array* objects[threadsCount];

    double threadArrayPart = ((double)arrayLength) / threadsCount;
    for(int i = 0; i < threadsCount; i++){
        int from = (int)(threadArrayPart * i);
        int to =   (int)(threadArrayPart * (i+1));
        objects[i] = new Array(array, from, to, arrayLength, true);
    }

    // Fill Array
    printf("Fill with numbers...\n");
    for(int i = 0; i < threadsCount; i++){
        pthread_create(&threads[i], nullptr, fill, (void*)objects[i]);
        pthread_join(threads[i], nullptr);
    }
    //for(int i = 0; i < threadsCount; i++){ objects[i]->print(); printf("\n"); }
    printf("\n");

    // Swap Random
    printf("Swap Random...\n");
    swapRandom(objects[0]);
    //for(int i = 0; i < threadsCount; i++){ objects[i]->print(); printf("\n"); }
    printf("\n");

    // Selection Sort
    printf("Selection Sort...\n");
    for(int i = 0; i < threadsCount; i++){
        pthread_create(&threads[i], nullptr, selectionSort, (void*)objects[i]);
        pthread_join(threads[i], nullptr);
    }
    //for(int i = 0; i < threadsCount; i++){ objects[i]->print(); printf("\n"); }
    printf("\n");

    // Merge
    printf("Merge...\n");
    for(int i = 1; i < threadsCount; i++){
        pthread_create(&threads[i], nullptr, merge, (void*)objects[i]);
        pthread_join(threads[i], nullptr);
    }
    for(int i = 0; i < threadsCount; i++){ objects[i]->print(); printf("\n"); }
    printf("\n");

    return 0;
}
