import Data.List (sortBy)
huffman :: [(Char, Int)] -> [(Char, String)]
huffman xs = tmp [(x,[(ch,"")]) |(ch, x) <- xs]

tmp :: [(Int, [(Char, String)])] -> [(Char, String)]
tmp list =
    let
        sorted = sortBy (\ (x1,_) (x2,_) -> compare x1 x2) list
        add number xs = [(ch,number:code) |(ch,code)<-xs]
        join [(_,x)] = x
        join ((a1,a2):(b1,b2):xs) = tmp ((a1+b1, add '0' a2 ++ add '1' b2) : xs)
    in join sorted