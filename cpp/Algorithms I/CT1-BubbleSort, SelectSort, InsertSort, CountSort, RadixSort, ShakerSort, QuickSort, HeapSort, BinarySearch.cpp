#include <iostream>

using namespace std;

// Vytvoří nové pole
void novePole(int* &pole, int pocet) {
	pole = new int[pocet];
	for (int i = 0; i < pocet; i++) {
		switch (i) {
		case 0: pole[i] = 11; break;
		case 1: pole[i] = 3;  break;
		case 2: pole[i] = 27; break;
		case 3: pole[i] = 8;  break;
		case 4: pole[i] = 50; break;
		case 5: pole[i] = 22; break;
		case 6: pole[i] = 12; break;
		}
	}
}

// Vypíše seřazené pole
void vypis(int* pole, int pocet) {
	for (int i = 0; i < pocet; i++) {
		cout << pole[i] << " ";
	}
	cout << endl;

	delete[] pole;
}

// Bubble Sort (N^2)
// Přehazuje dvojice prvků a nejvyšší číslo vždy probublá na konec pole (dále již není potřeba s koncem porovnávat, protože již obsahuje nejvyšší čísla)
void bubbleSort(int* pole, int pocet) {
	for (int i = 0; i < pocet - 1; i++) {
		bool swapped = false;
		// Poslední prvky jsou již setříděné (proto lze použít pocet-i-1 místo pocet-1)
		for (int j = 0; j < pocet - i - 1; j++) {
			if (pole[j] > pole[j + 1]) {
				swap(pole[j], pole[j + 1]);
				swapped = true;
			}
		}
		// Pole již je setříděné a není potřeba znovu porovnávat prvky
		if (!swapped) {
			break;
		}
	}
}

// Selection Sort (N^2)
// Vybere vždy první prvek nesetříděného pole jako minimum a kontroluje zda ve zbytku nesetříděného pole je menší prvek a pokud ano, prohodí vybraný první prvek s nalezeným minimem
void selectionSort(int* pole, int pocet) {
	for (int i = 0; i < pocet - 1; i++)
	{
		// Najde nejmenší prvek v nesetříděném poli 
		int min_ID = i;
		for (int j = i + 1; j < pocet; j++) {
			if (pole[j] < pole[min_ID]) {
				min_ID = j;
			}
		}

		// Prohodí nalezený nejmenší prvek s prvním prvkem 
		swap(pole[min_ID], pole[i]);
	}
}

// Insertion Sort (N^2)
// Začne druhým prvkem a zpětně kontroluje zda před ním nejsou vyšší prvky, pokud ano, posune tyto prvky dopředu a zařadí se mezi nejvyšší menší prvek a nejmenší vyšší prvek
// Následně pokračuje dalšími prvky pole a vždy kontroluje zpětně, zda něco není vyšší
void insertionSort(int* pole, int pocet) {
	for (int i = 1; i < pocet; i++) {
		// Vybere klíč, pro porovnávání
		int klic = pole[i];

		// Vybere poslední prvek pole před klíčem
		int j = i - 1;

		// Posunujeme prvky (větší než klíč) o jednu pozici vpřed
		while (pole[j] > klic && j >= 0)
		{
			pole[j + 1] = pole[j];
			j--;
		}

		// Správná pozice pro klíč
		pole[j + 1] = klic;
	}
}

// A utility function to get maximum value in arr[] 
int getMax(int arr[], int n)
{
	int mx = arr[0];
	for (int i = 1; i < n; i++)
		if (arr[i] > mx)
			mx = arr[i];
	return mx;
}

// A function to do counting sort of arr[] according to 
// the digit represented by exp. 
// Count Sort (N+k) - N je počet prvků pole, k je délka prvku s nejvíce ciframi
void countSort(int arr[], int n, int exp)
{
	int* output = new int[n]; // output array 
	int i, count[10] = { 0 };

	// Store count of occurrences in count[] 
	for (i = 0; i < n; i++)
		count[(arr[i] / exp) % 10]++;

	// Change count[i] so that count[i] now contains actual 
	//  position of this digit in output[] 
	for (i = 1; i < 10; i++)
		count[i] += count[i - 1];

	// Build the output array 
	for (i = n - 1; i >= 0; i--)
	{
		output[count[(arr[i] / exp) % 10] - 1] = arr[i];
		count[(arr[i] / exp) % 10]--;
	}

	// Copy the output array to arr[], so that arr[] now 
	// contains sorted numbers according to current digit 
	for (i = 0; i < n; i++)
		arr[i] = output[i];
}

