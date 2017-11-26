(ns clojure-example.example)

(defmacro defmapping [name & fields]
  `(do
     (defmulti parse-line (fn [x] x))
     (defmethod parse-line name [x] name)))

