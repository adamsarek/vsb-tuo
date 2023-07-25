#include <iostream>

using namespace std;

void printBoard(int** board, int rows, int cols) {
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			cout << board[i][j] << "\t";
		}
		cout << endl;
	}
	cout << "----------------------------------------------------------" << endl;
}

void setKnightPos(int** board, int x, int y, int move) {
	board[x][y] = move;
}

bool isMoveValid(int** board, int rows, int cols, int x, int y) {
	if (x >= 0 && x < rows && y >= 0 && y <= cols && board[x][y] == 0) {
		return true;
	}
	return false;
}

bool runIt(int** board, int rows, int cols, int x, int y, int movesX[], int movesY[], int move) {
	if (move >= rows*cols+1) {
		return true;
	}
	for (int i = 0; i < 8; i++) {
		int newX = x + movesX[i];
		int newY = y + movesY[i];

		if (isMoveValid(board,rows,cols,newX,newY)) {
			setKnightPos(board,newX,newY,move);
			if (runIt(board, rows, cols, newX, newY, movesX, movesY, move + 1)) {
				return true;
			}
			setKnightPos(board,newX,newY,0);
		}
	}

	return false;
}

int main() {
	int rows = 8;
	int cols = 8;

	int startX = 0;
	int startY = 0;

	int movesX[] = {-2,-1,1,2,2,1,-1,-2};
	int movesY[] = {-1,-2,-2,-1,1,2,2,1};

	// Alokace
	int** board = new int*[rows];
	for (int i = 0; i < rows; i++) {
		board[i] = new int[cols];
	}
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			board[i][j] = 0;
		}
	}

	setKnightPos(board,startX,startY,1);
	printBoard(board,rows,cols);

	runIt(board,rows,cols,startX,startY,movesX,movesY,2);
	
	printBoard(board, rows, cols);

	// Dealokace
	for (int i = 0; i < rows; i++) {
		delete[] board[i];
	}
	delete[] board;
	board = nullptr;

	system("pause");
	return 0;
}