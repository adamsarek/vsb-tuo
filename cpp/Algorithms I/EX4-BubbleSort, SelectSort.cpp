#include <iostream>
#include <ctime>

using namespace std;

void print(int *p, int N) {
	for (int i = 0; i < N; i++) {
		cout << p[i] << "\t";
	}
	cout << endl;
}

// Důležité vědět!!!
void bubbleSort(int *p, int N) {
	for (int i = 0; i < N-1; i++) {
		bool changed = false;

		for (int j = 0; j < N-1; j++) {
			if (p[j] > p[j + 1]) {
				int tmp = p[j];
				p[j] = p[j + 1];
				p[j + 1] = tmp;
				// Dělá to stejné co 3 řádky nad tím
				/*
				swap(p[j],p[j + 1]);
				*/

				changed = true;
			}
		}
		if (!changed) {
			break;
		}
	}
}

void selectionSort(int *p, int N) {
	for (int i = 0; i < N-1; i++) {
		int min = i;

		// Doma nalezena chyba: místo N-1 použít N
		for (int j = i + 1; j < N; j++) {
			if (p[j] < p[min]) {
				min = j;
			}
		}
		swap(p[i], p[min]);
	}
}

int main() {
	int N = 10;
	int *p = new int[N];

	srand((unsigned int)time(nullptr));

	// Bubble sort
	for(int i = 0; i < N; i++) {
		p[i] = rand() % 100;
	}
	time_t start = time(nullptr);
	//print(p,N);
	bubbleSort(p,N);
	print(p, N);
	time_t end = time(nullptr);
	cout << "Bubble sort time: " << (end - start) << " s" << endl;

	// Selection sort
	for (int i = 0; i < N; i++) {
		p[i] = rand() % 100;
	}
	start = time(nullptr);
	//print(p,N);
	selectionSort(p, N);
	print(p, N);
	end = time(nullptr);
	cout << "Selection sort time: " << (end - start) << " s" << endl;

	delete[] p;
	p = nullptr;

	system("pause");
	return 0;
}