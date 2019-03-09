#pragma once

#include "pch.h"
#include<exception>
#include<string>
#include<iostream>

using namespace std;

__interface IWordListException
{
public:
	string OnExceptionCaught();
	string OnExceptionCreated();
};

class IllegalCommandKeyWord : exception, IWordListException {
public:
	IllegalCommandKeyWord();
};
class MissingMustContainedKeyWord : exception, IWordListException {
public:
	MissingMustContainedKeyWord();
};
class InvalidStartChar : exception, IWordListException {
public:
	InvalidStartChar();
};
class StartCharMissing : exception, IWordListException {
public:
	StartCharMissing();
};
class InvalidEndChar : exception, IWordListException {
public:
	InvalidEndChar();
};
class EndCharMissing : exception, IWordListException {
public:
	EndCharMissing();
};
class UnExpectedCharAfterCWRCommands : exception, IWordListException {
public:
	UnExpectedCharAfterCWRCommands();
};
class FileOpenFailed : exception, IWordListException {
public:
	FileOpenFailed();
};
class IllegalFileOnNonRCommand : exception, IWordListException {
public:
	IllegalFileOnNonRCommand();
};

/*static enum CommandExcepitonEnum {
		IllegalCommandKeyWord,
		MissingMustContainedKeyWord,
		InvalidStartChar,
		StartCharMissing,
		InvalidEndChar,
		EndCharMissing,
		UnExpectedCharAfterCWRCommands
	};

	static enum FileExceptionEnum {
		FileOpenFailed,
		IllegalFileOnNonRCommand
	};*/