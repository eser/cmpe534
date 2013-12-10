#include <iostream>
#include <string>
#include <map>
#include "Parsing/Parser.h"
#include "Abstraction/PropositionVariable.h"

using namespace std;
using namespace DeductionCpp::Abstraction;

int main(int argc, char* argv[])
{
    string prop = "(((A & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f | t & D";

    map<char, bool> values = {
        { 'A', true },
        { 'B', false },
        { 'C', true } // ,
        // { 'D', false }
    };

    DeductionCpp::Parsing::Parser parser(prop);
    auto members = parser.Parse();

    cout << "proposition: " << prop << endl;    

    cin.get();

    return 0;
}
