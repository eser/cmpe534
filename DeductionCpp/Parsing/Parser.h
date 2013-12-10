#pragma once

#include <string>
#include "../Abstraction/PropositionArray.h"

namespace DeductionCpp { namespace Parsing {

class Parser
{
protected:
    std::string line;
    unsigned int currentPosition;

    inline char GetNext()
    {
        if (this->currentPosition >= this->line.length()) {
            return NULL; // '\0';
        }

        return this->line[this->currentPosition++];
    }

    std::string GetInsideParanthesis();

public:
    Parser(std::string line)
    {
        this->line = line;
        this->currentPosition = 0;
    }

    //virtual ~Parser()
    //{
    //}

    DeductionCpp::Abstraction::PropositionArray Parse();
};

} }