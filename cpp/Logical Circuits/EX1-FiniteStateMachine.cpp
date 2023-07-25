#include <iostream>

using namespace std;

typedef enum {
    S0, S1, S2
} STAV;

STAV dalsiStav(bool bit, STAV aktualStav)
{
    if(aktualStav == S0)
    {
        return ((bit) ? S1 : S0);
    }
    else if (aktualStav == S1)
    {
        return ((bit) ? S0 : S2);
    }
    else if (aktualStav == S2)
    {
        return ((bit) ? S2 : S1);
    }
    return S0;
}

int main()
{
    char znak;

    STAV aktualniStav = S0;

    while (true)
    {
        cin.get(znak);
        if (znak != '\n')
        {
            if ((int)znak == 48 || (int)znak == 49)
            {
                bool bit = ((int)znak == 48) ? false : true;

                aktualniStav = dalsiStav(bit, aktualniStav);
            }
        }
        else
        {
            switch (aktualniStav)
            {
                case S0:
                    cout << "Cislo je beze zbytku";
                    return 0;
                case S1:
                    cout << "Cislo ma zbytek 1";
                    return 0;
                case S2:
                    cout << "Cislo ma zbytek 2";
                    return 0;
            }
        }
    }


    return 0;
}
