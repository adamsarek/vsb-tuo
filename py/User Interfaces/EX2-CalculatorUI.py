# -*- coding: utf-8 -*-
#------------------------------------------------------------------------------#
# Kalkulacka                                                                   #
#------------------------------------------------------------------------------#

from tkinter import * 
from tkinter import font as tkFont
from math import *
#import tkFont

def callback(f):
    return lambda: f()

def callback(f, *a):
    """
    Vraci funkci f s nastavenym parametrem a.
    Tato funkce umi osetrit i vice nez jeden parametr.

    Pouziti:
        callback(funkce, arg1, arg2, ..., argn )
    """
    return lambda: f(*a)

class MyApp:

    def _big(self):
        self._font.configure(size=18, weight="normal")

    def _normal(self):
        self._font.configure(size=12, weight="normal")

    def _clear(self):
        self._display.set("0")
        
        self._replaceAllowed = True
        self._pointAllowed = True
        self._equalsAllowed = False

        self._clearButton.config(state=DISABLED)
        self._plusButton.config(state=DISABLED)
        self._minusButton.config(state=DISABLED)
        self._multiplyButton.config(state=DISABLED)
        self._divideButton.config(state=DISABLED)
        self._equalsButton.config(state=DISABLED)
        self._pointButton.config(state=DISABLED)
        self._squareButton.config(state=DISABLED)
        self._squareRootButton.config(state=DISABLED)
        self._factorialButton.config(state=DISABLED)
        self._logButton.config(state=DISABLED)

    def _square(self):
        value = eval(self._display.get())

        result = pow(value,2)

        self._display.set(result)

        self._replaceAllowed = True
        self._pointAllowed = True
        self._equalsAllowed = False

        self._equalsButton.config(state=DISABLED)
        self._pointButton.config(state=DISABLED)

    def _squareRoot(self):
        value = eval(self._display.get())

        result = sqrt(value)

        self._display.set(result)

        self._replaceAllowed = True
        self._pointAllowed = True
        self._equalsAllowed = False

        self._equalsButton.config(state=DISABLED)
        self._pointButton.config(state=DISABLED)

    def _factorial(self):
        value = eval(self._display.get())

        result = factorial(value)

        self._display.set(result)

        self._replaceAllowed = True
        self._pointAllowed = True
        self._equalsAllowed = False

        self._equalsButton.config(state=DISABLED)
        self._pointButton.config(state=DISABLED)

    def _log(self):
        value = eval(self._display.get())

        result = log10(value)

        self._display.set(result)

        self._replaceAllowed = True
        self._pointAllowed = True
        self._equalsAllowed = False

        self._equalsButton.config(state=DISABLED)
        self._pointButton.config(state=DISABLED)

    def _solve(self):
        result = eval(self._display.get())

        self._display.set(result)

        self._replaceAllowed = True
        self._pointAllowed = True
        self._equalsAllowed = False

        self._equalsButton.config(state=DISABLED)
        self._pointButton.config(state=DISABLED)

    def _insert_key(self, sign):
        display = self._display.get()
        
        if(sign == "0"
        or sign == "1"
        or sign == "2"
        or sign == "3"
        or sign == "4"
        or sign == "5"
        or sign == "6"
        or sign == "7"
        or sign == "8"
        or sign == "9"):
            self._clearButton.config(state=NORMAL)
            self._plusButton.config(state=NORMAL)
            self._minusButton.config(state=NORMAL)
            self._multiplyButton.config(state=NORMAL)
            self._divideButton.config(state=NORMAL)
            if self._equalsAllowed == True:
                self._equalsButton.config(state=NORMAL)
            if self._pointAllowed == True:
                self._pointButton.config(state=NORMAL)
            if self._replaceAllowed == True:
                self._display.set("")
                if sign != "0":
                    self._replaceAllowed = 0
            if sign == "0" and display[-1] == "/":
                self._equalsButton.config(state=DISABLED)
                self._squareButton.config(state=DISABLED)
                self._squareRootButton.config(state=DISABLED)
                self._factorialButton.config(state=DISABLED)
                self._logButton.config(state=DISABLED)
            else:
                self._squareButton.config(state=NORMAL)
                self._squareRootButton.config(state=NORMAL)
                self._factorialButton.config(state=NORMAL)
                self._logButton.config(state=NORMAL)
        elif(sign == "+"
        or sign == "-"
        or sign == "*"
        or sign == "/"
        or sign == "."):
            self._plusButton.config(state=DISABLED)
            self._minusButton.config(state=DISABLED)
            self._multiplyButton.config(state=DISABLED)
            self._divideButton.config(state=DISABLED)
            self._equalsButton.config(state=DISABLED)
            self._pointButton.config(state=DISABLED)
            self._squareButton.config(state=DISABLED)
            self._squareRootButton.config(state=DISABLED)
            self._factorialButton.config(state=DISABLED)
            self._logButton.config(state=DISABLED)
            self._0Button.config(state=NORMAL)
            self._1Button.config(state=NORMAL)
            self._2Button.config(state=NORMAL)
            self._3Button.config(state=NORMAL)
            self._4Button.config(state=NORMAL)
            self._5Button.config(state=NORMAL)
            self._6Button.config(state=NORMAL)
            self._7Button.config(state=NORMAL)
            self._8Button.config(state=NORMAL)
            self._9Button.config(state=NORMAL)
            if sign == ",":
                self._pointAllowed = False
            else:
                self._pointAllowed = True
                self._equalsAllowed = True
            self._replaceAllowed = False
        
        self._display.set(self._display.get() + sign)
        
    def __init__(self, root):
        self._root = root
        self._mode = StringVar()
        self._display = StringVar()
        self._display.set("0")
        self._replaceAllowed = True
        self._pointAllowed = True
        self._equalsAllowed = False
        self._root.title("Calculator")
        self._font = tkFont.Font(size=12, weight="normal")
    
        self._displayLabel = Label(
            self._root, text="0", background="#ffffff", 
            anchor=E, relief=SUNKEN, height=2, font=self._font,
            textvariable=self._display
        )
        self._displayLabel.pack(fill=X, side=TOP, padx=8, pady=5)
    
        self._optionsFrame = Frame(self._root, relief=GROOVE)
        self._optionsFrame.pack()
        
        self._normalRadio = Radiobutton(
            self._optionsFrame, text="Normal", variable=self._mode, value="normal", 
            command=self._normal, font=self._font
        )
        self._normalRadio.pack(side=LEFT)
        
        self._bigRadio = Radiobutton(
            self._optionsFrame, text="Big", variable=self._mode, value="big", 
            command=self._big, font=self._font
        )
        self._bigRadio.pack(side=LEFT)
        
        self._numbersFrame = Frame(self._root)
        self._numbersFrame.pack(fill=BOTH, expand=1, padx=4, pady=4)
    
        # create buttons
        self._clearButton = Button(
            self._numbersFrame, text="Clr",width=5, height=2, 
            font=self._font, 
            command=callback(self._clear)
        )
        self._plusButton = Button(
            self._numbersFrame, text="+",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "+")
        )
        self._minusButton = Button(
            self._numbersFrame, text="-",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "-")
        )
        self._multiplyButton = Button(
            self._numbersFrame, text="×",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "*")
        )
        self._divideButton = Button(
            self._numbersFrame, text="÷",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "/")
        )
        self._equalsButton = Button(
            self._numbersFrame, text="=",width=5, height=2, 
            font=self._font, 
            command=callback(self._solve)
        )
        self._pointButton = Button(
            self._numbersFrame, text=",",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, ".")
        )
        self._squareButton = Button(
            self._numbersFrame, text="x²",width=5, height=2, 
            font=self._font,
            fg = "#0000ff",
            command=callback(self._square)
        )
        self._squareRootButton = Button(
            self._numbersFrame, text="√",width=5, height=2, 
            font=self._font, 
            fg = "#0000ff",
            command=callback(self._squareRoot)
        )
        self._factorialButton = Button(
            self._numbersFrame, text="n!",width=5, height=2, 
            font=self._font, 
            fg = "#0000ff",
            command=callback(self._factorial)
        )
        self._logButton = Button(
            self._numbersFrame, text="log",width=5, height=2, 
            font=self._font, 
            fg = "#0000ff",
            command=callback(self._log)
        )
        self._0Button = Button(
            self._numbersFrame, text="0",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "0")
        )
        self._1Button = Button(
            self._numbersFrame, text="1",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "1")
        )
        self._2Button = Button(
            self._numbersFrame, text="2",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "2")
        )
        self._3Button = Button(
            self._numbersFrame, text="3",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "3")
        )
        self._4Button = Button(
            self._numbersFrame, text="4",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "4")
        )
        self._5Button = Button(
            self._numbersFrame, text="5",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "5")
        )
        self._6Button = Button(
            self._numbersFrame, text="6",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "6")
        )
        self._7Button = Button(
            self._numbersFrame, text="7",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "7")
        )
        self._8Button = Button(
            self._numbersFrame, text="8",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "8")
        )
        self._9Button = Button(
            self._numbersFrame, text="9",width=5, height=2, 
            font=self._font, 
            command=callback(self._insert_key, "9")
        )
        self._infoLabel = Label(
            self._numbersFrame, text="Calculator by @SAR0083", 
            height=2, font=self._font
        )
        
        # add to grid
        self._squareButton.grid(row=0, column=0, sticky=W+E+N+S, padx=2, pady=2)
        self._squareRootButton.grid(row=0, column=1, sticky=W+E+N+S, padx=2, pady=2)
        self._factorialButton.grid(row=0, column=2, sticky=W+E+N+S, padx=2, pady=2)
        self._logButton.grid(row=0, column=3, sticky=W+E+N+S, padx=2, pady=2)
        
        self._clearButton.grid(row=1, column=0, sticky=W+E+N+S, padx=2, pady=2)
        self._divideButton.grid(row=1, column=1, sticky=W+E+N+S, padx=2, pady=2)
        self._multiplyButton.grid(row=1, column=2, sticky=W+E+N+S, padx=2, pady=2)
        self._minusButton.grid(row=1, column=3, sticky=W+E+N+S, padx=2, pady=2)

        self._7Button.grid(row=2, column=0, sticky=W+E+N+S, padx=2, pady=2)
        self._8Button.grid(row=2, column=1, sticky=W+E+N+S, padx=2, pady=2)
        self._9Button.grid(row=2, column=2, sticky=W+E+N+S, padx=2, pady=2)
        self._plusButton.grid(row=2, column=3, rowspan=2, sticky=W+E+N+S, padx=2, pady=2)

        self._4Button.grid(row=3, column=0, sticky=W+E+N+S, padx=2, pady=2)
        self._5Button.grid(row=3, column=1, sticky=W+E+N+S, padx=2, pady=2)
        self._6Button.grid(row=3, column=2, sticky=W+E+N+S, padx=2, pady=2)
        
        self._1Button.grid(row=4, column=0, sticky=W+E+N+S, padx=2, pady=2)
        self._2Button.grid(row=4, column=1, sticky=W+E+N+S, padx=2, pady=2)
        self._3Button.grid(row=4, column=2, sticky=W+E+N+S, padx=2, pady=2)
        self._equalsButton.grid(row=4, column=3, rowspan=2, sticky=W+E+N+S, padx=2, pady=2)

        self._0Button.grid(row=5, column=0, columnspan=2, sticky=W+E+N+S, padx=2, pady=2)
        self._pointButton.grid(row=5, column=2, sticky=W+E+N+S, padx=2, pady=2)

        self._infoLabel.grid(row=6, column=0, columnspan=4, sticky=W+E+N+S, padx=2, pady=2)

        # setup grid weight
        for x in range(4):
            Grid.columnconfigure(self._numbersFrame, x, weight=1)

        for y in range(7):
            Grid.rowconfigure(self._numbersFrame, y, weight=1)
        
        # available/disable buttons
        self._clearButton.config(state=DISABLED)
        self._plusButton.config(state=DISABLED)
        self._minusButton.config(state=DISABLED)
        self._multiplyButton.config(state=DISABLED)
        self._divideButton.config(state=DISABLED)
        self._equalsButton.config(state=DISABLED)
        self._pointButton.config(state=DISABLED)
        self._squareButton.config(state=DISABLED)
        self._squareRootButton.config(state=DISABLED)
        self._factorialButton.config(state=DISABLED)
        self._logButton.config(state=DISABLED)
        
        self._normalRadio.select()

def main():
    root = Tk()
    app = MyApp(root)
    root.mainloop()
    root.destroy()

main()
    

