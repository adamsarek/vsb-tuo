#include <iostream>
#include <conio.h>
#include <windows.h>
#include "List.h"

using namespace ADSLibrary::DataStructures::LinkedStructures::Procedural;

// Create list of n-items
void CreateList(List& list, int n) {
	for (int i = 1; i <= n; i++) {
		Append(list, to_string(i));
	}
}

// Exclude every m-th item from the list
string ExcludeFromList(List& list, int m, ListItem*& pos) {
	// Find item for exclusion
	ListItem* item = pos;
	for (int i = 0; i < m; i++) {
		item = (item == list.Tail ? list.Head : item->Next);
	}

	// Save default position for next iteration
	pos = (item->Prev == nullptr ? list.Tail : item->Prev);

	// Exclude item
	string excludedNumber = item->Value;
	Remove(list, item);

	// Return excluded number
	return excludedNumber;
}

int main(void) {
	int n = 9, m = 5;

	List list;
	Init(list);

	CreateList(list, n);

	// Default position
	ListItem* pos = list.Tail;

	// Exclude items until the list is empty & display all excluded items
	for (int i = 0; i < n; i++) {
		Report(list);
		cout << (i < n - 1 ? "Byl vyrazen muz cislo: " : "Novy nacelnik je muz cislo: ") << ExcludeFromList(list, m, pos) << endl;
	}

	system("pause");
	return 0;
}