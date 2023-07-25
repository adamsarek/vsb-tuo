#include "Graph.h"

void Graph::insert(Vertex* vertex) {
	this->vertices.insertItem(vertex);
}

void Graph::insert(Vertex* vertex, int* neighbours, int count) {
	this->vertices.insertItem(vertex);

	for (int i = 0; i < count; i++) {
		Vertex* neighbour = this->vertices.findItem(neighbours[i]);
		
		if (neighbour != nullptr) {
			vertex->addNeighbour(neighbour);

			if (vertex != neighbour) {
				neighbour->addNeighbour(vertex);
			}
		}
	}
}