install.packages("readxl")
install.packages("moments") # pro výpočet šikmosti a špičatosti
install.packages("dplyr")   # pro efektivní práci s datovým souborem
install.packages("tidyr")   # pro efektivní práci s datovým souborem (pivot_longer)
install.packages("ggplot2") # pro hezčí grafiku
install.packages("ggpubr")  # pro kombinování grafů z ggplot2
install.packages("rstatix") # pro identifikaci odlehlých pozorování
install.packages("lawstat")
install.packages("BSDA")

# Aktivace knihovny (nutno opakovat při každém novém spuštění Rka, vhodné mít na začátku skriptu)
library(readxl)
library(moments)
library(dplyr)
library(tidyr)
library(ggplot2)
library(ggpubr)
library(rstatix)
library(lawstat)
library(BSDA)
library(FSA)
library(car)

options(OutDec= ",",
        digits = 7,           # nastavení počtu des. míst ve výstupech základního Rka
        pillar.sigfig = 10)    # nastavení počtu platných cifer ve výstupech balíčku dplyr

amber = read_excel("ukol_111.xlsx", 
                   sheet = "Amber",           # specifikace listu v xlsx souboru
                   skip = 0)                 # řádky, které je potřeba přeskočit
bright = read_excel("ukol_111.xlsx", 
                    sheet = "Bright",           # specifikace listu v xlsx souboru
                    skip = 0)                 # řádky, které je potřeba přeskočit
clear = read_excel("ukol_111.xlsx", 
                   sheet = "Clear",           # specifikace listu v xlsx souboru
                   skip = 0)                 # řádky, které je potřeba přeskočit
dim = read_excel("ukol_111.xlsx", 
                 sheet = "Dim",           # specifikace listu v xlsx souboru
                 skip = 0)                 # řádky, které je potřeba přeskočit

# Přejmenování sloupců - je-li nutné
colnames(amber)  = c("id", "lm_22", "lm_5", "vyrobce")
colnames(bright) = c("id", "lm_22", "lm_5", "vyrobce")
colnames(clear)  = c("id", "lm_22", "lm_5", "vyrobce")
colnames(dim)    = c("id", "lm_22", "lm_5", "vyrobce")

# Převedení na data.frame
amber = as.data.frame(amber)
bright = as.data.frame(bright)
clear = as.data.frame(clear)
dim = as.data.frame(dim)
class(amber)
class(bright)
class(clear)
class(dim)

# Preprocessing
amber$vyrobce  <- "A"
bright$vyrobce <- "B"
clear$vyrobce  <- "C"
dim$vyrobce <- "D"
dataS = rbind(amber, bright, clear, dim)
dataS$id = dataS$id + 1

# Definování nové proměnné pokles
dataS$pokles = dataS$lm_22 - dataS$lm_5

# Charakteristiky
charakteristiky_dle_vyrobce=
  dataS %>%
  group_by(vyrobce) %>%  # uprav
  summarise(rozsah = length(na.omit(pokles)),
            minimum = min(pokles, na.rm=T),
            Q1 = quantile(pokles, 0.25, na.rm=T),
            prumer = mean(pokles, na.rm=T),
            median = median(pokles, na.rm=T),
            Q3 = quantile(pokles, 0.75, na.rm=T),
            maximum = max(pokles, na.rm=T),
            rozptyl = var(pokles, na.rm=T),
            smerodatna_odchylka = sd(pokles,na.rm=T),
            variacni_koeficient = (100*abs(smerodatna_odchylka/prumer)), 
            sikmost = (moments::skewness(pokles, na.rm=T)),
            stand_spicatost = (moments::kurtosis(pokles, na.rm=T)-3),
            dolni_mez_hradeb = Q1-1.5*(Q3-Q1),
            horni_mez_hradeb = Q3+1.5*(Q3-Q1))
t(charakteristiky_dle_vyrobce)

# Odebrání odlehlých pozorování
outliers_pokles = 
  dataS %>% # uprav nazev dat
  group_by(vyrobce) %>% # uprav nazev promenne
  identify_outliers(pokles)  # uprav nazev promenne

dataS$pokles_out = ifelse(dataS$id %in% outliers_pokles$id,NA,dataS$pokles)

