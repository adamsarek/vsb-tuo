module Stack(Stack, emptyS, push, pop, top, isEmpty) where
  data Stack a = Stack [a] deriving Show

  emptyS :: Stack a
  emptyS = Stack []

  push :: a -> Stack a -> Stack a
  push x (Stack y) = Stack (x:y)
  
  pop :: Stack a -> Stack a
  pop (Stack (_:xs)) = Stack xs

  top :: Stack a -> a
  top (Stack (x:_)) = x

  isEmpty :: Stack a ->Bool
  isEmpty (Stack []) = True
  isEmpty _ = False

  -- DÚ zkusit si queue (lab 10) + lab 11