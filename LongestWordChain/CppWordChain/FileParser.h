#pragma once

#include "pch.h"
#include<list>
#include<string>

using namespace std;
class FileParser {
	//some private graph instance
	string FilePath;
public:
	FileParser(string _FilePath);

	void ParseToGetGraph();

};