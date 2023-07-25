#ifndef _LIST_H_
#define _LIST_H_

#include "Vertex.h"

/* List class */

class List
{
private:
	Vertex* head;

	void internalInsertItem(Vertex* item, Vertex* lastItem);

public:
	List();
	List(Vertex* head);

	Vertex* getNextItem(Vertex* item);
	Vertex* getPrev();	
	void insertItem(Vertex* item);
	void deleteItem(int value);
	Vertex* findItem(int id);

	/* NEW */
	Vertex* getHead(void) { return(this->head); };
	void eraseList(void);
};

#endif