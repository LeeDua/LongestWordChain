#include "pch.h"
#include <iostream>
#include <algorithm>
#include <stack>
using namespace std;

char *v;//顶点数组，下标为顶点号，值为顶点名称（用在创建有向无环图中）
int **e;//边的二维矩阵（用在创建有向无环图中）

int *indegree;//保存顶点入度数的数组（求拓扑排序）
int *topo;//保存拓扑排序结果的数组（求拓扑排序）

int *maxPath;//保存到此点的最长路径（求最长路径）

//创建有向无环图
void creatGraph(int vSize, int eSize)
{
	//初始化
	int i, j, k, c;
	char a, b;
	indegree = new int[vSize];
	topo = new int[vSize];
	v = new char[vSize];
	e = new int*[vSize];
	for (i = 0; i < vSize; i++)
		e[i] = new int[vSize];
	for (i = 0; i < vSize; i++)
		for (j = 0; j < vSize; j++)
			e[i][j] = 0;
	for (i = 0; i < vSize; i++)
	{
		indegree[i] = 0;
		topo[i] = 0;
	}

	//建图
	cout << endl << "请输入各顶点名称：";
	for (i = 0; i < vSize; i++)
		cin >> v[i];
	cout << endl << "请先后输入顶点V1和V2（表示V1->V2）以及权值，按换行键继续" << endl;
	for (i = 0; i < eSize; i++)
	{
		cin >> a >> b >> c;
		for (j = 0; j < vSize; j++)
			if (v[j] == a) break;
		for (k = 0; k < vSize; k++)
			if (v[k] == b) break;
		e[j][k] = c;
		indegree[k]++;//入度+1

	}
}

//拓扑排序
void topologicalSort(int vSize)
{
	int i, j, k;
	for (i = 0; i < vSize; i++) //vSize次循环
	{
		j = 0;
		while (j < vSize&&indegree[j] != 0) j++;//找到入度为0的顶点
		topo[i] = j;//保存
		indegree[j] = -1;// 设结点j为入度为-1，以免再次输出j

		for (k = 0; k < vSize; k++)//更新其他入度点
			if (e[j][k] != 0)
				indegree[k]--;
	}
}

//最长路径
void getMaxPath(int vSize)
{
	//初始化
	int i, j, k, v1, v2, max = 0, **path, *p;
	maxPath = new int[vSize];            //保存最长路径，表示到顶点N的最长路径
	p = new int[vSize];                  //用来处理最长路线的选择顶点
	path = new int*[vSize];	           //用来保存点到点的最长路径矩阵
	for (i = 0; i < vSize; i++)
		path[i] = new int[vSize];
	for (i = 0; i < vSize; i++)
		for (j = 0; j < vSize; j++)
			path[i][j] = 0;
	for (i = 0; i < vSize; i++)
	{
		maxPath[i] = 0;
		p[i] = 0;
	}

	//关键代码，求最长路径
	for (j = 0; j < vSize; j++)
	{
		v2 = topo[j];
		for (k = 0; k < j; k++)
		{
			v1 = topo[k];
			if (e[v1][v2] != 0)                     //表示有路
			{
				if (maxPath[v1] + e[v1][v2] > maxPath[v2])
					maxPath[v2] = maxPath[v1] + e[v1][v2];

				if (maxPath[v2] > max)
				{
					max = maxPath[v2];//保存长度
					path[v1][v2] = max;
				}
			}
		}
	}

	//求最长路线
	stack<int> s;//保存线路
	for (i = vSize - 1; i > 0;)
	{
		for (j = 0; j < vSize; j++)
		{
			p[j] = path[j][i];
		}
		sort(p, p + vSize);
		int maxValue = p[j - 1];
		if (maxValue != 0)
		{
			for (j = 0; j < vSize; j++)
			{
				if (path[j][i] == maxValue)
				{
					s.push(i);
					i = j;
					if (i == 0)
						s.push(i);
					break;
				}
			}
		}
	}

	//输出结果
	cout << endl << "最长路径的长度是：" << max << endl << "最长路径为：";
	int len = s.size();
	for (i = 0; i < len; i++)
	{
		if (i != 0) cout << " -> ";
		cout << v[s.top()];
		s.pop();
	}
	cout << endl << endl;
}

/*
int main()
{
	int vSize, eSize, i;
	while (1)
	{
		cout << "·请分别输入有向无环图的顶点数和边数：";
		cin >> vSize >> eSize;

		creatGraph(vSize, eSize);//创建图
		topologicalSort(vSize);//拓扑排序
		getMaxPath(vSize);//最长路径

	}
	return 0;
}
*/