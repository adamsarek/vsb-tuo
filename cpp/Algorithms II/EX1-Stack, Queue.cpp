#include <iostream>
#include <stack>
#include <queue>

using namespace std;

const int capacity = 3;
int cnt = 0;

// Zásobník (stack)
void push(int stack[], int &top, const int value) {
	if (top >= capacity - 1) {
		cout << "Stack is FULL!" << endl;
		return;
	}

	stack[++top] = value;
}

int pop(int stack[], int &top) {
	if (top <= -1) {
		cout << "Stack is EMPTY!" << endl;
		return INT_MAX;	// Maximální číslo v int, určené jako nepravděpodobné na použití
	}

	return(stack[top--]);
}

// Fronta (queue)
/*void put(int queue[], int &tail, const int value) {
	if (tail >= capacity - 1) {
		cout << "Queue is FULL!" << endl;
		return;
	}

	queue[++tail] = value;
}

int get(int queue[], int &head, const int tail) {
	if (head >= tail) {
		cout << "Queue is EMPTY!" << endl;
		return INT_MAX;	// Maximální číslo v int, určené jako nepravděpodobné na použití
	}

	return(queue[++head]);
}*/

void put(int queue[], int &tail, const int value) {
	if (cnt >= capacity) {
		cout << "Queue is FULL!" << endl;
		return;
	}

	++tail;
	if (tail >= capacity) {
		tail = 0;
	}

	queue[tail] = value;
	cnt++;
}

int get(int queue[], int &head, const int tail) {
	if (cnt <= 0) {
		cout << "Queue is EMPTY!" << endl;
		return INT_MAX;	// Maximální číslo v int, určené jako nepravděpodobné na použití
	}

	++head;
	if (head >= capacity) {
		head = 0;
	}

	cnt--;
	return(queue[head]);
}

int main(void) {
	// Zásobník (stack)
	int stack[capacity];
	int top = -1;

	push(stack, top, 1);
	push(stack, top, 2);
	push(stack, top, 3);
	push(stack, top, 4);

	cout << pop(stack, top) << endl;
	cout << pop(stack, top) << endl;
	cout << pop(stack, top) << endl;
	cout << pop(stack, top) << endl;

	cout << "-------------------------------------------" << endl;

	// Fronta (queue)
	int queue[capacity];
	int head = -1, tail = -1;

	put(queue, tail, 1);
	put(queue, tail, 2);
	put(queue, tail, 3);
	put(queue, tail, 4);

	cout << get(queue, head, tail) << endl;
	cout << get(queue, head, tail) << endl;
	cout << get(queue, head, tail) << endl;
	cout << get(queue, head, tail) << endl;

	cout << "-------------------------------------------" << endl;

	std::stack<int> st;

	st.push(5);
	st.push(7);
	st.push(9);

	cout << st.top() << endl;
	st.pop();
	cout << st.top() << endl;
	st.pop();
	cout << st.top() << endl;
	st.pop();

	cout << "-------------------------------------------" << endl;

	std::queue<int> que;

	que.push(5);
	que.push(7);
	que.push(9);

	cout << que.front() << endl;
	que.pop();
	cout << que.front() << endl;
	que.pop();
	cout << que.front() << endl;
	que.pop();

	system("pause");
	return 0;
}