{-
   Homework 3: Conversion of regular expressions to deterministic finite automaton (1)
   Task: Define a data structure describing regular expressions.
         Create a function converting given regular expression to non-deterministic finite automaton.
         Create a function converting this non-deterministic automaton to its deterministic counterpart.

   Student: Adam Šárek (SAR0083)
-}

import Data.Set (Set)
import qualified Data.Set as S
import Data.List hiding (union)

-- Examples ----------------------------------------------------------------------------------------------------
-- regToDfa (Union (Concat (Literal 'b') (Literal 'a')) (Union Epsilon (Iterate (Literal 'a'))))

-- Solution ----------------------------------------------------------------------------------------------------
data Reg = Epsilon        -- ε
         | Literal Char   -- A
         | Union Reg Reg  -- A+B
         | Concat Reg Reg -- AB
         | Iterate Reg    -- A*
           deriving (Eq)

data Nfa a = NFA (Set a)
                 (Set (Move a))
                 a
                 (Set a)
             deriving (Eq,Show)

data Move a = Move a Char a
            | EpsilonMove a a
              deriving (Eq,Ord,Show)

-- Print ----------------------------------------------------------------------------------------------------
literals :: Reg -> [Char]
literals Epsilon        = []
literals (Literal c)    = [c]
literals (Union rX rY)  = literals rX ++ literals rY
literals (Concat rX rY) = literals rX ++ literals rY
literals (Iterate r)    = literals r

printReg :: Reg -> [Char]
printReg Epsilon        = "_"
printReg (Literal c)    = [c]
printReg (Union rX rY)  = "(" ++ printReg rX ++ "|" ++ printReg rY ++ ")"
printReg (Concat rX rY) = "(" ++ printReg rX        ++ printReg rY ++ ")"
printReg (Iterate r)    = "(" ++ printReg r                        ++ ")*"

instance Show Reg where
    show = printReg

regToDfa :: Reg -> IO ()
regToDfa r = putStr ("--------------------------------------------------\n1) Reg: " ++ printReg r ++ "\n--------------------------------------------------\n" ++
    "2) Nfa:\n" ++ printNfa (build r) ++ "--------------------------------------------------\n" ++
    "3) Dfa:\n" ++ printNfa (makeDeterministic (build r)) ++ "--------------------------------------------------\n")

pp :: Nfa Int -> IO ()
pp (NFA states moves start finish) = putStr (printNfa (NFA states moves start finish))

printNfa :: Nfa Int -> [Char]
printNfa (NFA states moves start finish) = "      - States: " ++ "{" ++ showStates (S.toList states) ++ "}\n" ++
    "      - Moves:\n" ++ concat (map printMove (S.toList moves)) ++ "\n" ++
    "      - Start:  " ++ "[" ++ show start ++ "]" ++ "\n" ++
    "      - Finish: " ++ "{" ++  showStates (S.toList finish) ++ "}\n"

showStates :: [Int] -> [Char]
showStates [x] = "[" ++ show x ++ "]"
showStates (x:xs) = "[" ++ show x ++ "], " ++ showStates xs

printMove :: Move Int -> [Char]
printMove (Move sX c sY) = "                " ++ "[" ++ show sX ++ "]" ++ " --- (" ++ [c] ++ ") --> " ++ "[" ++ show sY ++ "]" ++ "\n"
printMove (EpsilonMove sX sY) = "                " ++ "[" ++ show sX ++ "]" ++ " --- (_) --> " ++ "[" ++ show sY ++ "]" ++ "\n"

-- Reg -> Nfa ----------------------------------------------------------------------------------------------------
matches :: Reg -> String -> Bool
matches Epsilon s        = s == ""
matches (Literal c) s    = s == [c]
matches (Union rX rY) s  = matches rX s || matches rY s
matches (Concat rX rY) s = or [matches rX sX && matches rY sY | (sX,sY) <- splits s]
matches (Iterate r) s    = matches Epsilon s || or [matches r sX && matches (Iterate r) sY | (sX,sY) <- frontSplits s]

splits :: [a] -> [([a],[a])]
splits s = [splitAt i s | i <- [0 .. length s]]

frontSplits :: [a] -> [([a],[a])]
frontSplits s = [splitAt i s | i <- [1 .. length s]]

build :: Reg -> Nfa Int
build Epsilon        = NFA (S.fromList [0..1]) (S.singleton (EpsilonMove 0 1)) 0 (S.singleton 1)
build (Literal c)    = NFA (S.fromList [0..1]) (S.singleton (Move 0 c 1)) 0 (S.singleton 1)
build (Union rX rY)  = mUnion (build rX) (build rY)
build (Concat rX rY) = mConcat (build rX) (build rY)
build (Iterate r)    = mIterate (build r)

mUnion :: Nfa Int -> Nfa Int -> Nfa Int
mUnion (NFA statesX movesX startX finishX)
        (NFA statesY movesY startY finishY) = NFA (statesX' `S.union` statesY' `S.union` newstates)
                                                  (movesX' `S.union` movesY' `S.union` newmoves)
                                                  0
                                                  (S.singleton (mX+mY+1)) where
                                                      mX = S.size statesX
                                                      mY = S.size statesY
                                                      statesX'  = S.map (renumber 1) statesX
                                                      statesY'  = S.map (renumber (mX+1)) statesY
                                                      newstates = S.fromList [0, (mX+mY+1)]
                                                      movesX'   = S.map (renumberMove 1) movesX
                                                      movesY'   = S.map (renumberMove (mX+1)) movesY
                                                      newmoves  = S.fromList [EpsilonMove 0 1, EpsilonMove 0 (mX+1),
                                                                           EpsilonMove mX (mX+mY+1), EpsilonMove (mX+mY) (mX+mY+1)]

