(ns clojure-example.example)

(defmulti parse-line (fn [x] x))

(defmacro defmapping [name & fields]
  `(defmethod parse-line ~name [x#]
     (for [el# '~fields]
       (let [[start# end# property#]
             el#]
         (println start#)
         (println end#)
         (println property#)
         ))))

