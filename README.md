This little example should demostrate what is Clojure and generally all Lisp dialects good for - building domain specific languages. It is motivated by now quite old but probably still relevant [article](https://martinfowler.com/articles/languageWorkbench.html) written by Martin Fowler.

It also illustrates differences in an approach one might have trying to implement the solution in C# or Java (Fowler's article contains solution in C#), and in a Lisp. Typically in C# one might have object-oriented approach of creating reusable classes (like Reader, ReaderStrategy and FieldExtractor) which are then instantiated for every different piece of input data. The other approach might take configuration out of C# and write it into an XML or configuration language with custom syntax.

Common Lisp implementation is done by Rainer Joswig, who described it [here](https://vimeo.com/77280671). It will also contains Clojure implementation.

This very simple example can be used to illustrate to Java or C# programmers, used to class-based OOP style of (mostly) imperative programming, why they might sometimes, when complications in their implementation far exceed the complexity of the problem, consider using a language with syntactic abstractions, like Common Lisp or Clojure.

There is more general point which this example illustrates, on a very basic, but also very vivid level, the one which [SICP](https://mitpress.mit.edu/sicp/) (there are some related quotes [here](https://github.com/lyssphacker/quotes/blob/master/creating-languages.md)) highlights as one of the main approaches at handling complexity - metalingustic abstraction or building languages.

Crucial differences between Lisp and C# approach:
1. In Lisp code is data so what in C# solution is written in configuration file, which is by itself not executable, can in Lisp be made to be executable via macros. "Putting parentheses around data" is approach taken in Joswig's solution. Whether configuration is done in XML or in some other data format, in C# solution there will a need for some library which will help with reading and traversing that data. Approach without configuration file is more straightforward but makes extending the implementation with more data tedious to distanced from the problem domain, since everything has to be expressed in Java.
2. There is no need to create special classes which will hold data for each mapping, like in C# implementation. *defclass* will be executed for the name of each mapping.
3. All the work done by ReaderStrategy and FieldExtractor classes from C# implementation in Lisp implementation is done by parse-line-for-class method. It takes advantage of the fact that *fields* macro argument can be treated as list of lists and can be manipulated accordingly with destructuring, which makes this method very general.

Generally, Lisp implementation is:
1. More concise
2. Easier to extend
3. More general
