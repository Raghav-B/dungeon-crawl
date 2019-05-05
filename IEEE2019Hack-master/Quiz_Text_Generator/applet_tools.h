#pragma once
#define APPLET_TOOLS_H
#include <iostream>
#include <sstream>
#include <fstream>
#include "Questions_Data.h"
using namespace std;

class applet_tools {
public:
	string file_location;
	vector<Data> questions;
	applet_tools() {
		file_location = "C:/Users/Raghav Bhardwaj/Desktop/";
	}
	applet_tools(string file) {
		this->file_location = file;
	}
	void set_file(string file) {
	    file_location =file_location+file;
		cout << file_location;
	}
	void toFile(){
		ofstream file;
		file.open(file_location,ios::out);

		for (int j = 0; j < questions.size(); j++) {
			file << questions[j].to_String() << endl;
		}
		//printf("a\n");
		file.close();
		//printf("a\n");
	}
	void addQuestion(Data question) {
		questions.push_back(question);
	}
	void modifyQuestion(int pos, Data question) {
		questions.erase(questions.begin() + pos-1);
		questions.emplace(questions.begin() + pos-1, question);
	}
	void view() {
		for (int j = 0; j < questions.size(); j++) {
			cout << j+1<<" " <<questions[j].to_String() << endl;
		}
	}
	int fromFile() {
		ifstream file;
		file.open(file_location, ios::in);
		string line;
		while (getline(file,line))
		{
			Data temp;
			temp.add_data(line);
			questions.push_back(temp);
		}
		file.close();
		return 0;
	}

};