#pragma once
#define QUESTIONS_DATA_H

#include <iostream>
#include <vector>
#include <string>
#include<stdio.h>
using namespace std;

class Data
{
public:
	string Question;
	int num_options;
	vector<string>options;
	int answer;

	unsigned int size;
	Data() {
		Question = "";
		num_options = 0;
		answer = 0;
	}
	//checks fro validity of data fields and entered options
	int check() {
		if (num_options < answer) {
			return 1;
		}
		if (options.size() > num_options) {
			return 2;
		}
	}

	/*void calc_size() {
		size = 0;
		size = size + (Question.size + 1);
		
		for (int k = 1; k <= options.size(); k++) {
			size = size + options[k].size+1;
		}
		size+= 4;

	}*/
	
	int add_data(string data_string) {
		vector<string> data_strings;
		stringstream input(data_string);
		string parsed;
		if (getline(input, parsed, '|'))
		{
			data_strings.push_back(parsed);
		}
		num_options = stoi(data_strings[0]);
		answer = stoi(data_strings[1]);
		this->Question = data_strings[2];
		for (int j = 3; j < data_strings.size(); j++) {
			options.push_back(data_strings[j]);
		}
		return check();
	}
	int add_data(string &Question, vector<string>&options, int &answer, int &num_opt) {
		this->Question = Question;
		this->options = options;
		this->answer = answer;
		this->num_options = num_opt;

		return check();
	}


	string to_String() {
		string k;
		k.clear();
		//char intStr[1];
		//itoa(num_options,intStr,10);
		k = k + to_string(num_options) + "|" + to_string(answer) + "|" + Question +"|";
		for (int j = 0; j < options.size(); j++) {
			k = k + options[j] + "|";
		}
		return k;
	}
};