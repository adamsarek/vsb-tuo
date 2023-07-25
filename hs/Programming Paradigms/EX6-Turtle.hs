-- Homework 5 - Turtle
type Result = [String]

pp :: Result -> IO ()
pp x = putStr (concat (map (++"\n") x))

draw :: [(Char, Int)] -> Result
draw moves = let
        pos = positions (0,0) moves
	    rows = map (fst) pos
	    (minRow,maxRow) = (minimum rows, maximum rows)
	    columns = map (snd) pos
	    (minColumn,maxColumn) = (minimum columns, maximum columns)
    in [[if elem (row,column) pos then 'x' else ' ' |column<-[minColumn..maxColumn]] |row<-[minRow..maxRow]]

positions :: (Int, Int) -> [(Char, Int)] -> [(Int, Int)]
positions _ [] = []
positions (row, column) (m:moves) = let
        pos = oneLine row column m
    in pos ++ positions (last pos) moves

oneLine row column (direction,length)
    | direction == 'l' = [(row, column-offset) |offset<-[1..length]]
    | direction == 'r' = [(row, column+offset) |offset<-[1..length]]
    | direction == 'u' = [(row-offset, column) |offset<-[1..length]]
    | direction == 'd' = [(row+offset, column) |offset<-[1..length]]