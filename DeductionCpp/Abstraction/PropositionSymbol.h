#pragma once

#include "IPropositionMemberValuable.h"

namespace DeductionCpp { namespace Abstraction {

class PropositionSymbol : public IPropositionMemberValuable
{
protected:
    bool negated;
    bool constant;
    char letter;

public:
    static IPropositionMember* CreateInstance(DomainMember& symbolInfo)
    {
        return new PropositionSymbol(symbolInfo.SymbolChar, (symbolInfo.Type == DomainMemberTypes::Constant));
    }

    PropositionSymbol(char letter, bool constant, bool negated = false) : IPropositionMemberValuable(), letter(letter), constant(constant), negated(negated)
    {
    }

    //virtual ~PropositionSymbol()
    //{
    //}

    virtual inline bool operator==(const PropositionSymbol& other)
    {
        return (this->GetLetter() == other.GetLetter() && this->GetNegated() == other.GetNegated());
    }

    virtual std::string ToString()
    {
        std::string final = "";

        if (this->GetNegated())
        {
            final += '!';
        }

        final += this->GetLetter();

        return final;
    }

    virtual DomainMember* GetDomainMember()
    {
        return NULL;
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