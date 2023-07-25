#pragma once

#include <iostream>
#include <string>

using namespace std;

namespace ADSLibrary
{
	namespace DataStructures
	{
		namespace LinkedStructures
		{
			namespace Procedural
			{
				struct ListItem
				{
					string Value;
					ListItem* Prev;
					ListItem* Next;
				};

				struct List
				{
					ListItem* Head;
					ListItem* Tail;
				};

				void Init(List& L);
				void Init(List& L, const string& InitValue);
				void Init(List& L, const string InitValues[], const int N);
				void Init(List& L, const List& Other);

				void Prepend(List& L, const string& NewValue);
				void Append(List& L, const string& NewValue);
				void InsertBefore(List& L, ListItem* CurrentItem, const string& NewValue);
				void InsertAfter(List& L, ListItem* CurrentItem, const string& NewValue);

				void RemoveHead(List& L);
				void RemoveTail(List& L);
				void RemoveFirst(List& L, const string& ValueToRemove);
				void RemoveLast(List& L, const string& ValueToRemove);
				void RemoveAll(List& L, const string& ValueToRemove);
				void Remove(List& L, const ListItem* ItemToRemove);
				void Clear(List& L);

				ListItem* Search(const List& L, const string& Value);
				ListItem* ReverseSearch(const List& L, const string& Value);

				bool Contains(const List& L, const string& Value);

				bool IsEmpty(const List& L);
				int Count(const List& L);

				void Report(const List& L);
				void ReportStructure(const List& L);

				void InternalCreateSingleElementList(List& L, const string& NewValue);
				void InternalRemove(List& L, const ListItem* ItemToDelete);
			}
		}
	}
}