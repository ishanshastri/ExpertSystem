This file contains the multiple elements of the knowledge-base:

1. Facts 
2. Observations
3. Tests (Conditions)
4. Clauses
5. Rules

There are a few additional elements used for the functioning of the knowledge base. In order to evaluate elements (Rules, Clauses and Tests evaluate to either true, false, unkown or undefined; Facts and Observations evaluate to known, unkown or undefined), enums represent the state of a given element, and are contained in a folder to view.

Enums are also used to define operations for evaluation and definition of facts. For example, to check if the value of an integer fact is greater than some given value, the 'GreaterThan' operator is used. A different set of operators are used to define facts (to set the value of a given fact to a given value).

Note that there are different kinds of facts and observations (currently only different kinds of facts). There is also a generic fact being considered at the moment as an option to replace the need for many different kinds of facts to store different types of values (eg. boolean, integer, string, etc.).
