#pragma once

#include <vector>
#include "DomainMember.h"

namespace DeductionCpp { namespace Abstraction {

class Domain
{
protected:
    static Domain* DomainInstance;

    Domain();

    //virtual ~Domain()
    //{
    //}

public:
    static Domain& Instance();
    static void Dispose();

    std::vector<DomainMember> Members;

    DomainMember* GetMemberBySymbolChar(char symbolChar);
};

} }