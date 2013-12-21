#include <iostream>
#include <string>
#include <map>
#include "Parsing/Parser.h"
#include "Parsing/Dumper.h"
#include "Abstraction/Domain.h"

using namespace std;

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

    auto dumped = members.ToString();

    cout << "proposition: " << prop << endl;
    cout << "dumped     : " << dumped << endl;

    DeductionCpp::Abstraction::Domain s = DeductionCpp::Abstraction::Domain::Instance();
    // cout << s.a << endl;

    cin.get();

    return 0;
}
