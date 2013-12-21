#pragma once

#include <string>
#include "IPropositionMember.h"

namespace DeductionCpp { namespace Abstraction {

class BinaryConnective : public IPropositionMember
{
protected:

public:
    BinaryConnective() : IPropositionMember()
    {
    }

    //virtual ~BinaryConnective()
    //{
    //}

    virtual bool Operation(bool left, bool right) = 0;

    virtual std::string ToString()
    {
        std::string final = " ";

        final += this->GetDomainMember()->SymbolChar;
        final += ' ';

        return final;
    }
};

} }