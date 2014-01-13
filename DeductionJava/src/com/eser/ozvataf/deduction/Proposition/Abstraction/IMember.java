package com.eser.ozvataf.deduction.Proposition.Abstraction;

/**
 * Created with IntelliJ IDEA.
 * User: larukedi
 * Date: 13/01/14
 * Time: 04:52
 * To change this template use File | Settings | File Templates.
 */
public interface IMember extends Cloneable {
    boolean getIsAtomic();

    IMember copyClone() throws CloneNotSupportedException;
}
