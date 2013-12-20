#pragma once

#include "IPropositionValue.h"
#include "PropositionMemberTypes.h"

namespace DeductionCpp { namespace Abstraction {

class PropositionConstant : public IPropositionValue
{
protected:
    bool negated;

public:
    PropositionConstant(bool negated = false) : IPropositionValue(), negated(negated)
    {
    }

    //virtual ~PropositionConstant()
    //{
    //}

    virtual inline bool operator==(const PropositionConstant& other)
    {
        return (this->GetValue() == other.GetValue() && this->GetNegated() == other.GetNegated());
    }

    virtual PropositionMemberTypes GetType() const
    {
        return PropositionMemberTypes::Constant;
    }

    virtual bool GetNegated() const
    {
        return this->negated;
    }

    virtual void SetNegated(bool value)
    {
        this->negated = value;
    }

    virtual bool GetValue() const = 0;
};

} }