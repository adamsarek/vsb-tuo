#include <iostream>
#include "BSTree.h"

BSTree::Node::Node(int data)//vnorena trida
{
	this->data = data;
}

void BSTree::insert(int data)//pretizena metoda
{
	this->insert(data, this->root);
}

void BSTree::insert(int data, Node*& root)
{
	if (root == nullptr)
	{
		root = new Node(data);//do rootu ukladam nove vygenerovany Node
		return;
	}

	if (data < root->data)
	{
		//jdu doleva
		this->insert(data, root->left);
	}

	if (data > root->data)
	{
		//jdu doprava
		this->insert(data, root->right);
	}
}

void BSTree::print(bool asc)
{
	this->print(asc, this->root);
}

void BSTree::print(bool asc, Node* root)
{
	if (root == nullptr)
	{
		return;
	}

	this->print(asc, asc ? root->left : root->right);
	std::cout << root->data << std::endl;
	this->print(asc, asc ? root->right : root->left);
}

int BSTree::count(void)
{
	return(this->count(this->root));
}

int BSTree::count(Node * root)
{
	if (root == nullptr)
	{
		return(0);
	}

	return(this->count(root->left) + this->count(root->right) + 1);
	//du - tohle cyklicky (dobrovolne, asi se ani nekontroluje)
}

bool BSTree::contains(int data)
{
	return(this->contains(data, this->root));
}

bool BSTree::contains(int data, Node * root)
{
	if (root == nullptr)
	{
		return(false);
	}

	if (root->data == data)
	{
		return(true);
	}

	if (data < root->data)
	{
		return(this->contains(data, root->left));
	}

	return(this->contains(data, root->right));
}