// The main function to that sorts arr[] of size n using  
// Radix Sort (N*k) - N je počet prvků pole, k je délka prvku s nejvíce ciframi
// Seřadí pole podle cifer daných čísel zleva
void radixSort(int arr[], int n)
{
	// Find the maximum number to know number of digits 
	int m = getMax(arr, n);

	// Do counting sort for every digit. Note that instead 
	// of passing digit number, exp is passed. exp is 10^i 
	// where i is current digit number 
	for (int exp = 1; m / exp > 0; exp *= 10)
		countSort(arr, n, exp);
}

// Shaker Sort (N^2)
// Nejprve protlačí jeden nejvyšší prvek na konec a pak hned jeden nejnižší prvek na začátek
// Funguje tedy stejně jako Bubble Sort, že číslo probublá, avšak v tomto případě probublává na obě strany
void shakerSort(int* pole, int pocet) {
	bool swapped = true;
	int start = 0;
	int end = pocet - 1;

	while (swapped) {
		// reset the swapped flag on entering 
		// the loop, because it might be true from 
		// a previous iteration. 
		swapped = false;

		// loop from left to right same as 
		// the bubble sort 
		for (int i = start; i < end; ++i) {
			if (pole[i] > pole[i + 1]) {
				swap(pole[i], pole[i + 1]);
				swapped = true;
			}
		}

		// if nothing moved, then array is sorted. 
		if (!swapped)
			break;

		// otherwise, reset the swapped flag so that it 
		// can be used in the next stage 
		swapped = false;

		// move the end point back by one, because 
		// item at the end is in its rightful spot 
		--end;

		// from right to left, doing the 
		// same comparison as in the previous stage 
		for (int i = end - 1; i >= start; --i) {
			if (pole[i] > pole[i + 1]) {
				swap(pole[i], pole[i + 1]);
				swapped = true;
			}
		}

		// increase the starting point, because 
		// the last stage would have moved the next 
		// smallest number to its rightful spot. 
		++start;
	}
}

/* This function takes last element as pivot, places
   the pivot element at its correct position in sorted
	array, and places all smaller (smaller than pivot)
   to left of pivot and all greater elements to right
   of pivot */
int partition(int arr[], int low, int high)
{
	int pivot = arr[high];    // pivot 
	int i = (low - 1);  // Index of smaller element 

	for (int j = low; j <= high - 1; j++)
	{
		// If current element is smaller than or 
		// equal to pivot 
		if (arr[j] <= pivot)
		{
			i++;    // increment index of smaller element 
			swap(arr[i], arr[j]);
		}
	}
	swap(arr[i + 1], arr[high]);
	return (i + 1);
}

/* The main function that implements QuickSort
 arr[] --> Array to be sorted,
  low  --> Starting index,
  high  --> Ending index */
  // Quick Sort (N^2)
  // Vybere střední prvek jako pivot a postupuje zleva a zprava pivota, dokud nenajde vlevo vyšší prvek než pivot a vpravo menší
  // Jakmile je na obou stranách najde, prohodí je mezi sebou a pokračuje dál
  // Poté co jsou všechny menší nebo rovno prvky vlevo pivota a vyšší vpravo, pole rozdělí v místě pivota a provede Quick Sort těchto dvou částí
void quickSort(int arr[], int low, int high)
{
	if (low < high)
	{
		/* pi is partitioning index, arr[p] is now
		   at right place */
		int pi = partition(arr, low, high);

		// Separately sort elements before 
		// partition and after partition 
		quickSort(arr, low, pi - 1);
		quickSort(arr, pi + 1, high);
	}
}

// To heapify a subtree rooted with node i which is 
// an index in arr[]. n is size of heap 
void heapify(int arr[], int n, int i)
{
	int largest = i; // Initialize largest as root 
	int l = 2 * i + 1; // left = 2*i + 1 
	int r = 2 * i + 2; // right = 2*i + 2 

	// If left child is larger than root 
	if (l < n && arr[l] > arr[largest])
		largest = l;

	// If right child is larger than largest so far 
	if (r < n && arr[r] > arr[largest])
		largest = r;

	// If largest is not root 
	if (largest != i)
	{
		swap(arr[i], arr[largest]);

		// Recursively heapify the affected sub-tree 
		heapify(arr, n, largest);
	}
}

