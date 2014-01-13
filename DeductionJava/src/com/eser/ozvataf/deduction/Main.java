package com.eser.ozvataf.deduction;

import com.eser.ozvataf.deduction.Proposition.Abstraction.*;
import com.eser.ozvataf.deduction.Proposition.Parsing.*;

public class Main {

    public static void main(String[] args) {
        Registry registry = new Registry();
        registry.addMembers(
            new RegistryMember() {{ symbolChar = "!"; type = Connective.class; precedence = 1; isRightAssociative = true; aliases = new String[] { "not" }; }},
            new RegistryMember() {{ symbolChar = "&"; type = Connective.class; precedence = 4; aliases = new String[] { "and" }; }},
            new RegistryMember() {{ symbolChar = "|"; type = Connective.class; precedence = 5; aliases = new String[] { "or" }; }},
            new RegistryMember() {{ symbolChar = ">"; type = Connective.class; precedence = 6; aliases = new String[] { "implies" }; }},
            new RegistryMember() {{ symbolChar = "="; type = Connective.class; precedence = 6; aliases = new String[] { "equals" }; }},

            new RegistryMember() {{ symbolChar = "["; type = Parenthesis.class; precedence = 101; closesWith = "]"; }},
            new RegistryMember() {{ symbolChar = "("; type = Parenthesis.class; precedence = 101; closesWith = ")"; }},

            new RegistryMember() {{ symbolChar = "f"; type = Constant.class; precedence = 0; value = false; aliases = new String[] { "0", "false" }; }},
            new RegistryMember() {{ symbolChar = "t"; type = Constant.class; precedence = 0; value = true; aliases = new String[] { "1", "true" }; }}
        );
    }
}
