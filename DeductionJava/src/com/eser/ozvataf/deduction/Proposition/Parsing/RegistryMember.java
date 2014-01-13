package com.eser.ozvataf.deduction.Proposition.Parsing;

/**
 * Created with IntelliJ IDEA.
 * User: larukedi
 * Date: 13/01/14
 * Time: 06:18
 * To change this template use File | Settings | File Templates.
 */
public class RegistryMember {
    public String symbolChar;
    public Class<?> type;
    public int precedence;
    public int parameters;
    public Boolean value;
    public String[] aliases;
    public String closesWith;
    public boolean isRightAssociative;

    public RegistryMember() {
    }

    public String getSymbolChar() {
        return this.symbolChar;
    }

    public Class<?> getType() {
        return this.type;
    }

    public int getPrecedence() {
        return this.precedence;
    }

    public int getParameters() {
        return this.parameters;
    }

    public Boolean getValue() {
        return this.value;
    }

    public String[] getAliases() {
        return this.aliases;
    }

    public String getClosesWith() {
        return this.closesWith;
    }

    public boolean isRightAssociative() {
        return this.isRightAssociative;
    }
}
