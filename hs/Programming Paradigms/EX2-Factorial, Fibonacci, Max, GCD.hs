import Distribution.Simple.Utils (xargs)
factorial :: Int -> Int
factorial 0 = 1
factorial n = n * factorial (n-1)

fib :: Int -> Int
fib 0 = 1
fib 1 = 1
fib n = fib (n-1) + fib (n-2)

fib2 n = step 1 1 n where
    step a b 1 = b
    step a b n = step b (a+b) (n-1)

leapYear :: Int -> Bool
leapYear y = y `mod` 400 == 0 || y `mod` 4 == 0 && y `mod` 100 /= 100

leapYear2 y | y `mod` 400 == 0 = True
            | y `mod` 100 == 0 = False
            | otherwise = y `mod` 400 == 0

max2 :: Int -> Int -> Int
max2 x y | x >= y = x
         | otherwise = y

--max2 x y = if x < y then y else x

max3 :: Int -> Int -> Int -> Int
max3 x y z | x >= y && x >= z = x
           | y >= x && y >= z = y
           | otherwise = z

--max3 x y z = max2 (max2 x y) z

gcd' :: Int -> Int -> Int
gcd' a b | a == b = a
         | a < b = gcd' a (b-a)
         | a > b = gcd' (a-b) b

isPrime :: Int -> Bool
isPrime n = test n (n-1) where
	test n 1 = True
    test n c | n `mod` c == 0 = False
             | otherwise = test n (c-1)

isPrime :: Int -> Bool
isPrime n = test n (truncate (sqrt (fromIntegral n))) where
	test::Int -> Int -> Bool
	test n 1 = True
    test n c | n `mod` c == 0 = False
             | otherwise = test n (c-1)

isPrime :: Int -> Bool
isPrime n = test (truncate (sqrt (fromIntegral n))) where
	test::Int -> Bool
	test 1 = True
    test c | n `mod` c == 0 = False
           | otherwise = test (c-1)

sumIt :: [Int] -> Int
sumIt [] = 0
sumIt (x:xs) = x + sumIt xs

getHead :: [a] -> a
getHead (x:xs) = x

getLast :: [a] -> a
getLast [x] = x
getLast (x:xs) = getLast xs

-- dodělat FP Laboratory 3 funkce