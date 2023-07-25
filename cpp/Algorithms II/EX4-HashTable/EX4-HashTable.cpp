#include "HashTable.h"
#include <iostream>
#include <algorithm>

unsigned int HashTable::hash(std::string str) {
	return (str.length() % this->capacity);
}

void HashTable::insert(std::string str) {
	this->table[this->hash(str)].emplace_back(str);
}

void HashTable::print(void) {
	for (unsigned int i = 0; i < this->capacity; i++) {
		std::cout << i << ": ";
		// Funguje jako foreach
		for (const auto &s : this->table[i]) {
			std::cout << s << " -> ";
		}
		std::cout << std::endl;
	}
}

bool HashTable::contains(std::string str) {
	/*for (unsigned int i = 0; i < this->capacity; i++) {
		if (std::find(this->table[i].begin(), this->table[i].end(), str) != this->table[i].end()) {
			return true;
		}
	}
	return false;*/
	unsigned int index = this->hash(str);

	for (const auto& s : this->table[index]) {
		if (s == str) {
			return true;
		}
	}
	return false;
}