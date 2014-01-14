CMPE534 Automated Deduction
===========================

This repository contains some practical implementations for CMPE534 Automated Deduction course.


Project #1: Deduction
---------------------
This project is designed to be a basic interpreter for proving theorems in propositional logic. It is basically accepts propositional statements in a specific syntax in order to prove its validity. For non-valid statements, all falsifying valuations will be extracted from counter-examples.


**To-dos:**

* 'Properties' class with 'Distribute' method needed, to resolve inside parenthesis.

* more simplifications


**Roadmap:**

* Gentzen G for First-Order Logic

* Resolution ?


**Output:**
``` bash
proposition               = (First | Second) & (A | B) & C
Dumper.Dump()             = (((First | Second) & (A | B)) & C)
Substitutor.Substitute()  = (((First | First) & (A | t)) & C)
Simplifier.Simplify()     = ((First & t) & C)

Deduction tree of: (B > C) & (D | E) -> (A & B), (C | D)

sequent = ((B > C) & (D | E)) -> (A & B), (C | D)
        sequent = (B > C), (D | E) -> C, D, A
                sequent = (D | E) -> B, C, D, A
                        sequent = D -> B, C, D, A
                                  ** axiom node **

                        sequent = E -> B, C, D, A
                                  ** counter-example node **

                sequent = C, (D | E) -> C, D, A
                          ** axiom node **

        sequent = (B > C), (D | E) -> C, D, B
                sequent = (D | E) -> B, C, D, B
                        sequent = D -> B, C, D, B
                                  ** axiom node **

                        sequent = E -> B, C, D, B
                                  ** counter-example node **

                sequent = C, (D | E) -> C, D, B
                          ** axiom node **

Formula is not valid, details below.
+ Counter-examples:
  .. E -> B, C, D, A
  .. E -> B, C, D, B

+ Falsifying valuations:
  .. Valuation #1:
  ..                 E -> t
  ..                 B -> f
  ..                 C -> f
  ..                 D -> f
  ..                 A -> f
  .. Valuation #2:
  ..                 E -> t
  ..                 B -> f
  ..                 C -> f
  ..                 D -> f
```


License
-------
See [LICENSE](LICENSE)


Contributing
------------
Contributions are welcome

[Eser Ozvataf](http://eser.ozvataf.com/)
