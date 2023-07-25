#include <iostream>
#include "Graph.h"
#include "Vertex.h"

using namespace std;

int main(void) {
	Graph g;

	Vertex* v1 = new Vertex(1, 1);
	Vertex* v2 = new Vertex(2, 2);
	Vertex* v3 = new Vertex(3, 3);
	Vertex* v4 = new Vertex(4, 4);
	Vertex* v5 = new Vertex(5, 5);

	g.insert(v1);

	int neighbours[4];
	neighbours[0] = 1;
	g.insert(v2, neighbours, 1);

	neighbours[0] = 2;
	g.insert(v3, neighbours, 1);

	g.insert(v4, neighbours, 1);

	neighbours[1] = 4;
	g.insert(v5, neighbours, 2);

	system("pause");
	return 0;
}