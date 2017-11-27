(ns clojure-example.example)

(import '(java.io BufferedReader StringReader))

(defmulti parse-line (fn [x y] x))

(defmacro defmapping [name & fields]
  `(defmethod parse-line ~name [x# line#]
     (let [m# {}]
       (for [el# '~fields]
         (let [[start# end# property#] el#]
           (assoc m#
             property#
             (subs line# start# (+ end# 1))))))))

(defmapping ::SVCL
            (4 18 customer-name)
            (19 23 customer-id)
            (24 27 call-type-code)
            (28 35 date-of-call-string))

(defmapping ::USGE
            (4 8 customer-id)
            (9 22 customer-name)
            (30 30 cycle)
            (31 36 read-date))

(def test-lines
  "SVCLFOWLER         10101MS0120050313.........................
SVCLHOHPE          10201DX0320050315........................
SVCLTWO           x10301MRP220050329..............................
USGE10301TWO          x50214..7050329...............................")

(defn process-lines
  [lines]
  (map
    #(parse-line (subs % 0 4) %)
    (line-seq
      (BufferedReader.
        (StringReader. lines)))))

