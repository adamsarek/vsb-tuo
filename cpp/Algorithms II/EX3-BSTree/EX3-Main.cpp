#include <iostream>
#include "BSTree.h"

using namespace std;

int main(void)
{
	BSTree tree;//binary search tree

	tree.insert(8);
	tree.insert(0);
	tree.insert(42);
	tree.insert(6);
	tree.insert(32);
	tree.insert(69);
	tree.insert(40);
	tree.insert(4);
	tree.insert(2);
	tree.insert(5);

	tree.print(true);//vzestupne
	cout << endl;

	tree.print(false);//sestupne
	cout << endl;

	cout << tree.count() << endl;//pocet uzlu

	cout << tree.contains(69) << endl;
	cout << tree.contains(-69) << endl;

	system("pause");
	return(0);
}