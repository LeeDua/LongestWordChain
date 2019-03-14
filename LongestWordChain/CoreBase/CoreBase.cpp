// CoreAlgorism.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include <iostream> 
using namespace std;





int TestDllAdd(int a, int b) {
	return a + b;
}

int TestString(char **str, int len)
{
	int LetterCount = 0;
	for (int i = 0; i < len; i++)
	{
		for (int j = 0; ; j++)
		{
			LetterCount += 1;
			cout << str[i][j];
			if (str[i][j] == '\0')
			{
				break;
			}
		}
		//cout << " ";
	}
	return LetterCount;
}

int SingleStringTest(char *str, int len)
{
	int LetterCount = 0;
	for (int i = 0; i < len; i++)
	{

		LetterCount += 1;
		cout << str[i];
		if (str[i] == '\0')
		{
			break;
		}
	}
	cout << " ";

	return LetterCount;
}

#include <iostream>
#include <fstream>
#include <cassert>
#include <string>
#include <vector>

using namespace std;

struct word {
	string s = "";
	int num;
	int count;
	char head;
	char tail;
	int lastnum;
	bool issource;
	vector<struct word*> next;

};

struct path {
	int length;
	vector<string> p;
};

word wordlist[10000];
int t_word = 0;
vector<struct word> queue;
vector<struct path> dilg;
bool visit[10000];
int lengthwithcircle;
vector<string> pathwithcircle;
int t_length;
vector<string> t_path;

void str_copy(char *str1, char *str2) {
	int i = 0;
	while (str2[i] != '\0') {
		str1[i] = str2[i];
		i++;
	}
	str1[i] = '\0';
}

int searchword(string w, int n) {
	int i = 0;
	for (i = 0; i < n; i++) {
		if (w.compare(wordlist[i].s) == 0) {
			return 1;
		}
	}
	return 0;
}

void addword() {
	int i;
	for (i = 0; i < t_word; i++) {
		if (wordlist[i].head == wordlist[t_word].tail) {
			wordlist[i].lastnum++;
			wordlist[i].issource = false;
			wordlist[t_word].next.push_back(&wordlist[i]);
		}
		if (wordlist[i].tail == wordlist[t_word].head) {
			wordlist[t_word].lastnum++;
			wordlist[t_word].issource = false;
			wordlist[i].next.push_back(&wordlist[t_word]);
		}
		//cout << wordlist[i].lastnum << endl;
	}
	t_word++;
}

bool toposort() {
	int i, j, k;
	bool nocircle;
	//vector<struct word> t_q;
	//vector<struct word> t_l=wordlist;
	for (i = 0; i < t_word; i++) {
		if (queue.size() == t_word) {
			break;
		}
		nocircle = false;
		for (j = 0; j < t_word; j++)
		{
			if (wordlist[j].lastnum == 0) {
				nocircle = true;
				break;
			}
		}
		if (!nocircle) {
			return false;
		}
		queue.push_back(wordlist[j]);//保存
		wordlist[j].lastnum = -1;// 设结点j为入度为-1，以免再次输出j
		for (k = 0; k < wordlist[j].next.size(); k++) {
			wordlist[(*wordlist[j].next[k]).num].lastnum--;
		}
	}
	return true;
}

