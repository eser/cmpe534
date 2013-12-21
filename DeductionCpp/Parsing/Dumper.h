#pragma once

#include <string>
#include "../Abstraction/PropositionArray.h"

namespace DeductionCpp { namespace Parsing {

class Dumper
{
protected:
    Dumper()
    {
    }

public:
    //virtual ~Dumper()
    //{
    //}

    static std::string GetString(DeductionCpp::Abstraction::PropositionArray& input);
};

} }