# Charakteristiky
charakteristiky_dle_vyrobce_out=
  dataS %>%
  group_by(vyrobce) %>%  # uprav
  summarise(rozsah = length(na.omit(pokles_out)),
            minimum = min(pokles_out, na.rm=T),
            Q1 = quantile(pokles_out, 0.25, na.rm=T),
            prumer = mean(pokles_out, na.rm=T),
            median = median(pokles_out, na.rm=T),
            Q3 = quantile(pokles_out, 0.75, na.rm=T),
            maximum = max(pokles_out, na.rm=T),
            rozptyl = var(pokles_out, na.rm=T),
            smerodatna_odchylka = sd(pokles_out,na.rm=T),
            variacni_koeficient = (100*abs(smerodatna_odchylka/prumer)), 
            sikmost = (moments::skewness(pokles_out, na.rm=T)),
            stand_spicatost = (moments::kurtosis(pokles_out, na.rm=T)-3),
            dolni_mez_hradeb = Q1-1.5*(Q3-Q1),
            horni_mez_hradeb = Q3+1.5*(Q3-Q1))
t(charakteristiky_dle_vyrobce_out)

# Krabicový graf, původni data (900 x 450 px)
#dataS_plot = filter(dataS, vyrobce != "C" & vyrobce != "D")
dataS$vyrobce = factor(dataS$vyrobce, 
                       levels = c("A", "B", "C", "D"),
                       labels = c("Výrobce Amber", "Výrobce Bright", "Výrobce Clear", "Výrobce Dim"))

ggplot(dataS, # uprav
       aes(x = vyrobce,
           y = pokles))+  # uprav
  stat_boxplot(geom = "errorbar",
               width = 0.15)+
  geom_boxplot()+
  labs(x = "", y = "Pokles světelného toku zářivek po 30 sekundách od zapnutí \npři snížení okolní teploty z 22°C na 5°C (lm)")+ # uprav
  theme_light()+
  theme(axis.ticks.x = element_blank(),
        axis.text = element_text(color = "black", size = 11))

# Krabicový graf, data po odstranění odlehlých pozorování
ggplot(dataS, # uprav
       aes(x = vyrobce,
           y = pokles_out))+  # uprav
  stat_boxplot(geom = "errorbar",
               width = 0.15)+
  geom_boxplot()+
  labs(x = "", y = "Pokles světelného toku zářivek po 30 sekundách od zapnutí \npři snížení okolní teploty z 22°C na 5°C (lm)")+ # uprav
  theme_light()+
  theme(axis.ticks.x = element_blank(),
        axis.text = element_text(color = "black", size = 11))

# Histogram, původni data
ggplot(dataS, # uprav
       aes(x = pokles))+ # uprav
  geom_histogram(binwidth = 2.8, # uprav
                 color = "black",
                 fill = "grey55")+
  labs(x = "Pokles světelného toku zářivek po 30 sekundách od zapnutí \npři snížení okolní teploty z 22°C na 5°C (lm)", y = "Četnost")+ # uprav
  theme_light()+
  theme(axis.text = element_text(color = "black", size = 11))+
  facet_wrap("vyrobce",  # uprav
             dir = "v")

# Histogram, data po odstranění odlehlých pozorování
ggplot(dataS, # uprav
       aes(x = pokles_out))+ # uprav
  geom_histogram(binwidth = 2.8, # uprav
                 color = "black",
                 fill = "grey55")+
  labs(x = "Pokles světelného toku zářivek po 30 sekundách od zapnutí \npři snížení okolní teploty z 22°C na 5°C (lm)", y = "Četnost")+ # uprav
  theme_light()+
  theme(axis.text = element_text(color = "black", size = 11))+
  facet_wrap("vyrobce",  # uprav
             dir = "v")

# QQ-graf, původni data
ggplot(dataS, # uprav
       aes(sample = pokles))+ # uprav
  stat_qq()+
  stat_qq_line()+
  labs(x = "Teoretické normované kvantily", y = "Výběrové kvantily")+
  theme_light()+
  theme(axis.text = element_text(color = "black", size = 11))+
  facet_wrap("vyrobce", # uprav
             ncol = 1, # uprav
             scales = "free")

