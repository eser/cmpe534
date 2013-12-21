#pragma once

#include <string>
#include "IPropositionMember.h"

namespace DeductionCpp { namespace Abstraction {

class UnaryConnective : public IPropositionMember
{
protected:

public:
    UnaryConnective() : IPropositionMember()
    {
    }

    //virtual ~UnaryConnective()
    //{
    //}

    virtual bool Operation(bool value) = 0;

    virtual std::string ToString()
    {
        std::string final = "";

        final += this->GetDomainMember()->SymbolChar;

        return final;
    }
};

} }