void longest_path_t(bool isword) {
	int i, j, k;
	/*vector<struct path> dilg;
	struct path n_p;
	n_p.length = 0;
	for (i = 0; i < t_word; i++) {
		dilg.push_back(n_p);
	}*/
	for (i = 0; i < t_word; i++) {
		if (queue[i].issource) {
			if (isword) {
				dilg[i].length++;
			}
			else {
				dilg[i].length += queue[i].count;
			}
			dilg[i].p.push_back(queue[i].s);
			for (j = 0; j < queue[i].next.size(); j++) {
				for (k = i + 1; k < t_word; k++) {
					if ((*queue[i].next[j]).num == queue[k].num) {
						if (isword) {
							if (dilg[i].length + 1 > dilg[k].length) {
								dilg[k].length = dilg[i].length + 1;
								dilg[k].p = dilg[i].p;
								dilg[k].p.push_back(queue[k].s);
								break;
							}
						}
						else {
							if (dilg[i].length + queue[k].count > dilg[k].length) {
								dilg[k].length = dilg[i].length + queue[k].count;
								dilg[k].p = dilg[i].p;
								dilg[k].p.push_back(queue[k].s);
								break;
							}
						}
					}
				}
			}
		}
		else {
			for (j = 0; j < queue[i].next.size(); j++) {
				for (k = i + 1; k < t_word; k++) {
					if ((*queue[i].next[j]).num == queue[k].num) {
						if (isword) {
							if (dilg[i].length + 1 > dilg[k].length) {
								dilg[k].length = dilg[i].length + 1;
								dilg[k].p = dilg[i].p;
								dilg[k].p.push_back(queue[k].s);
								break;
							}
						}
						else {
							if (dilg[i].length + queue[k].count > dilg[k].length) {
								dilg[k].length = dilg[i].length + queue[k].count;
								dilg[k].p = dilg[i].p;
								dilg[k].p.push_back(queue[k].s);
								break;
							}
						}
					}
				}
			}
		}
	}

}

void longest_path_h(bool isword, char head) {
	int i, j, k, start = -1;
	/*vector<struct path> dilg;
	struct path n_p;
	n_p.length = 0;
	for (i = 0; i < t_word; i++) {
		dilg.push_back(n_p);
	}*/
	for (i = 0; i < t_word; i++) {
		if (queue[i].head == head) {
			start = i;
			break;
		}
	}//寻找拓扑排序中第一个符合首字母的单词
	if (start < 0) {
		return;
	}
	for (i = start; i < t_word; i++) {
		if (queue[i].head == head && dilg[i].length == 0) {
			if (isword) {
				dilg[i].length++;
			}
			else {
				dilg[i].length += queue[i].count;
			}
			dilg[i].p.push_back(queue[i].s);
			for (j = 0; j < queue[i].next.size(); j++) {
				for (k = i + 1; k < t_word; k++) {
					if ((*queue[i].next[j]).num == queue[k].num) {
						if (isword) {
							if (dilg[i].length + 1 > dilg[k].length) {
								dilg[k].length = dilg[i].length + 1;
								dilg[k].p = dilg[i].p;
								dilg[k].p.push_back(queue[k].s);
								break;
							}
						}
						else {
							if (dilg[i].length + queue[k].count > dilg[k].length) {
								dilg[k].length = dilg[i].length + queue[k].count;
								dilg[k].p = dilg[i].p;
								dilg[k].p.push_back(queue[k].s);
								break;
							}
						}
					}
				}
			}
		}
		else {
			for (j = 0; j < queue[i].next.size(); j++) {
				for (k = i + 1; k < t_word; k++) {
					if ((*queue[i].next[j]).num == queue[k].num) {
						if (isword) {
							if (dilg[i].length + 1 > dilg[k].length) {
								dilg[k].length = dilg[i].length + 1;
								dilg[k].p = dilg[i].p;
								dilg[k].p.push_back(queue[k].s);
								break;
							}
						}
						else {
							if (dilg[i].length + queue[k].count > dilg[k].length) {
								dilg[k].length = dilg[i].length + queue[k].count;
								dilg[k].p = dilg[i].p;
								dilg[k].p.push_back(queue[k].s);
								break;
							}
						}
					}
				}
			}
		}
	}
}

