#include <iostream>
#include "HashTable.h"

int main(void) {
	HashTable ht;

	ht.insert("ahoj");
	ht.insert("cau");
	ht.insert("cus");
	ht.insert("dzus");
	ht.insert("zdar");
	ht.insert("cest");
	ht.insert("nejkulatoulinkatejsiho");
	ht.insert("pisemka v pondeli");

	ht.print();

	std::cout << "Contains: " << ht.contains("zdar") << std::endl;
	
	system("pause");
	return 0;
}