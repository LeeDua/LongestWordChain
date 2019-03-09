// A C++ program to find single source longest distances in a DAG
#include "pch.h"
#include <iostream>
#include <list>
#include <stack>
#include <limits.h>
#define NINF INT_MIN
using namespace std;

//图通过邻接表来描述。5，以及边的权值。

//定义了邻接表链表里的结点，没有定义这个结点是和谁连的是因为它手动添加的 搜索-mark 1
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
	int V;    // No. of vertices’

	// Pointer to an array containing adjacency lists
	//邻接链表，每个元素存的是结点u的 临界结点链表 的指针
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
	//有多少结点数，生成多大的邻接链表
	adj = new list<AdjListNode>[V];

}

void Graph::addEdge(int u, int v, int weight)
{
	AdjListNode node(v, weight);
    //mark 1: 在加入边的时候在u结点的链表里加入 v结点
	adj[u].push_back(node); // Add v to u’s list
}

// 通过递归求出拓扑序列. 详细描述，可参考下面的链接。
// http://www.geeksforgeeks.org/topological-sorting/

void Graph::topologicalSortUtil(int v, bool visited[], stack<int> &Stack)
{
	// 标记当前顶点为已访问
	visited[v] = true;

	// 对所有邻接点执行递归调用
	list<AdjListNode>::iterator i;
	for (i = adj[v].begin(); i != adj[v].end(); ++i)
	{
		AdjListNode node = *i;
		if (!visited[node.getV()])
			topologicalSortUtil(node.getV(), visited, Stack);
	}

	// 当某个点没有邻接点时，递归结束，将该点存入栈中。
	Stack.push(v);
}
// 根据传入的顶点，求出到到其它点的最长路径. longestPath使用了
// topologicalSortUtil() 方法获得顶点的拓扑序。
void Graph::longestPath(int s)
{
	stack<int> Stack;
	
	int *dist = new int[V];

	// 标记所有的顶点为未访问
	bool *visited = new bool[V];
	for (int i = 0; i < V; i++)
		visited[i] = false;

	// 对每个顶点调用topologicalSortUtil，最终求出图的拓扑序列存入到Stack中。
	for (int i = 0; i < V; i++)
		if (visited[i] == false)
			topologicalSortUtil(i, visited, Stack);
	//排序后的序列在堆栈中，最上面的是要拓扑序列的最前端

	//初始化到所有顶点的距离为负无穷
	//到源点的距离为0
	//推荐按 https://blog.csdn.net/u010519432/article/details/26751867 手动画一遍更新过程
	for (int i = 0; i < V; i++)
		dist[i] = NINF;
	dist[s] = 0;

	// 处理拓扑序列中的点
	while (Stack.empty() == false)
	{
		//取出拓扑序列中的第一个点
		int u = Stack.top();
		Stack.pop();

		// 更新到所有邻接点的距离，动态规划过程
		
		
		list<AdjListNode>::iterator i;

		if (dist[u] != NINF)
		{
			//c++迭代器，相当于就是for int i = 0 ,i < adj.count ; i++
			//用于遍历结点u的邻接结点链表里的所有结点
			//可以用i访问当前结点的getv getweight
			for (i = adj[u].begin(); i != adj[u].end(); ++i)
				if (dist[i->getV()] < dist[u] + i->getWeight())
					dist[i->getV()] = dist[u] + i->getWeight();
		}
	}

	// 打印最长路径
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