void searchpath(int i, bool isword, char tail) {
	int j;
	bool end;
	if (isword) {
		t_length++;
	}
	else {
		t_length += wordlist[i].count;
	}
	t_path.push_back(wordlist[i].s);
	visit[i] = true;
	int k;
	/*for (k = 0; k < t_path.size(); k++) {
		cout << t_path[k] << " ";
	}
	cout << endl;*/
	if (tail == 0) {
		if (wordlist[i].next.size() > 0) {
			end = true;
			for (j = 0; j < wordlist[i].next.size(); j++) {
				if (!visit[(*wordlist[i].next[j]).num]) {
					end = false;
					searchpath((*wordlist[i].next[j]).num, isword, tail);
				}
			}
			if (end) {
				if (t_length > lengthwithcircle) {
					lengthwithcircle = t_length;
					pathwithcircle = t_path;
				}
			}
		}
		else {
			if (t_length > lengthwithcircle) {
				lengthwithcircle = t_length;
				pathwithcircle = t_path;
			}
		}
	}
	else {
		if (wordlist[i].tail == tail) {
			if (t_length > lengthwithcircle) {
				lengthwithcircle = t_length;
				pathwithcircle = t_path;
			}
		}
		if (wordlist[i].next.size() > 0) {
			for (j = 0; j < wordlist[i].next.size(); j++) {
				if (!visit[(*wordlist[i].next[j]).num]) {
					searchpath((*wordlist[i].next[j]).num, isword, tail);
				}
			}
		}
	}
	if (isword) {
		t_length--;
	}
	else {
		t_length -= wordlist[i].count;
	}
	visit[i] = false;
	t_path.pop_back();
}

void longest_path_withcircle(bool isword, char head, char tail) {
	//寻找带环的最长单词链
	int i, j;
	bool end;
	lengthwithcircle = 0;
	for (i = 0; i < 10000; i++) {
		visit[i] = false;
	}
	if (head == 0) { //无首字母要求时
		for (i = 0; i < t_word; i++) {
			if (isword) {
				t_length++;
			}
			else {
				t_length += wordlist[i].count;
			}
			t_path.push_back(wordlist[i].s);
			visit[i] = true;
			if (tail == 0) {
				if (wordlist[i].next.size() > 0) {
					end = true;
					for (j = 0; j < wordlist[i].next.size(); j++) {
						if (!visit[(*wordlist[i].next[j]).num]) {
							end = false;
							searchpath((*wordlist[i].next[j]).num, isword, tail);
						}
					}
					if (end) {
						if (t_length > lengthwithcircle) {
							lengthwithcircle = t_length;
							pathwithcircle = t_path;
						}
					}
				}
				else {
					if (t_length > lengthwithcircle) {
						lengthwithcircle = t_length;
						pathwithcircle = t_path;
					}
				}
			}
			else {
				if (wordlist[i].tail == tail) {
					if (t_length > lengthwithcircle) {
						lengthwithcircle = t_length;
						pathwithcircle = t_path;
					}
				}
				if (wordlist[i].next.size() > 0) {
					for (j = 0; j < wordlist[i].next.size(); j++) {
						if (!visit[(*wordlist[i].next[j]).num]) {
							searchpath((*wordlist[i].next[j]).num, isword, tail);
						}
					}
				}
			}
			if (isword) {
				t_length--;
			}
			else {
				t_length -= wordlist[i].count;
			}
			visit[i] = false;
			t_path.pop_back();
		}
	}
	else { //有首字母要求时
		for (i = 0; i < t_word; i++) {
			if (wordlist[i].head == head) {
				if (isword) {
					t_length++;
				}
				else {
					t_length += wordlist[i].count;
				}
				t_path.push_back(wordlist[i].s);
				visit[i] = true;
				if (tail == 0) {
					if (wordlist[i].next.size() > 0) {
						end = true;
						for (j = 0; j < wordlist[i].next.size(); j++) {
							if (!visit[(*wordlist[i].next[j]).num]) {
								end = false;
								searchpath((*wordlist[i].next[j]).num, isword, tail);
							}
						}
						if (end) {
							if (t_length > lengthwithcircle) {
								lengthwithcircle = t_length;
								pathwithcircle = t_path;
							}
						}
					}
					else {
						if (t_length > lengthwithcircle) {
							lengthwithcircle = t_length;
							pathwithcircle = t_path;
						}
					}
				}
				else {
					if (wordlist[i].tail == tail) {
						if (t_length > lengthwithcircle) {
							lengthwithcircle = t_length;
							pathwithcircle = t_path;
						}
					}
					if (wordlist[i].next.size() > 0) {
						for (j = 0; j < wordlist[i].next.size(); j++) {
							if (!visit[(*wordlist[i].next[j]).num]) {
								searchpath((*wordlist[i].next[j]).num, isword, tail);
							}
						}
					}
				}
				if (isword) {
					t_length--;
				}
				else {
					t_length -= wordlist[i].count;
				}
				visit[i] = false;
				t_path.pop_back();
			}
		}
	}
}

