#pragma once

#include <vector>
#include "Domain.h"
#include "IPropositionMemberValuable.h"

namespace DeductionCpp { namespace Abstraction {

class PropositionArray : public IPropositionMemberValuable
{
protected:
    bool negated;

public:
    std::vector<IPropositionMember*> Items;

    static IPropositionMember* CreateInstance(DomainMember& symbolInfo)
    {
        return new PropositionArray();
    }

    PropositionArray(std::vector<IPropositionMember*> items) : IPropositionMemberValuable(), Items(items), negated(negated)
    {
    }

    PropositionArray() : IPropositionMemberValuable(), Items(), negated(negated)
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

    virtual std::string ToString()
    {
        std::string final = "";
        int size = this->Items.size();

        if (size == 0)
        {
            return final;
        }

        if (this->GetNegated())
        {
            final += '!';
        }

        if (size > 1) {
            final += '(';
        }

        for (auto it = this->Items.begin(); it != this->Items.end(); it++) {
            final += (*it)->ToString();
        }

        if (size > 1) {
            final += ')';
        }

        return final;
    }

    virtual DomainMember* GetDomainMember()
    {
        return Domain::Instance().GetMemberBySymbolChar('(');
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