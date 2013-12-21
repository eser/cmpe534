#pragma once

#include <string>
#include "DomainMember.h"

namespace DeductionCpp { namespace Abstraction {

class IPropositionMember
{
protected:

public:
    IPropositionMember()
    {
    }

    //virtual ~IPropositionMember()
    //{
    //}

    virtual DomainMember* GetDomainMember() = 0;
    virtual std::string ToString() = 0;
};

} }