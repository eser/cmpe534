#pragma once

#include "IPropositionValue.h"
#include "PropositionMemberTypes.h"

namespace DeductionCpp { namespace Abstraction {

class PropositionVariable : public IPropositionValue
{
protected:
    bool negated;
    char letter;

public:
    PropositionVariable(char letter, bool negated = false) : IPropositionValue(), letter(letter), negated(negated)
    {
    }

    //virtual ~PropositionVariable()
    //{
    //}

    virtual inline bool operator==(const PropositionVariable& other)
    {
        return (this->GetLetter() == other.GetLetter() && this->GetNegated() == other.GetNegated());
    }

    virtual PropositionMemberTypes GetType() const
    {
        return PropositionMemberTypes::Variable;
    }

    virtual bool GetNegated() const
    {
        return this->negated;
    }

    virtual void SetNegated(bool value)
    {
        this->negated = value;
    }

    virtual char GetLetter() const
    {
        return this->letter;
    }

    virtual void SetLetter(char value)
    {
        this->letter = value;
    }
};

} }