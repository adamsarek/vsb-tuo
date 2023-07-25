type Pic = [String]

pic :: Pic
pic = [ "....#....",
        "...###...",
        "..#.#.#..",
        ".#..#..#.",
        "....#....",
        "....#....",
        "....#####"]

pp :: Pic -> IO ()
pp x = putStr (concat (map (++"\n") x))

flipV :: Pic -> Pic
flipV = map reverse 

flipV' :: Pic -> Pic
flipV' xs = [reverse x|x<-xs]

flipH :: Pic -> Pic
flipH = reverse

above :: Pic -> Pic -> Pic
above x y = x ++ y

sideBySide :: Pic -> Pic -> Pic
sideBySide (x:xs) (y:ys) = (x ++ y) : sideBySide xs ys
sideBySide _ _ = []

sideBySide' :: Pic -> Pic -> Pic
sideBySide' = zipWith (++)

rotateR :: Pic -> Pic
rotateR [x] = lineToRow x
rotateR (x:xs) = rotateR xs `sideBySide` lineToRow x

lineToRow :: String -> Pic
lineToRow line = [[ch] |ch<-line]

-- rotateL :: Pic -> Pic


data Point = Point2D Float Float
           | Point3D Float Float Float

-- :i

data Color = White | Black deriving (Show)
