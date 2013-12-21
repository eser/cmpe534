#include "Domain.h"
#include "Connectives/And.h"
#include "Connectives/Or.h"
#include "Connectives/Not.h"
#include "Connectives/Implication.h"
#include "Connectives/Equivalence.h"
#include "Connectives/And.h"
#include "PropositionSymbol.h"
#include "PropositionArray.h"

using namespace std;

namespace DeductionCpp { namespace Abstraction {
    Domain* Domain::DomainInstance = 0;

    Domain::Domain()
    {
        Domain::DomainInstance = this;

        this->Members = std::vector<DomainMember>();
        this->Members.push_back(DomainMember('&', DomainMemberTypes::BinaryConnective, 1, &Connectives::And::CreateInstance));
        this->Members.push_back(DomainMember('|', DomainMemberTypes::BinaryConnective, 2, &Connectives::Or::CreateInstance));
        this->Members.push_back(DomainMember('!', DomainMemberTypes::UnaryConnective, 0, &Connectives::Not::CreateInstance));
        this->Members.push_back(DomainMember('>', DomainMemberTypes::BinaryConnective, 3, &Connectives::Implication::CreateInstance));
        this->Members.push_back(DomainMember('=', DomainMemberTypes::BinaryConnective, 3, &Connectives::Equivalence::CreateInstance));

        this->Members.push_back(DomainMember('f', DomainMemberTypes::Constant, 0, &PropositionSymbol::CreateInstance));
        this->Members.push_back(DomainMember('t', DomainMemberTypes::Constant, 0, &PropositionSymbol::CreateInstance));

        this->Members.push_back(DomainMember('(', DomainMemberTypes::Array, 0, &PropositionArray::CreateInstance));
    }

    Domain& Domain::Instance() {
        if (Domain::DomainInstance == 0)
        {
            new Domain();
            atexit(&Domain::Dispose);
        }

        return *Domain::DomainInstance;
    }

    void Domain::Dispose()
    {
        delete Domain::DomainInstance;
        Domain::DomainInstance = 0;
    }

    DomainMember* Domain::GetMemberBySymbolChar(char symbolChar)
    {
        for (auto it = this->Members.begin(); it != this->Members.end(); it++)
        {
            if (it->SymbolChar == symbolChar)
            {
                return &(*it);
            }
        }

        return NULL;
    }
} }