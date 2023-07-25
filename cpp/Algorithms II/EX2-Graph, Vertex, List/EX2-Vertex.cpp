#include <iostream>
#include "Vertex.h"

Vertex::Vertex(int id, int data) {
	this->id = id;
	this->data = data;

	this->neighbours = new Vertex*[this->neighCap];
	for (int i = 0; i < this->neighCap; i++) {
		this->neighbours[i] = nullptr;
	}
}


void Vertex::addNeighbour(Vertex* neighbour) {
	int i = 0;

	while (this->neighbours[i] != nullptr && i < this->neighCap) {
		i++;
	}

	if (i >= this->neighCap) {
		std::cout << "No more room for neighbours!" << std::endl;
		return;
	}

	this->neighbours[i] = neighbour;
}
