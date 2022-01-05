This folder consists of the functionality to verify a proxy knowledge base during the process of deserialization from a proxy knowledge-base to a real knowledge-base. The multiple classes here are used to convey information regarding any encountered errors such that errors can be effectively caught and presented to the user in a meaningful manner. In effect, this is analogous to the error-catching facility of the IDE program, used while writing code. The 'code' in this case is the XML written by the knowledge-engineer to define the elements of a knowledge-base (eg. rules, facts, observations, preamble). 

For an explanation of how proxy and real elements of a knowledge-base work together, here is an explanation. This is repeated from the ReadMe file in the ExpertSystem_UserFacing folder:

Proxy knowledge-bases mirror the real Knowledge-Base, and have proxy versions for each element of the real Knowledge-Base, but the values they hold are only the string identifiers of their real counterparts, and they contian no methods related to the functioning of the knowledge-base, only methods for validation; this is because it is created from an XML file, and then its elements are parsed and used to create real elements.

The purpose of these proxy elements is to enable the serialization and deserialization of the knowledge base. This enables the knowledge engineer (who defines the rules, facts, etc. for a given knowledge-base) to script out a knowledge-base using XML, and then load this knowledge base so that the expert-system can refer to it and function accordingly (eg. a medical diagnosis expert system will have a different knowledge base from a car-mechanic expert-system; they both use the same inference engine, only the contents of their respective knowledge bases are different). 

DESERIALIZATION:

1. XML file is automatically deserialized to create a proxy knowledge base
2. parse and validate the elements in the proxy knowledge base
3. Indicate any errors. If none, then proceed to populate real knowledge base and all its elements

SERIALIZATION:

1. Copy the identifying information of real elements (eg. the name of a fact) into respective proxy elements
2. Create an heirarchy of proxy elements siminar to the real heirarchy of elements (an entire ProxyKnowledgeBase will contain multiple ProxyRules, etc.)
3. Use the XML serializer to write the top-level class (ProxyKnowledgeBase) to an XML file
