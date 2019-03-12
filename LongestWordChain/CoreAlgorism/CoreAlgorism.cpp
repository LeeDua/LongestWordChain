// CoreAlgorism.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include <iostream> 
using namespace std;

int* gen_chain_cpp(char* words[], int len, char* result[], char* ParsedCommand[]) {
	cout << "Not implemented yet" << endl;
	return new int[2] {-5, -6};
}


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


 //
//  main.cpp
//  wordchain
//
//  Created by 暴伟杰 on 2019/3/6.
//  Copyright © 2019年 暴伟杰. All rights reserved.
//

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
	 vector<struct word> next;
 };

 struct path {
	 int length;
	 vector<string> p;
 };

 vector<struct word> wordlist;
 int t_word = 0;
 vector<struct word> queue;
 vector<struct path> dilg;
 bool visit[10000];
 int lengthwithcircle;
 vector<string> pathwithcircle;
 int t_length;
 vector<string> t_path;

 int searchword(string w, int n) {
	 int i = 0;
	 for (i = 0; i < n; i++) {
		 if (w.compare(wordlist[i].s) == 0) {
			 return 1;
		 }
	 }
	 return 0;
 }

 void addword(struct word w) {
	 int i;
	 for (i = 0; i < t_word; i++) {
		 if (wordlist[i].head == w.tail) {
			 wordlist[i].lastnum++;
			 wordlist[i].issource = false;
			 w.next.push_back(wordlist[i]);
		 }
		 if (wordlist[i].tail == w.head) {
			 w.lastnum++;
			 w.issource = false;
			 wordlist[i].next.push_back(w);
		 }
		 //cout << wordlist[i].lastnum << endl;
	 }
	 wordlist.push_back(w);
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
			 wordlist[wordlist[j].next[k].num].lastnum--;
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
					 if (queue[i].next[j].num == queue[k].num) {
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
					 if (queue[i].next[j].num == queue[k].num) {
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
	 int i, j, k, start = 0;
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
					 if (queue[i].next[j].num == queue[k].num) {
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
					 if (queue[i].next[j].num == queue[k].num) {
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
				 if (!visit[wordlist[i].next[j].num]) {
					 end = false;
					 searchpath(wordlist[i].next[j].num, isword, tail);
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
				 if (!visit[wordlist[i].next[j].num]) {
					 searchpath(wordlist[i].next[j].num, isword, tail);
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
						 if (!visit[wordlist[i].next[j].num]) {
							 end = false;
							 searchpath(wordlist[i].next[j].num, isword, tail);
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
						 if (!visit[wordlist[i].next[j].num]) {
							 searchpath(wordlist[i].next[j].num, isword, tail);
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
							 if (!visit[wordlist[i].next[j].num]) {
								 end = false;
								 searchpath(wordlist[i].next[j].num, isword, tail);
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
							 if (!visit[wordlist[i].next[j].num]) {
								 searchpath(wordlist[i].next[j].num, isword, tail);
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

 void outputpath(char tail) {
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
	 cout << "NONcircular condition output" << endl;
	 outfile.open("F:\\OutputTest\\solutionWithoutCircle.txt");
	 for (i = 0; i < dilg[result].p.size(); i++) {
		 outfile << dilg[result].p[i] << endl;
	 }
	 outfile.close();
 }

 void outputwithcircle() {
	 int i;
	 ofstream outfile;
	 outfile.open("F:\\OutputTest\\solutionWithCircle.txt");
	 cout << "circular condition output"<< endl;
	 for (i = 0; i < pathwithcircle.size(); i++) {
		 outfile << pathwithcircle[i] << endl;
	 }
	 outfile.close();
 }

 int get_wordchain(char **str, int len) {
	 bool isword = true;
	 bool iscircle = false;;
	 char havehead = 0;
	 char havetail = 0;
	 int i;
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
		 }
		 if (strcmp(str[i], "-t") == 0) {
			 i++;
			 havetail = str[i][0];
		 }
	 }
	 ifstream infile;
	 cout << "Reached here" << endl;
	 infile.open(str[len - 1]);   //将文件流对象与文件连接起来
	 if (!infile.is_open()) {
		 cout << str[len - 1] << endl;
		 return 1;
	 }//若失败,则输出错误消息,并终止程序运行
	 char c;
	 int c_n = 0;
	 int flag = 0;
	 struct word w;
	 infile >> noskipws;
	 while (infile.peek() != EOF) {
		 infile >> c;
		 if (c >= 'a'&&c <= 'z') {
			 flag = 1;
			 c_n++;
			 w.s += c;
		 }
		 else if (c >= 'A'&&c <= 'Z') {
			 flag = 1;
			 c = c + 'a' - 'A';
			 c_n++;
			 w.s += c;
		 }
		 else {
			 if (flag == 1) {
				 if (searchword(w.s, t_word) == 0) {
					 w.num = t_word;
					 w.count = c_n;
					 w.head = w.s[0];
					 w.tail = w.s[c_n - 1];
					 w.lastnum = 0;
					 w.issource = true;
					 addword(w);
				 }
				 c_n = 0;
				 w.s = "";
				 flag = 0;
			 }
		 }
	 }
	 if (flag == 1) {
		 if (searchword(w.s, t_word) == 0) {
			 w.num = t_word;
			 w.count = c_n;
			 w.head = w.s[0];
			 w.tail = w.s[c_n - 1];
			 w.lastnum = 0;
			 w.issource = true;
			 addword(w);
		 }
	 }
	 infile.close();
	 /*for(i=0;i<t_word;i++){
		 cout<<wordlist[i].s<<" "<<wordlist[i].count<<endl;
	 }*/
	 /*int l_count;
	 for(i=0;i<t_word;i++){
		 l_count=0;
		 //cout<<wordlist[i].s<<" ";
		 for(j=0;j<t_word;j++){
			 if(i==j){
				 continue;
			 }
			 if(wordlist[i].head==wordlist[j].tail&&j!=i){
				 l_count++;
			 }
			 if (wordlist[i].tail == wordlist[j].head&&j!=i) {
				 wordlist[i].next.push_back(wordlist[j]);
				 //cout << wordlist[j].s << " ";
			 }
		 }
		 wordlist[i].lastnum=l_count;
		 if (l_count == 0) {
			 wordlist[i].issource = true;
		 }
		 //cout << wordlist[i].lastnum << endl;
	 }*/
	 if (!toposort()) {
		 if (iscircle) {
			 longest_path_withcircle(isword, havehead, havetail);
			 outputwithcircle();
		 }
		 else {
			 return 2;
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
		 //longest_path_h(false, 'k');
		 outputpath(havetail);
		 /*int result;
		  result = longest_path();*/
		  //cout << "finish" << endl;
	 }
	 return 0;
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
