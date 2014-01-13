package com.eser.ozvataf.deduction.Proposition.Abstraction;

import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;

/**
 * Created with IntelliJ IDEA.
 * User: larukedi
 * Date: 13/01/14
 * Time: 05:47
 * To change this template use File | Settings | File Templates.
 */
public class Constant extends Symbol {
    protected final boolean value;

    public Constant(String letter, boolean value) {
        super(letter);
        this.value = value;
    }

    public boolean getValue()
    {
        return this.value;
    }

    @Override
    protected Object clone() throws CloneNotSupportedException {
        try {
            Class<?> thisClass = this.getClass();
            Constructor<?> thisConstructor = thisClass.getConstructor(String.class, boolean.class);

            return thisConstructor.newInstance(new Object[] { this.getLetter(), this.getValue() });
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
}