// main function to do heap sort 
// Heap Sort (N * log N)
// Stromová struktura:
//								#0
//				#1								#2
//		#3				#4				#5				#6
//	#7		#8		#9		#10		#11		#12		#13		#14
// Nejprve shora dolů kontroluje zda některý ze 2 prvků pod kořenem není větší, pokud ano, prohodí jej s kořenem
// Pokračuje vždy v té polovině, na které byl prohozen prvek větší než kořen
// Jakmile jsou všechny kořeny větší než jejich větve, prohodí se v poli první a poslední prvek
// Poslední prvek je tímto již seřazen a je odebrán z další iterace cyklu, který se nyní opakuje stále dokola, dokud nezbyde pouze jeden prvek (pole je seřazené)
void heapSort(int arr[], int n)
{
	// Build heap (rearrange array) 
	for (int i = n / 2 - 1; i >= 0; i--)
		heapify(arr, n, i);

	// One by one extract an element from heap 
	for (int i = n - 1; i >= 0; i--)
	{
		// Move current root to end 
		swap(arr[0], arr[i]);

		// call max heapify on the reduced heap 
		heapify(arr, i, 0);
	}
}

// Bubble Sort (N^2)
// Selection Sort (N^2)
// Insertion Sort (N^2)
// Count Sort (N+k) - N je počet prvků pole, k je délka prvku s nejvíce ciframi
// Radix Sort (N*k) - N je počet prvků pole, k je délka prvku s nejvíce ciframi
// Shaker Sort (N^2)
// Quick Sort (N^2)
// Heap Sort (N * log N)

bool findElement(int *p, int N, int v) {
	int l = 0;
	int r = N - 1;
	int M;

	while (l <= r) {
		M = (l + r) / 2;

		if (v == p[M]) {
			return true;
		}
		if (v < p[M]) {
			r = M - 1;
		}
		if (v > p[M]) {
			l = M + 1;
		}
	}

	return false;
}

bool findElement_rec(int *p, int v, int l, int r) {
	if (r < l) {
		return false;
	}

	int M = (l + r) / 2;

	if (v == p[M]) {
		return true;
	}
	if (v < p[M]) {
		return findElement_rec(p, v, l, M - 1);
	}
	if (v > p[M]) {
		return findElement_rec(p, v, M + 1, r);
	}
}

int main() {
	const int pocet = 7;
	int* pole;

	cout << "Vstupni pole: ";
	novePole(pole, pocet);
	vypis(pole, pocet);
	cout << endl;

	// Třídění
	novePole(pole, pocet);
	cout << "   Bubble Sort: "; bubbleSort(pole, pocet); vypis(pole, pocet);

	novePole(pole, pocet);
	cout << "Selection Sort: "; selectionSort(pole, pocet); vypis(pole, pocet);

	novePole(pole, pocet);
	cout << "Insertion Sort: "; insertionSort(pole, pocet); vypis(pole, pocet);

	novePole(pole, pocet);
	cout << "    Radix Sort: "; radixSort(pole, pocet); vypis(pole, pocet);

	novePole(pole, pocet);
	cout << "   Shaker Sort: "; shakerSort(pole, pocet); vypis(pole, pocet);

	novePole(pole, pocet);
	cout << "    Quick Sort: "; quickSort(pole, 0, pocet - 1); vypis(pole, pocet);

	novePole(pole, pocet);
	cout << "     Heap Sort: "; heapSort(pole, pocet); vypis(pole, pocet);

	cout << endl;

	// Vyhledávání
	novePole(pole, pocet);
	bubbleSort(pole, pocet);
	int hledat = 11;
	cout << "[SEKVENCNI VYHLEDAVANI] Je v setridenem poli cislo(" << hledat << "): " << (findElement(pole, pocet, hledat) ? "ANO" : "NE") << endl;
	cout << "I: 0, P: 3" << endl;
	cout << "I: 1, P: 8" << endl;
	cout << "I: 2, P: 11" << endl;
	cout << "Nalezen!" << endl;
	cout << "  [BINARNI VYHLEDAVANI] Je v setridenem poli cislo(" << hledat << "): " << (findElement_rec(pole, hledat, 0, pocet - 1) ? "ANO" : "NE") << endl;
	cout << "L: 0, R: 6, M: 3, P: 12" << endl;
	cout << "L: 0, R: 2, M: 1, P: 8" << endl;
	cout << "L: 2, R: 2, M: 2, P: 11" << endl;
	cout << "Nalezen!" << endl;

	system("pause");
	return 0;
}