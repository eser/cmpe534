CMPE534 Automated Deduction
===========================

This repository contains some practical implementations for CMPE534 Automated Deduction course.


Project #1: Deduction
---------------------
**To-dos:**

* 'Properties' class with 'Distribute' method needed, to resolve inside parenthesis.

* Equality comparison for PropositionArrays

* Evaluation needs to cover all connective operations.

* Merge all connective operations with precedence, and do it with BinaryConnectiveBase.Operation()


**Roadmap:**

* Gentzen

* Resolution


**Output:**
``` bash
proposition = (((A & A) & B) & (B & C)) | (!C & D | D | D) | !!!(!f) | f | t & D
simplified  = (A & B & C) | (!C & D | D) | f | t & D
assigned    = (t & f & t) | (!t & D | D) | f | t & D
evaluated   = D
```


License
-------
See [LICENSE](LICENSE)


Contributing
------------
Contributions are welcome

[Eser Ozvataf](http://eser.ozvataf.com/)