mConcat :: Nfa Int -> Nfa Int -> Nfa Int
mConcat (NFA statesX movesX startX finishX)
         (NFA statesY movesY startY finishY) = NFA (statesX `S.union` statesY')
                                                   (movesX `S.union` movesY')
                                                   startX
                                                   finishY' where
                                                       statesY' = S.map (renumber k) statesY
                                                       movesY'  = S.map (renumberMove k) movesY
                                                       finishY' = S.map (renumber k) finishY
                                                       k        = S.size statesX - 1

mIterate :: Nfa Int -> Nfa Int
mIterate (NFA states moves start finish) = NFA (states' `S.union` newstates)
                                                (moves' `S.union` newmoves)
                                                0
                                                (S.singleton (m+1)) where
                                                    m = S.size states
                                                    states'   = S.map (renumber 1) states
                                                    newstates = S.fromList [0, m+1]
                                                    moves'    = S.map (renumberMove 1) moves
                                                    newmoves  = S.fromList [EpsilonMove 0 1, EpsilonMove m 1, EpsilonMove 0 (m+1), EpsilonMove m (m+1)]

renumber :: Int -> Int -> Int
renumber n st = n + st

renumberMove :: Int -> Move Int -> Move Int
renumberMove k (Move sX c sY) = Move (renumber k sX) c (renumber k sY)
renumberMove k (EpsilonMove sX sY) = EpsilonMove (renumber k sX) (renumber k sY)

closure :: Ord a => Nfa a -> Set a -> Set a
closure (NFA states moves start term) = setlimit add where
                                            add stateset = S.union stateset (S.fromList accessible) where
                                                                accessible = [s | x <- S.toList stateset,
                                                                                  EpsilonMove y s <- S.toList moves,
                                                                                  y==x]

setlimit :: Eq a => (Set a -> Set a) -> Set a -> Set a
setlimit f s
    | s == next = s
    | otherwise = setlimit f next where
        next = f s

onetrans :: Ord a => Nfa a -> Char -> Set a -> Set a
onetrans mach c x = closure mach (onemove mach c x)

onemove :: Ord a => Nfa a -> Char -> Set a -> Set a
onemove (NFA states moves start term) c x = S.fromList [s | t <- S.toList x,
                                                         Move z d s <- S.toList moves,
                                                         z==t, c==d]

startstate :: Nfa a -> a
startstate (NFA states moves start final) = start

alphabet :: Nfa a -> [Char]
alphabet (NFA s moves st f) = nub [c | Move s c t <- S.toList moves]

-- Nfa -> Dfa ----------------------------------------------------------------------------------------------------
makeDeterministic :: Nfa Int -> Nfa Int
makeDeterministic = number . makeDeter

number :: Nfa (Set Int) -> Nfa Int
number (NFA states moves start finish) = NFA states' moves' start' finish' where
                                             statelist   = S.toList states
                                             lookup l a  = look 0 l a
                                             look n [] a = error "lookup"
                                             look n (b:y) a
                                                 | b==a      = n
                                                 | otherwise = look (n+1) y a
                                             change      = lookup statelist
                                             states'     = S.map change states
                                             moves'      = S.map newmove moves where
                                                 newmove (Move s c t)      = Move (change s) c (change t)
                                                 newmove (EpsilonMove s t) = EpsilonMove (change s) (change t)
                                             start'      = change start
                                             finish'     = S.map change finish

makeDeter :: Nfa Int -> Nfa (Set Int)
makeDeter mach = deterministic mach (alphabet mach)

deterministic :: Nfa Int -> [Char] -> Nfa (Set Int)
deterministic mach alpha = nfaLimit (addstep mach alpha) startmach where
                               startmach                = NFA (S.singleton starter) S.empty starter finish
                               starter                  = closure mach (S.singleton start)
                               finish
                                   | (term `S.intersection` starter) == S.empty = S.empty
                                   | otherwise                       = S.singleton starter
                               (NFA sts mvs start term) = mach

addstep :: Nfa Int -> [Char] -> Nfa (Set Int) -> Nfa (Set Int)
addstep mach alpha dfa = add_aux mach alpha dfa (S.toList states) where
                             (NFA states m s f)               = dfa
                             add_aux mach alpha dfa []        = dfa
                             add_aux mach alpha dfa (st:rest) = add_aux mach alpha (addmoves mach st alpha dfa) rest

addmoves :: Nfa Int -> Set Int -> [Char] -> Nfa (Set Int) -> Nfa (Set Int)
addmoves mach x [] dfa    = dfa
addmoves mach x (c:r) dfa = addmoves mach x r (addmove mach x c dfa)

addmove :: Nfa Int -> Set Int -> Char -> Nfa (Set Int) -> Nfa (Set Int)
addmove mach x c (NFA states moves start finish) = NFA states' moves' start finish' where
                                                       states'          = states `S.union` S.singleton new
                                                       moves'           = moves `S.union` S.singleton (Move x c new)
                                                       finish'
                                                           | S.empty /= (term `S.intersection` new) = finish `S.union` S.singleton new
                                                           | otherwise                   = finish
                                                       new              = onetrans mach c x
                                                       (NFA s m q term) = mach

nfaLimit :: Eq a => (Nfa a -> Nfa a) -> Nfa a -> Nfa a
nfaLimit f n
    | nfaEq n next = n
    | otherwise       = nfaLimit f next where
          next                      = f n
          nfaEq (NFA sX nX stX fX)
                 (NFA sY nY stY fY) = sX == sY && nX == nY && stX == stY && fX == fY