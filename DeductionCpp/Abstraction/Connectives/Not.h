#pragma once

#include "../UnaryConnective.h"

namespace DeductionCpp { namespace Abstraction { namespace Connectives {

class Not : public UnaryConnective
{
protected:

public:
    Not() : UnaryConnective()
    {
    }

    //virtual ~Not()
    //{
    //}

    virtual bool Operation(bool value)
    {
        return !value;
    }
};

} } }