(defmacro defmapping (name mapping &body fields)
  `(progn
     (defclass ,name ()
       ,(loop for (nil nil slot) in fields
              collect slot))
     (defmethod find-mapping-class-name ((mapping (eql ',(intern mapping))))
       ',name)
     (defmethod parse-line-for-class (line (class-name (eql ',name)))

       (let ((object (make-instance class-name)))
         (loop for (start end slot) in ',fields
               do (setf (slot-value object slot)
                        (subseq line start (1+ end))))
         object))))

(defmapping service-call "SVCL"
  (04 18 customer-name)
  (19 23 customer-id)
  (24 27 call-type-code)
  (28 35 date-of-call-string))

(defmapping usage "USGE"
  (04 08 customer-id)
  (09 22 customer-name)
  (30 30 cycle)
  (31 36 read-date))

(defparameter *test-lines*
  "SVCLFOWLER         10101MS0120050313.........................
SVCLHOHPE          10201DX0320050315........................
SVCLTWO           x10301MRP220050329..............................
USGE10301TWO          x50214..7050329...............................")

;; expression to parse *test-lines*
(map nil 'describe
     (with-input-from-string (stream *test-lines*)
       (loop for line = (read-line stream nil nil)
             while line
             collect (parse-line-for-class line (find-mapping-class-name
                                                 (intern (subseq line 0 4)))))))
