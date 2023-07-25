/* List definitions */
#include "List.h"
#include <stdio.h>

List::List()
{
	this->head = NULL;
}

List::List(Vertex* item)
{
	this->head = head;
}

void List::insertItem(Vertex* item)
{	
	if (this->head == NULL)
	{
		this->head = item;
		return;
	}

	internalInsertItem(item, this->head);
}

void List::internalInsertItem(Vertex* item, Vertex* lastItem)
{
	if (lastItem == NULL)
	{
		lastItem = item;
		return;
	}

	if (lastItem->getNext() == NULL)
	{
		lastItem->setNext(item);
		item->setPrev(lastItem);
		return;
	}

	internalInsertItem(item, lastItem->getNext());
	return;
}

void List::deleteItem(int value)
{
	Vertex* item = findItem(value);

	if (item == NULL)
		return;

	if (item->getPrev() != NULL)
	{
		item->getPrev()->setNext(item->getNext());		
	}

	if (item->getNext() != NULL)
		item->getNext()->setPrev(item->getPrev());

	if (item == this->head)
		this->head = item->getNext();
	
	delete(item);

	return;
}

/* NEW */
void List::eraseList(void)
{
	if (this->head == NULL)
		return;

	Vertex* current = this->head;	
	Vertex* next;

	while(current != NULL)
	{
		next = current->getNext();
		delete(current);
		current = next;
	}

	this->head = NULL;
}

Vertex* List::findItem(int id)
{
	Vertex* lastItem = this->head;	

	while(lastItem != NULL)
	{
		if (lastItem->getId() != id)
		{
			lastItem = lastItem->getNext();
		}
		else
			break;
	}
	
	return(lastItem);	
}

Vertex* List::getNextItem(Vertex* item)
{
	return(item->getNext());
}