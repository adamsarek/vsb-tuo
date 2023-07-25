{-
   Homework 2: Poker (4)
   Task: Lets define a data types representing a deck of Poker cards.
         In poker, a player gets 5 cards into the hand.
		 Dealt hands are classified into several categories.
		 These categories are important to define who is the winner.
		 Rules for each category can be found here: https://cs.wikipedia.org/wiki/Poker
		 Write a function decide that takes 5 dealt cards - Hand and returns a poker category in which it fits.

   Student: Adam Šárek (SAR0083)
-}

import Data.List

type Result = [String]

pp :: Result -> IO ()
pp x = putStr (concat (map (++"\n") x))

-- Poker Hand Rankings (https://www.fgbradleys.com/images/poker_hands.gif)
-- Royal Flush     : decide [(Card (Numeric 10) Hearts),(Card Jack Hearts),(Card Queen Hearts),(Card King Hearts),(Card Ace Hearts)]
-- Straight Flush  : decide [(Card (Numeric 5) Diamonds),(Card (Numeric 6) Diamonds),(Card (Numeric 7) Diamonds),(Card (Numeric 8) Diamonds),(Card (Numeric 9) Diamonds)]
-- Four of a Kind  : decide [(Card Jack Clubs),(Card Jack Hearts),(Card Jack Spades),(Card Jack Diamonds),(Card Ace Spades)]
-- Full House      : decide [(Card (Numeric 10) Clubs),(Card (Numeric 10) Spades),(Card (Numeric 10) Diamonds),(Card (Numeric 2) Spades),(Card (Numeric 2) Hearts)]
-- Flush           : decide [(Card King Clubs),(Card (Numeric 10) Clubs),(Card (Numeric 9) Clubs),(Card (Numeric 7) Clubs),(Card (Numeric 6) Clubs)]
-- Straight        : decide [(Card (Numeric 2) Clubs),(Card (Numeric 3) Hearts),(Card (Numeric 4) Diamonds),(Card (Numeric 5) Clubs),(Card (Numeric 6) Spades)]
-- Three of a Kind : decide [(Card (Numeric 8) Clubs),(Card (Numeric 8) Spades),(Card (Numeric 8) Hearts),(Card Jack Spades),(Card (Numeric 5) Hearts)]
-- Two Pair        : decide [(Card Queen Clubs),(Card Queen Spades),(Card (Numeric 4) Spades),(Card (Numeric 4) Hearts),(Card Ace Clubs)]
-- Pair            : decide [(Card King Spades),(Card King Diamonds),(Card (Numeric 6) Hearts),(Card (Numeric 2) Diamonds),(Card (Numeric 3) Clubs)]
-- High Card       : decide [(Card Ace Diamonds),(Card (Numeric 9) Spades),(Card (Numeric 7) Spades),(Card (Numeric 5) Spades),(Card (Numeric 3) Diamonds)]

-- Solution
data Suit = Hearts | Clubs | Diamonds | Spades deriving (Eq, Show)
data Rank = Numeric Int | Jack | Queen | King | Ace deriving (Eq, Show)
data Card = Card Rank Suit deriving (Eq, Show)
type Hand = [Card]
data Category = RoyalFlush                   -- Straight Flush that has a high card rank of Ace.
              | StraightFlush                -- A five card sequence of the same suit.
              | Four                         -- All four cards of the same rank.
              | FullHouse                    -- Three of a kind combined with a pair.
              | Flush                        -- Any five cards of the same suit, but not in sequence.
              | Straight                     -- Five cards in sequence of mixed suits.
              | Three                        -- Three cards with the same rank.
              | TwoPair                      -- Two sets of equal rank cards.
              | Pair                         -- Two cards of equal rank.
              | HighCard deriving (Eq, Show) -- A hand with no other combination.



instance Ord Card where
    compare x y = compare (cardRank x) (cardRank y)

decide :: Hand -> Category
decide hand = decide' (reverse (sort hand))

decide' :: Hand -> Category
decide' hand | isRoyalFlush hand    = RoyalFlush
             | isStraightFlush hand = StraightFlush
             | isFour hand          = Four
             | isFullHouse hand     = FullHouse
             | isFlush hand         = Flush
             | isStraight hand      = Straight
             | isThree hand         = Three
             | isTwoPair hand       = TwoPair
             | isPair hand          = Pair
             | otherwise            = HighCard

isRoyalFlush :: Hand -> Bool
isRoyalFlush (x:y:xs) = isStraightFlush (x:y:xs) && cardRank x == 14 && cardRank y == 13 -- High Ace + King

isStraightFlush :: Hand -> Bool
isStraightFlush hand = isFlush hand && isStraight hand

isFour :: Hand -> Bool
isFour hand = head (handGroupLengths hand) == 4

isFullHouse :: Hand -> Bool
isFullHouse hand = head (handGroupLengths hand) == 3 && handGroupLengths hand !! 1 == 2

isFlush :: Hand -> Bool
isFlush [x] = True
isFlush (x:y:xs) | cardSuit x == cardSuit y = isFlush (y:xs)
                 | otherwise = False

isStraight :: Hand -> Bool
isStraight [x] = True
isStraight (x:y:xs) | cardRank x == 14 && cardRank y == 5 = isStraight (y:xs ++ [Card (Numeric 1) (cardSuit x)]) -- Low Ace Straight
                    | cardRank x - 1 == cardRank y = isStraight (y:xs)
                    | otherwise = False

isThree :: Hand -> Bool
isThree hand = head (handGroupLengths hand) == 3

isTwoPair :: Hand -> Bool
isTwoPair hand = head (handGroupLengths hand) == 2 && handGroupLengths hand !! 1 == 2

isPair :: Hand -> Bool
isPair hand = head (handGroupLengths hand) == 2



handGroupLengths :: Hand -> [Int]
handGroupLengths hand = reverse (sort (map length (handGroup hand)))

handGroup :: Hand -> [Hand]
handGroup = groupBy (\ x y -> cardRank x == cardRank y && cardSuit x /= cardSuit y)

cardRank :: Card -> Int
cardRank (Card rank _) | rank == Numeric 1 = 1
                       | rank == Numeric 2 = 2
                       | rank == Numeric 3 = 3
                       | rank == Numeric 4 = 4
                       | rank == Numeric 5 = 5
                       | rank == Numeric 6 = 6
                       | rank == Numeric 7 = 7
                       | rank == Numeric 8 = 8
                       | rank == Numeric 9 = 9
                       | rank == Numeric 10 = 10
                       | rank == Jack = 11
                       | rank == Queen = 12
                       | rank == King = 13
                       | rank == Ace = 14

cardSuit :: Card -> Suit
cardSuit (Card _ suit) = suit