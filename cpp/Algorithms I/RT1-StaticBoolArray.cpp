#include <iostream>

using namespace std;

void Clear(bool** &pole, int N) {
	for (int i = 0; i < N; i++) {
		for (int j = 0; j < N; j++) {
			pole[i][j] = false;
		}
	}
}

void Fill(bool** &pole, int N) {
	for (int i = 0; i <= N - 1; i++) {
		if (i <= N / 2) {
			for (int j = i; j < N - i; j++) {
				pole[i][j] = true;
			}
		}
		else {
			for (int j = N - i - (N % 2); j <= i - 1 + (N % 2); j++) {
				pole[i][j] = true;
			}
		}
	}
}

void Print(bool** pole, int N) {
	for (int i = 0; i < N; i++) {
		for (int j = 0; j < N; j++) {
			cout << (pole[i][j] ? "*" : " ");
		}
		cout << endl;
	}
}

void Delete(bool** &pole, int N) {
	for (int i = 0; i < N; i++) {
		delete[] pole[i];
	}
	delete[] pole;
	pole = nullptr;
}

int main() {
	const int N = 5;
	
	// Vytvoření pole A
	bool** A = new bool*[N];
	for (int i = 0; i < N; i++) {
		A[i] = new bool[N];
	}

	// Nastavení hodnot pole na false
	Clear(A,N);

	// Naplní pole obrazcem
	Fill(A,N);

	// Vypíše výsledek
	Print(A,N);

	// Smaže pole
	Delete(A,N);

	system("pause");
	return 0;
}