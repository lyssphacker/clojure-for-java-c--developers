(ns clojure-example.example)

(defmulti parse-line (fn [x] x))

(defmacro defmapping [name & fields]
  (let [x-sym (gensym "x")]
    (defmethod parse-line ~name [~x-sym] (name ~name))))

