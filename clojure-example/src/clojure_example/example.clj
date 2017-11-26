(ns clojure-example.example)

(defmulti parse-line (fn [x] x))

(defmacro defmapping [name & fields]
  `(defmethod parse-line ~name [x#] ~@fields))

