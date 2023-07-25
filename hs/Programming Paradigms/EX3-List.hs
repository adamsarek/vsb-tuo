isElement :: Eq a => a -> [a] -> Bool
isElement x [] = False
isElement x (y:ys) | x == y = True
                   | otherwise = isElement x ys

getTail :: [a] -> [a]
getTail (x:xs) = xs

getInit :: [a] -> [a]
getInit [x] = []
getInit (x:xs) = x : getInit xs

combine :: [a] -> [a] -> [a]
combine [] y = y
combine (x:xs) y = x : combine xs y

max' :: [Int] -> Int
max' (x:xs) = tmp x xs where
	tmp locMax [] = locMax
	tmp locMax (x:xs) | x >= locMax = tmp x xs
				      | otherwise = tmp locMax xs

max'' :: [Int] -> Int
max'' [x] = x
max'' (x:y:z) | x < y = max'' (y:z)
			  | otherwise = max'' (y:z)

reverse' :: [a] -> [a]
reverse' [] = []
reverse' (x:xs) = reverse' xs `combine` [x]

reverse'' :: [a] -> [a]
reverse'' xs = tmp xs [] where
	tmp [] ys = ys
	tmp (x:xs) ys = tmp xs (x:ys)

take' :: Int -> [a] -> [a]
take' 0 _ = []
take' n [] = []
take' n (x:xs) = x : take' (n-1) xs

drop' :: Int -> [a] -> [a]
drop' 0 xs = xs
drop' n [] = []
drop' n (_:xs) = drop' (n-1) xs

divisors :: Int -> [Int]
divisors n = tmp n where
	tmp 1 = [1]
	tmp c | n `mod` c == 0 = c : tmp (c-1)
		  | otherwise = tmp (c-1)

divisors' :: Int -> [Int]
divisors' n = [x | x <- [1..n], n `mod` x == 0]

-- úkol funkce zipThem, dotProduct, fibonacci, allToUpper, quicksort