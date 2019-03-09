#pragma once

#include "pch.h"
#include<list>
#include<string>
using namespace std;


class CommandLineInputParser {

	list<string> InitialCommandList;
	list<Command> ParsedCommandList;

	void FormatInitialList();
	void CheckCommandCombinationLegal();
	void AddCommandToInitialList();

public:
	CommandLineInputParser(int argCount, char * Commands);

	list<Command> GetParsedCommandList();
};


class Command {
	string legalCommands[5] = { "-w","-c","-r","-t","-h" };
	string command;
	char startOrEndChar;
public:
	Command(string _command, char _startOrEndChar);

};