int outputpath(char tail, char *result_in[]) {
	int lstpath = 0;
	int result = 0;
	int i;
	if (tail == 0) {
		for (i = 0; i < t_word; i++) {
			if (dilg[i].length > lstpath) {
				lstpath = dilg[i].length;
				result = i;
			}
		}
	}
	else {
		for (i = 0; i < t_word; i++) {
			if (queue[i].tail == tail) {
				if (dilg[i].length > lstpath) {
					lstpath = dilg[i].length;
					result = i;
				}
			}
		}
	}

	ofstream outfile;
	outfile.open("solution.txt");
	/*if (outfile.is_open()) {
		cout << "open success" << endl;
	}*/
	if (dilg[result].p.size() < 2) {
		return 0;
	}
	for (i = 0; i < dilg[result].p.size(); i++) {
		result_in[i] = new char[110];
		strcpy_s(result_in[i], strlen(dilg[result].p[i].c_str()) + 1, dilg[result].p[i].c_str());
		cout << result_in[i] << " ";
		outfile << dilg[result].p[i] << endl;
	}
	cout << endl;
	outfile.close();
	cout << "NONcircular condition output" << endl;
	return dilg[result].p.size();
}

int outputwithcircle(char *result[]) {
	int i;
	ofstream outfile;
	outfile.open("solution.txt");
	/*if (outfile.is_open()) {
		cout << "open success" << endl;
	}*/
	if (pathwithcircle.size() < 2) {
		return 0;
	}
	for (i = 0; i < pathwithcircle.size(); i++) {
		result[i] = new char[110];
		strcpy_s(result[i], strlen(pathwithcircle[i].c_str()) + 1, pathwithcircle[i].c_str());
		cout << result[i] << " ";
		outfile << pathwithcircle[i] << endl;
	}
	cout << endl;
	outfile.close();
	cout << "circular condition output" << endl;
	return pathwithcircle.size();
}



int* gen_chain_cpp(char* words[], int len, char* result[], bool isword, bool iscircle, char havehead, char havetail) {
	int i;
	int *r_num = new int[2]{ 0,0 };
	for (i = 0; i < len; i++) {
		struct word *w = new word();
		w->s = words[i];
		if (searchword(w->s, t_word) == 0) {
			w->num = t_word;
			w->count = w->s.size();
			w->head = w->s[0];
			w->tail = w->s[w->count - 1];
			w->lastnum = 0;
			w->issource = true;
			wordlist[t_word] = (*w);
			addword();
		}
	}
	cout << "build word list finished" << endl;
	bool toposortResult = toposort();
	cout << "toposort finished" << endl;
	if (!toposortResult) {
		if (iscircle) {
			longest_path_withcircle(isword, havehead, havetail);
			r_num[1] = outputwithcircle(result);
		}
		else {
			r_num[0] = 2;
			return r_num;
		}
	}
	else {
		struct path n_p;
		n_p.length = 0;
		for (i = 0; i < t_word; i++) {
			dilg.push_back(n_p);
		}
		if (havehead == 0) {
			longest_path_t(isword);
		}
		else {
			longest_path_h(isword, havehead);
		}
		r_num[1] = outputpath(havetail, result);
	}
	return r_num;
}




