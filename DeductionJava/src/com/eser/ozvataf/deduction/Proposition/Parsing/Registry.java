package com.eser.ozvataf.deduction.Proposition.Parsing;

import java.util.*;

/**
 * Created with IntelliJ IDEA.
 * User: larukedi
 * Date: 13/01/14
 * Time: 06:18
 * To change this template use File | Settings | File Templates.
 */
public class Registry {
    protected final ArrayList<RegistryMember> members;

    public Registry() {
        this.members = new ArrayList<RegistryMember>();
    }

    public ArrayList<RegistryMember> getMembers() {
        return this.members;
    }

    public void addMembers(RegistryMember... members) {
        for (RegistryMember registryMember : members) {
            this.getMembers().add(registryMember);
        }
    }

    public RegistryMember getMemberByType(Class<?> type) {
        for (RegistryMember registryMember : this.getMembers()) {
            if (registryMember.getType().equals(type)) {
                return registryMember;
            }
        }

        return null;
    }

    public RegistryMember getMemberBySymbolChar(String symbolChar) {
        for (RegistryMember registryMember : this.getMembers()) {
            if (registryMember.getSymbolChar().equals(symbolChar)) {
                return registryMember;
            }

            if (registryMember.getAliases() != null) {
                for (String alias : registryMember.getAliases()) {
                    if (alias.equals(symbolChar)) {
                        return registryMember;
                    }
                }
            }
        }

        return null;
    }
}
