# 1. Úloha
# b)
pnbinom(15, 4, 0.25, lower.tail = F)
# c)
ppois(34,32,lower.tail=F) / ppois(24,32,lower.tail=F)

# 2. Úloha
# a)
phyper(84, 270, 30, 100, lower.tail = F)
# b)
phyper(89, 270, 30, 100, lower.tail = F) / phyper(79, 270, 30, 100, lower.tail = F)
# c)
pbinom(84, 100, 0.9, lower.tail = F)

# 3. Úloha
# a)
x = seq(0, 12, 0.01)
y = pweibull(x, shape = 0.7, scale = 5)
plot(x, y)
# b)
pweibull(4, shape = 0.7, scale = 5, lower.tail = F)
# d)
dweibull(3, shape = 0.7, scale = 5) / pweibull(1, shape = 0.7, scale = 5, lower.tail = F)

# 4. Úloha
# a)
# Graf hustoty pravděpodobnosti
mi = 85
sigma = 12
x = seq(mi - sigma*3, mi + sigma*3, 0.01)
y = dnorm(x, mean = mi, sd = sigma)
plot(x, y)
# Distribuční funkce
mi = 85
sigma = 12
x = seq(mi - sigma*3, mi + sigma*3, 0.01)
y = pnorm(x, mean = mi, sd = sigma)
plot(x, y)
# b)
1 - pnorm(90, 85, 12)
# c)
qnorm(0.8, 0, 1)
qnorm(0.8, 85, 12)