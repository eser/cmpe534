#pragma once

#include <vector>
#include "IPropositionValue.h"

namespace DeductionCpp { namespace Abstraction {

class PropositionArray : public IPropositionValue
{
protected:
    bool negated;

public:
    std::vector<IPropositionMember> Items;

    PropositionArray(std::vector<IPropositionMember> items) : IPropositionValue(), Items(items), negated(negated)
    {
    }

    //virtual ~PropositionArray()
    //{
    //}

    virtual inline bool operator==(const PropositionArray& other)
    {
        // TODO: implement it.
        return false;
    }

    virtual PropositionMemberTypes GetType() const
    {
        return PropositionMemberTypes::Array;
    }

    virtual bool GetNegated() const
    {
        return this->negated;
    }

    virtual void SetNegated(bool value)
    {
        this->negated = value;
    }

    // GetCommonBinaryConnectiveType
};

} }