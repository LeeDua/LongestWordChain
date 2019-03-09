
#include "pch.h"
#include<exception>
#include<string>
#include<iostream>
#include "WordListException.h"

using namespace std;



/*
string WordListException::  OnExceptionCaught()
{
	cout <<ExceptionMessage << std::endl;
	return ExceptionMessage;
}

string WordListException:: OnExceptionCreated()
{
	cout << "Exception created: " << ExceptionMessage << std::endl;
	return ExceptionMessage;
}

WordListException :: WordListException(CommandExcepitonEnum CommandExceptionType, const string ExceptionInfo) {
	//raise different response
	switch (CommandExceptionType)
	{
	case CommandExcepitonEnum::IllegalCommandKeyWord:
	{
		ExceptionMessage = "EnterIllegalCommand:" + ExceptionInfo;
		break;
	}
	case CommandExcepitonEnum::MissingMustContainedKeyWord:
	{
		ExceptionMessage = "Should at least contain one of \"-c\" or \"-w\"";
		break;
	}
	case CommandExcepitonEnum::InvalidStartChar:
	{
		ExceptionMessage = "\"-h\" command should follow with char \'a' - \'z' or \'A' - \'Z' to define the start char of the word chain";
		break;
	}
	case CommandExcepitonEnum::InvalidEndChar:
	{
		ExceptionMessage = "\"-t\" command should follow with char \'a' - \'z' or \'A' - \'Z' to define the end char of the word chain";
		break;
	}
	case CommandExcepitonEnum::StartCharMissing:
	{
		ExceptionMessage = "Start char missing after \"-h\" command";
		break;
	}
	case CommandExcepitonEnum::EndCharMissing:
	{
		ExceptionMessage = "Start char missing after \"-t\" command";
		break;
	}
	case CommandExcepitonEnum::UnExpectedCharAfterCWRCommands:
	{
		ExceptionMessage = "\"-c\"  \"-w\" \"-r\" commands should not be followed by an char";
		break;
	}
	default:
		break;
	}
	OnExceptionCreated();
}
WordListException :: WordListException(FileExceptionEnum FileExceptionType) {
	//raise different response
	switch (FileExceptionType) {
	case FileExceptionEnum::FileOpenFailed:
	{
		ExceptionMessage = "FileOpenFailed";
		break;
	}
	case FileExceptionEnum::IllegalFileOnNonRCommand:
	{
		ExceptionMessage = "Word list in file is Acyclic but the command do not contain \'-r\'";
		break;
	}
	}
	OnExceptionCreated;
}
*/
