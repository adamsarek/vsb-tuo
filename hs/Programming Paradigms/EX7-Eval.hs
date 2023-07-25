import Stack

data Expr = Num Int
          | Add Expr Expr
          | Sub Expr Expr
          | Mul Expr Expr
          | Div Expr Expr
          | Var Char
      deriving (Eq)

eval :: Expr -> Int
eval (Num x) = x
eval (Add x y) = eval x + eval y
eval (Sub x y) = eval x - eval y
eval (Mul x y) = eval x * eval y
eval (Div x y) = eval x `div` eval y

showExpr :: Expr -> String
showExpr (Num x) = show x
showExpr (Var x) = show x
showExpr (Add x y) = "(" ++ showExpr x ++ "+" ++ showExpr y++")"
showExpr (Sub x y) = "(" ++ showExpr x ++ "-" ++ showExpr y++")"
showExpr (Mul x y) = "(" ++ showExpr x ++ "*" ++ showExpr y++")"
showExpr (Div x y) = "(" ++ showExpr x ++ "/" ++ showExpr y++")"

instance Show Expr where
    show expr = showExpr expr

deriv :: Expr-> Char -> Expr
deriv (Num _) x = Num 0
deriv (Var y) x | x == y = Num 1
                | otherwise = Num 0
deriv (Add x y) z = Add (deriv x z) (deriv y z)
deriv (Sub x y) z = Sub (deriv x z) (deriv y z)
deriv (Mul x y) z = Add (Mul (deriv x z) y) (Mul x (deriv y z))
deriv (Div x y) z = Div (Sub (Mul (deriv x z) y) (Mul x (deriv y z))) (Mul y y)