package com.eser.ozvataf.deduction.Proposition.Abstraction;

/**
 * Created with IntelliJ IDEA.
 * User: larukedi
 * Date: 13/01/14
 * Time: 05:43
 * To change this template use File | Settings | File Templates.
 */
public class Parenthesis extends Connective {
    public Parenthesis(IMember... parameters) {
        super(parameters);
    }

    @Override
    public int getParameterCount() {
        return 0;
    }

    @Override
    public boolean operation(boolean[] values) {
        return false;
    }
}
