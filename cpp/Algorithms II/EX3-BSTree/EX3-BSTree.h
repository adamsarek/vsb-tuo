#pragma once

class BSTree
{
private:
	class Node//vnorena trida, integrovana do bstree
	{
		//private://pristup ma pouze Node, ne i BSTree
	public://pristup ma Node a BSTree
		int data;
		Node* left = nullptr;
		Node* right = nullptr;

		Node(int);
	};

	Node* root = nullptr;

	void insert(int, Node*&);
	void print(bool, Node*);
	int count(Node*);
	bool contains(int, Node*);//pretizena rekurzivni varianta
public:
	void insert(int);//pretizeni
	void print(bool);
	int count(void);
	bool contains(int);
};