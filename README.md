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

* NOT simplifications


**Roadmap:**

* Gentzen

* Resolution


**Output:**
``` bash
proposition  = (First | Second) & (A | B) & C
dumped root  = (((First | Second) & (A | B)) & C)
assigned     = (((First | First) & (A | t)) & C)
simplified   = ((First & t) & C)
```


License
-------
See [LICENSE](LICENSE)


Contributing
------------
Contributions are welcome

[Eser Ozvataf](http://eser.ozvataf.com/)
