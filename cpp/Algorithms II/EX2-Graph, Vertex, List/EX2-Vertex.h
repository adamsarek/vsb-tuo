#pragma once

class Vertex {
	private:
		int id;
		int data;

		int neighCap = 10;

		Vertex** neighbours = nullptr;

		Vertex* next = nullptr;
		Vertex* prev = nullptr;
	public:
		//Vertex(int id, int data) { this->id = id; this->data = data; }
		Vertex(int, int);

		void addNeighbour(Vertex*);

		Vertex* getNext(void) { return this->next; }
		Vertex* getPrev(void) { return this->prev; }
		int getId(void) { return this->id; }

		void setNext(Vertex* next) { this->next = next; }
		void setPrev(Vertex* prev) { this->prev = prev; }
		void setId(int id) { this->id = id; }
};