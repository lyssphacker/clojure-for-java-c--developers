(ns clojure-example.example)

(defmulti parse-line (fn [x y] x))

(defmacro defmapping [name & fields]
  `(defmethod parse-line ~name [x# line#]
     (let [m# {}]
       (for [el# '~fields]
         (let [[start# end# property#] el#]
           (assoc m#
             property#
             (subs line# start# (+ 1 end#))))))))

