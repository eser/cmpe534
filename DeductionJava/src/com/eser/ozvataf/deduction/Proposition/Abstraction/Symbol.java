package com.eser.ozvataf.deduction.Proposition.Abstraction;

import java.lang.String;
import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;

/**
 * Created with IntelliJ IDEA.
 * User: larukedi
 * Date: 13/01/14
 * Time: 04:52
 * To change this template use File | Settings | File Templates.
 */
public class Symbol implements IMember {
    protected final String letter;

    public Symbol(String letter) {
        this.letter = letter;
    }

    public String getLetter() {
        return this.letter;
    }

    public boolean getIsAtomic() {
        return true;
    }

    public IMember copyClone() throws CloneNotSupportedException {
        return (IMember)this.clone();
    }

    @Override
    protected Object clone() throws CloneNotSupportedException {
        try {
            Class<?> thisClass = this.getClass();
            Constructor<?> thisConstructor = thisClass.getConstructor(String.class);

            return thisConstructor.newInstance(new Object[] { this.getLetter() });
        }
        catch (InstantiationException ex) {
            throw new CloneNotSupportedException("InstantiationException: " + ex.getMessage());
        }
        catch (NoSuchMethodException ex) {
            throw new CloneNotSupportedException("NoSuchMethodException: " + ex.getMessage());
        }
        catch (IllegalAccessException ex) {
            throw new CloneNotSupportedException("IllegalAccessException: " + ex.getMessage());
        }
        catch (InvocationTargetException ex) {
            throw new CloneNotSupportedException("InvocationTargetException: " + ex.getMessage());
        }
    }

    @Override
    public boolean equals(Object obj) {
        if (!(obj instanceof Symbol)) {
            return false;
        }

        Symbol symbol = (Symbol)obj;
        return (this.getLetter() == symbol.getLetter());
    }
}
