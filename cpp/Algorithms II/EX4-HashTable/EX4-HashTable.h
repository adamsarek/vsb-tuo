#pragma once

#include <string>
#include <vector>

class HashTable {
	private:
		unsigned int capacity = 10;
		std::vector<std::string> *table = nullptr;

		unsigned int hash(std::string);
	public:
		HashTable(void) { this->table = new std::vector<std::string>[this->capacity]; }
		~HashTable(void) { delete[] this->table; }

		void insert(std::string);
		void print(void);
		bool contains(std::string);
};