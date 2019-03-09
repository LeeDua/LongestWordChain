
#include "pch.h"
#include<list>
#include<string>
#include "CommandLineInputParser.h"
#include "WordListException.h"

using namespace std;

CommandLineInputParser::CommandLineInputParser(int argCount, char * Commands) {
	//foreach command, addcommandtoinitiallist
}

void CommandLineInputParser :: FormatInitialList() {
	// instantiate legalcommands
}
void CommandLineInputParser :: CheckCommandCombinationLegal() {
	//check combination of c w r		
}
void CommandLineInputParser :: AddCommandToInitialList() {

}

list<Command> CommandLineInputParser :: GetParsedCommandList() {
	return ParsedCommandList;
}


Command :: Command(string _command, char _startOrEndChar) {
	// do try exception to out rule invalid command key words
	// out rule -t -h with illegal char sets
	

	if (_command == "-w" || _command == "-c" || _command == "-r") {
		if (_startOrEndChar != NULL) {

			
		}
	}

	else if (_command == "-t") {

	}
	else if (_command == "-h") {

	}


}

