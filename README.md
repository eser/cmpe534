CMPE534 Automated Deduction
===========================

This repository contains some practical implementations for CMPE534 Automated Deduction course.


Project #1: Deduction
---------------------
**Todos:**

* 'not' operator might have been a composite data structure instead of a lexical symbol.
  if it could store a propositional symbol, simplifications will work more effective.

* 'Properties' class with 'Distribute' method needed, to resolve inside paranthesis.

* Connectives' simplify implementation needs to cover lot more than that.

* Equality comparision for PropositionArrays


**Output:**
``` bash
input      = (((A & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f
output     = (A & B & C) | (!C & D | D) | f
```


License
-------
See [LICENSE](LICENSE)


Contributing
------------
Contributions are welcome

[Eser Ozvataf](http://eser.ozvataf.com/)
