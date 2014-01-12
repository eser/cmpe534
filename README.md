CMPE534 Automated Deduction
===========================

This repository contains some practical implementations for CMPE534 Automated Deduction course.


Project #1: Deduction
---------------------
**To-dos:**

* 'Properties' class with 'Distribute' method needed, to resolve inside parenthesis.

* Evaluation needs to cover all connective operations.

* Merge all connective operations with precedence, and do it with BinaryConnectiveBase.Operation()

* NOT simplifications


**Roadmap:**

* Gentzen G for First-Order Logic

* Resolution ?


**Output:**
``` bash
proposition               = (First | Second) & (A | B) & C
Dumper.Dump()             = (((First | Second) & (A | B)) & C)
Substitutor.Substitute()  = (((First | First) & (A | t)) & C)
Simplifier.Simplify()     = ((First & t) & C)

sequent #1                = (B | C), (D | E) -> (A & B), (C | D)
sequent #1.isAxiom()      = False
sequent #1.isAtomic()     = False

        sequent #2                = B, (D | E) -> C, D, (A & B)
        sequent #2.isAxiom()      = False
        sequent #2.isAtomic()     = False

                sequent #3                = D, B -> C, D, (A & B)
                sequent #3.isAxiom()      = True
                sequent #3.isAtomic()     = False

                sequent #4                = E, B -> C, D, (A & B)
                sequent #4.isAxiom()      = False
                sequent #4.isAtomic()     = False

                        sequent #5                = E, B -> A, C, D
                        sequent #5.isAxiom()      = False
                        sequent #5.isAtomic()     = True

                        sequent #6                = E, B -> B, C, D
                        sequent #6.isAxiom()      = True
                        sequent #6.isAtomic()     = True

        sequent #7                = C, (D | E) -> C, D, (A & B)
        sequent #7.isAxiom()      = True
        sequent #7.isAtomic()     = False
```


License
-------
See [LICENSE](LICENSE)


Contributing
------------
Contributions are welcome

[Eser Ozvataf](http://eser.ozvataf.com/)
