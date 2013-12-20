#include "SymbolRegistry.h"

using namespace std;

namespace DeductionCpp { namespace Parsing {
    SymbolRegistry* SymbolRegistry::SymbolRegistryInstance = 0;

    SymbolRegistry::SymbolRegistry()
    {
        SymbolRegistry::SymbolRegistryInstance = this;

        this->Connectives = std::map<char, DeductionCpp::Abstraction::PropositionMemberTypes>();
        this->Constants = std::map<char, DeductionCpp::Abstraction::PropositionMemberTypes>();
    }

    SymbolRegistry& SymbolRegistry::Instance() {
        if (!SymbolRegistry::SymbolRegistryInstance)
        {
            new SymbolRegistry();
            atexit(&SymbolRegistry::Dispose);
        }

        return *SymbolRegistry::SymbolRegistryInstance;
    }

    void SymbolRegistry::Dispose()
    {
        delete SymbolRegistry::SymbolRegistryInstance;
        SymbolRegistry::SymbolRegistryInstance = 0;
    }

    char SymbolRegistry::GetConnectiveSymbol(DeductionCpp::Abstraction::PropositionMemberTypes type)
    {
        SymbolRegistry& instance = SymbolRegistry::Instance();

        for (auto it = instance.Connectives.begin(); it != instance.Connectives.end(); it++)
        {
            if (it->second == type)
            {
                return it->first;
            }
        }

        return NULL;
    }

    char SymbolRegistry::GetConstantSymbol(DeductionCpp::Abstraction::PropositionMemberTypes type)
    {
        SymbolRegistry& instance = SymbolRegistry::Instance();

        for (auto it = instance.Constants.begin(); it != instance.Constants.end(); it++)
        {
            if (it->second == type)
            {
                return it->first;
            }
        }

        return NULL;
    }
} }