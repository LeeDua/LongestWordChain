// A C++ program to find single source longest distances in a DAG
#include "pch.h"
#include <iostream>
#include <list>
#include <stack>
#include <limits.h>
#define NINF INT_MIN
using namespace std;

//ͼͨ���ڽӱ���������5���Լ��ߵ�Ȩֵ��

//�������ڽӱ�������Ľ�㣬û�ж����������Ǻ�˭��������Ϊ���ֶ���ӵ� ����-mark 1
class AdjListNode
{
	int v;
	int weight;
public:
	AdjListNode(int _v, int _w) { v = _v;  weight = _w; }
	int getV() { return v; }
	int getWeight() { return weight; }
};

// Class to represent a graph using adjacency list representation
class Graph
{
	int V;    // No. of vertices��

	// Pointer to an array containing adjacency lists
	//�ڽ�����ÿ��Ԫ�ش���ǽ��u�� �ٽ������� ��ָ��
	list<AdjListNode> *adj;


	// A function used by longestPath
	void topologicalSortUtil(int v, bool visited[], stack<int> &Stack);
public:
	Graph(int V);   // Constructor

	// function to add an edge to graph
	void addEdge(int u, int v, int weight);

	// Finds longest distances from given source vertex
	void longestPath(int s);
};

Graph::Graph(int V) // Constructor
{
	this->V = V;
	//�ж��ٽ���������ɶ����ڽ�����
	adj = new list<AdjListNode>[V];

}

void Graph::addEdge(int u, int v, int weight)
{
	AdjListNode node(v, weight);
    //mark 1: �ڼ���ߵ�ʱ����u������������� v���
	adj[u].push_back(node); // Add v to u��s list
}

// ͨ���ݹ������������. ��ϸ�������ɲο���������ӡ�
// http://www.geeksforgeeks.org/topological-sorting/

void Graph::topologicalSortUtil(int v, bool visited[], stack<int> &Stack)
{
	// ��ǵ�ǰ����Ϊ�ѷ���
	visited[v] = true;

	// �������ڽӵ�ִ�еݹ����
	list<AdjListNode>::iterator i;
	for (i = adj[v].begin(); i != adj[v].end(); ++i)
	{
		AdjListNode node = *i;
		if (!visited[node.getV()])
			topologicalSortUtil(node.getV(), visited, Stack);
	}

	// ��ĳ����û���ڽӵ�ʱ���ݹ���������õ����ջ�С�
	Stack.push(v);
}
// ���ݴ���Ķ��㣬���������������·��. longestPathʹ����
// topologicalSortUtil() ������ö����������
void Graph::longestPath(int s)
{
	stack<int> Stack;
	
	int *dist = new int[V];

	// ������еĶ���Ϊδ����
	bool *visited = new bool[V];
	for (int i = 0; i < V; i++)
		visited[i] = false;

	// ��ÿ���������topologicalSortUtil���������ͼ���������д��뵽Stack�С�
	for (int i = 0; i < V; i++)
		if (visited[i] == false)
			topologicalSortUtil(i, visited, Stack);
	//�����������ڶ�ջ�У����������Ҫ�������е���ǰ��

	//��ʼ�������ж���ľ���Ϊ������
	//��Դ��ľ���Ϊ0
	//�Ƽ��� https://blog.csdn.net/u010519432/article/details/26751867 �ֶ���һ����¹���
	for (int i = 0; i < V; i++)
		dist[i] = NINF;
	dist[s] = 0;

	// �������������еĵ�
	while (Stack.empty() == false)
	{
		//ȡ�����������еĵ�һ����
		int u = Stack.top();
		Stack.pop();

		// ���µ������ڽӵ�ľ��룬��̬�滮����
		
		
		list<AdjListNode>::iterator i;

		if (dist[u] != NINF)
		{
			//c++���������൱�ھ���for int i = 0 ,i < adj.count ; i++
			//���ڱ������u���ڽӽ������������н��
			//������i���ʵ�ǰ����getv getweight
			for (i = adj[u].begin(); i != adj[u].end(); ++i)
				if (dist[i->getV()] < dist[u] + i->getWeight())
					dist[i->getV()] = dist[u] + i->getWeight();
		}
	}

	// ��ӡ�·��
	for (int i = 0; i < V; i++) {
		if (dist[i] == NINF) 
		{
			cout << "INF ";
		}
		else 
		{
			cout << dist[i];
		}
	}
	

}

// Driver program to test above functions
/*int main()
{
	// Create a graph given in the above diagram.  Here vertex numbers are
	// 0, 1, 2, 3, 4, 5 with following mappings:
	// 0=r, 1=s, 2=t, 3=x, 4=y, 5=z
	Graph g(6);
	g.addEdge(0, 1, 5);
	g.addEdge(0, 2, 3);
	g.addEdge(1, 3, 6);
	g.addEdge(1, 2, 2);
	g.addEdge(2, 4, 4);
	g.addEdge(2, 5, 2);
	g.addEdge(2, 3, 7);
	g.addEdge(3, 5, 1);
	g.addEdge(3, 4, -1);
	g.addEdge(4, 5, -2);

	int s = 1;
	cout << "Following are longest distances from source vertex " << s << " \n";
	g.longestPath(s);

	return 0;
}
*/