int get_wordchain(char **str, int len) {
	bool isword = true;
	bool iscircle = false;;
	char havehead = 0;
	char havetail = 0;
	int i;
	int r;
	for (i = 0; i < len - 1; i++) {
		if (strcmp(str[i], "-w") == 0) {
			isword = true;
		}
		if (strcmp(str[i], "-c") == 0) {
			isword = false;
		}
		if (strcmp(str[i], "-r") == 0) {
			iscircle = true;
		}
		if (strcmp(str[i], "-h") == 0) {
			i++;
			havehead = str[i][0];
			if (havehead >= 'A'&&havehead <= 'Z') {
				havehead = havehead + 'a' - 'A';
			}
		}
		if (strcmp(str[i], "-t") == 0) {
			i++;
			havetail = str[i][0];
			if (havetail >= 'A'&&havetail <= 'Z') {
				havetail = havetail + 'a' - 'A';
			}
		}
	}
	ifstream infile;
	//cout << "Reached here" << endl;
	infile.open(str[len - 1]);   //将文件流对象与文件连接起来
	if (!infile.is_open()) {
		//cout << "no file" << endl;
		return 1;
	}//若失败,则输出错误消息,并终止程序运行
	char c;
	char *words[10000];
	char *result[10000];
	char tempword[110];
	int words_len;
	int c_n = 0;
	int flag = 0;
	infile >> noskipws;
	words_len = 0;
	while (infile.peek() != EOF) {
		infile >> c;
		if (c >= 'a'&&c <= 'z') {
			flag = 1;
			tempword[c_n++] = c;
		}
		else if (c >= 'A'&&c <= 'Z') {
			flag = 1;
			c = c + 'a' - 'A';
			tempword[c_n++] = c;
		}
		else {
			if (flag == 1) {
				tempword[c_n] = '\0';
				c_n = 0;
				flag = 0;
				words[words_len] = new char[110];
				str_copy(words[words_len], tempword);
				words_len++;
			}
		}
	}
	if (flag == 1) {
		tempword[c_n] = '\0';
		c_n = 0;
		flag = 0;
		words[words_len] = new char[110];
		str_copy(words[words_len], tempword);
		words_len++;
	}
	/*cout << words_len << endl;
	for (i = 0; i < words_len; i++) {
		cout << words[i] << endl;
	}
	return 0;*/
	//cout << "Reached here" << endl;
	return gen_chain_cpp(words, words_len, result, isword, iscircle, havehead, havetail)[0];	
}
/*
int main() {
	char **str = new char*[2];
	str[0] = new char[100];
	str[1] = new char[200];
	strcpy_s(str[0], 3, "-c");

	strcpy_s(str[1], 199, "F:\\我爱学习学习爱我\\SoftwareEngineeringCourse\\LongestWordChain\\LongestWordChain\\LongestWordChain\\InputTest\\CompressedInput.txt");
	int len = 2;
	get_wordchain(str, len);
	return 0;
}

*/

/*

int main() {
	bool isword = true;
	bool iscircle = true;;
	char havehead = '\0';
	char havetail = '\0';
	
	ifstream infile;
	//cout << "Reached here" << endl;
	infile.open("F:\\InputTest\\input.txt");   //将文件流对象与文件连接起来
	if (!infile.is_open()) {
		cout << "no file" << endl;
		exit(-1);
	}//若失败,则输出错误消息,并终止程序运行
	char c;
	char *words[10000];
	char *result[10000];
	char tempword[110];
	int words_len;
	int c_n = 0;
	int flag = 0;
	infile >> noskipws;
	words_len = 0;
	while (infile.peek() != EOF) {
		infile >> c;
		if (c >= 'a'&&c <= 'z') {
			flag = 1;
			tempword[c_n++] = c;
		}
		else if (c >= 'A'&&c <= 'Z') {
			flag = 1;
			c = c + 'a' - 'A';
			tempword[c_n++] = c;
		}
		else {
			if (flag == 1) {
				tempword[c_n] = '\0';
				c_n = 0;
				flag = 0;
				words[words_len] = new char[110];
				str_copy(words[words_len], tempword);
				words_len++;
			}
		}
	}
	if (flag == 1) {
		tempword[c_n] = '\0';
		c_n = 0;
		flag = 0;
		words[words_len] = new char[110];
		str_copy(words[words_len], tempword);
		words_len++;
	}
	
	//cout << "Reached here" << endl;
	gen_chain_cpp(words, words_len, result, isword, iscircle, havehead, havetail);
}
*/