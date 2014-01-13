package com.eser.ozvataf.deduction.Proposition.Abstraction;

import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;

/**
 * Created with IntelliJ IDEA.
 * User: larukedi
 * Date: 13/01/14
 * Time: 05:10
 * To change this template use File | Settings | File Templates.
 */
public abstract class Connective implements IMember {
    protected final ArrayList<IMember> parameters;

    public Connective(IMember... parameters) {
        this.parameters = new ArrayList<IMember>();
        for (IMember member : parameters) {
            this.parameters.add(member);
        }
    }

    public ArrayList<IMember> getParameters() {
        return this.parameters;
    }

    public boolean getIsAtomic() {
        return false;
    }

    public abstract int getParameterCount();
    public abstract boolean operation(boolean[] values);

    public IMember Simplify() {
        return null;
    }

    public IMember copyClone() throws CloneNotSupportedException {
        return (IMember)this.clone();
    }

    @Override
    protected Object clone() throws CloneNotSupportedException {
        try {
            Class<?> thisClass = this.getClass();
            Constructor<?> thisConstructor = thisClass.getConstructor(IMember[].class);

            Object[] parameters = new Object[this.getParameterCount()];
            for (int i = 0; i < parameters.length; i++) {
                parameters[i] = this.getParameters().get(i).copyClone();
            }

            return thisConstructor.newInstance(parameters);
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
        if (!(obj instanceof Connective)) {
            return false;
        }

        Connective connective = (Connective)obj;

        int length = this.getParameters().size();
        if (length != connective.getParameters().size()) {
            return false;
        }

        for (int i = length - 1; i >= 0; i--) {
            if (!this.getParameters().get(i).equals(connective.getParameters().get(i))) {
                return false;
            }
        }

        return true;
    }
}
