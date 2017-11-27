(ns clojure-example.example)

(defmulti parse-line (fn [x y] x))

(defmacro defmapping [name & fields]
  `(defmethod parse-line [~name String] [x# line#]
     (for [el# '~fields]
       (let [[start# end# property#]
             el#]
         (println start#)
         (println end#)
         (println property#)
         ))
     line#))

