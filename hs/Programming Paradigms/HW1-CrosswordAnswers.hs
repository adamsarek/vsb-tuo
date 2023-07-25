{-
   Homework 1: Crossword Answers (2 | 2021/2022)
   Task: Implement the function answers that takes as an input a crossword puzzle's solution
         and outputs all words from this solution. Words will be divided into two lists,
         first for lines (from left to right) and second for columns (from top to bottom).
         A word is written only if it is longer than just one character.
         TIP: Use the function words :: String -> [String] to split lines into sequence of words.

   Student: Adam Šárek (SAR0083)
-}

type Result = [String]

pp :: Result -> IO ()
pp x = putStr (concat (map (++"\n") x))

toRow :: String -> Result
toRow xs = map (\x -> [x]) xs -- [[x]|x<-xs]

rotateL :: Result -> Result
rotateL [x] = reverse(toRow x)
rotateL (x:xs) = reverse(toRow x) `sideBySide` (rotateL xs)

sideBySide :: Result -> Result -> Result
sideBySide = zipWith (++)

-- Solution
solution1 = ["DAD  SEND",
             "O EAST  A",
             "W A  ITSY",
             "NERF N T ",
             " A ARK U ",
             " S T SYNC",
             "MESH  A A",
             "A  EVER R",
             "NEAR  D D"]

answers :: Result -> ([String],[String])
answers x = (rowAnswers x, colAnswers x)

rowAnswers :: [String] -> [String]
rowAnswers x = [word | word <- words (unwords x), length word > 1]

colAnswers :: [String] -> [String]
colAnswers x = rowAnswers (reverse (rotateL x))