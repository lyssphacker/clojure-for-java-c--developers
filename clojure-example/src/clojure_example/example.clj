(ns clojure-example.example)

(defmacro defmapping [name mapping & fields]
  `(do
     (println name)
     (println ~mapping)
     (println '~@fields)))