# QQ-graf, data po odstranění odlehlých pozorování
ggplot(dataS, # uprav
       aes(sample = pokles_out))+ # uprav
  stat_qq()+
  stat_qq_line()+
  labs(x = "Teoretické normované kvantily", y = "Výběrové kvantily")+
  theme_light()+
  theme(axis.text = element_text(color = "black", size = 11))+
  facet_wrap("vyrobce", # uprav
             ncol = 1, # uprav
             scales = "free")

0.8197368 - 2 * 3.110542
0.8197368 + 2 * 3.110542

0.7353659 - 2 * 3.094734
0.7353659 + 2 * 3.094734

#----------------------------------------------------------------------------
# Úkol 2

amber_out=
  dataS %>%
  filter(vyrobce == "Výrobce Amber")
bright_out=
  dataS %>%
  filter(vyrobce == "Výrobce Bright")

# Amber
moments::skewness(amber_out$pokles_out, na.rm=T)   # Šikmost
moments::kurtosis(amber_out$pokles_out, na.rm=T)-3 # Standardizovaná špičatost
shapiro.test(amber_out$pokles_out)
symmetry.test(amber_out$pokles_out, boot = FALSE)

median(amber_out$pokles_out, na.rm=T)
SIGN.test(amber_out$pokles_out, md = median(amber_out$pokles_out, na.rm=T), alternative = "greater", conf.level = 0.95)

# Bright
moments::skewness(bright_out$pokles_out, na.rm=T)   # Šikmost
moments::kurtosis(bright_out$pokles_out, na.rm=T)-3 # Standardizovaná špičatost
shapiro.test(bright_out$pokles_out)
symmetry.test(bright_out$pokles_out, boot = FALSE)

median(bright_out$pokles_out, na.rm=T)
SIGN.test(bright_out$pokles_out, md = median(bright_out$pokles_out, na.rm=T), alternative = "greater", conf.level = 0.95)

# c)
median(amber_out$pokles_out, na.rm=T) - median(bright_out$pokles_out, na.rm=T)
#median(amber_out$pokles_out, na.rm=T) / median(bright_out$pokles_out, na.rm=T)

wilcox.test(dataS$pokles_out[dataS$vyrobce=="Výrobce Amber"], 
            dataS$pokles_out[dataS$vyrobce=="Výrobce Bright"], 
            alternative = "two.sided", 
            conf.level = 0.95,
            conf.int = T)

#----------------------------------------------------------------------------
# Úkol 3

# a)
# Odebrání odlehlých pozorování
outliers_lm_5 = 
  dataS %>% # uprav nazev dat
  group_by(vyrobce) %>% # uprav nazev promenne
  identify_outliers(lm_5)  # uprav nazev promenne

dataS$lm_5_out = ifelse(dataS$id %in% outliers_lm_5$id,NA,dataS$lm_5)

# Krabicový graf, původni data (900 x 450 px)
ggplot(dataS, # uprav
       aes(x = vyrobce,
           y = lm_5))+  # uprav
  stat_boxplot(geom = "errorbar",
               width = 0.15)+
  geom_boxplot()+
  labs(x = "", y = "Světelný tok")+ # uprav
  theme_light()+
  theme(axis.ticks.x = element_blank(),
        axis.text = element_text(color = "black", size = 11))

# Krabicový graf, data po odstranění odlehlých pozorování
ggplot(dataS, # uprav
       aes(x = vyrobce,
           y = lm_5_out))+  # uprav
  stat_boxplot(geom = "errorbar",
               width = 0.15)+
  geom_boxplot()+
  labs(x = "", y = "Světelný tok")+ # uprav
  theme_light()+
  theme(axis.ticks.x = element_blank(),
        axis.text = element_text(color = "black", size = 11))

# Histogram, data po odstranění odlehlých pozorování (600 x 600 px)
ggplot(dataS, # uprav
       aes(x = lm_5_out))+ # uprav
  geom_histogram(binwidth = 5, # uprav
                 color = "black",
                 fill = "grey55")+
  labs(x = "Světelný tok", y = "Četnost")+ # uprav
  theme_light()+
  theme(axis.text = element_text(color = "black", size = 11))+
  facet_wrap("vyrobce",  # uprav
             dir = "v", ncol = 1)

