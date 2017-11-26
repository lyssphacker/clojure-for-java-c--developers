(ns clojure-example.example)

(defmacro defmapping [name & fields]
  (let [x-sym (gensym "x")]
    `(do
       (defmulti parse-line (fn [~x-sym] ~x-sym))
       (defmethod parse-line ~name [~x-sym] (name ~name)))))

