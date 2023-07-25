#include "List.h"

namespace ADSLibrary
{
	namespace DataStructures
	{
		namespace LinkedStructures
		{
			namespace Procedural
			{
				void Init(List& L)
				{
					L.Head = nullptr;
					L.Tail = nullptr;
				}

				void Init(List& L, const string& InitValue)
				{
					InternalCreateSingleElementList(L, InitValue);
				}

				void Init(List& L, const string InitValues[], const int N)
				{
					Init(L);
					for (int i = 0; i < N; i++)
					{
						Append(L, InitValues[i]);
					}
				}

				void Init(List& L, const List& Other)
				{
					Init(L);
					for (ListItem* p = Other.Head; p != nullptr; p = p->Next)
					{
						Append(L, p->Value);
					}
				}

				void Prepend(List& L, const string& NewValue)
				{
					if (IsEmpty(L))
					{
						InternalCreateSingleElementList(L, NewValue);
					}
					else
					{
						ListItem* NewItem = new ListItem;
						NewItem->Value = NewValue;
						NewItem->Prev = nullptr;
						NewItem->Next = L.Head;
						L.Head->Prev = NewItem;
						L.Head = NewItem;
					}
				}

				void Append(List& L, const string& NewValue)
				{
					if (IsEmpty(L))
					{
						InternalCreateSingleElementList(L, NewValue);
					}
					else
					{
						ListItem* NewItem = new ListItem;
						NewItem->Value = NewValue;
						NewItem->Next = nullptr;
						L.Tail->Next = NewItem;
						NewItem->Prev = L.Tail;
						L.Tail = NewItem;
					}
				}

				void InsertBefore(List& L, ListItem* CurrentItem, const string& NewValue)
				{
					if (CurrentItem != nullptr)
					{
						if (CurrentItem == L.Head)
						{
							Prepend(L, NewValue);
						}
						else
						{
							ListItem* NewItem = new ListItem;
							NewItem->Value = NewValue;
							ListItem* P = CurrentItem->Prev;
							P->Next = NewItem;
							NewItem->Prev = P;
							CurrentItem->Prev = NewItem;
							NewItem->Next = CurrentItem;
						}
					}
				}

				void InsertAfter(List& L, ListItem* CurrentItem, const string& NewValue)
				{
					if (CurrentItem != nullptr)
					{
						if (CurrentItem == L.Tail)
						{
							Append(L, NewValue);
						}
						else
						{
							ListItem* NewItem = new ListItem;
							NewItem->Value = NewValue;
							ListItem* N = CurrentItem->Next;
							N->Prev = NewItem;
							NewItem->Next = N;
							CurrentItem->Next = NewItem;
							NewItem->Prev = CurrentItem;
						}
					}
				}

				void RemoveHead(List& L)
				{
					if (!IsEmpty(L))
					{
						InternalRemove(L, L.Head);
					}
				}

				void RemoveTail(List& L)
				{
					if (!IsEmpty(L))
					{
						InternalRemove(L, L.Tail);
					}
				}

				void RemoveFirst(List& L, const string& ValueToRemove)
				{
					ListItem* p = Search(L, ValueToRemove);
					if (p != nullptr)
					{
						InternalRemove(L, p);
					}
				}

				void RemoveLast(List& L, const string& ValueToRemove)
				{
					ListItem* p = ReverseSearch(L, ValueToRemove);
					if (p != nullptr)
					{
						InternalRemove(L, p);
					}
				}

				void RemoveAll(List& L, const string& ValueToRemove)
				{
					ListItem* p;
					while ((p = Search(L, ValueToRemove)) != nullptr)
					{
						InternalRemove(L, p);
					}
				}

				void Remove(List& L, const ListItem* ItemToRemove)
				{
					if (ItemToRemove != nullptr)
					{
						InternalRemove(L, ItemToRemove);
					}
				}

				void Clear(List& L)
				{
					while (!IsEmpty(L))
					{
						InternalRemove(L, L.Head);
					}
				}

				ListItem* Search(const List& L, const string& Value)
				{
					for (ListItem* p = L.Head; p != nullptr; p = p->Next)
					{
						if (p->Value == Value)
						{
							return p;
						}
					}
					return nullptr;
				}

				ListItem* ReverseSearch(const List& L, const string& Value)
				{
					for (ListItem* p = L.Tail; p != nullptr; p = p->Prev)
					{
						if (p->Value == Value)
						{
							return p;
						}
					}
					return nullptr;
				}

				bool Contains(const List& L, const string& Value)
				{
					return Search(L, Value) != nullptr;
				}

				bool IsEmpty(const List& L)
				{
					return L.Head == nullptr;
				}

				int Count(const List& L)
				{
					int counter = 0;
					for (ListItem* p = L.Head; p != nullptr; p = p->Next)
					{
						counter += 1;
					}
					return counter;
				}

				void Report(const List& L)
				{
					for (ListItem* p = L.Head; p != nullptr; p = p->Next)
					{
						cout << p->Value << "\t";
					}
					cout << endl;
				}

				void ReportStructure(const List& L)
				{
					cout << "L.Head: " << L.Head << endl;
					cout << "L.Tail: " << L.Tail << endl;
					for (ListItem* p = L.Head; p != nullptr; p = p->Next)
					{
						cout << "Item address: " << p << endl;
						cout << "Value: " << p->Value << endl;
						cout << "Prev: " << p->Prev << endl;
						cout << "Next: " << p->Next << endl;
						cout << endl;
					}
				}

				void InternalCreateSingleElementList(List& L, const string& NewValue)
				{
					L.Head = L.Tail = new ListItem;
					L.Head->Value = NewValue;
					L.Head->Prev = nullptr;
					L.Head->Next = nullptr;
				}

				void InternalRemove(List& L, const ListItem* ItemToDelete)
				{
					if (ItemToDelete == L.Head && ItemToDelete == L.Tail)
					{
						L.Head = nullptr;
						L.Tail = nullptr;
					}
					else
					{
						if (ItemToDelete == L.Head)
						{
							L.Head = L.Head->Next;
							L.Head->Prev = nullptr;
						}
						else
						{
							if (ItemToDelete == L.Tail)
							{
								L.Tail = L.Tail->Prev;
								L.Tail->Next = nullptr;
							}
							else
							{
								ListItem* P = ItemToDelete->Prev;
								ListItem* N = ItemToDelete->Next;
								P->Next = N;
								N->Prev = P;
							}
						}
					}
					delete ItemToDelete;
				}
			}
		}
	}
}
