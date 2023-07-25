#include <iostream>
#include <ctime>

using namespace std;

void print(bool** matrix, unsigned int rows, unsigned int cols) {
	for (unsigned int i = 0; i < rows; i++) {
		for (unsigned int j = 0; j < cols; j++) {
			cout << (matrix[i][j] ? "X" : "O") << "\t";
		}
		cout << endl;
	}
}

// Zbytecne slozite??
void invertRowCol(bool** matrix, unsigned int rows, unsigned int cols, unsigned int rowIndex, unsigned int colIndex) {
	// Moje reseni
	/*matrix[rowIndex][colIndex] = (matrix[rowIndex][colIndex] ? false : true);
	for (unsigned int i = 0; i < cols; i++) {
		if (i == colIndex) { continue; }
		matrix[rowIndex][i] = (matrix[rowIndex][i] ? false : true);
	}
	for (unsigned int i = 0; i < rows; i++) {
		if (i == rowIndex) { continue; }
		matrix[i][colIndex] = (matrix[i][colIndex] ? false : true);
	}*/
	// Reseni ucitele
	for (int i = 0; i < rows; i++) {
		matrix[i][colIndex] = !matrix[i][colIndex];
	}
	for (int i = 0; i < cols; i++) {
		matrix[rowIndex][i] = !matrix[rowIndex][i];
	}
	matrix[rowIndex][colIndex] = !matrix[rowIndex][colIndex];
}

bool isAllTrue(bool** matrix, unsigned int rows, unsigned int cols) {
	for (unsigned int i = 0; i < rows; i++) {
		for (unsigned int j = 0; j < cols; j++) {
			if (!matrix[i][j]) {
				return false;
			}
		}
	}
	return true;
}

int main() {
	srand((unsigned int)time(nullptr));

	const unsigned int rows = 2;
	const unsigned int cols = 2;
	if (rows > 0 && cols > 0) {
		int maxIterations;
		cout << "Zadejte max. pocet iteraci: ";
		cin >> maxIterations;
		if (cin.good() && maxIterations > 0) {
			// Alokace pole
			bool** matrix = new bool*[rows];
			for (unsigned int i = 0; i < rows; i++) {
				matrix[i] = new bool[cols];
				for (unsigned int j = 0; j < rows; j++) {
					matrix[i][j] = (rand() % 2 == 1 ? true : false);
				}
			}

			// Zmena hodnot v radcich a sloupcich
			// Moje reseni
			/*int i = 0;
			bool allTrue = isAllTrue(matrix, rows, cols);
			while (allTrue == false && i < maxIterations) {
				invertRowCol(matrix, rows, cols, (rand() % rows), (rand() % cols));
				print(matrix, rows, cols);
				allTrue = isAllTrue(matrix, rows, cols);
				i++;
				system("pause");
			}
			if (allTrue == true) { cout << "Vsechny cleny matice byly TRUE!" << endl; }
			else { cout << "Bylo dosazeno max. poctu iteraci!" << endl; }*/
			// Reseni ucitele
			for (int i = 0; i < maxIterations && !isAllTrue(matrix, rows, cols); i++) {
				int r = rand() % rows;
				int c = rand() % cols;

				invertRowCol(matrix,rows,cols,r,c);
				print(matrix,rows,cols);
				system("pause");
			}

			// Dealokace pole
			for (unsigned int i = 0; i < rows; i++) {
				delete[] matrix[i];
			}
			delete[] matrix;
			matrix = nullptr;
		}
		else {
			cout << "Zadan neplatny max. pocet iteraci!" << endl;
		}
	}
	else {
		cout << "Zadan neplatny pocet radku nebo sloupcu!" << endl;
	}

	system("pause");
	return 0;
}