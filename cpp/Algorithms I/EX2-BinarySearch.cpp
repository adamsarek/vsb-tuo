#include <iostream>
#include <ctime>

using namespace std;

int main() {
	// Složitost algoritmu O(N^2)
	/*// time(0) - počet sekund od 1.1.1970
	srand(time(0));	// Zajistí náhodné hodnoty v rand(), "seed rand", REALTIME TEST!!!

	int N = 7;
	int *p = new int[N];

	for (int i = 0; i < N; i++) {
		p[i] = rand() % 10;

		for (int j = 0; j < i; j++) {
			if (p[j] == p[i]) {
				i--;
				break;
			}
		}
	}

	for (int i = 0; i < N; i++) {
		cout << p[i] << "\t";
	}
	cout << endl;

	delete[] p;
	p = nullptr;*/

	// Složitost algoritmu O(N)
	/*srand(time(0));

	int N = 7;	// N musí být menší než R -> jinak program nebude konečný
	int R = 10;
	int *p = new int[N];

	for (int i = 0; i < R; i++) {
		used[i] = false;
	}

	for (int i = 0; i < N; i++) {
		int n = rand() % R;

		if (!used[n]) {
			p[i] = n;
			used[n] = true;
		}
		else {
			i--;
		}
	}

	for (int i = 0; i < N; i++) {
		cout << p[i] << "\t";
	}
	cout << endl;

	// Sekvenční (lineární) vyhledávání v poli
	{	// <-- Odstraní proměnnou i pro zbytek programu
		int v = 8;
		int i;
		for (i = 0; i < N; i++) {
			if (p[i] == v) {
				break;
			}
		}
		if (i < N) { cout << "FOUND IT!" << endl; }
		else { cout << "NOT FOUND!" << endl; }
	}

	delete[] p;
	p = nullptr;*/

	// Vyhledávání v poli O(log2 N)
	srand(time(0));

	int N = 7;	// N musí být menší než R -> jinak program nebude konečný
	int R = 10;
	int *p = new int[N];
	bool *used = new bool[R];

	for (int i = 0; i < R; i++) {
		used[i] = false;
	}

	for (int i = 0; i < N; i++) {
		p[i] = i * i;
	}

	for (int i = 0; i < N; i++) {
		cout << p[i] << "\t";
	}
	cout << endl;
	
	int v = 8;	// Co hledám
	int l = 0;
	int r = N - 1;
	int M;
	// Cyklus bude dělit pole na poloviny, poloviny polovin atd. dokud nenajde číslo proměnné v nebo nedojde na konec pole (používá vždy pouze smysluplnou polovinu pole)
	while (l <= r) {
		M = (l + r) / 2;	// Získá střed rozdělené poloviny

		if (p[M] == v) {
			cout << "FOUND IT!" << endl;
			break;
		}

		if (v < p[M]) {r = M - 1;}	// Použije pouze levou část rozdělené poloviny
		if (v > p[M]) {l = M + 1;}	// Použije pouze pravou část rozdělené poloviny
	}


	delete[] p;
	p = nullptr;

	delete[] used;
	used = nullptr;

	system("pause");
	return 0;
}