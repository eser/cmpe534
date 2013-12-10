#pragma once

#include <string>

namespace DeductionCpp { namespace Parsing {

class Parser
{
protected:
    std::string line;
    int currentPosition;

    // char* GetNext();
    // std::string GetInsideParanthesis();

public:
    Parser(std::string line)
    {
        this->line = line;
        this->currentPosition = 0;
    }

    //virtual ~Parser()
    //{
    //}

    // PropositionArray Parse();
};

} }