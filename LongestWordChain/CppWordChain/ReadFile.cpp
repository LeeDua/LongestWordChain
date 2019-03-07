#include "pch.h"
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
	vector<struct word> last;
};

struct word wordlist[500];
int max_length = 0;
int t_length = 0;
int t_word = 0;
vector<string> path;

int searchword(string w, int n) {
	int i = 0;
	for (i = 0; i < n; i++) {
		if (w.compare(wordlist[i].s) == 0) {
			return 1;
		}
	}
	return 0;
}

int main(int argc, const char * argv[]) {
	ifstream infile;
	string file = "C:\\Users\\Administrator\\source\\repos\\wordlist.txt";
	infile.open(file.data());
	assert(infile.is_open());
	infile >> noskipws;
	char c;
	int i, j, c_n = 0;
	int flag = 0;
	struct word w;
	while (!infile.eof()) {
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
					wordlist[t_word++] = w;
				}
				c_n = 0;
				w.s = "";
				flag = 0;
			}
		}
	}
	/*for(i=0;i<t;i++){
		cout<<wordlist[i].s<<" "<<wordlist[i].num<<" "<<wordlist[i].count<<endl;
	}*/
	/*int l_count;
	for(i=0;i<t_word;i++){
		l_count=0;
		for(j=0;j<t_word;j++){
			if(i==j){
				continue;
			}
			if(wordlist[i].head==wordlist[j].tail){
				l_count++;
				worlidt[i].last.push_back(<#const_reference __x#>)
				break;
			}
		}
	}*/
	cout << "finish" << endl;
	return 0;
}
