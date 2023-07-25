#pragma once

#include "List.h"

class Graph {
	private:
		List vertices;
	public:
		void insert(Vertex*);
		void insert(Vertex*, int*, int);
};