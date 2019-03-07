
#include "pch.h"
#include<list>
using namespace std;

typedef basic_string<char> string;

class CommandLineInputParser {
	
	list<string> InitialCommandList;
	list<LegalCommand> ParsedCommandList;
	
	void FormatInitialList() {
		// instantiate legalcommands

	}
	void CheckCommandCombinationLegal() {
		//check combination of c w r
		
	}
	void AddCommandToInitialList() {

	}

public:
	CommandLineInputParser(int argCount, char * Commands) {
		//foreach command, addcommandtoinitiallist
	}
	
	list<LegalCommand> GetParsedCommandList() {
		return ParsedCommandList;
	}
};


class LegalCommand {
	enum LegalCommandWords {

	};

public:
	LegalCommand(string* command, char ExtraRequirement) {
		// do try exception to out rule invalid command key words
		// out rule -t -h with illegal char sets
	}

};