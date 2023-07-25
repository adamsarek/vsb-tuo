#include <iostream>

using namespace std;

void allocate(int** &p, int rows, int cols) {
	p = new int*[rows];
}
void initialize(int **p,int rows,int cols) {
	for (int i = 0; i < rows; i++) {
		p[i] = new int[cols];
	}
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			p[i][j] = 0;
		}
	}
}
void print(int **p, int rows, int cols) {
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			cout << p[i][j] << "\t";
		}
		cout << endl;
	}
}
void deallocate(int** &p, int rows) {
	for (int i = 0; i < rows; i++) {
		delete[] p[i];
	}
	delete[] p;
	p = nullptr;
}

int main(void) {
	/*const int N = 5;
	int p[N];
	cout << sizeof(p) << endl;
	cout << p << " | " << &p << " | " << &p[0] << endl;*/

	/*int N = 5;
	int *p = new int[N];
	for (int i = 0; i < N; i++) {
		p[i] = i * i;
	}
	for (int i = 0; i < N; i++) {
		cout << p[i] << endl;
	}
	delete[] p;
	p = nullptr;*/

	/*int rows = 5;
	int cols = 4;
	int **p = new int*[rows];
	for (int i = 0; i < rows; i++) {
		p[i] = new int[cols];
	}
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			p[i][j] = 0;
		}
	}
	p[1][3] = 128;
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			cout << p[i][j] << "\t";
		}
		cout << endl;
	}
	// Dealokace
	for (int i = 0; i < rows; i++) {
		delete[] p[i];
	}
	delete[] p;
	p = nullptr;*/

	int rows = 5;
	int cols = 4;
	int **p;

	allocate(p, rows, cols);
	initialize(p, rows, cols);
	print(p, rows, cols);
	deallocate(p, rows);

	system("pause");
	return 0;
}