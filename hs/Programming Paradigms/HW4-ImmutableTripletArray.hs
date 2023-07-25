{-
   Homework 4: Immutable array of triplets
   Task: Addresses and data are structured in triplets. In the deepest layer are the actual data.
         1) Array is immutable so the size is defined at start (unused space in triplet is left empty).
         2) It is possible to get n-th value (max. complexity of n). It should search for n-th branch.
         3) It is possible to set n-th value by taking old array + item and returning new array.
         4) It can be written in any functional language (even C#, if we set parameters at start and don't change them later).

   Student: Adam Šárek (SAR0083)
-}

-- Examples ----------------------------------------------------------------------------------------------------
-- arrayZ
-- getItem arrayZ 21
-- setItem arrayZ 21 '#'
-- getItem (setItem arrayZ 21 '#') 21

-- Solution ----------------------------------------------------------------------------------------------------
-- Immutable arrays
arrayX = DataTriplet ('a','b','c')
arrayY = AddressTriplet (DataTriplet ('a','b','c'),
                         DataTriplet ('d','e','f'),
                         DataTriplet ('g','h','i'))
arrayZ = AddressTriplet (AddressTriplet (DataTriplet ('a','b','c'),
                                         DataTriplet ('d','e','f'),
                                         DataTriplet ('g','h','i')),
                         AddressTriplet (DataTriplet ('j','k','l'),
                                         DataTriplet ('m','n','o'),
                                         DataTriplet ('p','q','r')),
                         AddressTriplet (DataTriplet ('s','t','u'),
                                         DataTriplet ('v','w','x'),
                                         DataTriplet ('y','z','!')))

-- Type / Data
type Data = Char
type DataTriplet = (Data,Data,Data)
data Triplet = AddressTriplet (Triplet,Triplet,Triplet)
             | DataTriplet (Data,Data,Data)
               deriving (Eq)
instance Show Triplet where
    show (AddressTriplet (a,b,c)) = "( " ++ show a ++ "," ++ show b ++ "," ++ show c ++ " )"
    show (DataTriplet (a,b,c)) = "(" ++ show a ++ "," ++ show b ++ "," ++ show c ++ ")"

-- Get triplet item
getItem :: Triplet -> Int -> Data
getItem t n = getItemData t n (getSize t)

getItemData :: Triplet -> Int -> Int -> Data
getItemData (AddressTriplet t) n size = getItemData (getItemDataTriplet (AddressTriplet t) n size) (n `mod` 3) 3
getItemData (DataTriplet (a,b,c)) n _ | n == 0 = a
                                      | n == 1 = b
                                      | n == 2 = c

getItemDataTriplet :: Triplet -> Int -> Int -> Triplet
getItemDataTriplet (AddressTriplet (a,b,c)) n size | (fromIntegral n / fromIntegral size) < (1/3) = getItemDataTriplet a n (size `div` 3)
                                                   | (fromIntegral n / fromIntegral size) < (2/3) = getItemDataTriplet b (n - (size `div` 3)) (size `div` 3)
                                                   | (fromIntegral n / fromIntegral size) < 1     = getItemDataTriplet c (n - (size `div` 3) * 2) (size `div` 3)
                                                   | otherwise                                    = getItemDataTriplet (AddressTriplet (a,b,c)) (n `mod` size) size -- Rotate for n > size - 1
getItemDataTriplet (DataTriplet t) _ _ = DataTriplet t

-- Set triplet item
setItem :: Triplet -> Int -> Data -> Triplet
setItem t n x = setItemData t n (getSize t) x

setItemData :: Triplet -> Int -> Int -> Data -> Triplet
setItemData (AddressTriplet (a,b,c)) n size x | (fromIntegral n / fromIntegral size) < (1/3) = AddressTriplet ((setItemData a n (size `div` 3) x),b,c)
                                              | (fromIntegral n / fromIntegral size) < (2/3) = AddressTriplet (a,(setItemData b (n - (size `div` 3)) (size `div` 3) x),c)
                                              | (fromIntegral n / fromIntegral size) < 1     = AddressTriplet (a,b,(setItemData c (n - (size `div` 3) * 2) (size `div` 3) x))
                                              | otherwise                                    = setItemData (AddressTriplet (a,b,c)) (n `mod` size) size x -- Rotate for n > size - 1
setItemData (DataTriplet (a,b,c)) n size x | n == 0 = DataTriplet (x,b,c)
                                           | n == 1 = DataTriplet (a,x,c)
                                           | n == 2 = DataTriplet (a,b,x)

-- Get triplet size
getSize :: Triplet -> Int
getSize t = getSize' t 0

getSize' :: Triplet -> Int -> Int
getSize' (AddressTriplet (a,_,_)) e = getSize' a (e+1)
getSize' (DataTriplet _) e          = 3 ^ (e+1)