# QQ-graf, data po odstranění odlehlých pozorování (300 x 600 px)
ggplot(dataS, # uprav
       aes(sample = lm_5_out))+ # uprav
  stat_qq()+
  stat_qq_line()+
  labs(x = "Teoretické normované kvantily", y = "Výběrové kvantily")+
  theme_light()+
  theme(axis.text = element_text(color = "black", size = 11))+
  facet_wrap("vyrobce", # uprav
             ncol = 1, # uprav
             scales = "free")

# b)
amber_out=
  dataS %>%
  filter(vyrobce == "Výrobce Amber")
bright_out=
  dataS %>%
  filter(vyrobce == "Výrobce Bright")
clear_out=
  dataS %>%
  filter(vyrobce == "Výrobce Clear")
dim_out=
  dataS %>%
  filter(vyrobce == "Výrobce Dim")

# Amber
moments::skewness(amber_out$lm_5_out, na.rm=T)   # Šikmost
moments::kurtosis(amber_out$lm_5_out, na.rm=T)-3 # Standardizovaná špičatost
shapiro.test(amber_out$lm_5_out)
symmetry.test(amber_out$lm_5_out, boot = FALSE)

# Bright
moments::skewness(bright_out$lm_5_out, na.rm=T)   # Šikmost
moments::kurtosis(bright_out$lm_5_out, na.rm=T)-3 # Standardizovaná špičatost
shapiro.test(bright_out$lm_5_out)
symmetry.test(bright_out$lm_5_out, boot = FALSE)

# Clear
moments::skewness(clear_out$lm_5_out, na.rm=T)   # Šikmost
moments::kurtosis(clear_out$lm_5_out, na.rm=T)-3 # Standardizovaná špičatost
shapiro.test(clear_out$lm_5_out)
symmetry.test(clear_out$lm_5_out, boot = FALSE)

# Dim
moments::skewness(dim_out$lm_5_out, na.rm=T)   # Šikmost
moments::kurtosis(dim_out$lm_5_out, na.rm=T)-3 # Standardizovaná špičatost
shapiro.test(dim_out$lm_5_out)
symmetry.test(dim_out$lm_5_out, boot = FALSE)

# c)
podklady = 
  dataS %>% 
  group_by(vyrobce) %>% 
  summarise(rozsah = length(na.omit(lm_5_out)),
            sikmost = moments::skewness(lm_5_out, na.rm = T),
            spicatost = moments::kurtosis(lm_5_out, na.rm = T)-3, 
            rozptyl = var(lm_5_out, na.rm = T),
            sm_odch = sd(lm_5_out, na.rm = T),
            prumer = mean(lm_5_out, na.rm = T),
            median = quantile(lm_5_out, 0.5, na.rm = T),
            Shapiruv_Wilkuv_phodnota = shapiro.test(lm_5_out)$p.value)
t(podklady)

2233.0830 / 940.8083

bartlett.test(dataS$lm_5_out~dataS$vyrobce)

# d)
median(amber_out$lm_5_out, na.rm=T)
wilcox.test(amber_out$lm_5_out, md = median(amber_out$lm_5_out, na.rm=T), alternative = "two.sided", conf.level = 0.95, conf.int = T)

median(bright_out$lm_5_out, na.rm=T)
wilcox.test(bright_out$lm_5_out, md = median(bright_out$lm_5_out, na.rm=T), alternative = "two.sided", conf.level = 0.95, conf.int = T)

median(clear_out$lm_5_out, na.rm=T)
wilcox.test(clear_out$lm_5_out, md = median(clear_out$lm_5_out, na.rm=T), alternative = "two.sided", conf.level = 0.95, conf.int = T)

median(dim_out$lm_5_out, na.rm=T)
wilcox.test(dim_out$lm_5_out, md = median(dim_out$lm_5_out, na.rm=T), alternative = "two.sided", conf.level = 0.95, conf.int = T)

# e)
kruskal.test(dataS$lm_5_out~dataS$vyrobce)

dunnTest(lm_5_out ~ vyrobce, 
         data = dataS, 
         method = "bonferroni")
