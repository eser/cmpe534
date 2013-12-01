CMPE534 Automated Deduction
===========================

This repository contains some practical implementations for CMPE534 Automated Deduction course.


Project #1: Deduction
---------------------
**To-dos:**

* 'not' operator might have been a composite data structure instead of a lexical symbol.
  if it could store a propositional symbol, simplifications will work more effective.

* 'Properties' class with 'Distribute' method needed, to resolve inside parenthesis.

* Connectives' simplify implementation needs to cover lot more than that.

* Equality comparison for PropositionArrays

* Evaluator.Evaluate


**Roadmap:**

* Gentzen

* Resolution


**Output:**
``` bash
proposition = (((A & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f
simplified  = (A & B & C) | (!C & D | D) | f
evaluated   = (t & f & t) | (!t & f | f) | f
```


License
-------
See [LICENSE](LICENSE)


Contributing
------------
Contributions are welcome

[Eser Ozvataf](http://eser.ozvataf.com/)
